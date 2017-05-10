using DocuSign.eSign.Model;
using GeoffsDocuSignDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeoffsDocuSignDemo.Controllers
{
    public class HomeController : Controller
    {
        //Obviously this is a demo kludge- not acceptable for multi-tenant application
        public static string s_accountId = "";
        public static string s_envelopeId = "";
        public static string s_username = "";
        public static string s_password = "";
        public static string s_recipientName;
        public static string s_recipientEmail;
        public static string[,] s_tmpFilenames = new string[2,10]; // format is {originalFilename, tmpFullPathname} Cutting user off at 10 files 
        //end of kludge

        public IActionResult Index()
        {

            return View("Index");
            // get list of files
            // convert to string array
            // put in viewmodel
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Choose()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Send()
        {
            return View();
        }

        public IActionResult DocuSign()
        {
            if (s_accountId == "")
            {
                ViewData["Message"] = "You need to sign in before signing documents.";
                return View("Login");
            }

            if (s_envelopeId == "")
            {
                ViewData["Message"] = "No Documents to sign";
                return View("Index");
            }

            DocuSignDemo.DocuSignDemo demo = new DocuSignDemo.DocuSignDemo();

            SignRequestModel signRequest = new SignRequestModel();

            signRequest.Username = s_username;
            signRequest.Password = s_password;
            signRequest.RecipientName = s_recipientName;
            signRequest.RecipientEmail = s_recipientEmail;
            signRequest.EnvelopeId = s_envelopeId;
            signRequest.AccountId = s_accountId;


            ViewUrl recipientView = demo.SignDocument(ref signRequest);

            //Test Working Console Code
            //ViewUrl recipientView = demo.SignDocument(s_envelopeId);
            if (recipientView != null)
            {
                //return recipientView;
                return Redirect(recipientView.Url);
                //return View();
            }
            else
            {
                ViewData["Message"] = signRequest.Message;
                return View("Sign");
            }
        }

        public IActionResult Sign()
        {

            return View("Sign");

        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();

        }

        [HttpPost]
        public IActionResult SendFormSubmit(SendRequestModel sendRequest)   //(string email, string fileName)
        {
            if (s_username != "" && s_password != "")
            {
                DocuSignDemo.DocuSignDemo demo = new DocuSignDemo.DocuSignDemo();

                sendRequest.SenderAccountId = s_accountId;

                ViewData["Message"] = demo.SendSignDocumentRequest(s_tmpFilenames, ref sendRequest);

                s_envelopeId = sendRequest.EnvelopeId;
                s_recipientName = sendRequest.RecipientName;
                s_recipientEmail = sendRequest.RecipientEmailAddress;

                //clean out array of file names
                for (int i = 0; i < 10; i++)
                {
                    s_tmpFilenames[0, i] = null;
                    s_tmpFilenames[1, i] = null;
                }

            }

            else
            {
                ViewData["Message"] = "You need to sign in before you can send documents.";
            }


            return View("Index");
        }


        [HttpPost]
        public IActionResult LoginFormSubmit(LoginRequestModel loginRequest)
        {
            DocuSignDemo.DocuSignDemo demo = new DocuSignDemo.DocuSignDemo();


            string accountId = demo.Login(loginRequest);

            if (accountId.Substring(0, 5) == "Error")
            {//Error Condition
                ViewData["Message"] = accountId;  //accountId contains error message in failing case
                return View("About");
            }
            else
            {// Working Condition
                ViewData["Message"] = "User " + loginRequest.Username + " is signed on.";
                s_accountId = accountId;
                s_username = loginRequest.Username;
                s_password = loginRequest.Password;

                return View("Index");
            }

         }

        [HttpPost]
        public IActionResult ProcessForm(string Signon, string Choose, string Send, String Sign)
        {
            if (!string.IsNullOrEmpty(Signon))
            {
                return View("Login");
            }
            if (!string.IsNullOrEmpty(Send))
            {
                return View("Send");
            }

            if (!string.IsNullOrEmpty(Choose))
            {
                return View("Choose");
            }

            if (!string.IsNullOrEmpty(Sign))
            {
                //return View("Sign");
                return Redirect("/Home/Sign");
            }

            return View("Index");
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            if (s_username != "" && s_password != "")
            {
                try
                {
                    long size = files.Sum(f => f.Length);

                    // full path to file in temp location


                    string fileNamesString = "";
                    int count = 0;
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0 && count < 10)
                        {
                            var filePath = Path.GetTempFileName();
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                                s_tmpFilenames[0, count] = formFile.FileName;
                                s_tmpFilenames[1, count] = filePath;
                            }
                            fileNamesString = fileNamesString + formFile.FileName + ", ";
                            count++;

                        }
                    }
                    ViewData["Message"] = fileNamesString;
                    return View("Send");
                }
                catch (Exception e)
                {
                    ViewData["Message"] = e.Message;
                    return View("Send");
                }
            }
            else
            {
                ViewData["Message"] = "You need to sign in before you can send documents.";
                return View("Index");
            }
        }

    }
}
    


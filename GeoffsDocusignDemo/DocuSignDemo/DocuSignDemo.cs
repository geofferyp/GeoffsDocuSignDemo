// DocuSign References
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSign.eSign.Client;

// Additional Reference
using System;
using System.Collections.Generic;
using System.IO;
using GeoffsDocusignDemo;
using GeoffsDocuSignDemo.Models;


namespace GeoffsDocuSignDemo.DocuSignDemo
{
    public class DocuSignDemo
    {
        //Authenticates the user against DocuSign
        public string Login(LoginRequestModel user)
        {
            try
            { 
            // Credential come from login form via the LoginRequestModel
            string Username = user.Username; //[EMAIL]";
            string Password = user.Password;  //[PASSWORD]";
            string integratorKey = GlobalVariables.integratorKey;
            // instantiate api client with appropriate environment (for production change to www.docusign.net/restapi)
            string basePath = "https://demo.docusign.net/restapi";

            // instantiate a new api client
            ApiClient apiClient = new ApiClient(basePath);

            // set client in global config so we don't need to pass it to each API object
            Configuration.Default.ApiClient = apiClient;

            string authHeader = "{\"Username\":\"" + Username + "\", \"Password\":\"" + Password + "\", \"IntegratorKey\":\"" + integratorKey + "\"}";
            Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

            // we will retrieve this from the login() results
            string accountId = null;

            // the authentication api uses the apiClient (and X-DocuSign-Authentication header) that are set in Configuration object
            AuthenticationApi authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            // user might be a member of multiple accounts
            accountId = loginInfo.LoginAccounts[0].AccountId;
            //GlobalVariables.LoggedInAccountId = accountId;
            return accountId;
            }
            catch (Exception e)
            {                
                return "Error " + e.Message;
            }
        }

        public string SendOauthRequest()

        {
            string token = "";

            AuthenticationApi authApi = new AuthenticationApi("https://account-d.docusign.com");
            OauthAccess oAuth = authApi.GetOAuthToken();
            token = oAuth.AccessToken;

            return token;
        }

        //Method sends a document signing request to user specified on View Send form.
        public string SendSignDocumentRequest(string[,] fileNamesPaths, ref SendRequestModel sendRequest)
        {
            try
            {
               
                for (int i = 0; i < 10; i++)

                {
                    if (fileNamesPaths[0, i] != null)
                    {
                        // Read a file from disk to use as a document
                        //byte[] fileBytes = File.ReadAllBytes(sendRequest.FileName);
                        byte[] fileBytes = File.ReadAllBytes(fileNamesPaths[1,i]);

                        EnvelopeDefinition envDef = new EnvelopeDefinition();
                        envDef.EmailSubject = "[DocuSign C# SDK] - Please sign this doc";

                        // Add a document to the envelope
                        Document doc = new Document();
                        doc.DocumentBase64 = System.Convert.ToBase64String(fileBytes);
                        //doc.Name = sendRequest.FileName;
                        doc.Name = fileNamesPaths[0,i];
                        doc.DocumentId = "1";


                        envDef.Documents = new List<Document>();
                        envDef.Documents.Add(doc);

                        // Add a recipient to sign the documeent
                        Signer signer = new Signer();
                        signer.Name = sendRequest.RecipientName;
                        signer.Email = sendRequest.RecipientEmailAddress;
                        signer.RecipientId = "1";  //"3d8b53e4-d35c-4f4d-ac9a-9fa8fd0e3aba";

                        // must set |clientUserId| to embed the recipient
                        signer.ClientUserId = "1234";

                        // Create a |SignHere| tab somewhere on the document for the recipient to sign
                        signer.Tabs = new Tabs();
                        signer.Tabs.SignHereTabs = new List<SignHere>();
                        SignHere signHere = new SignHere();
                        signHere.DocumentId = "1";
                        signHere.PageNumber = "1";
                        signHere.RecipientId = "1";
                        signHere.XPosition = "400";
                        signHere.YPosition = "300";
                        signer.Tabs.SignHereTabs.Add(signHere);

                        envDef.Recipients = new Recipients();
                        envDef.Recipients.Signers = new List<Signer>();
                        envDef.Recipients.Signers.Add(signer);

                        // set envelope status to "sent" to immediately send the signature request
                        envDef.Status = "sent";

                        // Use the EnvelopesApi to create and send the signature request
                        EnvelopesApi envelopesApi = new EnvelopesApi();
                        EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(sendRequest.SenderAccountId, envDef);
                        sendRequest.EnvelopeId = envelopeSummary.EnvelopeId;
                        //return JsonConvert.SerializeObject(envelopeSummary.EnvelopeId).ToString();
                    }
                }
                return "Success! Signing Request sent to " + sendRequest.RecipientEmailAddress;

            }

            catch (Exception e)
            {
                //handle error
                return e.Message.ToString();
            }

        }        
        
        //Method to embed a document signing in a new browser tab
        public ViewUrl SignDocument(ref SignRequestModel signRequest)
        {
            try
            {
                string Username = signRequest.Username; //[email]";
                string Password = signRequest.Password;  //[password]";
                string IntegratorKey = GlobalVariables.integratorKey;  //[INTEGRATOR_KEY]";

                // Enter recipient (signer) name and email address
                string recipientName = signRequest.RecipientName;     //[SIGNER_NAME]";
                string recipientEmail = signRequest.RecipientEmail;   //[SIGNER_EMAIL]";

                // instantiate api client with appropriate environment (for production change to www.docusign.net/restapi)
                string basePath = "https://demo.docusign.net/restapi";

                // instantiate a new api client
                ApiClient apiClient = new ApiClient(basePath);

                // set client in global config so we don't need to pass it to each API object
                Configuration.Default.ApiClient = apiClient;

                string authHeader = "{\"Username\":\"" + Username + "\", \"Password\":\"" + Password + "\", \"IntegratorKey\":\"" + IntegratorKey + "\"}";
                Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

                // we will retrieve this from the login() results
                string accountId = null;

                // the authentication api uses the apiClient (and X-DocuSign-Authentication header) that are set in Configuration object
                AuthenticationApi authApi = new AuthenticationApi();
                LoginInformation loginInfo = authApi.Login();

                // user might be a member of multiple accounts
                accountId = loginInfo.LoginAccounts[0].AccountId;

                //// Use the EnvelopesApi to create and send the signature request
                EnvelopesApi envelopesApi = new EnvelopesApi();

                //Giving the recipient the view.

                RecipientViewRequest viewOptions = new RecipientViewRequest()
                {
                    ReturnUrl = "https://www.docusign.com/devcenter",
                    ClientUserId = "1234",  // must match clientUserId set in step #2!
                    AuthenticationMethod = "email",
                    UserName = recipientName,
                    Email = recipientEmail
                };

                // create the recipient view (aka signing URL)
                ViewUrl recipientView = envelopesApi.CreateRecipientView(signRequest.AccountId, signRequest.EnvelopeId, viewOptions);

                // print the JSON response
                //Console.WriteLine("ViewUrl:\n{0}", JsonConvert.SerializeObject(recipientView));

                // Start the embedded signing session!
                return recipientView;
            }
            catch(Exception e)
            {
                signRequest.Message = e.Message;
                return null;

            }


        }

    }
}

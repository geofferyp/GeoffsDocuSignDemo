
namespace GeoffsDocuSignDemo.Models
{
    public class SignRequestModel
    {
        ///<summary>
        /// Gets or sets EnvelopeId. This is the document to sign
        ///</summary>
        public string EnvelopeId { get; set; }

        ///<summary>
        /// Gets or sets Password.
        ///</summary>
        public string Password { get; set; }

        ///<summary>
        /// Gets or sets Username.
        ///</summary>
        public string Username { get; set; }

        ///<summary>
        /// Gets or sets RecipientName.
        ///</summary>
        public string RecipientName { get; set; }

        ///<summary>
        /// Gets or sets RecipientEmail.
        ///</summary>
        public string RecipientEmail { get; set; }

        ///<summary>
        /// Gets or sets Message. Used to return results or errors
        ///</summary>
        public string Message { get; set; }
        public string AccountId { get; internal set; }
    }
}

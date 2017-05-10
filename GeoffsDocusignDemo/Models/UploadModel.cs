namespace GeoffsDocuSignDemo.Models
{
    public class UploadModel
    {
        ///<summary>
        /// Gets or sets EmailAddress.
        ///</summary>
        public string RecipientEmailAddress { get; set; }

        ///<summary>
        /// Gets or sets EmailAddress.
        ///</summary>
        public string RecipientName { get; set; }

        ///<summary>
        /// Gets or sets EmailAddress.
        ///</summary>
        public string SenderAccountId { get; set; }

        ///<summary>
        /// Gets or sets FileName.
        ///</summary>
        public string FileName { get; set; }

        ///<summary>
        /// Gets or sets Message. Can be used to pass exceptions back to HomeController.
        ///</summary>
        public string Message { get; set; }

        ///<summary>
        /// Gets or sets EnvelopeId.
        ///</summary>
        public string EnvelopeId { get; set; }
    }
}


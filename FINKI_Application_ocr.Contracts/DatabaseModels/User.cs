using System;

namespace FINKI_Application_ocr.Contracts.DatabaseModels
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string BrowserUsed { get; set; }
        public string IpAddress { get; set; }
        public string Email { get; set; }
        public DateTime TokenExpirationDate { get; set; }

    }
}

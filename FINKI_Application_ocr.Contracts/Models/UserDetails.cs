namespace FINKI_Application_ocr.Contracts.Models
{
    public class UserDetails
    {
        private string _firstName;
        private string _lastName;
        private string _emailAddress;

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }


        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

    }
}

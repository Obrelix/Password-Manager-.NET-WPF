using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class Credentials
    {
        #region "Private Properties"
        private string _userName { get; set; }
        private string _password { get; set; }
        private string _description { get; set; }
        private string _comments { get; set; }
        #endregion

        #region "Public Properties

        public string UserName { get { return _userName; } set { _userName = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Comments { get { return _comments; } set { _comments = value; } }

        #endregion

        #region "Constractors"
        public Credentials() { }

        public Credentials(string UserName, string Password, string Description, string Comments)
        {
            try
            {
                this.UserName = UserName;
                this.Password = Password;
                this.Description = Description;
                this.Comments = Comments;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}

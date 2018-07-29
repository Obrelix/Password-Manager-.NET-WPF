using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class User
    {
        public string password { get; set; }

        public User() {  }

        public User( string password)
        {
            this.password = password;
        }
    }
}

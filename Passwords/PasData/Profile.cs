using System;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    class Profile : AProfile
    {
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Service { get { return service; } set { service = value; } }
        public EMail Email { get { return email; } set { email = value; } }

        public Profile() { }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    public class Profile : AProfile
    {
        private const char FieldSplit = ':';
        private const char ProfileSplit = ';';

        public string Service { get { return service; } set { service = value; } }
        public EMail Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Username { get { return username; } set { username = value; } }                

        public Profile() { }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(Service);
            ret.Append(FieldSplit);
            ret.Append(Email);
            ret.Append(FieldSplit);
            ret.Append(Password);
            ret.Append(FieldSplit);
            ret.Append(Username);
            ret.Append(ProfileSplit);

            return ret.ToString();
        }

    }
}

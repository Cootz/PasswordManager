using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Passwords.PasData
{
    public class Profile : AProfile
    {
        private const char FieldSplit = ':';
        private const char ProfileSplit = ';';
        private const string NullMessage = "null";

        public string Service { get { return service; } set { service = value; } }
        public EMail Email { get { return eMail; } set { eMail = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Username { get { return username; } set { username = value; } }

        public Profile() { }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(Service ?? NullMessage).Append(FieldSplit);
            ret.Append(Email).Append(FieldSplit);
            ret.Append(Password ?? NullMessage).Append(FieldSplit);
            ret.Append(Username ?? NullMessage).Append(ProfileSplit);

            return ret.ToString();
        }

        public override bool Equals([AllowNull] Profile other)
        {
            bool[] equals = { Service == other.Service, Email == other.Email, Password == other.Password, Username == other.username };

            return equals[0] & equals[1] & equals[2] & equals[3];
        }

        public static bool operator != (Profile left, Profile right)
        {
            return !left.Equals(right);
        }

        public static bool operator == (Profile left, Profile right)
        { 
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return Equals(obj as Profile);
        }

        public override int GetHashCode() => base.GetHashCode();
        
    }
}

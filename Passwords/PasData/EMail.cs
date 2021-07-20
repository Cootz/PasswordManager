using System;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    public class EMail
    {
        private const char SplitChar = '@';

        public string Adress { 
            get 
            {
                return _adr + SplitChar + _postfix; 
            } 
            set 
            { 
                ParseEmail(value); 
            } 
        }
        
        private string _adr;
        private string _postfix;

        public EMail()
        { 
        
        }

        public EMail(string adress, string postfix)
        {
            _adr = adress;
            this._postfix = postfix;
        }

        private void ParseEmail(string email)
        {
            string[] parsed = email.Split(SplitChar);

            _adr = parsed[0];
            _postfix = parsed[1];

        }

        public static bool operator !=(EMail left, EMail right)
        {
            return left.Adress != right.Adress;
        }

        public static bool operator ==(EMail left, EMail right)
        {
            return left.Adress == right.Adress;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (EMail)obj;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}

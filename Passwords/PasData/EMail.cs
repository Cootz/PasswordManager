using System;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    class EMail
    {
        private char SplitChar = '@';

        public string Adress { 
            get 
            {
                return adr + SplitChar + postfix; 
            } 
            set 
            { 
                ParseEmail(value); 
            } 
        }
        
        private string adr;
        private string postfix;

        public EMail()
        { 
        
        }

        public EMail(string adress, string postfix)
        {
            adr = adress;
            this.postfix = postfix;
        }

        private void ParseEmail(string email)
        {
            string[] parsed = email.Split(SplitChar);

            adr = parsed[0];
            postfix = parsed[1];

        }

    }
}

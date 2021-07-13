using System;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    abstract class AProfile
    {
        protected string username;
        protected string password;
        protected EMail email;
        protected string service;
    }
}

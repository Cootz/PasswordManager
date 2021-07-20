using System;
using System.Diagnostics.CodeAnalysis;

namespace Passwords.PasData
{
    public abstract class AProfile : IEquatable<Profile>
    {
        protected string username;
        protected string password;
        protected EMail eMail;
        protected string service;

        public abstract bool Equals([AllowNull] Profile other);
    }
}
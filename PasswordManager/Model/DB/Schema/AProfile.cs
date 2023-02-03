using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Model.DB.Schema;

public abstract class AProfile : IEquatable<Profile>
{
    protected string username;
    protected string password;
    protected string service;

    public abstract bool Equals([AllowNull] Profile other);
}
using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Model.DB.Schema;

[Obsolete]
public abstract class AProfile : IEquatable<ProfileInfo>
{
    protected string username;
    protected string password;
    protected string service;

    public abstract bool Equals([AllowNull] ProfileInfo other);
}
using Realms;
using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Model.DB.Schema;

public partial class ServiceInfo : RealmObject, IEquatable<ServiceInfo>
{
    /// <summary>
    /// Array of services that must be in the database by default
    /// </summary>
    public static readonly ServiceInfo[] DefaultServices =
    {
        new() { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "steam" },
        new() { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "discord" },
        new() { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "gog" },
        new() { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "origin" }
    };

    [PrimaryKey] public Guid ID { get; set; }

    /// <summary>
    /// Service name
    /// </summary>
    [Indexed]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// List of <see cref="ProfileInfo"/>s linked to this <see cref="ServiceInfo"/>
    /// </summary>
    [Backlink(nameof(ProfileInfo.Service))]
    public IQueryable<ProfileInfo> Profiles { get; }

    public ServiceInfo(string name) : base() => Name = name ?? string.Empty;

    public ServiceInfo() => ID = Guid.NewGuid();

    public bool Equals([AllowNull] ServiceInfo other)
    {
        if (ReferenceEquals(this, other)) return true;

        if (other is null) return false;

        if (!IsValid || !other.IsValid) return false;
        
        bool[] equals = { ID == other.ID, Name == other.Name };

        return equals[0] & equals[1];
    }

    public static bool operator !=(ServiceInfo left, ServiceInfo right) => !left?.Equals(right) ?? false;

    public static bool operator ==(ServiceInfo left, ServiceInfo right) => left?.Equals(right) ?? false;

    public override int GetHashCode() => ID.GetHashCode();

    public override bool Equals(object obj) => obj is ServiceInfo ? Equals(obj as ServiceInfo) : false;
}
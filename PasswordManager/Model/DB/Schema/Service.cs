using Realms;
using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Model.DB.Schema
{
    public partial class Service : RealmObject, IEquatable<Service>
    {
        public static readonly Service[] DefaultServices =
        {
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "steam" },
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "discord" },
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "gog" },
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "origin" }
        };

        [PrimaryKey]
        public Guid ID { get; set; }

        [Indexed]
        public string Name { get; set; } = string.Empty;

        public Service(string name) : base()
        {
            Name = name ?? string.Empty;
        }

        public Service()
        {         
            ID = Guid.NewGuid();
        }

        public bool Equals([AllowNull] Service other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is null)
            {
                return false;
            }

            bool[] equals = {ID == other.ID, Name == other.Name};

            return equals[0] & equals[1];
        }

        public static bool operator !=(Service left, Service right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Service left, Service right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode() => ID.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is Service ? Equals(obj as Service) : false;
        }
    }
}
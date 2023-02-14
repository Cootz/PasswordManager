using Realms;
using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Model.DB.Schema
{
    public partial class Service : RealmObject, IEquatable<Service>
    {
        public static readonly Service[] defaultServices =
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

        public override bool Equals([AllowNull] object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return Equals(obj as Service);
        }
    }
}
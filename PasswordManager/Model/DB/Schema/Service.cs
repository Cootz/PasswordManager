using Realms;

namespace PasswordManager.Model.DB.Schema
{
    public partial class Service : RealmObject
    {
        public readonly Service[] defaultServices =
        {
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "steam" },
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "discord" },
            new Service { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "gog" },
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
    }
}
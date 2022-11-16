using Passwords.PasData;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Passwords.Model
{
    internal class RecentModel : AbstractModel
    {
        public ObservableCollection<Profile> Profiles { get { return _profiles; } set { _profiles = value; } }
        private ObservableCollection<Profile> _profiles;

        public RecentModel()
        {
            ObservableCollection<Profile> serviceResutl = new(PasswordController.SearhProfiles("Service LIKE 'steam'").Result ?? new());

            serviceResutl.Add(new Profile()
            {
                Service = "steam",
                Email = new EMail()
                {
                    Adress = "test@fda.td"
                },
                Password = "psw",
                Username = null
            });
            _profiles = serviceResutl;
        }
    }
}

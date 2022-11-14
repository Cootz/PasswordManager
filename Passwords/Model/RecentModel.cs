using Passwords.PasData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords.Model
{
    internal class RecentModel : AbstractModel
    {
        public List<Profile> Profiles { get { return _profiles; } set { _profiles = value; OnPorpertyChanged(); } }
        private List<Profile> _profiles;   

        public RecentModel()
        { 
            List<Profile> serviceResutl = PasswordController.SearhProfiles("Service LIKE 'steam'").Result ?? new();
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

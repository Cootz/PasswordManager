using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PasswordManager.Model
{
    public class RecentModel
    {
        public async Task<IEnumerable<Profile>> getProfiles()
        {
            List<Profile> profiles =  /*await PasswordController.SearhProfiles("Service LIKE 'steam'") ??*/ new();
            for (int i = 0; i < 20; i++)
            {
                profiles.Add(new Profile()
                {
                    Service = "steam",
                    Email = new EMail()
                    {
                        Adress = $"test{i}@fda.td"
                    },
                    Password = $"psw{i}{Random.Shared.Next(0, i + 32)}",
                    Username = null
                });
            }
            return profiles;
        }
    }
}

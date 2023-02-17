using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.TestData
{
    public static class ProfileData
    {
        public static Profile GetTestProfile() => new()
        {
            Service = new Service("test service"),
            Username = "test username",
            Password = "test password",
        };

        public static Profile[] GetTestProfiles()
        {
            var steam = Service.DefaultServices.Single(s => s.Name == "steam");
            var origin = Service.DefaultServices.Single(s => s.Name == "origin");
            var gog = Service.DefaultServices.Single(s => s.Name == "gog");

            return new Profile[]
            {
                new ()
                {
                    Service = steam,
                    Username = "coo",
                    Password = "P@ssw0rd"
                },
                new ()
                {
                    Service = steam,
                    Username = "Rimo",
                    Password = "Passw0rd"
                },
                new ()
                {
                    Service = steam,
                    Username = "Iro",
                    Password = "Password"
                },
                new ()
                {
                    Service = origin,
                    Username = "Ica",
                    Password = "P@ssword"
                },
                new ()
                {
                    Service = gog,
                    Username = "Tenno",
                    Password = "p@ssword"
                }
            };
        }

    }
}

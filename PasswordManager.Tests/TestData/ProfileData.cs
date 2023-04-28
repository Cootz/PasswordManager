using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Tests.TestData;

public static class ProfileData
{
    public static ProfileInfo GetTestProfile() =>
        new()
        {
            Service = new ServiceInfo("test service"),
            Username = "test username",
            Password = "test password"
        };

    public static ProfileInfo[] GetTestProfiles()
    {
        ServiceInfo? steam = ServiceInfo.DefaultServices.Single(s => s.Name == "steam");
        ServiceInfo? origin = ServiceInfo.DefaultServices.Single(s => s.Name == "origin");
        ServiceInfo? gog = ServiceInfo.DefaultServices.Single(s => s.Name == "gog");

        return new ProfileInfo[]
        {
            new()
            {
                Service = steam,
                Username = "coo",
                Password = "P@ssw0rd"
            },
            new()
            {
                Service = steam,
                Username = "Rimo",
                Password = "Passw0rd"
            },
            new()
            {
                Service = steam,
                Username = "Iro",
                Password = "Password"
            },
            new()
            {
                Service = origin,
                Username = "Ica",
                Password = "P@ssword"
            },
            new()
            {
                Service = gog,
                Username = "Tenno",
                Password = "p@ssword"
            }
        };
    }

    public static ProfileInfo[] GetTestProfiles(ServiceInfo service)
    {
        return new ProfileInfo[]
        {
            new()
            {
                Service = service,
                Username = "coo",
                Password = "P@ssw0rd"
            },
            new()
            {
                Service = service,
                Username = "Rimo",
                Password = "Passw0rd"
            },
            new()
            {
                Service = service,
                Username = "Iro",
                Password = "Password"
            },
            new()
            {
                Service = service,
                Username = "Ica",
                Password = "P@ssword"
            },
            new()
            {
                Service = service,
                Username = "Tenno",
                Password = "p@ssword"
            }
        };
    }
}
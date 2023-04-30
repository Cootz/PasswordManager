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

    public static ProfileInfo[] GetTestProfiles(ServiceInfo? service = null)
    {
        ServiceInfo? steam = ServiceInfo.DefaultServices.Single(s => s.Name == "steam");
        ServiceInfo? origin = ServiceInfo.DefaultServices.Single(s => s.Name == "origin");
        ServiceInfo? gog = ServiceInfo.DefaultServices.Single(s => s.Name == "gog");

        return new ProfileInfo[]
        {
            new()
            {
                Service = service ?? steam,
                Username = "coo",
                Password = "P@ssw0rd"
            },
            new()
            {
                Service = service ?? steam,
                Username = "Rimo",
                Password = "Passw0rd"
            },
            new()
            {
                Service = service ?? steam,
                Username = "Iro",
                Password = "Password"
            },
            new()
            {
                Service = service ?? origin,
                Username = "Ica",
                Password = "P@ssword"
            },
            new()
            {
                Service = service ?? gog,
                Username = "Tenno",
                Password = "p@ssword"
            }
        };
    }
}
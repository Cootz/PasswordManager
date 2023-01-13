using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model;

public class RecentModel
{
    public async Task<IEnumerable<Profile>> getProfiles()
    {
        PasswordController.SavePasswords(new List<Profile>(){ new Profile()
        {
            Service = "steam",
            Email = $"dataFormDb@fda.td",
            Password = $"pswDb",
            Username = "RelM"
        } }.ToArray());

        List<Profile> profiles =  await PasswordController.SearhProfiles(x => x.Service == "Steam") ?? new();
        for (int i = 0; i < 20; i++)
        {
            profiles.Add(new Profile()
            {
                Service = "steam",
                Email = $"test{i}@fda.td",
                Password = $"psw{i}{Random.Shared.Next(0, i + 32)}",
                Username = null
            });
        }
        return profiles;
    }
}

using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model;

public class RecentModel
{
    public async Task<IEnumerable<Profile>> getProfiles()
    {
        List<Profile> profiles = PasswordController.SearhProfiles(x => x.Service == "steam") ?? new();
        return profiles;
    }
}

using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Utils
{
    public static class ProfileHelper
    {

        /// <summary>
        /// Verify profile data
        /// </summary>
        /// <param name="profile">Profile to verify</param>
        /// <returns>Verified profile</returns>
        /// <exception cref="ArgumentNullException">Throw if <see cref="Profile.Service"/> is null</exception>
        /// <exception cref="ArgumentException">Throw if <see cref="Profile.Username"/> or <see cref="Profile.Password"/> have incorrect format</exception>
        public static Profile Verify(this Profile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            
            if (profile.Service == null) throw new ArgumentNullException(nameof(profile));
            if (String.IsNullOrEmpty(profile.Username)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");
            if (String.IsNullOrEmpty(profile.Password)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");
            
            return profile;
        }
    }
}

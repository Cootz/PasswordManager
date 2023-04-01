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
        /// <exception cref="ArgumentNullException">Throw if <see cref="ProfileInfo.Service"/> is null</exception>
        /// <exception cref="ArgumentException">Throw if <see cref="ProfileInfo.Username"/> or <see cref="ProfileInfo.Password"/> have incorrect format</exception>
        public static ProfileInfo Verify(this ProfileInfo profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            if (profile.Service is null) throw new ArgumentNullException(nameof(profile));
            if (String.IsNullOrEmpty(profile.Username)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");
            if (String.IsNullOrEmpty(profile.Password)) throw new ArgumentException($"Incorrect {nameof(profile.Service)} format.");

            return profile;
        }
    }
}

using PasswordManager.Model.DB.Schema;
using PasswordManager.Tests.TestData;
using PasswordManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.Utils
{
    public class ProfileHelperTest
    {
        [Test]
        public void TestPositiveVerification()
        {
            ProfileInfo profile = ProfileData.GetTestProfile(); 

            Assert.DoesNotThrow(() => profile.Verify());
        }

        [Test]
        public void TestNegativeServiceVerification()
        {
            ProfileInfo profile = ProfileData.GetTestProfile();

            profile.Service = null;

            Assert.Throws<ArgumentNullException>(() => profile.Verify());
        }

        [Test]
        public void TestNegativeLoginVerification()
        {
            ProfileInfo profile = ProfileData.GetTestProfile();

            profile.Username = String.Empty;

            Assert.Throws<ArgumentException>(() => profile.Verify());
        }

        [Test]
        public void TestNegativePasswordVerification()
        {
            ProfileInfo profile = ProfileData.GetTestProfile();

            profile.Password = String.Empty;

            Assert.Throws<ArgumentException>(() => profile.Verify());
        }

    }
}

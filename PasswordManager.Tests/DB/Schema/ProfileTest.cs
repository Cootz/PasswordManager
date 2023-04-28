using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Tests.DB.Schema;

[TestFixture]
public class ProfileTest
{
    [Test]
    public void CompareTwoEqualProfilesTest()
    {
        ServiceInfo? providedService = new("steam");

        ProfileInfo? Profile = new()
        {
            Username = "testUser",
            Password = "testPassword",
            Service = providedService
        };

        ProfileInfo? otherProfile = new()
        {
            Username = "testUser",
            Password = "testPassword",
            Service = providedService
        };

        Assert.That(Profile.Equals(otherProfile), Is.True);
    }

    [Test]
    public void CompareTwoDifferentProfilesTest()
    {
        ServiceInfo? steamService = new("steam");

        ProfileInfo? Profile = new()
        {
            Username = "testUser",
            Password = "testPassword",
            Service = steamService
        };

        ProfileInfo? otherProfile = new()
        {
            Username = "diffTestUser",
            Password = "diffTestPassword",
            Service = steamService
        };

        Assert.That(Profile.Equals(otherProfile), Is.False);
    }

    [Test]
    public void CompareProfileWithNullTest()
    {
        ServiceInfo? steamService = new("steam");

        ProfileInfo? Profile = new()
        {
            Username = "testUser",
            Password = "testPassword",
            Service = steamService
        };

        Assert.That(Profile.Equals(null), Is.False);
    }


    [Test]
    public void CompareNullWithProfileTest()
    {
        ServiceInfo? steamService = new("steam");

        ProfileInfo? Profile = null;

        Assert.That(Profile == new ProfileInfo(), Is.False);
    }
}
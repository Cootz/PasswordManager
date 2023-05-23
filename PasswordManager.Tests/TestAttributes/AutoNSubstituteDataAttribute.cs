using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;

namespace PasswordManager.Tests.TestAttributes
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoNSubstituteCustomization
                {
                    ConfigureMembers = true
                }))
        {
        }
    }
}
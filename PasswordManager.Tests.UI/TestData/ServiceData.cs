using PasswordManager.Model.DB.Schema;
using PasswordManager.Tests.UI.View;

namespace PasswordManager.Tests.UI.TestData
{
    public class ServiceData
    {
        public static IEnumerable<TestCaseData> ServiceTestCases
        {
            get
            {
                return ServiceInfo.DefaultServices.Skip(1).Select(service => new TestCaseData(
                        new object[]
                        {
                            service.Name
                        })
                    .SetName(
                        $"{nameof(RecentPageTest.DisplayAddedProfileTest)}({service.Name})"));
            }
        }
    }
}

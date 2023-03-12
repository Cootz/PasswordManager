using PasswordManager.Services;

namespace PasswordManager.Tests.Services
{
    public class MockSuccessfulNavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null!) => Task.CompletedTask;
        public Task PopAsync() => Task.CompletedTask;
    }
}

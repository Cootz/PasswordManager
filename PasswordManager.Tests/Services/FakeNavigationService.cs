using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.Services
{
    public class MockSuccessfulNavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null) => Task.CompletedTask;
        public Task PopAsync() => Task.CompletedTask;
    }
}

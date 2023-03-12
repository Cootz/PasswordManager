namespace PasswordManager.Services
{
    /// <summary>
    /// Handeles navigation
    /// </summary>
    public sealed class NavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            return
                routeParameters is not null
                    ? Shell.Current.GoToAsync(route, routeParameters)
                    : Shell.Current.GoToAsync(route);
                
        }
        public Task PopAsync() => Shell.Current.GoToAsync("..");
    }
}

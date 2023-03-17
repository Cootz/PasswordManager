namespace PasswordManager.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to the page async
        /// </summary>
        /// <param name="route">Path to the page</param>
        /// <param name="routeParameters">Parameters to pass</param>
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

        /// <summary>
        /// Removes the current page from the navigation stack
        /// </summary>
        public Task PopAsync();
    }
}

using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly DatabaseService databaseService;

        public IQueryable<ServiceInfo> ServiceInfos
        {
            get => databaseService.Select<ServiceInfo>();
        }

        public AppTheme CurrentTheme
        {
            get => (AppTheme)Preferences.Default.Get("app_theme", 0);
            set
            {
                Preferences.Default.Set("app_theme", (int)value);
                App.Current.UserAppTheme = CurrentTheme;
            }
        }

        public SettingsService(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
    }
}
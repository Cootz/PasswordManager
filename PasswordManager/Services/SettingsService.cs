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
            get => Preferences.Default.Get("app_theme", AppTheme.Unspecified); 
            set => Preferences.Default.Set("app_theme", value);
        }

        public SettingsService(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly DatabaseService databaseService;

        public IQueryable<ProfileInfo> ProfileInfos
        {
            get => databaseService.Select<ProfileInfo>();
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

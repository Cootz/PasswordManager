using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Services
{
    public interface ISettingsService
    {
        IQueryable<ServiceInfo> ServiceInfos { get; }
        AppTheme CurrentTheme { get; set; }
    }
}
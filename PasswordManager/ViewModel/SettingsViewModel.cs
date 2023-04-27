using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

namespace PasswordManager.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private IQueryable<ServiceInfo> serviceInfos;

        private DatabaseService databaseService;
        private IAlertService alertService;

        public SettingsViewModel(DatabaseService db, IAlertService alertService)
        {
            databaseService = db;
            this.alertService = alertService;

            ServiceInfos = db.Select<ServiceInfo>();
        }

        [RelayCommand]
        private async void AddService()
        {
            string response = await alertService.ShowPromptAsync("Service", "Enter service name");

            if (string.IsNullOrEmpty(response))
            {
                return;
            }

            ServiceInfo newService = new()
            {
                Name = response
            };

            databaseService.Add(newService);
        }

        [RelayCommand]
        private async void RemoveService(ServiceInfo info) =>
            await databaseService.RealmQuery(async (realm) =>
            {
                await realm.WriteAsync(() =>
                {
                    realm.RemoveRange(info.Profiles);
                    realm.Remove(info);
                });
            });
    }
}
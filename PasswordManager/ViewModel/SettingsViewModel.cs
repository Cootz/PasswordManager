using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;

namespace PasswordManager.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private IQueryable<ServiceInfo> serviceInfos;

        private DatabaseService databaseService;

        public SettingsViewModel(DatabaseService db)
        {
            databaseService = db;

            ServiceInfos = db.Select<ServiceInfo>();
        }

        [RelayCommand]
        private async void AddService()
        {
            string response = await App.AlertService.ShowPromptAsync("Service", "Enter service name");

            if (!string.IsNullOrEmpty(response))
            {
                ServiceInfo newService = new ServiceInfo();

                newService.Name = response;

                databaseService.Add(newService);
            }
        }

        [RelayCommand]
        private async void RemoveService(ServiceInfo info)
        {
            List<ProfileInfo> profilesPendingDeleting = new List<ProfileInfo>(info.Profiles);

            await databaseService.RealmQuerry(async (realm) =>
            {
                await realm.WriteAsync(() =>
                {
                    realm.RemoveRange(info.Profiles);
                    realm.Remove(info);
                });
            });
        }
    }
}

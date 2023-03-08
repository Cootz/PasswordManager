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

        public SettingsViewModel(DatabaseService db) 
        {
            ServiceInfos = db.Select<ServiceInfo>();
        }

        [RelayCommand]
        private async void AddService()
        {
            await Shell.Current.CurrentPage.DisplayPromptAsync("Service", "Enter service name");
        }
    }
}

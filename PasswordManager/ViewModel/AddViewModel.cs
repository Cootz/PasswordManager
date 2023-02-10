using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.ViewModel
{
    [QueryProperty("PasswordController", "db")]
    public partial class AddViewModel : ObservableObject
    {
        [ObservableProperty]
        public PasswordController passwordController;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private string service;

        [RelayCommand]
        async Task AddProfile()
        {
            PasswordController?.Add(new Profile()
            { 
                Username = username,
                Password = password,
                Service= service
            });

           await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

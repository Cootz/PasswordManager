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
        private IQueryable<Service> service;

        [ObservableProperty]
        private int selectedIndex;

        [RelayCommand]
        async Task AddProfile()
        {
            PasswordController?.Add(new Profile()
            {
                Username = Username,
                Password = Password,
                Service = Service.ElementAt(SelectedIndex)
            });

           await GoBack();
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

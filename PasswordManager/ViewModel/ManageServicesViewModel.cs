using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Services;

namespace PasswordManager.ViewModel
{
    public class ManageServicesViewModel : ObservableObject
    {
        private DatabaseService databaseService;

        public ManageServicesViewModel(DatabaseService db)
        {
            databaseService = db;
        }
    }
}

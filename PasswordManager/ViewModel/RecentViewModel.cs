using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using PasswordManager.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        IQueryable<Profile> profiles;

        private PasswordController db;

        public RecentViewModel()
        {
            var IOInit = AppDirectoryManager.Initialize();
            db = new PasswordController(new RealmController());
            Debug.Write(nameof(db));

            Task.WhenAll(IOInit).Wait();

            Task[] Inits = {
                db.Initialize(),
            };

            Task.WhenAll(Inits);

            Profiles = db.GetProfiles();
        }

        [RelayCommand]
        async Task AddNote()
        {
            await Shell.Current.GoToAsync(nameof(AddPage),
                new Dictionary<string, object> 
                {
                    { nameof(db), db }
                }
            );
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Model;

namespace PasswordManager.ViewModel
{
    internal partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        private RecentModel model;

        public RecentViewModel() => Model = new();
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        ProfileInfo profileInfo;

        public ProfileViewModel() { }



    }
}

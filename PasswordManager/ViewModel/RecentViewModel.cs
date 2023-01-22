﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Model;
using PasswordManager.Model.DB.Schema;
using System.Collections.ObjectModel;

namespace PasswordManager.ViewModel
{
    public partial class RecentViewModel : ObservableObject
    {
        [ObservableProperty]
        private RecentModel model;
        [ObservableProperty]
        ObservableCollection<Profile> profiles;

        public RecentViewModel()
        {
            Model = new();
            Task.Run(async () => Profiles = new(await Model.getProfiles()));
        }

        [RelayCommand]
        void AddNote()
        { 
            
        }
    }
}

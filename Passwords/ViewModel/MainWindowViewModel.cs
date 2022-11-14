using Passwords.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Passwords.ViewModer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPorpertyChanged();
            }
        }
        private MainWindowModel _model;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            _model = new MainWindowModel();
        }

        private void OnPorpertyChanged([CallerMemberName] string property = "")
        {
            if (property != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


    }
}

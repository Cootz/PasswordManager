using Passwords.Model;
using Passwords.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Passwords.ViewModer
{
    public class MainWindowViewModel : AbstractViewModel
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

        public MainWindowViewModel()
        {
            _model = new MainWindowModel();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace Passwords.Model
{
    public class MainWindowModel : AbstractModel
    {
        private Shell _shell;

        public Page CurrentPage 
        {
            get 
            {
                return _currentPage;
            }
            set 
            {
                _currentPage = value;
                OnPorpertyChanged();
            }
        }
        private Page _currentPage = new Page();

        public MainWindowModel()
        {
            _shell = new Shell();
            _shell.SubscribeOnEvent(OnPageChange);
            _shell.RegisterPage(nameof(Recent), new Recent());
        }

        private void OnPageChange()
        {
            _currentPage = Shell.CurrentPage;
        }
    }
}

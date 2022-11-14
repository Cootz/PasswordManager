using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Passwords.ViewModel
{
    /// <summary>
    /// Provides default fucntionality for every ViewModel class
    /// </summary>
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPorpertyChanged([CallerMemberName] string property = "")
        {
            if (property != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

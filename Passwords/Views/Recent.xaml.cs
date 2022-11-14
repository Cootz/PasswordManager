using Passwords.PasData;
using Passwords.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Passwords
{
    /// <summary>
    /// Interaction logic for Recent.xaml
    /// </summary>
    public partial class Recent : Page
    {
        private RecentViewModel viewModel;

        public Recent()
        {
            viewModel = new();
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}

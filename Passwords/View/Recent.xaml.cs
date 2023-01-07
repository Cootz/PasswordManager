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
        public Recent()
        {
            DataContext = new RecentViewModel();
            
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as RecentViewModel).Model.Profiles.Add(new Profile()
            {
                Service = "origin",
                Email = new EMail()
                {
                    Adress = "test2@fda.td"
                },
                Password = "psw",
                Username = "Crio"
            });
        }
    }
}

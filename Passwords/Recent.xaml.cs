using Passwords.PasData;
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
            InitializeComponent();

            //PasswordController.SavePasswords(new Profile[] {
            //    new Profile()
            //    {
            //        Service = "steam",
            //        Email = new EMail()
            //        {
            //            Adress = "test@fda.td"
            //        },
            //        Password = "psw",
            //        Username = null
            //    }
            //});

            List<Profile> profiles = PasswordController.SearhProfiles("Service=\'steam\'").Result;

            foreach (var prof in profiles)
            {

            }
        }
    }
}

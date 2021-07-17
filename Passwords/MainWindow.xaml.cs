using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Passwords.PasData;

namespace Passwords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Task t = new Task(() => PasswordController.Initialize());//Async initializetion
            t.Start();

            InitializeComponent();

            t.Wait();
            PasswordController.SavePasswords(new Profile[] {
                new Profile()
                {
                    Service = "steam",
                    Email = new EMail()
                    {
                        Adress = "test@fda.td"
                    },
                    Password = "psw",
                    Username = null
                } 
            });


        }
    }
}

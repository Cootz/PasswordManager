using System.Threading.Tasks;
using System.Windows;
using Passwords.PasData;
using Passwords.ViewModer;

namespace Passwords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Task initializeTask = PasswordController.Initialize();//Async initializetion

            InitializeComponent();

            DataContext = new MainWindowViewModel();

            Task.WhenAll(initializeTask);

            Shell.GoTo(nameof(Recent));
        }
    }
}

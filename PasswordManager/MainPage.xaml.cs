using PasswordManager.View;

namespace PasswordManager
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;

        public MainPage()
        {
            InitializeComponent();
            Shell.Current.GoToAsync(nameof(RecentPage));
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                CounterBtn.Text = $"Clicked {count} time";
            }
            else
            {
                CounterBtn.Text = $"Clicked {count} times";
            }

            Shell.Current.GoToAsync(nameof(RecentPage));

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
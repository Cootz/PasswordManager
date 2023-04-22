using System.Windows.Input;

namespace PasswordManager.Model.Behavior
{
    public class PasswordValidationBehavior : Behavior<Entry>
    {
        public readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(PasswordValidationBehavior));

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;

            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            ICommand command = GetValue(CommandProperty) as ICommand;

            if (command is not null) 
                MainThread.BeginInvokeOnMainThread(() => command.Execute(null));
        }
    }
}

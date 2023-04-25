using PasswordManager.Validation;
using PasswordManager.ViewModel;
using System.Windows.Input;

namespace PasswordManager.Model.Behavior
{
    public class PasswordValidationBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty ValidationProperty =
            BindableProperty.Create("Validation", typeof(IValidity), typeof(PasswordValidationBehavior));

        public IValidity Validation
        {
            get => (IValidity)GetValue(ValidationProperty);
            set => SetValue(ValidationProperty, value);
        }

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
            Validation?.Validate();
        }
    }
}

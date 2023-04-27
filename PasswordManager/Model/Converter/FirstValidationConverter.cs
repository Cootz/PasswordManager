using PasswordManager.Utils;
using System.Globalization;

namespace PasswordManager.Model.Converter
{
    public class FirstValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IEnumerable<string> validation && validation.Any() ? validation.First() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            ThrowHelper.ThrowNotSupportedException();
    }
}
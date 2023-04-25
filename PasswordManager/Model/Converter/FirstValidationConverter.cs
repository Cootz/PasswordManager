using PasswordManager.Utils;
using PasswordManager.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model.Converter
{
    public class FirstValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var validation = value as IEnumerable<string>;
            return validation.Any() ? validation.First() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ThrowHelper.ThrowNotSupportedException();
    }
}

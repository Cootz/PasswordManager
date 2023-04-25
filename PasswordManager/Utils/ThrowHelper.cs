using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Utils
{
    internal static class ThrowHelper
    {
        internal static T ThrowNotSupportedException<T>() => (T)ThrowNotSupportedException(typeof(T));

        internal static object ThrowNotSupportedException( Type conversionType ) 
        {
            throw new NotSupportedException($"Converting to type {conversionType} is not supported");
        }

        internal static object ThrowNotSupportedException(string message = null)
        { 
            throw new NotSupportedException(message);
        }
    }
}

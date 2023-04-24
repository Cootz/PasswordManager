using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Utils
{
    internal static class ThrowHelper
    {
        internal static T ThrowNotSupportedException<T>()
        {
            throw new NotSupportedException($"Converting to type {typeof(T)} is not supported");
        }
    }
}

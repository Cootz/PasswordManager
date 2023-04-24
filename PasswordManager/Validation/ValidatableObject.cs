using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Validation
{
    /// <summary>
    /// Validates <see cref="Value"/> based on <see cref="Validations"/> rules
    /// </summary>
    public partial class ValidatableObject<T> : ObservableObject, IValidity, IConvertible
    {
        [ObservableProperty]
        private IEnumerable<string> _errors;

        [ObservableProperty]
        private bool _isValid;

        [ObservableProperty]
        private T _value = default;

        public List<IValidationRule<T>> Validations { get; } = new();

        public ValidatableObject()
        { 
            IsValid = true;
            Errors = Enumerable.Empty<string>();
        }

        public bool Validate()
        {
            Errors = Validations
                ?.Where(v => !v.Check(Value))
                ?.Select(v => v.ValidationMessage)
                ?.ToArray()
                ?? Enumerable.Empty<string>();

            IsValid = !Errors.Any();
            return IsValid;
        }

        public TypeCode GetTypeCode() => throw new NotSupportedException();
        public bool ToBoolean(IFormatProvider provider) => throw new NotImplementedException();
        public byte ToByte(IFormatProvider provider) => throw new NotImplementedException();
        public char ToChar(IFormatProvider provider) => throw new NotImplementedException();
        public DateTime ToDateTime(IFormatProvider provider) => throw new NotImplementedException();
        public decimal ToDecimal(IFormatProvider provider) => throw new NotImplementedException();
        public double ToDouble(IFormatProvider provider) => throw new NotImplementedException();
        public short ToInt16(IFormatProvider provider) => throw new NotImplementedException();
        public int ToInt32(IFormatProvider provider) => throw new NotImplementedException();
        public long ToInt64(IFormatProvider provider) => throw new NotImplementedException();
        public sbyte ToSByte(IFormatProvider provider) => throw new NotImplementedException();
        public float ToSingle(IFormatProvider provider) => throw new NotImplementedException();
        public string ToString(IFormatProvider provider) => throw new NotImplementedException();
        public object ToType(Type conversionType, IFormatProvider provider) => Convert.DefaultToType(this, conversionType, provider);
        public ushort ToUInt16(IFormatProvider provider) => throw new NotImplementedException();
        public uint ToUInt32(IFormatProvider provider) => throw new NotImplementedException();
        public ulong ToUInt64(IFormatProvider provider) => throw new NotImplementedException();
    }
}

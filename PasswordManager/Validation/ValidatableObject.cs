using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Utils;
using PasswordManager.Validation.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public TypeCode GetTypeCode() => ThrowHelper.ThrowNotSupportedException<TypeCode>();
        public bool ToBoolean(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<bool>();
        public byte ToByte(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<byte>();
        public char ToChar(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<char>();
        public DateTime ToDateTime(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<DateTime>();
        public decimal ToDecimal(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<decimal>();
        public double ToDouble(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<double>();
        public short ToInt16(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<byte>();
        public int ToInt32(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<int>();
        public long ToInt64(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<long>();
        public sbyte ToSByte(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<sbyte>();
        public float ToSingle(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<float>();
        public string ToString(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<string>();
        public ushort ToUInt16(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<ushort>();
        public uint ToUInt32(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<uint>();
        public ulong ToUInt64(IFormatProvider provider) => ThrowHelper.ThrowNotSupportedException<ulong>();

        public object ToType(Type conversionType, IFormatProvider provider) => conversionType switch
        {
            IValidity or INotifyPropertyChanged or INotifyPropertyChanging or IConvertible => this,
            _ => ThrowHelper.ThrowNotSupportedException(conversionType)
        };
    }
}

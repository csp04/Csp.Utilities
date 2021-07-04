using System;
using System.Globalization;

namespace Csp.Utilities.Wpf.Converters
{
    public abstract class BoolToAnyValueConverter<T> : BaseValueConverter<bool, T>
    {
        public abstract T TrueValue { get; set; }
        public abstract T FalseValue { get; set; }

        protected override T InternalConvert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? TrueValue : FalseValue;
        }

        protected override bool InternalConvertBack(T value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(TrueValue);
        }
    }
}

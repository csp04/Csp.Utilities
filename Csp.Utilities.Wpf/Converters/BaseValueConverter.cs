using System;
using System.Globalization;
using System.Windows.Data;

namespace Csp.Utilities.Wpf.Converters
{
    public abstract class BaseValueConverter<T1, T2> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return InternalConvert((T1)value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return InternalConvertBack((T2)value, targetType, parameter, culture);
        }

        protected abstract T2 InternalConvert(T1 value, Type targetType, object parameter, CultureInfo culture);

        protected abstract T1 InternalConvertBack(T2 value, Type targetType, object parameter, CultureInfo culture);
    }
}

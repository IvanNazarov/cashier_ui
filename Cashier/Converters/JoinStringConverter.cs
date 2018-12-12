using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Cashier.Converters
{
    public class JoinStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;
            if (targetType != typeof(string))
                return null;
            var separator = parameter as string ?? " ";

            return string.Join(separator, values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var separator = parameter as string ?? " ";
            return (value as string)?.Split(new[] { separator }, StringSplitOptions.None).Cast<object>().ToArray();}
    }
}

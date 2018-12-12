using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Cashier.Converters
{
    public class MultiplicationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;
            if (targetType != typeof(string))
                return null;
            var result = 1.0;
            foreach (var item in values)
            {
                result *= (double)item;
            }

            return result.ToString("F2");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

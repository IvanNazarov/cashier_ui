using System;
using System.Globalization;
using System.Windows.Data;

namespace Cashier.Converters
{
    public class SubstractionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            if (targetType != typeof(string))
                return null;

            var result = 0.0;
            var valueLength = values.Length;

            if (valueLength > 0)
                result = (double)values[0];

            for (int i = 1; i < valueLength; i++)
            {
                result -= (double)values[i];
            }
            return result.ToString("# ### ##0.00 руб.",CultureInfo.InvariantCulture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

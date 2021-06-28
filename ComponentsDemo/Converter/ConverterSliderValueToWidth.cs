using System;
using System.Globalization;
using System.Windows.Data;

namespace ComponentsDemo
{
    [ValueConversion(typeof(double), typeof(double))]
    class ConverterSliderValueToWidth : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 10d + 100d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

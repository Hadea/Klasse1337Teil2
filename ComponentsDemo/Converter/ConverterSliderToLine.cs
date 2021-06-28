using System;
using System.Globalization;
using System.Windows.Data;

namespace ComponentsDemo
{
    [ValueConversion(typeof(double), typeof(double))]
    class ConverterSliderToLine : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 300 - (double)value*30;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace ComponentsDemo
{
    /// <summary>
    /// Konvertiert Werte von 0 bis 10 zu 300 bis 0
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    class ConverterSliderToLine : IValueConverter
    {
        // pull
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 300 - (double)value * 30;
        }

        // push (brauchen wir nicht)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

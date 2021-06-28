using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComponentsDemo
{
    [ValueConversion(typeof(bool), typeof(string))]
    class ConverterBoolToString : IValueConverter
    {

        /// <summary>
        /// Konvertiert den Bool in einen String
        /// </summary>
        /// <param name="value">Wert der konvertiert werden soll</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string == "jo");
        }

        /// <summary>
        /// Konvertiert den String in einen Bool
        /// </summary>
        /// <param name="value">Wert der konvertiert werden soll</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value ? "jo" : "nö");
        }
    }
}

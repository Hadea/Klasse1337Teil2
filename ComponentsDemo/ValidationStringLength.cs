using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComponentsDemo
{
    class ValidationStringLength : ValidationRule
    {
        public int MinLength { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ( (value as string).Length < MinLength)
            {
                return new ValidationResult(false, "Zu Kurz!");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MemoryUI
{
    class ValidationName : ValidationRule
    {
        public string RegExRule { get; set; }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regEx = new(RegExRule, RegexOptions.Singleline);
            if (value is null || !regEx.IsMatch(value.ToString()))
                return new ValidationResult(false, "Ungültiger Name");
            return new ValidationResult(true, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAN_XLIX.Validation
{
    class ValidCode : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string code = value as string;

            int num = 0;
            int.TryParse(code, out num);
            if (num>1 && num<1000)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, null);

            }
        }
    }
}

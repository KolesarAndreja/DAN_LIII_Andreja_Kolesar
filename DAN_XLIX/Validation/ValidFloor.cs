using System.Globalization;
using System.Windows.Controls;

namespace DAN_XLIX.Validation
{
    class ValidFloor : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string floor = value as string;

            int num = 0;
            bool b = int.TryParse(floor,out num);
            if (Service.Service.SupervisedFloor(num))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                if (b)
                {
                    return new ValidationResult(false, "This floor has no manager yet");
                }
                else
                {
                    return new ValidationResult(false, "Must be a number");
                }
                
            }
        }
    }
}

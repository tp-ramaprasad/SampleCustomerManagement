using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Common.Validators;

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int minimumAge;
    private readonly int maximumAge;

    public MinimumAgeAttribute(int minimumAge,int maximumAge)
    {
        this.minimumAge = minimumAge;
        this.maximumAge = maximumAge;
    }

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true; // Allow null values
        }

        if (DateTime.TryParse(value.ToString(), out var date))
        {
            return date>= DateTime.Now.AddYears(-maximumAge) && date.AddYears(-minimumAge) <= DateTime.Now;
        }

        return false;
    }
}

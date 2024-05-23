using System.ComponentModel.DataAnnotations;
using Pinewood.Customers.API.Models.Common;

namespace Pinewood.Customers.API.Models.Request;

/// <summary>
/// class for managing the customer contact details
/// </summary>
public class CustomerContactModel : BaseCustomerModel
{
    [Required(ErrorMessage = "Phone Number is mandatory")]
    [Phone]
    [StringLength(14, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 11)]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email Address is mandatory")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {2}")]
    public string EmailAddress { get; set; }
}

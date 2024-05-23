using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models;

/// <summary>
/// class for managing the customer contact details
/// </summary>
public class ContactModel : BaseModel
{
    [DisplayName("Phone Number")]
    [Required(ErrorMessage = "Phone Number is mandatory")]
    [Phone(ErrorMessage ="Invalid phone number")]
    [StringLength(14, ErrorMessage = "Phone number length must be between {2} and {1}", MinimumLength = 11)]
    public string PhoneNumber { get; set; }

    [DisplayName("Email Address")]
    [Required(ErrorMessage = "Email Address is mandatory")]
    [EmailAddress(ErrorMessage ="Invalid Email Address")]
    [StringLength(255)]
    public string EmailAddress { get; set; }
}

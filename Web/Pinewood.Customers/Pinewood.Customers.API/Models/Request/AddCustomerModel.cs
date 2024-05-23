using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.API.Models.Request;

/// <summary>
/// model for adding a new customer details
/// </summary>
public class AddCustomerModel
{
    [Required(ErrorMessage = "First Name is mandatory")]
    [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is mandatory")]
    [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date Of Birth is mandatory")]
    public DateTime DateOfBirth { get; set; }
    public int? GenderId { get; set; }
    public CustomerContactModel Contact { get; set; }
    public AddressModel? Address { get; set; }
    public PreferenceModel? Preference { get; set; }
}

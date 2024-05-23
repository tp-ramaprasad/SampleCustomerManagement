using Pinewood.Customers.API.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.API.Models.Request;

public class UpdateCustomerModel : BaseCustomerModel
{
    [Required(ErrorMessage = "First Name is mandatory")]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is mandatory")]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date Of Birth is mandatory")]
    public DateTime DateOfBirth { get; set; }
    public GenderModel? Gender { get; set; }
    public CustomerContactModel Contact { get; set; }
    public AddressModel? Address { get; set; }
    public PreferenceModel? Preference { get; set; }
}

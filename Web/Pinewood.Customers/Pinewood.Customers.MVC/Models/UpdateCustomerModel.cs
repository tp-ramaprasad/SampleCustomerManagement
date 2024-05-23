using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models;

public class UpdateCustomerModel : BaseActionModel
{
    [DisplayName("First Name")]
    [Required(ErrorMessage = "First Name is mandatory")]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }

    [DisplayName("Last Name")]
    [Required(ErrorMessage = "Last Name is mandatory")]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }

    [DisplayName("Date Of Birth")]
    [Required(ErrorMessage = "Date Of Birth is mandatory")]
    public DateTime DateOfBirth { get; set; }
    public GenderModel? Gender { get; set; }
    public ContactModel Contact { get; set; }
    public AddressModel? Address { get; set; }
    public PreferenceModel? Preference { get; set; }
}

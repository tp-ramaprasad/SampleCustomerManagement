using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models;

public class PreferenceModel : BaseModel
{

    [DisplayName("Vehicle Type")]
    [StringLength(50)]
    public string? VehicleType { get; set; }
    
    [StringLength(100)]
    public string? Brand { get; set; }

    //Email or Phone or SMS
    [DisplayName("Contact Preference")]
    [StringLength(10)]
    [Required(ErrorMessage ="Contact Preference is mandatory")]
    public string? ContactPreference { get; set; }
}

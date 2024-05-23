using System.ComponentModel.DataAnnotations;
using Pinewood.Customers.API.Models.Common;

namespace Pinewood.Customers.API.Models.Request;

public class PreferenceModel : BaseCustomerModel
{
    [StringLength(50)]
    public string? VehicleType { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    //Email or Phone or SMS
    [StringLength(10)]
    public string? ContactPreference { get; set; }
}

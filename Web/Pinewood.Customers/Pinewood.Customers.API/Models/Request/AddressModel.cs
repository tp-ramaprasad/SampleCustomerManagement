using System.ComponentModel.DataAnnotations;
using Pinewood.Customers.API.Models.Common;

namespace Pinewood.Customers.API.Models.Request;

public class AddressModel : BaseCustomerModel
{
    [StringLength(255)]
    public string AddressLine1 { get; set; }

    [StringLength(255)]
    public string? AddressLine2 { get; set; }
    
    [StringLength(100)]
    public string City { get; set; }
    
    [StringLength(100)]
    public string? State { get; set; }
    
    public int? CountryId { get; set; }

    [StringLength(7)]
    public string ZipCode { get; set; }

    public CountryModel? Country { get; set; }
}
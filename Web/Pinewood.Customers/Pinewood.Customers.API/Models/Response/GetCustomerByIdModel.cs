using Pinewood.Customers.API.Models.Common;
using Pinewood.Customers.API.Models.Request;

namespace Pinewood.Customers.API.Models.Response;

public class GetCustomerByIdModel: BaseCustomerModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public GenderModel? Gender { get; set; }
    public CustomerContactModel Contact { get; set; }
    public AddressModel Address { get; set; }
    public PreferenceModel? Preference { get; set; }
}

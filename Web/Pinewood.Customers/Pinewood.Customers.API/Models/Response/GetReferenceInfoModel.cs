using Pinewood.Customers.API.Models.Common;

namespace Pinewood.Customers.API.Models.Response;

public class GetReferenceInfoModel
{
    public IEnumerable<GenderModel> Genders { get; set; }
    
    public IEnumerable<string> Preferences { get; set; }

    public IEnumerable<CountryModel> Countries { get; set; }
}

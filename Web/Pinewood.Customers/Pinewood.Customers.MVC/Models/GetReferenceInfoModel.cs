using System.ComponentModel;

namespace Pinewood.Customers.MVC.Models;
public class GetReferenceInfoModel
{
    [DisplayName("Gender")]
    public IEnumerable<GenderModel> Genders { get; set; }

    [DisplayName("Contact Preference")]
    public IEnumerable<string> Preferences { get; set; }

    [DisplayName("Country")]
    public IEnumerable<CountryModel> Countries { get; set; }
}

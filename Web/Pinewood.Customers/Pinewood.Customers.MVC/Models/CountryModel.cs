using System.ComponentModel;

namespace Pinewood.Customers.MVC.Models;

public class CountryModel
{    
    public int? Id { get; set; }

    [DisplayName("Country")]
    public string Name { get; set; }
}
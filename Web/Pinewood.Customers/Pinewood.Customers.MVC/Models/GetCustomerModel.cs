using System.ComponentModel;

namespace Pinewood.Customers.MVC.Models
{
    public class GetCustomerModel : BaseActionModel
    {
        [DisplayName("First Name")]
        public required string FirstName { get; set; }

        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }        
        public GenderModel? Gender { get; set; }
        public ContactModel Contact { get; set; }
        public AddressModel Address { get; set; }
        public PreferenceModel? Preference { get; set; }
    }
}

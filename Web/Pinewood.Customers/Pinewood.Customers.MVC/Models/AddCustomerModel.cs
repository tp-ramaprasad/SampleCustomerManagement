using Pinewood.Customers.MVC.Common.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models
{
    public class AddCustomerModel:BaseActionModel
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is mandatory")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is mandatory")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string LastName { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Of Birth is mandatory")]
        [MinimumAge(18,120,ErrorMessage ="Please select a valid date")]
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// holds selected gender from gender dropdown
        /// </summary>
        public int? GenderId { get; set; }

        [Required(ErrorMessage = "Contact details are mandatory")]
        public ContactModel Contact { get; set; }
        public AddressModel Address { get; set; }
        public PreferenceModel Preference { get; set; }        
    }
}

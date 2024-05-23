using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models
{
    public class AddressModel:BaseModel
    {
        [Required(ErrorMessage ="Address Line1 is mandatory")]
        [StringLength(255)]
        [DisplayName("Address Line1")]
        public string? AddressLine1 { get; set; }

        [StringLength(255)]
        [DisplayName("Address Line2")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is mandatory")]
        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        public int? CountryId { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(7)]
        [Required(ErrorMessage = "Zip Code is mandatory")]
        public string? ZipCode { get; set; }

        public CountryModel? Country { get; set; }
    }
}
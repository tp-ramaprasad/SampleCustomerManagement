using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.Core.Entities
{
    public class Preference
    {
        [Key]
        public int Id { get; set; }
        public string? VehicleType { get; set; }
        public string? Brand { get; set; }
        public string? ContactPreference { get; set; }        
        public int CustomerId { get; set; } // Required foreign key
        public virtual Customer Customer { get; set; } // Reference navigation
    }
}

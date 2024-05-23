using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.Core.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Address> Addresses { get; set; }
    }
}
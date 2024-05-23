using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.Core.Entities
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual IEnumerable<Customer> Customer { get; set; }
    }
}
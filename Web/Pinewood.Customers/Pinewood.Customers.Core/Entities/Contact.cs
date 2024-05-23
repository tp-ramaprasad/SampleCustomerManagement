using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinewood.Customers.Core.Entities;

/// <summary>
/// class for managing the customer contact details
/// </summary>
/// 

[ComplexType]
public class Contact
{
    [Key]
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public int CustomerId { get; set; } // Required foreign key
    public virtual Customer Customer { get; set; } // Reference navigation
}

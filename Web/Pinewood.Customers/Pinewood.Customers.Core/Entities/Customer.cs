using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinewood.Customers.Core.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastUpdatedDateTime { get; set; }
    public int? GenderId { get; set; }
    public virtual Gender? Gender { get; set; }
    public virtual required Contact Contact { get; set; }
    public virtual Address? Address { get; set; }
    public virtual Preference? Preference { get; set; }  
}

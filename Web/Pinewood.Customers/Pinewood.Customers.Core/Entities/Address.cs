using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinewood.Customers.Core.Entities
{
    [ComplexType]
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }        
        public string? ZipCode { get; set; }

        public int CustomerId { get; set; } // Required foreign key
        public virtual Customer Customer { get; set; } // Reference navigation
        public int? CountryId { get; set; } // Foreign key to Country        
        public virtual Country Country { get; set; } // Reference navigation
    }
}
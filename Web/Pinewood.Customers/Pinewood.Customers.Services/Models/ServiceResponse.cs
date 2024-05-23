using Pinewood.Customers.Services.Enums;

namespace Pinewood.Customers.Services.Models;

public class ServiceResponse
{
    public int Id { get; set; }
    public CustomerOperationStatus Status { get; set; }
    public string? StatusDescription { get; set; }
}

using Pinewood.Customers.Services.Enums;

namespace Pinewood.Customers.API.Models.Response;

public class CustomerOpsResponseModel
{
    public int Id { get; set; }
    public CustomerOperationStatus Status { get; set; }
}

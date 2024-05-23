using Pinewood.Customers.MVC.Common;

namespace Pinewood.Customers.Services.Models;

public class OperationsResponseModel
{
    public int Id { get; set; }

    public int Status { get; set; }
    public CustomerOperationStatus StatusDescription { get { return (CustomerOperationStatus)Status; } }
}

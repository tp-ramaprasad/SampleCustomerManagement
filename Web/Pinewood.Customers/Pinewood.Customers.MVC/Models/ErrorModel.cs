namespace Pinewood.Customers.MVC.Models
{
    public class ErrorViewModel
    {
        public string Error { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    //public class CustomErrorModel
    //{
    //    public int StatusCode { get; set; }
    //    public string? Message { get; set; }
    //    public string? Details { get; set; }

    //    public CustomErrorModel(int statusCode, string? message, string? details = null)
    //    {
    //        StatusCode = statusCode;
    //        Message = message;
    //        Details = details;

    //    }

    //}
}
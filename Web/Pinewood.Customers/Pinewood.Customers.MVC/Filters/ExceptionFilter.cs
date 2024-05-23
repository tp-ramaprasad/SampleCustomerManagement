using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pinewood.Customers.MVC.Models;
using System.Net;

namespace Pinewood.Customers.MVC.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new CustomErrorModel
        (
            (int)HttpStatusCode.InternalServerError,
            context.Exception.Message,
            context.Exception.StackTrace?.ToString()
        );

        context.Result = new JsonResult(error);
    }
}

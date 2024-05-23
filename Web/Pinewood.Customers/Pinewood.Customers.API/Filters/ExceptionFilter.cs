using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pinewood.Customers.API.Models;
using Pinewood.Customers.Helper;
using System.Net;

namespace Pinewood.Customers.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger=logger??throw new ArgumentNullException(nameof(logger));
    }   

    public void OnException(ExceptionContext context)
    {
        logger.LogError(message: context.Exception.Message, args: context.Exception);

        var message = context.Exception.Message;
        int statusCode;
        switch (context.Exception)
        {
            case AppException:
                statusCode = context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                message = context.Exception.Message;
                break;
            case InvalidOperationException:
                statusCode = context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = "Invalid operation";
                break;
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var error = new ErrorModel(statusCode, message, context.Exception.StackTrace?.ToString());

        context.Result = new JsonResult(error);
    }
}

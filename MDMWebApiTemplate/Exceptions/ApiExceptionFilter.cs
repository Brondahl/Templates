using System;
using MDMWebApiTemplate.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StartupPlugins
{
  public class ApiExceptionFilter : ExceptionFilterAttribute
  {
    public class ApiError
    {
      public string message { get; set; }
      public string stackTrace { get; set; }
      public string originalException { get; set; }
      public string allDetails { get; set; }

      public void PopulateFromException(Exception ex)
      {
        message = ex.Message;
        stackTrace = ex.StackTrace;
        originalException = null;
        allDetails = null;

        var baseEx = ex.GetBaseException();
        if (!ReferenceEquals(baseEx, ex))
        {
          originalException = ex.GetBaseException().ToString();
          var innerException = ex.InnerException;
          if (!ReferenceEquals(innerException, baseEx))
          {
            allDetails = ex.InnerException == null ? null : ex.ToString();
          }
        }
      }
    }

    public override void OnException(ExceptionContext context)
    {
      int statusCode;
      var apiError = new ApiError();
      var exception = context.Exception;

      if (exception is HttpStatusException)
      {
        var ex = exception as HttpStatusException;
        statusCode = (int)ex.StatusCode;
        apiError.PopulateFromException(ex);
      }
      else if (context.Exception is UnauthorizedAccessException)
      {
        statusCode = 401;
        apiError.message = "Unauthorized Access";
      }
      else // Other, unrecognised errors
      {
#if !DEBUG
        apiError.message = "An unhandled error occurred.";                
#else
        apiError.PopulateFromException(exception);
#endif
        statusCode = 500;
      }

      // handle logging here

      context.HttpContext.Response.StatusCode = statusCode;
      context.Result = new JsonResult(apiError);
      context.Exception = null;
    }
  }
}

using System;
using System.Net;

namespace MDMWebApiTemplate.Exceptions
{
  public class HttpStatusException : Exception
  {
    public HttpStatusCode StatusCode;

    public HttpStatusException(HttpStatusCode code, string message = null, Exception innerException = null)
      : base(message, innerException)
    {
      StatusCode = code;
    }
  }
}

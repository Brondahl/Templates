using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MDMWebApiTemplate.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MDMWebApiTemplate.Controllers
{
  /*
   * Demonstrates two notable forms of exception handling:
   * A) Handling of errors during databinding (before main the Controller code has been hit)
   * B) Throwing an exception which knows what HTTP response it should cause.
   */
  public class Param
  {
    public Param(int foo, string bar)
    {
      throw new Exception("During Databinding");
    }
  }

  [Route("api/[controller]")]
  [ApiController]
  public class ExceptionHandlingDemoController : ControllerBase
  {
    // GET api/values
    [HttpGet("GeneralExceptionTest")]
    public ActionResult<string> TestGet()
    {
      throw new NotImplementedException("Exception thrown.");
    }

    // GET api/values
    [HttpPost("DatabindingExceptionTest")]
    public ActionResult<string> TestPost(Param input)
    {
      return "";
    }

    // GET api/values/5
    [HttpGet("HttpStatusExceptionTest")]
    public ActionResult<string> Get()
    {
      try
      {
        throw new NotImplementedException("oh dear", new Exception("foobar"));
      }
      catch (Exception e)
      {
        throw new HttpStatusException(HttpStatusCode.Ambiguous, "outer",e);
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MDMWebApiTemplate.Controllers
{
  /*
   * Demonstrates that Enums are serialised to their enum names, rather than to the underlying int value.
   */
  public enum Choice
  {
    DefaultOption,
    BestOption,
    AlternativeOption
  }

  [Route("api/[controller]")]
  [ApiController]
  public class EnumSerialisationDemoController : ControllerBase
  {
    // GET api/values
    [HttpGet("GetEnumValues")]
    public ActionResult<Choice[]> Get()
    {
      return new [] { Choice.DefaultOption, Choice.BestOption, Choice.AlternativeOption };
    }
  }
}

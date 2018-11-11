using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MDMWebApiTemplate.Controllers
{
  /*
   * Demonstrates both automatic binding of all classes to their interface, and also the IEnumerable<> functionality in Autofac
   */
  public interface IChooser
  {
    string Choose();
  }

  public class LazyChooser : IChooser
  {
    public string Choose()
    {
      return "Default";
    }
  }

  public class BestChooser : IChooser
  {
    public string Choose()
    {
      return "Best";
    }
  }

  public class WeirdChooser : IChooser
  {
    public string Choose()
    {
      return "Alternative";
    }
  }

  public class ImplementationWithUnrelatedName : IChooser
  {
    public string Choose()
    {
      return "WeirdName";
    }
  }


  [Route("api/[controller]")]
  [ApiController]
  public class AutofacDemoController : ControllerBase
  {
    private readonly string[] options;
    public AutofacDemoController(IEnumerable<IChooser> choosers)
    {
      options = choosers.Select(chooser => chooser.Choose()).ToArray();
    }

    // GET api/values
    [HttpGet("GetChoiceOptions")]
    public ActionResult<IEnumerable<string>> Get()
    {
      return options;
    }
  }
}

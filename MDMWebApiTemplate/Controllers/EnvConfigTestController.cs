using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StartupPlugins;

namespace MDMWebApiTemplate.Controllers
{
  /*
   * Demonstrates reading config for different environments.
   */
  [Route("api/[controller]")]
  [ApiController]
  public class EnvConfigTestController : ControllerBase
  {
    private readonly SettingsTest config;
    public EnvConfigTestController(IOptions<SettingsTest> config)
    {
      this.config = config.Value;
    }

    // GET api/values
    [HttpGet("ShowConfigValues")]
    public ActionResult<string> Get()
    {
      return $"Global: {config.SettingGlobal} | Per Env: {config.SettingPerEnv}";
    }
  }
}

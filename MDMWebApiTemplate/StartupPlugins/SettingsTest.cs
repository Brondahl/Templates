using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupPlugins
{
  public class SettingsTest
  {
    public string SettingPerEnv { get; set; } = "Not read from File";
    public string SettingGlobal { get; set; } = "Not read from File";
  }
}

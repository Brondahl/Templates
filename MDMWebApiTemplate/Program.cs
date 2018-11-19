using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using StartupPlugins;

namespace MDMWebApiTemplate
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHost(args)
        .MigrateDatabase()
        .Run();
    }

    public static IWebHost CreateWebHost(string[] args)
    {
      return CreateWebHostBuilder(args).Build();
    }

    /// <remarks>
    /// Note that some frameworks (e.g. EntityFramework Core) rely
    /// on finding this method by reflection and invoking it.
    /// Thus refactoring can cause significant problems.  :(
    /// </remarks>
    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(c => c.AddAutofac())
        .UseStartup<Startup>();
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MDMWebApiTemplate
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHost(args).Run();
    }

    public static IWebHost CreateWebHost(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(c => c.AddAutofac())
        .UseStartup<Startup>()
        .Build();
    }
  }
}

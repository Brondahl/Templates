using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using StartupPlugins;

namespace MDMWebApiTemplate
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options =>
        {
          options.Filters.Add(new ApiExceptionFilter());
        })
        .AddJsonOptions(opt =>
        {
          opt.SerializerSettings.Converters.Add(new StringEnumConverter() /* { CamelCaseText = true } */);
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.Configure<SettingsTest>(Configuration);
      services.ConfigureSwaggerService();
    }

    /// <summary>
    ///   All Autoface DI registration code goes in here.
    /// </summary>
    /// <remarks>
    ///   Method is found by Convention by the framework.
    ///   Note that `Configure{Production/Development}Container` methods would also get noticed, by convention.
    /// </remarks>
    /// <seealso cref="https://riptutorial.com/asp-net-core/example/9096/configuring-multiple-environments"/>
    /// <seealso cref="https://github.com/aspnet/Docs/blob/master/aspnetcore/performance/caching/distributed/sample/src/DistCacheSample/Startup.cs"/>
    /// <seealso cref="https://github.com/aspnet/Hosting/blob/rel/1.1.0/src/Microsoft.AspNetCore.Hosting/Internal/StartupLoader.cs"/>
    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        var oneMonth = 60 * 60 * 24 * 30;
        var policyCollection = new HeaderPolicyCollection()
          .AddDefaultSecurityHeaders()
          .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: oneMonth); //https://www.tunetheweb.com/blog/dangerous-web-security-features/

        app.UseSecurityHeaders(policyCollection);
      }

      app
        .UseHttpsRedirection()
        .UseMvc()
        .ConfigureSwagger()
        .UseCors(conf => conf.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    }
  }
}

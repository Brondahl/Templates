using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace StartupPlugins
{
  public static class SwaggerStartup
  {
    public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
      });
      return services;
    }

    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IHostingEnvironment env = null)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
      });
      return app;
    }
  }
}
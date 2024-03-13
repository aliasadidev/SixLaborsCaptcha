using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLaborsCaptcha.Mvc.Core;

namespace AspNetCoreWebAppSample;

#pragma warning disable IDE0300
#pragma warning disable IDE0290

public class Startup
{
  public Startup(IConfiguration configuration)
    => Configuration = configuration;
  public IConfiguration Configuration { get; }

  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddSixLabCaptcha(x =>
    {
      x.DrawLines = 5;
      x.TextColor = new Color[] { Color.Gray, Color.Gray };
    });

    services.AddRazorPages();
  }

  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Error");
    }

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapRazorPages();
      endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
    });
  }
}


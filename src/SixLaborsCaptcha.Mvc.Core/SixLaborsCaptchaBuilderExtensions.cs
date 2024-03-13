using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SixLaborsCaptcha.Core;

namespace SixLaborsCaptcha.Mvc.Core;

public static class SixLaborsCaptchaBuilderExtensions
{
  public static IServiceCollection AddSixLabCaptcha(
     this IServiceCollection services,
     Action<SixLaborsCaptchaOptions> setupAction = null)
  {
    var options = new SixLaborsCaptchaOptions();
    setupAction?.Invoke(options);

    services.TryAddSingleton<ISixLaborsCaptchaModule>(new SixLaborsCaptchaModule(options));
    return services;
  }
}

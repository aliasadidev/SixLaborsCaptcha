using System;
using Microsoft.Extensions.DependencyInjection;
using SixLaborsCaptcha.Core;

namespace SixLaborsCaptcha.Mvc.Core
{
	public static class SixLaborsCaptchaBuilderExtensions
	{
		public static IServiceCollection UseSixLabCaptcha(
		   this IServiceCollection services,
		   Action<SixLaborsCaptchaOptions> setupAction = null)
		{
			var options = new SixLaborsCaptchaOptions();
			if (setupAction != null)
			{
				setupAction(options);
			}

			services.AddSingleton<ISixLaborsCaptchaModule>(new SixLaborsCaptchaModule(options));
			return services;
		}
	}
}

using System.Threading;
using SixLaborsCaptcha.Core;
using SixLabors.ImageSharp;
using System.IO;

namespace ConsoleAppSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
			{
				DrawLines = 5,
				TextColor = new Color[] { Color.Gray },
				DrawLinesColor = new Color[] { Color.Gray }
			});

			for (int i = 0; i < 4; i++)
			{
				var key = Extentions.GetUniqueKey(6);
				var result = slc.Generate(key);
				File.WriteAllBytes($"six-labors-captcha-{i}.png", result);
			}

		}
	}
}

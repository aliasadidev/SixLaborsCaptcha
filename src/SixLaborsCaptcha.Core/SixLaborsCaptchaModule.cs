using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace SixLaborsCaptcha.Core
{
	public class SixLaborsCaptchaModule : ISixLaborsCaptchaModule
	{
		private readonly SixLaborsCaptchaOptions _options;
		public SixLaborsCaptchaModule(SixLaborsCaptchaOptions options)
		{
			_options = options;
		}

		public byte[] Generate(string stringText)
		{
			byte[] result;

			using (var imgText = new Image<Rgba32>(_options.Width, _options.Height))
			{
				float position = 0;
				Random random = new Random();
				byte startWith = (byte)random.Next(5, 10);
				imgText.Mutate(ctx => ctx.BackgroundColor(Color.Transparent));

				string fontName = _options.FontFamilies[random.Next(0, _options.FontFamilies.Length)];
				Font font = SystemFonts.CreateFont(fontName, _options.FontSize, _options.FontStyle);

				foreach (char c in stringText)
				{
					var location = new PointF(startWith + position, 8);
					imgText.Mutate(ctx => ctx.DrawText(c.ToString(), font, _options.TextColor[random.Next(0, _options.TextColor.Length)], location));
					position += TextMeasurer.Measure(c.ToString(), new RendererOptions(font, location)).Width;
				}

				//add rotation 
				AffineTransformBuilder rotation = getRotation();
				imgText.Mutate(ctx => ctx.Transform(rotation));

				// add the dynamic image to original image
				ushort size = (ushort)TextMeasurer.Measure(stringText, new RendererOptions(font)).Width;
				var img = new Image<Rgba32>(size + 10 + 5, _options.Height);
				img.Mutate(ctx => ctx.BackgroundColor(Color.White));



				for (int i = 0; i < _options.DrawLines; i++)
				{
					int x0 = random.Next(0, 20);
					int y0 = random.Next(10, img.Height);
					int x1 = random.Next(70, img.Width);
					int y1 = random.Next(0, img.Height);
					img.Mutate(ctx =>
					ctx.DrawLines(_options.TextColor[random.Next(0, _options.TextColor.Length)],
								  (float)new Random().NextDouble() * 2.5f * 0.7f,
								  new PointF[] { new PointF(x0, y0), new PointF(x1, y1) })
					);
				}

				img.Mutate(ctx => ctx.DrawImage(imgText, 0.80f));

				img.Mutate(x => x.Resize(_options.Width, _options.Height));

				using (var ms = new MemoryStream())
				{
					img.Save(ms, _options.Encoder);
					result = ms.ToArray();
				}
			}

			return result;

		}

		private AffineTransformBuilder getRotation()
		{
			Random random = new Random();
			var builder = new AffineTransformBuilder();
			var width = random.Next(10, _options.Width);
			var height = random.Next(10, _options.Height);
			var pointF = new PointF(width, height);
			var rotationDegrees = random.Next(0, _options.MaxRotationDegrees);
			var result = builder.PrependRotationDegrees(rotationDegrees, pointF);
			return result;
		}

	}
}

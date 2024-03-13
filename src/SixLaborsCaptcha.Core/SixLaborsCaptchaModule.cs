using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace SixLaborsCaptcha.Core;

public class SixLaborsCaptchaModule : ISixLaborsCaptchaModule
{
  private readonly SixLaborsCaptchaOptions _options;
  public SixLaborsCaptchaModule(SixLaborsCaptchaOptions options) => _options = options;

  public byte[] Generate(string stringText)
  {
    byte[] result;

    using (var imgText = new Image<Rgba32>(_options.Width, _options.Height))
    {
      float position = 0;
      var random = new Random();
      var startWith = (byte)random.Next(5, 10);
      imgText.Mutate(ctx => ctx.BackgroundColor(Color.Transparent));

      var fontName = _options.FontFamilies[random.Next(0, _options.FontFamilies.Length)];
      var font = SystemFonts.CreateFont(fontName, _options.FontSize, _options.FontStyle);

      foreach (var c in stringText)
      {
        var location = new PointF(startWith + position, random.Next(6, 13));
        imgText.Mutate(ctx => ctx.DrawText(c.ToString(), font, _options.TextColor[random.Next(0, _options.TextColor.Length)], location));
        position += TextMeasurer.MeasureSize(c.ToString(), new TextOptions(font)).Width;
      }

      // add rotation
      var rotation = getRotation();
      imgText.Mutate(ctx => ctx.Transform(rotation));

      // add the dynamic image to original image
      var size = (ushort)TextMeasurer.MeasureSize(stringText, new TextOptions(font)).Width;
      var img = new Image<Rgba32>(size + 10 + 5, _options.Height);
      img.Mutate(ctx => ctx.BackgroundColor(_options.BackgroundColor[random.Next(0, _options.BackgroundColor.Length)]));


      Parallel.For(0, _options.DrawLines, i =>
      {
        var x0 = random.Next(0, random.Next(0, 30));
        var y0 = random.Next(10, img.Height);
        var x1 = random.Next(img.Width - random.Next(0, (int)(img.Width * 0.25)), img.Width);
        var y1 = random.Next(0, img.Height);
        img.Mutate(ctx =>
                  ctx.DrawLine(_options.DrawLinesColor[random.Next(0, _options.DrawLinesColor.Length)],
                                Extensions.GenerateNextFloat(_options.MinLineThickness, _options.MaxLineThickness),
                                new PointF[] { new(x0, y0), new(x1, y1) })
                  );
      });

      img.Mutate(ctx => ctx.DrawImage(imgText, 0.80f));

      Parallel.For(0, _options.NoiseRate, i =>
      {
        var x0 = random.Next(0, img.Width);
        var y0 = random.Next(0, img.Height);
        img.Mutate(
                      ctx => ctx
                          .DrawLine(_options.NoiseRateColor[random.Next(0, _options.NoiseRateColor.Length)],
                          Extensions.GenerateNextFloat(0.5, 1.5), new PointF[] { new Vector2(x0, y0), new Vector2(x0, y0) })
                  );
      });

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
    var random = new Random();
    var builder = new AffineTransformBuilder();
    var width = random.Next(10, _options.Width);
    var height = random.Next(10, _options.Height);
    var pointF = new PointF(width, height);
    var rotationDegrees = random.Next(0, _options.MaxRotationDegrees);
    var result = builder.PrependRotationDegrees(rotationDegrees, pointF);
    return result;
  }

}


using System.Threading;
using SixLaborsCaptcha.Core;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System;

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
        DrawLinesColor = new Color[] { Color.Gray, Color.Black, Color.DarkGrey, Color.SlateGray },
        FontFamilies = new string[] { "ubuntu" },
      });


      for (int i = 0; i < 10; i++)
      {
        var key = Extensions.GetUniqueKey(6);
        var result = slc.Generate(key);
        File.WriteAllBytes($"six-labors-captcha-{i}.png", result);
        //System.Threading.Thread.Sleep(1000);
      }

    }
  }
}

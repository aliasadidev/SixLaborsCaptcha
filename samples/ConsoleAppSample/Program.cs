using System.IO;
using SixLabors.ImageSharp;
using SixLaborsCaptcha.Core;

#pragma warning disable IDE0300
#pragma warning disable IDE0290

var slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
{
  DrawLines = 5,
  TextColor = new[] { Color.Gray },
  DrawLinesColor = new[] { Color.Gray, Color.Black, Color.DarkGrey, Color.SlateGray },
});

for (var i = 0; i < 10; i++)
{
  var key = Extensions.GetUniqueKey(6);
  var result = slc.Generate(key);
  File.WriteAllBytes($"six-labors-captcha-{i}.png", result);
}

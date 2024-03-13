using System;
using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace SixLaborsCaptcha.Core;

public static class Extensions
{
  public static IImageEncoder GetEncoder(EncoderTypes encoderType)
  {
    IImageEncoder encoder = encoderType switch
    {
      EncoderTypes.Png => new PngEncoder(),
      EncoderTypes.Jpeg => new JpegEncoder(),
      _ => throw new ArgumentException($"Encoder '{encoderType}' not found!"),
    };
    return encoder;
  }


  private static readonly char[] chars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVXYZW23456789".ToCharArray();
  public static string GetUniqueKey(int size)
  {
    var data = new byte[4 * size];
    using (var rng = RandomNumberGenerator.Create())
    {
      rng.GetBytes(data);
    }
    var result = new StringBuilder(size);
    for (var i = 0; i < size; i++)
    {
      var rnd = BitConverter.ToUInt32(data, i * 4);
      var idx = rnd % chars.Length;
      result.Append(chars[idx]);
    }

    return result.ToString();
  }

  public static string GetUniqueKey(int size, char[] chars)
  {
    var data = new byte[4 * size];
    using (var rng = RandomNumberGenerator.Create())
    {
      rng.GetBytes(data);
    }
    var result = new StringBuilder(size);
    for (var i = 0; i < size; i++)
    {
      var rnd = BitConverter.ToUInt32(data, i * 4);
      var idx = rnd % chars.Length;
      result.Append(chars[idx]);
    }

    return result.ToString();
  }

  public static float GenerateNextFloat(double min = -3.40282347E+38, double max = 3.40282347E+38)
  {
    var random = new Random();
    var range = max - min;
    var sample = random.NextDouble();
    var scaled = (sample * range) + min;
    var result = (float)scaled;
    return result;
  }
}

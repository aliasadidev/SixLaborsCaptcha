using System;
using System.Text;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Cryptography;

namespace SixLaborsCaptcha.Core
{
  public static class Extensions
  {
    public static IImageEncoder GetEncoder(EncoderTypes encoderType)
    {
      IImageEncoder encoder;
      switch (encoderType)
      {
        case EncoderTypes.Png:
          encoder = new PngEncoder();
          break;
        case EncoderTypes.Jpeg:
          encoder = new JpegEncoder();
          break;
        default:
          throw new ArgumentException($"Encoder '{ encoderType }' not found!");
      };
      return encoder;
    }


    private static readonly char[] chars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVXYZW23456789".ToCharArray();
    public static string GetUniqueKey(int size)
    {
      byte[] data = new byte[4 * size];
      using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
      {
        crypto.GetBytes(data);
      }
      StringBuilder result = new StringBuilder(size);
      for (int i = 0; i < size; i++)
      {
        var rnd = BitConverter.ToUInt32(data, i * 4);
        var idx = rnd % chars.Length;
        result.Append(chars[idx]);
      }

      return result.ToString();
    }

    public static string GetUniqueKey(int size, char[] chars)
    {
      byte[] data = new byte[4 * size];
      using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
      {
        crypto.GetBytes(data);
      }
      StringBuilder result = new StringBuilder(size);
      for (int i = 0; i < size; i++)
      {
        var rnd = BitConverter.ToUInt32(data, i * 4);
        var idx = rnd % chars.Length;
        result.Append(chars[idx]);
      }

      return result.ToString();
    }

    public static float GenerateNextFloat(double min = -3.40282347E+38, double max = 3.40282347E+38)
    {
      Random random = new Random();
      double range = max - min;
      double sample = random.NextDouble();
      double scaled = (sample * range) + min;
      float result = (float)scaled;
      return result;
    }
  }
}

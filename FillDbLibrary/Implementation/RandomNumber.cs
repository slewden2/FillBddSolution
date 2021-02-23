using System;

namespace FillDbLibrary.Implementation
{
  /// <summary>
  /// Générateur de nombre aléatoire
  /// </summary>
  public class RandomNumber : IRandomNumber
  {
    internal Random random = new Random();

    public long Next(long min, long max) => Convert.ToInt64((max - min) * random.NextDouble()) + min;

    public double Next(double min, double max) => ((max - min) * random.NextDouble()) + min;

    public DateTime Next(DateTime min, DateTime max) => min.AddMilliseconds((max - min).TotalMilliseconds * random.NextDouble());

    public Guid Next() => Guid.NewGuid();
  }
}

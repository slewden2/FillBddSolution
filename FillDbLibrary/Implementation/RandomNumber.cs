using System;

namespace FillDbLibrary.Implementation
{
  /// <summary>
  /// Générateur de nombre aléatoire
  /// </summary>
  public class RandomNumber : IRandomNumber
  {
    internal Random random = new Random(DateTime.Now.Millisecond);

    /// <summary>
    /// Renvoie un nombre entier long généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    public long Next(long min, long max) => Convert.ToInt64((max - min) * random.NextDouble()) + min;

    /// <summary>
    /// Renvoie un nombre réel généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    public double Next(double min, double max) => ((max - min) * random.NextDouble()) + min;

    /// <summary>
    /// Renvoie une date et heure généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    public DateTime Next(DateTime min, DateTime max) => min.AddMilliseconds((max - min).TotalMilliseconds * random.NextDouble());

    /// <summary>
    /// Renvoie un identifiant unique généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    public Guid Next() => Guid.NewGuid();
  }
}

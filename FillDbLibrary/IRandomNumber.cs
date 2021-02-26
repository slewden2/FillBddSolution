using System;

namespace FillDbLibrary
{
  /// <summary>
  /// Générateur de nombre aléatoire
  /// </summary>
  public interface IRandomNumber
  {
    /// <summary>
    /// Renvoie un nombre entier long généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    long Next(long min, long max);

    /// <summary>
    /// Renvoie un nombre réel généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    double Next(double min, double max);

    /// <summary>
    /// Renvoie une date et heure généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    DateTime Next(DateTime min, DateTime max);

    /// <summary>
    /// Renvoie un identifiant unique généré aléatoirement
    /// </summary>
    /// <param name="min">Valeur minimale autorisée (inclue)</param>
    /// <param name="max">Valeur maximale autorisée (inclue)</param>
    /// <returns>le nombre</returns>
    Guid Next();
  }
}

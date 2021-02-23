using System;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Classe de base pour les générateurs de données
  /// </summary>
  internal abstract class BaseGenerator : ITypeGenerator
  {
    internal IRandomNumber random;

    public BaseGenerator(IRandomNumber rnd)
    {
      random = rnd;
    }


    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour </returns>
    public abstract string Generate();
  }
}

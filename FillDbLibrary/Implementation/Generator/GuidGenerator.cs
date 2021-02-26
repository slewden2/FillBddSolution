using System;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de types uniqueidentifier
  /// </summary>
  internal class GuidGenerator : BaseGenerator
  {
    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="GuidGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public GuidGenerator(IRandomNumber rnd)
      : base(rnd)
    {
    }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate()
     => $"'{{{this.GetValue()}}}'";

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>Le guid</returns>
    private Guid GetValue()
      => random.Next();
  }
}

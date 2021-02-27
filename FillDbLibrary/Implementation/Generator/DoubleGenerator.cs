using System.Globalization;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de type decimal, numeric, float, real, money, smallMoney
  /// </summary>
  internal class DoubleGenerator : BaseGenerator
  {
    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="DoubleGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public DoubleGenerator(IRandomNumber rnd)
      : base(rnd)
    {
    }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate()
      => string.Format($"{this.GetValue()}"
        .Replace(",", ".")
        .Replace(NumberFormatInfo.InvariantInfo.NumberGroupSeparator, ""));

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>La valeur double</returns>
    private double GetValue() => random.Next(-500.5, 500.6); // (double.MinValue + 1, double.MaxValue -1); 
  }
}

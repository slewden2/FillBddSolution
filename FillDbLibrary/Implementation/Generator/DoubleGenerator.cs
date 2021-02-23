using System.Globalization;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de type decimal, numeric, float, real, money, smallMoney
  /// </summary>
  internal class DoubleGenerator : BaseGenerator
  {

    public DoubleGenerator(IRandomNumber rnd)
      : base(rnd)
    {
    }

    public override string Generate()
      => string.Format($"{this.GetValue()}"
        .Replace(",", ".")
        .Replace(NumberFormatInfo.InvariantInfo.NumberGroupSeparator, ""));

    private double GetValue() => random.Next(double.MinValue, double.MaxValue); 
  }
}

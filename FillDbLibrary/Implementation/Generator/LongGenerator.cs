using System;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL des types tinyInt, SmallInt, Int, bigInt
  /// </summary>
  internal class LongGenerator : BaseGenerator
  {
    private readonly long minValue;
    private readonly long maxValue;

    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="LongGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public LongGenerator(IRandomNumber rnd, int precision)
      : base(rnd)
    {
      this.minValue = ComputeMin(precision);
      this.maxValue = ComputeMax(precision);
    }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate() => string.Format($"{this.GetValue()}");

    /// <summary>
    /// Calcule la valeur minimale acceptable en fonction de la précision
    /// </summary>
    /// <param name="precision">La précision associé au type SQL Server</param>
    /// <returns>La valeur minimale</returns>
    private static long ComputeMin(int precision)
      => precision switch
      {
        // tinyInt
        3 => byte.MinValue,
        // SmallInt
        5 => short.MinValue,
        // Int
        10 => int.MinValue,
        // bigInt
        19 => long.MinValue,
        // default
        _ => throw new ArgumentException("Wrong Precision (3, 5, 10, 19 expected", nameof(precision)),
      };

    /// <summary>
    /// Calcule la valeur maximale acceptable en fonction de la précision
    /// </summary>
    /// <param name="precision">La précision associé au type SQL Server</param>
    /// <returns>La valeur maximale</returns>
    private static long ComputeMax(int precision)
      => precision switch
      {
        // tinyInt
        3 => byte.MaxValue,
        // SmallInt
        5 => short.MaxValue,
        // Int
        10 => int.MaxValue,
        // bigInt
        19 => long.MaxValue,
        // default
        _ => throw new ArgumentException("Wrong Precision (3, 5, 10, 19 expected", nameof(precision)),
      };

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>La valeur entièr long</returns>
    private long GetValue() => random.Next(this.minValue, this.maxValue);
  }
}

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

    public LongGenerator(IRandomNumber rnd, int precision)
      : base(rnd)
    {
      this.minValue = ComputeMin(precision);
      this.maxValue = ComputeMax(precision);
    }

    public override string Generate() => string.Format($"{this.GetValue()}");

    private long GetValue() => random.Next(this.minValue, this.maxValue);

    private long ComputeMin(int precision)
      => precision switch
      {
        // tinyInt
        3 => byte.MinValue,
        // SmallInt
        5 => short.MinValue,
        // Int
        10 => int.MinValue,
        // bigInt (19)
        _ => long.MinValue,
      };

    private long ComputeMax(int precision)
      => precision switch
      {
        // tinyInt
        3 => byte.MaxValue,
        // SmallInt
        5 => short.MaxValue,
        // Int
        10 => int.MaxValue,
        // bigInt (19)
        _ => long.MaxValue,
      };
  }
}

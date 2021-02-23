using System;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL des types : Date, Time, DateTime, SmallDateTime, DateTime2 et DateTimeOffset
  /// </summary>
  internal class DateTimeGenerator : BaseGenerator
  {
    /// <summary>
    /// Les infos sur le type à générer
    /// </summary>
    private readonly DatTimeTypeInfos dateType;

    public DateTimeGenerator(IRandomNumber rnd, int precision, int maxLength)
      :base (rnd)
    {
      this.dateType = ComputeDateType(precision, maxLength);
    }

    public override string Generate()
    {
      DateTime dt = GetValue();
      string format = $"{{0:{this.dateType.Format}}}";
      
      if (this.dateType.withOffset)
      {
        var offset = random.Next(new DateTime(1, 1, 1), new DateTime(1, 1, 1, 14, 0, 0));
        var offsetPositive = random.Next(0, 1) == 1;
        format +=  $" {(offsetPositive ? "+":"-")}{{1:HH:mm}}";
        return string.Format($"'{format}'", dt, offset);
      }
      else
      {
        return string.Format($"'{format}'", dt);
      }
      
     
    }

    private DateTime GetValue() => random.Next(this.dateType.Min, this.dateType.Max);
   

    private DatTimeTypeInfos ComputeDateType(int precision, int maxLength)
    {
      switch (precision)
      {
        case 10:  // Date
          return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(9999, 12, 31), "yyyy-MM-dd");
        case 16:
          if (maxLength == 4)
          { // SmallDateTime
            return new DatTimeTypeInfos(new DateTime(1900, 1, 1), new DateTime(2079, 6, 6, 23, 59, 59), "yyyy-MM-ddTHH:mm:00");
          }
          else
          { // Time
            return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(0001, 1, 1, 23, 59, 59, 999), "HH:mm:ss.fffffff");
          }
        case 23: // DateTime
          return new DatTimeTypeInfos(new DateTime(1753, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 997), "yyyy-MM-ddTHH:mm:ss.fffffff");
        case 34: // DateTimeOffset
          return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 999), "yyyy-MM-dd HH:mm:ss.fffffff", isOffset: true);
        default: // DateTime2 (27)
          return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 999), "yyyy-MM-ddTHH:mm:ss.fffffff");
      }
    }

    private class DatTimeTypeInfos
    {
      public DatTimeTypeInfos(DateTime min, DateTime max, string format, bool isOffset = false)
      {
        this.Min = min;
        this.Max = max;
        this.Format = format;
        this.withOffset = isOffset;
      }

      public DateTime Min { get; private set; }
      public DateTime Max { get; private set; }
      public string Format { get; private set; }

      public bool withOffset { get; private set; }

      public TimeSpan Span => this.Max - this.Min;
    }
  }
}

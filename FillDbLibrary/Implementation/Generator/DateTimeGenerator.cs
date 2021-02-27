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

    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="DateTimeGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public DateTimeGenerator(IRandomNumber rnd, int precision, int maxLength)
      :base (rnd)
    {
      this.dateType = ComputeDateType(precision, maxLength);
    }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate()
    {
      DateTime dt = GetValue();
      string format = $"{{0:{this.dateType.Format}}}";
      
      if (this.dateType.WithOffset)
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

    /// <summary>
    /// Calcule les infos de ranges des valeurs de date en fonction de la précision et maxlength
    /// </summary>
    /// <param name="precision">Précision fournie par SQL Server</param>
    /// <param name="maxLength">Taille max (Permet de différencer SmallDateTime de time)</param>
    /// <returns>Les infos</returns>
    private static DatTimeTypeInfos ComputeDateType(int precision, int maxLength)
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
          return new DatTimeTypeInfos(new DateTime(1753, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 997), "yyyy-MM-ddTHH:mm:ss.fff");
        case 27: // DateTime2 
          return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 999), "yyyy-MM-ddTHH:mm:ss.fffffff");
        case 34: // DateTimeOffset
          return new DatTimeTypeInfos(new DateTime(1, 1, 1), new DateTime(9999, 12, 31, 23, 59, 59, 999), "yyyy-MM-dd HH:mm:ss.fffffff", isOffset: true);
        default:
          throw new ArgumentException("Precision not valid (10, [16 & 4], 16, 23, 27, 34 expected)", nameof(precision));
      }
    }

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>La valeur date et heure</returns>
    private DateTime GetValue() => random.Next(this.dateType.Min, this.dateType.Max);

    /// <summary>
    /// Classe interne pour géréer les ranges de valeurs d'un date time
    /// </summary>
    private class DatTimeTypeInfos
    {
      public DatTimeTypeInfos(DateTime min, DateTime max, string format, bool isOffset = false)
      {
        this.Min = min;
        this.Max = max;
        this.Format = format;
        this.WithOffset = isOffset;
      }

      public DateTime Min { get; private set; }
      public DateTime Max { get; private set; }
      public string Format { get; private set; }
      public bool WithOffset { get; private set; }
      public TimeSpan Span => this.Max - this.Min;
    }
  }
}

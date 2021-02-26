using EnsureThat;
using System.Linq;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL des types char, nchar 
  /// </summary>
  internal class FixedLengthStringGenerator : BaseGenerator
  {
    private readonly int length;
    private readonly bool isUnicode;

    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="FixedLengthStringGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public FixedLengthStringGenerator(IRandomNumber rnd, int maxLength, bool unicode)
      : base(rnd)
    {
      EnsureArg.IsGt(maxLength, 0, nameof(maxLength));

      this.length = maxLength;
      this.isUnicode = unicode;
    }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate()
      => $"{(this.isUnicode ? "N" : string.Empty)}'{this.GetValue().Replace("'", "''")}'";   

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>La valeur Chaine fixe</returns>
    private string GetValue()
      => new string(Enumerable.Repeat(CharMap.StandardChars, this.length).Select(s => s[(int)random.Next(0, s.Length)]).ToArray());
  }
}

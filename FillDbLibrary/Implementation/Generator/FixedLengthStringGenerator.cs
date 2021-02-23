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

    public FixedLengthStringGenerator(IRandomNumber rnd, int maxLength, bool unicode)
      : base(rnd)
    {
      this.length = maxLength;
      this.isUnicode = unicode;
    }

    public override string Generate()
      => $"{(this.isUnicode ? "N" : string.Empty)}'{this.GetValue()}'";   // pas de ' dans le texte généré !

    private string GetValue()
      => new string(Enumerable.Repeat(CharMap.StandardChars, this.length).Select(s => s[(int)random.Next(0, s.Length)]).ToArray());
  }
}

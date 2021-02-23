using System.Linq;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL des types nvarchar, varchar, ntext, text ou sysname
  /// </summary>
  internal class VariableLengthStringGenerator : BaseGenerator
  {
    private readonly int maximumlength;
    private readonly bool isUnicode;

    public VariableLengthStringGenerator(IRandomNumber rnd, int maxLength, bool unicode)
      : base (rnd)
    {
      this.maximumlength = maxLength;
      this.isUnicode = unicode;
    }
        
    public override string Generate()
      => $"{(this.isUnicode?"N":string.Empty)}'{this.GetValue()}'";   // pas de ' dans le texte généré !

    private string GetValue()
      => new string(Enumerable.Repeat(CharMap.StandardChars, (int)random.Next(0, this.maximumlength)).Select(s => s[(int)random.Next(0, s.Length)]).ToArray());
  }
}

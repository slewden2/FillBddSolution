namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de type Bit
  /// </summary>
  internal class BitGenerator : BaseGenerator
  {
    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="BitGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public BitGenerator(IRandomNumber rnd)
      : base(rnd)
    { }

    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour SQL</returns>
    public override string Generate() 
      => this.GetValue() ? "0" : "1";

    /// <summary>
    /// Calculer la valeur à formatter
    /// </summary>
    /// <returns>La valeur booléenne</returns>
    private bool GetValue()
      => random.Next(0, 1) == 0;
  }
}

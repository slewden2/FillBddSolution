namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de type Bit
  /// </summary>
  internal class BitGenerator : BaseGenerator
  {
    public BitGenerator(IRandomNumber rnd)
      : base(rnd)
    { }

    public override string Generate() 
      => this.GetValue() ? "0" : "1";


    private bool GetValue()
      => random.Next(0, 1) == 0;
  }
}

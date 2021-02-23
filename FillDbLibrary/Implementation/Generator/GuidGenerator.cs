using System;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Génère des textes représentant des valeurs SQL de types uniqueidentifier
  /// </summary>
  internal class GuidGenerator : BaseGenerator
  {
    public GuidGenerator(IRandomNumber rnd)
      : base(rnd)
    {
    }

    public override string Generate()
     => $"'{{{this.GetValue()}}}'";


    private Guid GetValue()
      => random.Next();
  }
}

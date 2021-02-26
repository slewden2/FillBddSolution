using EnsureThat;

namespace FillDbLibrary.Implementation.Generator
{
  /// <summary>
  /// Classe de base pour les générateurs de données
  /// </summary>
  internal abstract class BaseGenerator : ITypeGenerator
  {
    /// <summary>
    /// Mémorise le générateur aléatoire à utiliser
    /// </summary>
    protected IRandomNumber random;

    /// <summary>
    /// Initialise une nouvelle instance de la classes <see cref="BaseGenerator"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire</param>
    public BaseGenerator(IRandomNumber rnd)
    {
      EnsureArg.IsNotNull(rnd, nameof(rnd));

      this.random = rnd;
    }
    
    /// <summary>
    /// Génère une valeur formattée pour être insérée en SQL
    /// </summary>
    /// <returns>La valeur formattée pour </returns>
    public abstract string Generate();
  }
}

namespace FillDbLibrary
{
  /// <summary>
  /// Permet de générer une valeur pour un type de données
  /// </summary>
  public interface ITypeGenerator
    {
        /// <summary>
        /// Génère une valeur pour le type
        /// </summary>
        /// <returns>Le texte SQL correspondant à la valeur généré</returns>
        string Generate();
    }
}

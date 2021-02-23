using System;
using System.Collections.Generic;
using System.Text;

namespace FillDbLibrary
{
    /// <summary>
    /// Factorie en charge de l'instanciation du ou des générateurs
    /// </summary>
    public interface ITypeGeneratorFactory
    {
        /// <summary>
        /// Renvoie le générateur associé au type défini
        /// </summary>
        /// <param name="typeName">Le nom du type</param>
        /// <param name="precision">La précision (ou taille maximale)</param>
        /// <param name="maxLength">La longueur maximale</param>
        /// <returns>Le générateur</returns>
        ITypeGenerator GetGenerator(string typeName, int precision, int maxLength);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

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
        /// <returns></returns>
        string Generate();
    }
}

using EnsureThat;
using FillDbLibrary.Implementation.Generator;
using System;
using System.Collections.Generic;

namespace FillDbLibrary.Implementation
{
  /// <summary>
  /// Classe qui fournit le bon générateur aléatoire en fonction des informations de Type SQL Server
  /// </summary>
  public class TypeGeneratorFactory : ITypeGeneratorFactory
  {
    private Dictionary<string, ITypeGenerator> generators = new Dictionary<string, ITypeGenerator>();
    private readonly IRandomNumber random;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="TypeGeneratorFactory"/>
    /// </summary>
    /// <param name="rnd">Le générateur aléatoire à utiliser</param>
    public TypeGeneratorFactory(IRandomNumber rnd)
    {
      EnsureArg.IsNotNull(rnd, nameof(rnd));

      this.random = rnd;
    }

    /// <summary>
    /// Renvoie le générateur typé
    /// </summary>
    /// <param name="typeName">Nom du type sql Server</param>
    /// <param name="precision">Précision associé au type</param>
    /// <param name="maxLength">Taille maximale (principalement utilisé pour les chaines, mais aussi pour distinguer un smallDateTime d'une time)</param>
    /// <returns>Le générateur typé</returns>
    public ITypeGenerator GetGenerator(string typeName, int precision, int maxLength)
    {
      string key = GetKey(typeName, precision, maxLength);
      if (!generators.ContainsKey(key))
      {
        var gen = InsertGenerator(typeName, precision, maxLength);
        if (gen != null)
        {
          this.generators.Add(key, gen);
        }
        else
        {
          throw new NotImplementedException($"The type '{typeName}' is unknowed");
        }
      }

      return generators[key];
    }

    private string GetKey(string typeName, int precision, int maxLength) => $"{typeName}:{precision}:{maxLength}";

    private ITypeGenerator InsertGenerator(string typeName, int precision, int maxLength)
    {
      return (typeName.ToLower()) switch
      {
        "bit"
          => new BitGenerator(this.random),

        "char"
          => new FixedLengthStringGenerator(this.random, maxLength, false),

        "nchar"
          => new FixedLengthStringGenerator(this.random, maxLength, true),

        "date" or "datetime" or "datetime2" or "time" or "smalldatetime" or "datetimeoffset" 
          => new DateTimeGenerator(this.random, precision, maxLength),

        "nvarchar" or "ntext" or "sysname"
          => new VariableLengthStringGenerator(this.random, maxLength, true),

        "varchar" or "text" 
          => new VariableLengthStringGenerator(this.random, maxLength, false),

        "decimal" or "numeric" or "float" or "real" or "money" or "smallmoney"
          => new DoubleGenerator(this.random),

        "tinyint" or "smallint" or "int" or "bigint" 
          => new LongGenerator(this.random, precision),

        "uniqueidentifier" 
          => new GuidGenerator(this.random),

        "binary" or "geography" or "geometry" or "hierarchyid" or "image" or "sql_variant" or "varbinary" or "xml" 
          => throw new NotImplementedException($"The type '{typeName}' is valid, but it is not yet implemented"),

        "timestamp" 
          => throw new ArgumentException($"The type '{typeName}' is valid, but it cannot be generated"),

        _ => null,
      };
    }
  }
}
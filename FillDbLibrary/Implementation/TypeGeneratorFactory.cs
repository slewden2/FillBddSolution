using FillDbLibrary.Implementation.Generator;
using System;
using System.Collections.Generic;

namespace FillDbLibrary.Implementation
{
  public class TypeGeneratorFactory : ITypeGeneratorFactory
  {
    private Dictionary<string, ITypeGenerator> generators = new Dictionary<string, ITypeGenerator>();
    private readonly IRandomNumber random;

    public TypeGeneratorFactory(IRandomNumber rnd)
    {
      this.random = rnd;
    }

    public ITypeGenerator GetGenerator(string typeName, int precision, int maxLength)
    {
      string key = GetKey(typeName, precision, maxLength);
      if (!generators.ContainsKey(key))
      {
        InsertGenerator(typeName, precision, maxLength);
      }

      return generators[key];
    }

    private string GetKey(string typeName, int precision, int maxLength) => $"{typeName}:{precision}:{maxLength}";

    private void InsertGenerator(string typeName, int precision, int maxLength)
    {
      ITypeGenerator gen = null;
      switch (typeName.ToLower())
      {
        case "bit":
          gen = new BitGenerator(this.random);
          break;
        case "char":
          gen = new FixedLengthStringGenerator(this.random, maxLength, false);
          break;
        case "nchar":
          gen = new FixedLengthStringGenerator(this.random, maxLength, true);
          break;
        case "date":
        case "datetime":
        case "datetime2":
        case "time":
        case "smalldatetime":
        case "datetimeoffset":
          gen = new DateTimeGenerator(this.random, precision, maxLength);
          break;
        case "nvarchar":
        case "ntext":
        case "sysname":
          gen = new VariableLengthStringGenerator(this.random, maxLength, true);
          break;
        case "varchar":
        case "text":
          gen = new VariableLengthStringGenerator(this.random, maxLength, false);
          break;

        case "decimal":
        case "numeric":
        case "float":
        case "real":
        case "money":
        case "smallmoney":
          gen = new DoubleGenerator(this.random);
          break;

        case "tinyint":
        case "smallint":
        case "int":
        case "bigint":
          gen = new LongGenerator(this.random, precision);
          break;
        case "uniqueidentifier":
          gen = new GuidGenerator(this.random);
          break;

        case "binary":
        case "geography":
        case "geometry":
        case "hierarchyid":
        case "image":
        case "sql_variant":
        
        case "varbinary":
        case "xml":
          throw new NotImplementedException($"The type '{typeName}' is valid, but it is not yet implemented");

        case "timestamp":
          throw new ArgumentException($"The type '{typeName}' is valid, but it cannot be generated");
      }

      if (gen != null)
      {
        this.generators.Add(typeName, gen);
      }
      else
      {
        throw new NotImplementedException($"The type '{typeName}' is unknowed");
      }
    }
  }
}
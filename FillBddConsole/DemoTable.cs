using FillDbLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillBddConsole
{
  public class DemoTable
  {
    public IEnumerable<DemoRowDefinition> ColumnDefinitions { get; private set; }
    public string TableName { get; private set; }
    public DemoTable(IDBConnection cnn, ITypeGeneratorFactory factory, string table)
    {
      this.TableName = table;
      this.ColumnDefinitions = cnn.Query<DemoRowDefinition>(DemoRowDefinition.GetSqlSelect(this.TableName)).Where(cl => cl.IsGenerabe).OrderBy(x => x.Order).ToList();
      foreach (var c in ColumnDefinitions)
      {
        c.Generator = factory.GetGenerator(c.TypeName, c.Precision, c.MaxLength);
      }
    }

    public void Insert(IDBConnection cnn, int nbRows)
    {
      for (int i = 0; i < nbRows; i++)
      {
        StringBuilder sql = new StringBuilder();
        sql.Append($"INSERT INTO {this.TableName} (");
        sql.Append(string.Join(", ", this.ColumnDefinitions.Select(x => x.Column)));
        sql.Append(") VALUES (");
        sql.Append(string.Join(", ", this.ColumnDefinitions.Select(x => x.Generator.Generate())));
        sql.Append(")");

        Console.WriteLine(sql.ToString());

         cnn.Execute(sql.ToString());
      }
    }
  }
}

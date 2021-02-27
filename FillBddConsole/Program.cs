using Dapper;
using FillDbLibrary.Bdd;
using FillDbLibrary.Implementation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FillBddConsole
{
  public class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");


      using var cnn = new DBConnection<SqlServerDataBaseProvider>();
      var rnd = new RandomNumber();
      var factory = new TypeGeneratorFactory(rnd);
      var tbl = new DemoTable(cnn, factory, "[dbo].[demo]");
      tbl.Insert(cnn, 1);

      Console.WriteLine("fin");
    }
  }

  /*
////      var sql = @"
////SELECt n.Demo, n.Nom, t.c1,  t.c2
////FROM dbo.leNom n
////INNER JOIN dbo.Table1 t on n.c1 = t.c1
////;";
  //////var lst = cnn.Query<Table1>("SELECT * FROM dbo.Table1 WHERE c1 in @ids", new { ids = new int[] { 1, 2 } });
  ////var lst = cnn.Query<LeNom, Table1, LeNom>(sql, (leNom, table1) => 
  ////{ 
  ////  leNom.Fils = table1; 
  ////  return leNom; 
  ////}, splitOn:"c1");
  ////foreach(var t in lst)
  ////{
  ////  Console.WriteLine(t);
  ////}
  ////Console.WriteLine();
////        sql = @"
////SELECT * FROM dbo.leNom
////SELECT * FROM dbo.Table1
////";
////        using(var grid = cnn.QueryMultiple(sql))
////        {
////          var lstNom = grid.Read<LeNom>();
////          var lstOrders = grid.Read<Table1>();

////          Console.WriteLine("Les Noms");
////          foreach (var t in lstNom)
////          {
////            Console.WriteLine(t);
////          }

////          Console.WriteLine("Les Table1");
////          foreach (var t in lstOrders)
////          {
////            Console.WriteLine(t);
////          }
////        }

}
}
}

public class Table1
{
public int c1 { get; set; }
public string c2 { get; set; }

public override string ToString() => $"c1:{c1}, c2:{c2}";
}

public class LeNom
{
public int Demo { get; set; }
public string Nom { get; set; }

public int c1 { get; set; }
public Table1 Fils { get; set; }

public override string ToString() => $"Clé = {Demo} : {Nom} mon fils({c1}) => {Fils};";
}

*/
}

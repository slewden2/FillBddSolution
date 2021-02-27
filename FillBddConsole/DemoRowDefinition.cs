using FillDbLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillBddConsole
{
  public class DemoRowDefinition
  {

    public static string GetSqlSelect(string tableName)
      => $@"
SELECT  OBJECT_SCHEMA_NAME(t.object_id) as [schema], t.name as [table], c.name as [column], c.column_id as [order]
, y.name as type_name, c.max_length, c.precision
, c.is_nullable, c.is_rowguidcol, c.is_identity, c.is_computed
From sys.tables t 
INNER JOIN sys.columns c ON t.object_id = c.object_id
INNER JOIN sys.types y On c.system_type_id = y.system_type_id AND c.user_type_id = y.user_type_id
WHERE t.object_id =  OBJECT_ID('{tableName}')
;
";
    public string Schema { get; set; }
    public string Table { get; set; }
    public string Column { get; set; }
    public int Order { get; set; }
        
    public string TypeName { get; set; }
        
    public int MaxLength { get; set; }
    public int Precision { get; set; }
        
    public bool IsNullable { get; set; }
        
    public bool IsRowGuidCol { get; set; }
    
    public bool IsIdentity { get; set; }

    public bool IsComputed { get; set; }

    public override string ToString() => $"{Order:00} {Schema}.{Table}.{Column} [{TypeName}, {Precision}, {MaxLength}] => {(IsNullable?"Null":"not Null")}{(IsRowGuidCol ? ", RowGUIid" : "")}{(IsIdentity ? ", Identity" : "")}{(IsComputed ? ", Computed" : "")}";

    public bool IsGenerabe 
      => !this.IsComputed 
      && !this.IsIdentity 
      && !this.IsRowGuidCol 
      && this.TypeName.ToLower() != "timestamp"
      && this.TypeName.ToLower() != "xml";

    public ITypeGenerator Generator { get; set; }

  }
}

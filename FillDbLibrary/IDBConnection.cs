using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillDbLibrary
{
  public interface IDBConnection : IDisposable
  {
    IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);

    int Execute(string sql, object param = null, CommandType? commandType = null);
  }
}

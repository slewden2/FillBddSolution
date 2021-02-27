using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using static Dapper.SqlMapper;

namespace FillDbLibrary.Bdd
{
  public class DBConnection<TProvider> : IDBConnection where TProvider : DatabaseProvider
  {
    protected DbConnection connection;
    protected SqlTransaction transaction;
    protected int? commandTimeout = null;
    protected bool bufferd = true;

    public TProvider Provider { get; } = DatabaseProvider<TProvider>.Instance;

    public DBConnection()
    {
      Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public void Dispose()
    {
      this.connection?.Dispose();
      this.connection = null;
      this.Provider?.Dispose();
    }

    public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null)
    {
      return this.Connection.Query<T>(sql, param, transaction, bufferd, commandTimeout, commandType);
    }

    public T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null)
    {
      return this.Connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
    }
    public T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null)
    {
      return this.Connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType? commandType = null)
    {
      return this.Connection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, bufferd, splitOn, commandTimeout, commandType);
    }

    public GridReader QueryMultiple(string sql, object param = null, string splitOn = "Id", CommandType? commandType = null)
    {
      return this.Connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
    }

    public int Execute(string sql, object param = null, CommandType? commandType = null)
    {
      return this.Connection.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    public int ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null)
    {
      return this.Connection.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    protected DbConnection Connection => connection ??= Provider.GetOpenConnection();
    //protected DbConnection GetOpenConnection() => Provider.GetOpenConnection();
    //protected DbConnection GetClosedConnection() => Provider.GetClosedConnection();

    protected static CultureInfo ActiveCulture
    {
      get => Thread.CurrentThread.CurrentCulture;
      set => Thread.CurrentThread.CurrentCulture = value;
    }
  }
}

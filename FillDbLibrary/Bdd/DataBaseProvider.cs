using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;

namespace FillDbLibrary.Bdd
{
  public static class DatabaseProvider<TProvider> where TProvider : DatabaseProvider
  {
    public static TProvider Instance {  get; } = Activator.CreateInstance<TProvider>();
  }


  public abstract class DatabaseProvider
  {
    public abstract DbProviderFactory Factory { get; }

    public virtual void Dispose() { }

    public abstract string GetConnectionString();

    public DbConnection GetOpenConnection()
    {
      var conn = Factory.CreateConnection();
      conn.ConnectionString = GetConnectionString();
      conn.Open();
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidOperationException("should be open!");
      }

      return conn;
    }
    
    public DbConnection GetClosedConnection()
    {
      var conn = Factory.CreateConnection();
      conn.ConnectionString = GetConnectionString();
      if (conn.State != ConnectionState.Closed)
      {
        throw new InvalidOperationException("should be closed!");
      }

      return conn;
    }

    public DbParameter CreateRawParameter(string name, object value)
    {
      var p = Factory.CreateParameter();
      p.ParameterName = name;
      p.Value = value ?? DBNull.Value;
      return p;
    }

    protected static string GetConnectionString(IConfiguration configuration, string name)
                            => configuration.GetConnectionString(name);

  }
}

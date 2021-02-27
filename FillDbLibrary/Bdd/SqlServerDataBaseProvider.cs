using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;

namespace FillDbLibrary.Bdd
{
  public sealed class SqlServerDataBaseProvider : DatabaseProvider
  {
    private const string APPSETTINGCONNECTIONSTRINGRUBRIQUE = "base";
    private readonly string configurationString;

    public SqlServerDataBaseProvider()
    {
      var config = AppSettingsJSonConfiguration.Configuration;
      this.configurationString = config.GetConnectionString(APPSETTINGCONNECTIONSTRINGRUBRIQUE);
    }

    public override DbProviderFactory Factory => System.Data.SqlClient.SqlClientFactory.Instance;

    public override string GetConnectionString() => configurationString;
    public DbConnection GetOpenConnection(bool mars)
    {
      if (!mars)
      {
        return GetOpenConnection();
      }

      var scsb = Factory.CreateConnectionStringBuilder();
      scsb.ConnectionString = GetConnectionString();
      ((dynamic)scsb).MultipleActiveResultSets = true;

      var conn = Factory.CreateConnection();
      conn.ConnectionString = scsb.ConnectionString;
      conn.Open();
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidOperationException("should be open!");
      }

      return conn;
    }
  }
}

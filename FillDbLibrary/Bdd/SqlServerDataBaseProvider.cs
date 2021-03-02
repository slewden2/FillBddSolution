using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;

namespace FillDbLibrary.Bdd
{
  /// <summary>
  /// Fournit un moyen unifié de se connecter à un serveur SQL Server
  /// </summary>
  public sealed class SqlServerDataBaseProvider : DatabaseProvider
  {
    private const string APPSETTINGCONNECTIONSTRINGRUBRIQUE = "base";
    private readonly string configurationString;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="SqlServerDataBaseProvider"/>
    /// </summary>
    public SqlServerDataBaseProvider()
    {
      var config = AppSettingsJSonConfiguration.Configuration;
      this.configurationString = config.GetConnectionString(APPSETTINGCONNECTIONSTRINGRUBRIQUE);
    }

    /// <summary>
    /// Obtient une valeur indiquant le fournisseur de database
    /// </summary>
    public override DbProviderFactory Factory => System.Data.SqlClient.SqlClientFactory.Instance;

    /// <summary>
    /// Renvoie la chaine de connexion à la base de données
    /// </summary>
    /// <returns>la chaine de connexion</returns>
    public override string GetConnectionString() => configurationString;

    /// <summary>
    /// Renvoie une chaine de connexion
    /// </summary>
    /// <param name="mars">Indique si la connexiont doit accepter les jeux de résultats actifs multiples (Multiple Active Result Set)</param>
    /// <returns>une connexion ouverte</returns>
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

using System;
using System.Data;
using System.Data.Common;

namespace FillDbLibrary.Bdd
{
  /// <summary>
  ///  Factory pour la construction et le maintien d'une connection
  /// </summary>
  /// <typeparam name="TProvider">LE type de fournisseur de base</typeparam>
  public static class DatabaseProvider<TProvider> where TProvider : DatabaseProvider
  {
    /// <summary>
    /// Fournit le cnnecteur 
    /// </summary>
    public static TProvider Instance {  get; } = Activator.CreateInstance<TProvider>();
  }


  /// <summary>
  /// Classe de base pour les provider de connexion à une base de données
  /// </summary>
  public abstract class DatabaseProvider
  {
    /// <summary>
    /// Obtient une valeur indiquant le fournisseur de database
    /// </summary>
    public abstract DbProviderFactory Factory { get; }

    /// <summary>
    /// Ferme, nettoie et libère les ressources liées à la connexion
    /// </summary>
    public virtual void Dispose() { }

    /// <summary>
    /// Renvoie la chaine de connexion à la base de données
    /// </summary>
    /// <returns>la chaine de connexion</returns>
    public abstract string GetConnectionString();

    /// <summary>
    /// Renvoie une connexion ouverte
    /// </summary>
    /// <returns>une connexion ouverte</returns>
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

    /// <summary>
    /// Renvoie une connexion fermée (mais prête à être ouverte)
    /// </summary>
    /// <returns>une connexion fermée</returns>
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
  }
}

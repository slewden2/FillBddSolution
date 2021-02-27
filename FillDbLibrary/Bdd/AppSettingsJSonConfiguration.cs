using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FillDbLibrary.Bdd
{
  /// <summary>
  /// Helper pour fournir la configuration de l'application
  /// </summary>
  public static class AppSettingsJSonConfiguration
  {
    private static IConfiguration configuration = null;

    /// <summary>
    /// Renvoie la configuration de l'application
    /// </summary>
    public static IConfiguration Configuration
    {
      get
      {
        if (configuration == null)
        {
          configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        return configuration;
      }
    }
  }
}

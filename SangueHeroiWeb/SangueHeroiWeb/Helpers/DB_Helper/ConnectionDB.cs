using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class ConnectionDB
    {
        public static string connectionString { get; set; }
        public static string providerName { get; set; }

        static ConnectionDB()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["connectionName"].ConnectionString;
                providerName = ConfigurationManager.ConnectionStrings["connectionName"].ProviderName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ConnectionString
        {
            get { return connectionString; }
        }

        public static string ProviderName
        {
            get { return providerName; }
        }
    }
}
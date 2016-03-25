using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace SangueHeroiWeb.DAO
{
    public class DB
    {
        public string connectionString { get; set; }

        public DB()
        {
            try
            {
                connectionString = WebConfigurationManager.ConnectionStrings["BANCO"].ConnectionString;
            }
            catch (Exception)
            {
            }
        }
    }
}
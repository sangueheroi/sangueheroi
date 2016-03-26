using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class ContextHelpers
    {
        public static DbCommand CreateCommand(string cmdText, CommandType cmdType)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);

            DbConnection conn = factory.CreateConnection();

            conn.ConnectionString = ConnectionDB.ConnectionString;

            var cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 7200;

            return cmd;
        }

        public Object ExecuteCommand(string cmdText, CommandType cmdType, TypeCommand typeCmd)
        {
            DbCommand command = CreateCommand(cmdText, cmdType);

            Object objRetorno = null;

            try
            {
                command.Connection.Open();

                switch (typeCmd)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objRetorno = command.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objRetorno = command.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objRetorno = command.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        var table = new DataTable();
                        var reader = command.ExecuteReader();

                        table.Load(reader);

                        reader.Close();
                        objRetorno = table;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Executar Comando");
            }
            finally
            {
                if(typeCmd != TypeCommand.ExecuteReader)
                {
                    if(command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();

                    command.Connection.Dispose();
                }
            }

            return objRetorno;
        }

        public enum TypeCommand
        {
            ExecuteNonQuery,
            ExecuteReader,
            ExecuteScalar,
            ExecuteDataTable
        }
    }
}
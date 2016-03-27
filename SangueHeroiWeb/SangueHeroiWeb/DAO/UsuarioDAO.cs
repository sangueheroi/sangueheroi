using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class UsuarioDAO
    {
        ContextHelpers context;

        public UsuarioDAO()
        {
            context = new ContextHelpers();
        }



        public void IncluiOuAltera(UsuarioModel model)
        {
            var strQuery = "";

            var a = "aaa" + Environment.CommandLine + "aa";

            if (model.ID_USUARIO == 0)
            {
                //cadastrar
            }
            else
            {
                //editar
            }

            var sqlTeste = "SELECT * FROM TB_USUARIO";
            var dt = new DataTable();


            dt = (DataTable)context.ExecuteCommand(sqlTeste, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            foreach (DataRow data in dt.Rows)
            {
                var a1 = data["ID_USUARIO"];
                var b = data["NOME_USUARIO"];
            }
        }
    }
}
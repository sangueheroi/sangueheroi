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

            if (model.CODIGO_USUARIO == 0)
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

        public List<UsuarioHeroiModel> GetInformacoesHerois()
        {
            var strQuery = String.Format("SELECT * FROM USUARIO_HEROI");

            List<UsuarioHeroiModel> lst = new List<UsuarioHeroiModel>();

            DataTable dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    UsuarioHeroiModel md = new UsuarioHeroiModel();

                    md.CODIGO_HEROI = Convert.ToInt32(data["CODIGO_HEROI"].ToString());
                    md.NOME_HEROI = data["NOME_HEROI"].ToString();
                    md.CARACTERISTICA_HEROI = data["CARACTERISTICA_HEROI"].ToString();
                    md.DESCRICAO_HEROI = data["DESCRICAO_HEROI"].ToString();
                    lst.Add(md);
                }
            }

            return lst;
        }
    }
}
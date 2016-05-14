using SangueHeroiWeb.Helpers.Util_Helper;
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

        public int Registrar(UsuarioModel model)
        {
            int registroOK;

            string strQuery = "";

            string strQuery2 = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count == 0)
            {
                strQuery = "EXECUTE frmRegistrarUsuario " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_NASCIMENTO) + " , " + Environment.NewLine;

                if (model.DATA_ULTIMA_DOACAO != null)
                    strQuery = strQuery + UtilHelper.DateTimeParaSQLDate(model.DATA_ULTIMA_DOACAO) + " , " + Environment.NewLine;
                else
                    strQuery = strQuery + model.CODIGO_HEROI + " , " + Environment.NewLine
                 + model.FLAG_CADASTRO_REDE_SOCIAL + " ;";

                try
                {
                    var a = context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    registroOK = 1;
                }
                catch (Exception)
                {
                    registroOK = 2;
                }
            }
            else
                registroOK = 0;

            return registroOK;
        }

    }
}
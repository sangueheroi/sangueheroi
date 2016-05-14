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

        public int Registrar(UsuarioModel model)
        {
            int registroOK = 1;
            bool flag = model.FLAG_CADASTRO_REDE_SOCIAL;

            string strQueryUpdate = "";
            string strQueryInsert = "";

            string strQueryConsultaEmail = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);
            
            DataTable dt = new DataTable();
            
            dt = (DataTable)context.ExecuteCommand(strQueryConsultaEmail, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0 && flag == true)
            {
                int codigo_usuario = 0;

                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());
                
                model.FLAG_CADASTRO_REDE_SOCIAL = false;
                strQueryUpdate = "EXECUTE frmAtualizarUsuario " + Environment.NewLine
                 + codigo_usuario + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_NASCIMENTO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_ULTIMA_DOACAO) + " , " + Environment.NewLine
                 + model.CODIGO_HEROI + " , " + Environment.NewLine
                 + model.FLAG_CADASTRO_REDE_SOCIAL + " ;";

                 try
                 {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    registroOK = 1;
                 }
                 catch (Exception)
                 {
                    registroOK = 2;
                 }

            }
            if (dt.Rows.Count == 0)
            {
                strQueryInsert = "EXECUTE frmRegistrarUsuario " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_NASCIMENTO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_ULTIMA_DOACAO) + " , " + Environment.NewLine
                 + model.CODIGO_HEROI + " , " + Environment.NewLine
                 + model.FLAG_CADASTRO_REDE_SOCIAL + " ;";

                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    registroOK = 1;
                }
                catch (Exception)
                {
                    registroOK = 2;
                }
            }
            else if(dt.Rows.Count != 0 && flag == false)
                registroOK = 0;

            return registroOK;
        }

    }
}
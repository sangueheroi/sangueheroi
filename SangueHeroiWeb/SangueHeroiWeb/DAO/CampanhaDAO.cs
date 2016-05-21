using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class CampanhaDAO
    {
        ContextHelpers context;
        protected SmtpClient SmtpClient { get; set; }
        protected MailMessage MailMessage { get; set; }

        public CampanhaDAO()
        {
            context = new ContextHelpers();
        }

        public int CadastrarCampanha(CampanhaModel cmodel, UsuarioModel umodel)
        {
            int cadastroOK = (int) SITUACAO.DADOS_INVALIDOS;

            string strQueryInsert = "";

            string strQueryConsultaCodigo = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", umodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                int codigo_usuario = 0;

                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryInsert = "EXECUTE frmCadastrarCampanha " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.DESCRICAO_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_RECEPTOR) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_INICIO_DT) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_FIM_DT) + " , " + Environment.NewLine
                 + codigo_usuario + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_HOSPITAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.LOGRADOURO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.BAIRRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.ESTADO) + " ;";

                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    cadastroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

            }
            else if (dt.Rows.Count == 0)
            {
                cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return cadastroOK;
        }

        public int Cadastrar(CampanhaModel model)
        {
            string strQuery = "";

            strQuery = "EXECUTE frmCadastrarCampanha " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.DESCRICAO_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_INICIO_DT) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_FIM_DT) + " ;";

            try
            {
                var a = context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception)
            {
                throw;
                return 0;
            }

            return 1;
        }

        public List<CampanhaModel> consultarCampanhas()
        {
            string strQuery = "";

            strQuery = String.Format("SELECT U.NOME_USUARIO, C.CODIGO_CAMPANHA, C.NOME_CAMPANHA, C.DESCRICAO_CAMPANHA, C.NOME_RECEPTOR, C.TIPO_SANGUINEO, C.DATA_INICIO, C.DATA_FIM, CE.NOME_HOSPITAL, CE.LOGRADOURO, CE.BAIRRO, CE.CIDADE, CE.ESTADO, CE.CEP FROM CAMPANHA C INNER JOIN CAMPANHA_ENDERECO CE ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO ");

            DataTable dt = new DataTable();

            List<CampanhaModel> list = new List<CampanhaModel>();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    CampanhaModel campanha = new CampanhaModel();

                    campanha.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                    campanha.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    campanha.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();
                    campanha.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                    campanha.NOME_RECEPTOR = data["NOME_RECEPTOR"].ToString();
                    campanha.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                    campanha.NOME_HOSPITAL = data["NOME_HOSPITAL"].ToString();
                    campanha.LOGRADOURO = data["LOGRADOURO"].ToString();
                    campanha.BAIRRO = data["BAIRRO"].ToString();
                    campanha.CIDADE = data["CIDADE"].ToString();
                    campanha.CEP = data["CEP"].ToString();
                    campanha.ESTADO = data["ESTADO"].ToString();
                    campanha.DATA_INICIO_DT = Convert.ToDateTime(data["DATA_INICIO"].ToString());
                    campanha.DATA_FIM_DT = Convert.ToDateTime(data["DATA_FIM"].ToString());

                    list.Add(campanha);
                }
            }

            return list;
        }

        public List<CampanhaModel> getMinhasCampanhas(string email)
        {
            string strQuery = "";

            strQuery = String.Format("SELECT C.CODIGO_CAMPANHA, C.NOME_CAMPANHA, C.DESCRICAO_CAMPANHA, C.NOME_RECEPTOR, C.TIPO_SANGUINEO, C.DATA_INICIO, C.DATA_FIM, CE.NOME_HOSPITAL, CE.LOGRADOURO, CE.BAIRRO, CE.CIDADE, CE.ESTADO, CE.CEP FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO INNER JOIN CAMPANHA_ENDERECO CE ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA WHERE U.EMAIL_USUARIO = '{0}' ", email);

            DataTable dt = new DataTable();

            List<CampanhaModel> list = new List<CampanhaModel>();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    CampanhaModel campanha = new CampanhaModel();

                    campanha.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                    campanha.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    campanha.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();
                    campanha.NOME_RECEPTOR = data["NOME_RECEPTOR"].ToString();
                    campanha.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                    campanha.NOME_HOSPITAL = data["NOME_HOSPITAL"].ToString();
                    campanha.LOGRADOURO = data["LOGRADOURO"].ToString();
                    campanha.BAIRRO = data["BAIRRO"].ToString();
                    campanha.CIDADE = data["CIDADE"].ToString();
                    campanha.CEP = data["CEP"].ToString();
                    campanha.ESTADO = data["ESTADO"].ToString();
                    campanha.DATA_INICIO_DT = Convert.ToDateTime(data["DATA_INICIO"].ToString());
                    campanha.DATA_FIM_DT = Convert.ToDateTime(data["DATA_FIM"].ToString());

                    list.Add(campanha);
                }
            }

            return list;
        }

        public int deletarCampanha(CampanhaModel cmodel, UsuarioModel umodel)
        {
            int delete_campanha = (int)SITUACAO.SUCESSO;

            var strQuerySelect = String.Format("SELECT * FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE C.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);
            var strQueryDelete = String.Format("DELETE CE FROM CAMPANHA_ENDERECO CE INNER JOIN CAMPANHA C ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE CE.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);
            var strQueryDelete2 = String.Format("DELETE C FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE C.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable); 

            if (dt.Rows.Count > 0)
            {
                try
                {
                    var a = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var b = context.ExecuteCommand(strQueryDelete2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    delete_campanha = (int) SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    delete_campanha = (int) SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count <= 0)
                delete_campanha = (int) SITUACAO.NAO_POSSUI_CADASTRO;

            return delete_campanha;
        }
        
    }
}
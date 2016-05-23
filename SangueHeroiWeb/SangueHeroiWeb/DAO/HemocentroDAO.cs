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
    public class HemocentroDAO
    {
        ContextHelpers context;

        public HemocentroDAO()
        {
            context = new ContextHelpers();
        }

        public bool ParceriaHemocentro(HemocentroModel model)
        {
            bool registroOK = true;

            string strQuery = "";

            DataTable dt = (DataTable)context.ExecuteCommand(String.Format("SELECT * FROM HEMOCENTRO WHERE CNPJ = '{0}' AND EMAIL = '{1}'", model.CNPJ, model.EMAIL_HEMOCENTRO), CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count == 0)
            {
                strQuery = "EXECUTE frmCadastrarHemocentro " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + model.CODIGO_STATUS.ToString() + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP);


                try
                {
                    context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                }
                catch (Exception)
                {
                    registroOK = false;
                }
            }
            else
            {
                registroOK = false;
            }

            return registroOK;
        }

        public void AtivarHemocentro(int _idHemocentro)
        {
            string strSQL = string.Format(" UPDATE HEMOCENTRO SET CODIGO_STATUS = {0} WHERE CODIGO_HEMOCENTRO = {1} ", Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Ativo, _idHemocentro);

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
        }

        public bool Editar(HemocentroModel model)
        {
            bool registroOK = true;

            string strQuery = "";

            strQuery = "EXECUTE frmAtualizarHemocentro " + Environment.NewLine
                 + model.CODIGO_HEMOCENTRO + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP);

            try
            {
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception ex)
            {
                registroOK = false;
            }

            return registroOK;
        }

        public void Inativar(int _idHemocentro)
        {
            string strSQL = string.Format(" UPDATE HEMOCENTRO SET CODIGO_STATUS = {0} WHERE CODIGO_HEMOCENTRO = {1} ", Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado, _idHemocentro);

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
        }

        public List<HemocentroModel> Lista(string where = "")
        {

            string strSQL = " SELECT * FROM HEMOCENTRO H " + Environment.NewLine
                          + " INNER JOIN HEMOCENTRO_ENDERECO HE" + Environment.NewLine
                          + " ON H.CODIGO_HEMOCENTRO = HE.CODIGO_HEMOCENTRO " + Environment.NewLine;

            if (where.Trim() != "")
                strSQL = strSQL + where;

            List<HemocentroModel> lista = new List<HemocentroModel>();

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            try
            {
                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                        HemocentroModel hm = new HemocentroModel();

                        hm.CODIGO_HEMOCENTRO = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());
                        hm.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                        hm.CNPJ = data["CNPJ"].ToString();
                        hm.RAZAO_SOCIAL = data["RAZAO_SOCIAL"].ToString();
                        hm.CODIGO_STATUS = Convert.ToInt32(data["CODIGO_STATUS"].ToString());
                        hm.LOGIN_HEMOCENTRO = data["LOGIN_HEMOCENTRO"].ToString();
                        hm.SENHA_HEMOCENTRO = data["SENHA_HEMOCENTRO"].ToString();
                        hm.CODIGO_HEMOCENTRO_PERFIL = Convert.ToInt32(data["CODIGO_HEMOCENTRO_PERFIL"].ToString());
                        hm.EMAIL_HEMOCENTRO = data["EMAIL"].ToString();
                        hm.TELEFONE_HEMOCENTRO = data["TELEFONE_HEMOCENTRO"].ToString();
                        hm.PERIODO_FUNCIONAMENTO_HEMOCENTRO = data["PERIODO_FUNCIONAMENTO"].ToString();
                        hm.CIDADE_HEMOCENTRO = data["CIDADE"].ToString();
                        hm.ESTADO_HEMOCENTRO = data["ESTADO"].ToString();
                        hm.CEP = data["CEP"].ToString();

                        lista.Add(hm);
                    }

            }
            catch (Exception ex)
            {

            }

            return lista;
        }

        public HemocentroModel BuscaHemocentro(string where = "")
        {
            return Lista(where).FirstOrDefault();
        }
    }
}
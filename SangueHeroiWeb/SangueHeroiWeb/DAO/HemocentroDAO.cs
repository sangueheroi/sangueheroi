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

            DataTable dt = (DataTable)context.ExecuteCommand(String.Format("SELECT * FROM HEMOCENTRO WHERE CNPJ = '{0}'", model.CNPJ), CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count == 0)
            {
                strQuery = "EXECUTE frmParceriaHemocentro " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CODIGO_STATUS.ToString()) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_CRIACAO) + " , " + Environment.NewLine
                 + "1";

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

            strQuery = "EXECUTE frmEditarHemocentro " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_HEMOCENTRO) + " , " + Environment.NewLine;

            try
            {
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception)
            {
                registroOK = false;
            }

            return registroOK;
        }

        public bool Inativar(int _idHemocentro)
        {
            return true;
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
                        hm.CODIGO_STATUS = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());
                        hm.LOGIN_HEMOCENTRO = data["LOGIN_HEMOCENTRO"].ToString();
                        hm.CIDADE_HEMOCENTRO = data["CIDADE_HEMOCENTRO"].ToString();
                        hm.ESTADO_HEMOCENTRO = data["ESTADO_HEMOCENTRO"].ToString();
                        hm.CEP = data["CEP"].ToString();

                        lista.Add(hm);
                    }

            }
            catch (Exception)
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
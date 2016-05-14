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

            string strQuery2 = String.Format("SELECT * FROM HEMOCENTRO WHERE CNPJ = '{0}'", model.CNPJ);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count == 0)
            {
                strQuery = "EXECUTE frmParceriaHemocentro " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CODIGO_STATUS.ToString()) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_CRIACAO);

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

        public bool Editar(HemocentroModel model)
        {
            return true;
        }

        public bool Inativar(int _idHemocentro)
        {
            return true;
        }
    }
}
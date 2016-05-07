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
    }
}
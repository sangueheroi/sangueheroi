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
    public class DoacaoDAO
    {
        ContextHelpers context;
        protected SmtpClient SmtpClient { get; set; }
        protected MailMessage MailMessage { get; set; }

        public DoacaoDAO()
        {
            context = new ContextHelpers();
        }

        public List<DoacaoModel> getHistoricoDoacoes(string email)
        {
            string strQuery = "";

            strQuery = String.Format("SELECT NOME_HEMOCENTRO, DATA_DOACAO, LOGRADOURO_ENDERECO_DOACAO FROM DOACAO D INNER JOIN USUARIO U ON D.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}' ", email);

            DataTable dt = new DataTable();

            List <DoacaoModel> list = new List<DoacaoModel>();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    DoacaoModel doacao = new DoacaoModel();
                    doacao.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                    doacao.DATA_DOACAO_DT = Convert.ToDateTime(data["DATA_DOACAO"].ToString());
                    doacao.LOGRADOURO_ENDERECO_DOACAO = data["LOGRADOURO_ENDERECO_DOACAO"].ToString();

                    list.Add(doacao);
                }
            }

            return list;      
        }
    }
}
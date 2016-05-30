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

        public DoacaoDAO()
        {
            context = new ContextHelpers();
        }

        public List<DoacaoModel> getRankingDoadores()
        {

           string strSQL = " SELECT U.CODIGO_USUARIO," + Environment.NewLine
		                 + " U.NOME_USUARIO," + Environment.NewLine
                         + " UP.TIPO_SANGUINEO," + Environment.NewLine
                         + " U.EMAIL_USUARIO," + Environment.NewLine
                         + " COUNT(D.CODIGO_DOACAO) as QTD_DOACOES," + Environment.NewLine
                         + " SUM(D.PONTUACAO) AS PONTUACAO," + Environment.NewLine
                         + " SUM(D.QTD_VIDAS_SALVAS) AS QTD_VIDAS_SALVAS" + Environment.NewLine
                         + " FROM" + Environment.NewLine
                         + " DOACAO D" + Environment.NewLine
                         + " INNER JOIN" + Environment.NewLine
                         + " USUARIO U" + Environment.NewLine
                         + " ON" + Environment.NewLine
                         + " D.CODIGO_USUARIO = U.CODIGO_USUARIO" + Environment.NewLine
                         + " INNER JOIN" + Environment.NewLine
                         + " USUARIO_PERFIL UP" + Environment.NewLine
                         + " ON" + Environment.NewLine
                         + " U.CODIGO_USUARIO = UP.CODIGO_USUARIO" + Environment.NewLine
                         + " GROUP BY U.CODIGO_USUARIO, U.NOME_USUARIO, UP.TIPO_SANGUINEO, U.EMAIL_USUARIO;";

            List<DoacaoModel> lista = new List<DoacaoModel>();

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            try
            {
                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                        DoacaoModel dm = new DoacaoModel();

                        dm.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                        dm.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                        dm.QTD_DOACOES = Convert.ToInt32(data["QTD_DOACOES"].ToString());
                        dm.PONTUACAO = Convert.ToInt32(data["PONTUACAO"].ToString());
                        dm.QTD_VIDAS_SALVAS = Convert.ToInt32(data["QTD_VIDAS_SALVAS"].ToString());
                        dm.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();

                        lista.Add(dm);
                    }

            }
            catch (Exception)
            {

            }

            return lista;
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
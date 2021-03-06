﻿using SangueHeroiWeb.Helpers.Util_Helper;
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

        public string[] getInfoDoacao(UsuarioModel model)
        {
            var doacao = new string[5];

            var dtUltimaDoacao = "";
            var dtProximaDoacao = "";
            var nomeHemocentro = "";
            var sexo = "";
            var consultaOk = (int)SITUACAO.DADOS_INVALIDOS;

            var strQuery = $"SELECT " +
                           $"   U.DATA_CRIACAO, " +
                           $"   UP.DATA_ULTIMA_DOACAO, " +
                           $"   UP.DATA_PROXIMA_DOACAO " +
                           $"FROM USUARIO U " +
                           $"JOIN USUARIO_PERFIL UP " +
                           $"ON U.CODIGO_USUARIO = UP.CODIGO_USUARIO " +
                           $"WHERE U.EMAIL_USUARIO = '{model.EMAIL_USUARIO}'";

            var strQuerySelectDataUltimaDoacao = $"SELECT " +
                                                 $"     TOP 1 " +
                                                 $"     UP.DATA_ULTIMA_DOACAO, " +
                                                 $"     UP.DATA_PROXIMA_DOACAO, " +
                                                 $"     UP.SEXO, " +
                                                 $"     D.NOME_HEMOCENTRO, " +
                                                 $"     U.DATA_CRIACAO, " +
                                                 $"     D.DATA_DOACAO " +
                                                 $"FROM USUARIO_PERFIL UP " +
                                                 $"INNER JOIN USUARIO U " +
                                                 $"ON UP.CODIGO_USUARIO = U.CODIGO_USUARIO " +
                                                 $"LEFT JOIN DOACAO D " +
                                                 $"ON UP.CODIGO_USUARIO = D.CODIGO_USUARIO " +
                                                 $"WHERE U.EMAIL_USUARIO = '{model.EMAIL_USUARIO}' " +
                                                 $"order by d.data_doacao desc"; 

            var dt = new DataTable();
            var dt2 = new DataTable();

            try
            {
                dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
                dt2 = (DataTable)context.ExecuteCommand(strQuerySelectDataUltimaDoacao, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
                consultaOk = (int)SITUACAO.SUCESSO;
            }
            catch (Exception ex)
            {
                consultaOk = (int)SITUACAO.ERRO_DE_SISTEMA;
            }
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {

                    var dtCriacaoUsuario = Convert.ToDateTime(data["DATA_CRIACAO"]).ToShortDateString();
                    var dtUltimaDoacaoUsuario = Convert.ToDateTime(data["DATA_ULTIMA_DOACAO"]).ToShortDateString();
                    var dtProximaDoacaoUsuario = Convert.ToDateTime(data["DATA_PROXIMA_DOACAO"]).ToShortDateString();

                    if (dtCriacaoUsuario != dtUltimaDoacaoUsuario || dtCriacaoUsuario != dtProximaDoacaoUsuario)
                        foreach (DataRow data2 in dt2.Rows)
                        {
                            dtUltimaDoacao = dtCriacaoUsuario != dtUltimaDoacaoUsuario ? Convert.ToDateTime(data2["DATA_ULTIMA_DOACAO"].ToString()).ToShortDateString() : " ";
                            dtProximaDoacao = dtCriacaoUsuario != dtProximaDoacao ? Convert.ToDateTime(data2["DATA_PROXIMA_DOACAO"].ToString()).ToShortDateString() : " ";
                            nomeHemocentro = dtCriacaoUsuario != dtUltimaDoacaoUsuario ? data2["NOME_HEMOCENTRO"].ToString() : " ";
                            sexo = data2["SEXO"].ToString();
                        }
                    else
                        consultaOk = 5;
                }
                
            }

            doacao[0] = dtUltimaDoacao;
            doacao[1] = dtProximaDoacao;
            doacao[2] = nomeHemocentro;
            doacao[3] = sexo;
            doacao[4] = consultaOk.ToString();

            return doacao;
        }

        public int setInfoDoacao(UsuarioModel model)
        {
            int atualizacaoOK = (int)SITUACAO.SUCESSO;

            string strQueryUpdate = "";

            string strQueryConsultaEmail = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaEmail, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt.Rows.Count != 0)
            {
                int codigo_usuario = 0;

                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryUpdate = String.Format("UPDATE USUARIO_PERFIL SET DATA_PROXIMA_DOACAO = {0} WHERE CODIGO_USUARIO = {1}", Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(model.DATA_PROXIMA_DOACAO), codigo_usuario);

                try
                {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    atualizacaoOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    atualizacaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else
                atualizacaoOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return atualizacaoOK;
        }

        public int CadastrarDoacao(DoacaoModel dmodel)
        {
            int cadastroOK = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_usuario = 0;
            string sexo = "";

            string strQueryInsert = "";
            string strQueryUpdate = "";

            string strQueryConsultaCodigo = String.Format("SELECT U.CODIGO_USUARIO, UP.SEXO FROM USUARIO U INNER JOIN USUARIO_PERFIL UP ON U.CODIGO_USUARIO = UP.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}'", dmodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());
                    sexo = data["SEXO"].ToString();
                }
                
                strQueryInsert = "INSERT INTO DOACAO (CODIGO_USUARIO, NOME_HEMOCENTRO, LOGRADOURO_ENDERECO_DOACAO, CEP_ENDERECO_DOACAO, PONTUACAO, QTD_VIDAS_SALVAS, DATA_DOACAO) VALUES (" + codigo_usuario + ", '" + dmodel.NOME_HEMOCENTRO + "', '" + dmodel.LOGRADOURO_ENDERECO_DOACAO + "', '" + dmodel.CEP_ENDERECO_DOACAO + "', '3', '4', " + Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(DateTime.Now) + ");";

                if(sexo == "M" || sexo == "Masculino")
                    strQueryUpdate = "UPDATE USUARIO_PERFIL SET DATA_ULTIMA_DOACAO = " + Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(DateTime.Now) + ", DATA_PROXIMA_DOACAO = " + Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(DateTime.Now.AddDays(90)) + " WHERE CODIGO_USUARIO = " + codigo_usuario+ " ;"; 
                else if (sexo == "F" || sexo == "Feminino")
                    strQueryUpdate = "UPDATE USUARIO_PERFIL SET DATA_ULTIMA_DOACAO = " + Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(DateTime.Now) + ", DATA_PROXIMA_DOACAO = " + Helpers.Util_Helper.UtilHelper.DateTimeParaSQLDate(DateTime.Now.AddDays(120)) + " WHERE CODIGO_USUARIO = " + codigo_usuario + " ;";


                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var b = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    cadastroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception e)
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

    }
}
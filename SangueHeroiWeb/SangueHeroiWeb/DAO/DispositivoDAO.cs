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
    public class DispositivoDAO
    {
        ContextHelpers context;
        protected SmtpClient SmtpClient { get; set; }
        protected MailMessage MailMessage { get; set; }

        public DispositivoDAO()
        {
            context = new ContextHelpers();
        }

        /*public string DispararNotificacao(CampanhaModel cmodel, bool isHemocentro)
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            string strQuerySelectDispositivo = "";
            string strQuerySelectCodigoCampanha = "";
            string strQuerySelectCampanha = "";
            string envio = "";

            strQuerySelectDispositivo = String.Format("SELECT TOKEN FROM DISPOSITIVO");
            strQuerySelectCodigoCampanha = String.Format("SELECT MAX(CODIGO_CAMPANHA) AS CODIGO_CAMPANHA FROM CAMPANHA");

            List<string> dispositivos = new List<string>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelectDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            dt2 = (DataTable)context.ExecuteCommand(strQuerySelectCodigoCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    DispositivoModel dispositivo = new DispositivoModel();
                    dispositivos.Add(data["TOKEN"].ToString());
                }
            }

            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow data in dt2.Rows)
                {
                    cmodel.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                }
            }

            if (!isHemocentro)
            {
                strQuerySelectCampanha = String.Format("SELECT U.NOME_USUARIO, U.EMAIL_USUARIO FROM USUARIO U INNER JOIN CAMPANHA C ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

                dt3 = (DataTable)context.ExecuteCommand(strQuerySelectCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow data in dt3.Rows)
                    {
                        cmodel.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                        cmodel.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();
                    }
                }
            }
            else
            {
                strQuerySelectCampanha = String.Format("SELECT E.NOME_HEMOCENTRO, E.EMAIL FROM HEMOCENTRO E INNER JOIN CAMPANHA C ON C.CODIGO_HEMOCENTRO = E.CODIGO_HEMOCENTRO WHERE CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

                dt3 = (DataTable)context.ExecuteCommand(strQuerySelectCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow data in dt3.Rows)
                    {
                        cmodel.NOME_USUARIO = data["NOME_HEMOCENTRO"].ToString();
                        cmodel.EMAIL_USUARIO = data["EMAIL"].ToString();
                    }
                }
            }

            envio = gcm.EnviarNotificacaoCompleta(dispositivos, cmodel.DESCRICAO_CAMPANHA, cmodel.NOME_CAMPANHA, cmodel.CODIGO_CAMPANHA.ToString(), cmodel.NOME_USUARIO, cmodel.EMAIL_USUARIO, cmodel.NOME_RECEPTOR, cmodel.TIPO_SANGUINEO, cmodel.NOME_HOSPITAL, cmodel.ESTADO, cmodel.CIDADE, cmodel.BAIRRO, cmodel.LOGRADOURO, cmodel.CEP, cmodel.DATA_INICIO, cmodel.DATA_FIM);

            return envio;
        }*/

        public string DispararNotificacao(CampanhaModel cmodel, bool isHemocentro, string destinatario)
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            string strQuerySelectDispositivo = "";
            string strQuerySelectCodigoCampanha = "";
            string strQuerySelectCampanha = "";
            string envio = "";

            strQuerySelectDispositivo = String.Format("SELECT TOKEN FROM DISPOSITIVO");
            strQuerySelectCodigoCampanha = String.Format("SELECT MAX(CODIGO_CAMPANHA) AS CODIGO_CAMPANHA FROM CAMPANHA");

            List<string> dispositivos = new List<string>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelectDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            dt2 = (DataTable)context.ExecuteCommand(strQuerySelectCodigoCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                GrupoUsuarioModel destinatarios = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<GrupoUsuarioModel>(destinatario);

                if (destinatarios.DESTINATARIOS != null)
                {
                    foreach (var item in destinatarios.DESTINATARIOS)
                    {
                        string strQueryConsultaToken = String.Format("SELECT D.TOKEN FROM DISPOSITIVO D INNER JOIN USUARIO U ON U.CODIGO_USUARIO = D.CODIGO_USUARIO INNER JOIN USUARIO_GRUPO UG ON U.CODIGO_USUARIO = UG.CODIGO_USUARIO WHERE UG.CODIGO_GRUPO = {0}", item.CODIGO_GRUPO);
                        dt4 = (DataTable)context.ExecuteCommand(strQueryConsultaToken, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                        foreach (DataRow data4 in dt4.Rows)
                        {
                            DispositivoModel dispositivo = new DispositivoModel();
                            dispositivos.Add(data4["TOKEN"].ToString());
                        }
                    }
                }
                else if (destinatarios.DESTINATARIOS == null)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        DispositivoModel dispositivo = new DispositivoModel();
                        dispositivos.Add(data["TOKEN"].ToString());
                    }
                }
            }

            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow data in dt2.Rows)
                {
                    cmodel.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                }
            }

            if (!isHemocentro)
            {
                strQuerySelectCampanha = String.Format("SELECT U.NOME_USUARIO, U.EMAIL_USUARIO FROM USUARIO U INNER JOIN CAMPANHA C ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

                dt3 = (DataTable)context.ExecuteCommand(strQuerySelectCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow data in dt3.Rows)
                    {
                        cmodel.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                        cmodel.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();
                    }
                }
            }
            else
            {
                strQuerySelectCampanha = String.Format("SELECT E.NOME_HEMOCENTRO, E.EMAIL FROM HEMOCENTRO E INNER JOIN CAMPANHA C ON C.CODIGO_HEMOCENTRO = E.CODIGO_HEMOCENTRO WHERE CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

                dt3 = (DataTable)context.ExecuteCommand(strQuerySelectCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow data in dt3.Rows)
                    {
                        cmodel.NOME_USUARIO = data["NOME_HEMOCENTRO"].ToString();
                        cmodel.EMAIL_USUARIO = data["EMAIL"].ToString();
                    }
                }
            }

            envio = gcm.EnviarNotificacaoCompleta(dispositivos, cmodel.DESCRICAO_CAMPANHA, cmodel.NOME_CAMPANHA, cmodel.CODIGO_CAMPANHA.ToString(), cmodel.NOME_USUARIO, cmodel.EMAIL_USUARIO, cmodel.NOME_RECEPTOR, cmodel.TIPO_SANGUINEO, cmodel.NOME_HOSPITAL, cmodel.ESTADO, cmodel.CIDADE, cmodel.BAIRRO, cmodel.LOGRADOURO, cmodel.CEP, cmodel.DATA_INICIO, cmodel.DATA_FIM);
                          
            return envio;
        }

        public string DispararNotificacaoNiveisSanguineos(string nome_hemocentro, string titulo, string mensagem, string tipo_sanguineo, int valor_tipo_sanguineo, string destinatario)
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();
            DataTable dt = new DataTable(); 
            DataTable dt4 = new DataTable();
            string strQuerySelectDispositivo = "";
            string envio = "";
            
            strQuerySelectDispositivo = String.Format("SELECT TOKEN FROM DISPOSITIVO");

            List<string> dispositivos = new List<string>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelectDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt.Rows.Count > 0)
            {
                UsuarioModel destinatarios = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioModel>(destinatario);

                if (destinatarios.DESTINATARIOS != null)
                {
                    foreach (var item in destinatarios.DESTINATARIOS)
                    {
                        string strQueryConsultaToken = String.Format("SELECT D.TOKEN FROM DISPOSITIVO D INNER JOIN USUARIO U ON U.CODIGO_USUARIO = D.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = {0}", item.EMAIL_USUARIO);
                        dt4 = (DataTable)context.ExecuteCommand(strQueryConsultaToken, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                        foreach (DataRow data4 in dt4.Rows)
                        {
                            DispositivoModel dispositivo = new DispositivoModel();
                            dispositivos.Add(data4["TOKEN"].ToString());
                        }
                    }
                }
                else if (destinatarios.DESTINATARIOS == null)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        DispositivoModel dispositivo = new DispositivoModel();
                        dispositivos.Add(data["TOKEN"].ToString());
                    }
                }
            }

            envio = gcm.EnviarNotificacaoNiveisSanguineos(dispositivos, mensagem, titulo, nome_hemocentro, tipo_sanguineo, valor_tipo_sanguineo);

            return envio;
        }

        public string DispararNotificacaoProximaDoacao(DateTime data_proxima_doacao, string titulo, string mensagem, string destinatario)
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();
            DataTable dt = new DataTable();
            DataTable dt4 = new DataTable();
            string strQuerySelectDispositivo = "";
            string envio = "";

            strQuerySelectDispositivo = String.Format("SELECT TOKEN FROM DISPOSITIVO");

            List<string> dispositivos = new List<string>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelectDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                UsuarioModel destinatarios = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioModel>(destinatario);

                if (destinatarios.DESTINATARIOS != null)
                {
                    foreach (var item in destinatarios.DESTINATARIOS)
                    {
                        string strQueryConsultaToken = String.Format("SELECT D.TOKEN FROM DISPOSITIVO D INNER JOIN USUARIO U ON U.CODIGO_USUARIO = D.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}'", item.EMAIL_USUARIO);
                        dt4 = (DataTable)context.ExecuteCommand(strQueryConsultaToken, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                        foreach (DataRow data4 in dt4.Rows)
                        {
                            DispositivoModel dispositivo = new DispositivoModel();
                            dispositivos.Add(data4["TOKEN"].ToString());
                        }
                    }
                }
                else if (destinatarios.DESTINATARIOS == null)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        DispositivoModel dispositivo = new DispositivoModel();
                        dispositivos.Add(data["TOKEN"].ToString());
                    }
                }
            }

            envio = gcm.EnviarNotificacaoProximaDoacao(dispositivos, mensagem, titulo, data_proxima_doacao);

            return envio;
        }

        public string DispararNotificacao()
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string strQuerySelectDispositivo = "";
            string strQuerySelectMaxCodigoCampanha = "";
            string strQuerySelectCampanha = "";
            string envio = "";
            
            strQuerySelectDispositivo = String.Format("SELECT TOKEN FROM DISPOSITIVO");

            strQuerySelectMaxCodigoCampanha = String.Format("SELECT MAX(CODIGO_CAMPANHA) FROM CAMPANHA");

            try
            { 
                var max_codigo_campanha = context.ExecuteCommand(strQuerySelectMaxCodigoCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                strQuerySelectCampanha = String.Format("SELECT CODIGO_CAMPANHA, NOME_CAMPANHA, DESCRICAO_CAMPANHA FROM CAMPANHA WHERE CODIGO_CAMPANHA = '{0}'", max_codigo_campanha);
            }
            catch (Exception)
            {
               
            }
       
            List<string> dispositivos = new List<string>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelectDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    DispositivoModel dispositivo = new DispositivoModel();
                    dispositivos.Add(data["TOKEN"].ToString());
                }
            }
           
            dt2 = (DataTable)context.ExecuteCommand(strQuerySelectCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    CampanhaModel campanha = new CampanhaModel();

                    campanha.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                    campanha.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    campanha.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();

                    envio = gcm.EnviarNotificacao(dispositivos, campanha.DESCRICAO_CAMPANHA, campanha.NOME_CAMPANHA, campanha.CODIGO_CAMPANHA.ToString());
                }    
            }

            return envio;

        }

        public int CadastrarDispositivo(DispositivoModel dmodel)
        {
            int cadastroOK = (int)SITUACAO.DADOS_INVALIDOS;

            string strQueryInsert = "";
            string strQueryUpdate = "";
            int codigo_usuario = 0;
            string strQueryConsultaCodigoUsuario = "";
            string strQueryConsultaCodigoUsuarioDispositivo = "";
  
            strQueryConsultaCodigoUsuario = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", dmodel.EMAIL_USUARIO);
            strQueryConsultaCodigoUsuarioDispositivo = String.Format("SELECT CODIGO_USUARIO FROM DISPOSITIVO WHERE TOKEN = '{0}'", dmodel.TOKEN);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuarioDispositivo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt.Rows.Count == 0)
            {
                foreach (DataRow data in dt2.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryInsert = String.Format("INSERT INTO DISPOSITIVO (TOKEN, CODIGO_USUARIO) VALUES ('{0}', {1})", dmodel.TOKEN, codigo_usuario);

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
            else if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt2.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryUpdate = String.Format("UPDATE DISPOSITIVO SET TOKEN = '{0}' WHERE CODIGO_USUARIO = {1}", dmodel.TOKEN, codigo_usuario);

                try
                {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    cadastroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            
                //cadastroOK = (int)SITUACAO.JA_POSSUI_CADASTRO;
            }

            return cadastroOK;
        }

        public int AlterarDispositivo(DispositivoModel dmodel)
        {
            int alteracaoOK = (int)SITUACAO.DADOS_INVALIDOS;

            string strQueryUpdate = "";
            string strQueryConsultaCodigo = "";
            int codigo_dispositivo = 0;

            strQueryConsultaCodigo = String.Format("SELECT CODIGO_DISPOSITIVO FROM DISPOSITIVO WHERE TOKEN = '{0}'", dmodel.TOKEN);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                    codigo_dispositivo = Convert.ToInt32(data["CODIGO_DISPOSITIVO"].ToString());

                strQueryUpdate = "UPDATE DISPOSITIVO SET TOKEN = '" + Environment.NewLine
                + UtilHelper.TextForSql(dmodel.TOKEN) + "' WHERE CODIGO_DISPOSITIVO = '" + Environment.NewLine
                + codigo_dispositivo + "';";

                try
                {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    alteracaoOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    alteracaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

            }
            else if (dt.Rows.Count == 0)
            {
                alteracaoOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return alteracaoOK;
        }

        public int DeletarDispositivo(DispositivoModel dmodel)
        {
            int deleteOk = (int)SITUACAO.SUCESSO;
            int codigo_dispositivo = 0;

            var strQuerySelect = String.Format("SELECT * FROM DISPOSITIVO WHERE TOKEN = '{0}'", dmodel.TOKEN);
  
            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                    codigo_dispositivo = Convert.ToInt32(data["CODIGO_DISPOSITIVO"].ToString());
                    var strQueryDelete = String.Format("DELETE FROM DISPOSITIVO WHERE CODIGO_DISPOSITIVO = '{0}'", codigo_dispositivo);

                try
                { 
                    var a = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    deleteOk = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    deleteOk = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count <= 0)
                deleteOk = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return deleteOk;
        }

    }
}
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.DAO;
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
    public class LoginDAO
    {
        ContextHelpers context;

        public LoginDAO()
        {
            context = new ContextHelpers();
        }

        public int VerificarLogin(LoginUsuarioModel model)
        { 
            int loginOK = (int) SITUACAO.SUCESSO;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)       
                loginOK = (int) SITUACAO.SUCESSO;
            else
                loginOK = (int) SITUACAO.NAO_POSSUI_CADASTRO;

            return loginOK;
        }

        public string[] LogarUsuario(LoginUsuarioModel model)
        {
            Encrypt enc = new Encrypt();

            string[] usuario = new string[2];

            string nome = "";

            string senha_banco = "";

            int loginOK = (int) SITUACAO.SUCESSO;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);
 
            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    senha_banco = data["SENHA_USUARIO"].ToString();
                    nome = data["NOME_USUARIO"].ToString();
                    //model.SENHA = enc.DecryptoRSA(model.SENHA);
                    //senha_banco = enc.DecryptoRSA(senha_banco);

                    if (!model.SENHA.Equals(senha_banco))
                        loginOK = (int) SITUACAO.DADOS_INVALIDOS;
                }
            }
            else
            {
                nome = "";
                loginOK = (int) SITUACAO.NAO_POSSUI_CADASTRO;
            }

            usuario[0] = nome;
            usuario[1] = loginOK.ToString();

            return usuario;
        }

        public int LogarHemocentro(HemocentroModel model)
        {
            int loginOK = (int) SITUACAO.SUCESSO;

            var strQuery = String.Format("SELECT * FROM HEMOCENTRO WHERE LOGIN_HEMOCENTRO = '{0}'", model.LOGIN_HEMOCENTRO);

            DataTable dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if(!(Convert.ToInt32(data["CODIGO_STATUS"]) == Constantes.CADASTRO_STATUS.Bloqueado))
                    {
                        if (!model.SENHA_HEMOCENTRO.Equals(data["SENHA_HEMOCENTRO"]))
                            loginOK = (int)SITUACAO.DADOS_INVALIDOS;
                    }
                    else
                    {
                        loginOK = Convert.ToInt32(SITUACAO.CADASTRO_BLOQUEADO);
                    }
                   
                }
            }
            else
            {
                loginOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }
            return loginOK;
        }

        public bool EsqueciMinhaSenha(string emailHemocentro)
        {
            bool envioEmailOk = true;

            var strQuery = String.Format("SELECT * FROM HEMOCENTRO WHERE EMAIL_HEMOCENTRO = '{0}'", emailHemocentro);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt != null)
            {
                try
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        EmailHelper.EnviarEmail(data[""].ToString(), data[""].ToString(), true);
                    }

                }
                catch (SmtpFailedRecipientException ex)
                {
                    Console.WriteLine("Mensagem : {0} " + ex.Message);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                }
            }
            else
            {
                envioEmailOk = false;
            }
            return envioEmailOk;
        }
        
    }
}
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
    public class LoginDAO
    {
        ContextHelpers context;

        public LoginDAO()
        {
            context = new ContextHelpers();
        }

        public int LogarUsuario(LoginUsuarioModel model)
        {
            int loginOK = 1;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if (!model.SENHA.Equals(data["SENHA_USUARIO"]))
                        loginOK = 0;
                }
            }
            else
                loginOK = 2;

            return loginOK;
        }

        public bool LogarHemocentro(HemocentroModel model)
        {
            bool loginOK = true;

            var strQuery = String.Format("SELECT * FROM HEMOCENTRO WHERE LOGIN_HEMOCENTRO = '{0}'", model.LOGIN_HEMOCENTRO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if (!model.SENHA_HEMOCENTRO.Equals(data["SENHA_HEMOCENTRO"]))
                        loginOK = false;
                }
            }
            else
                loginOK = false;

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
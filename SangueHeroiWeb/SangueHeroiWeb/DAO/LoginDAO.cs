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
        protected SmtpClient SmtpClient { get; set; }
        protected MailMessage MailMessage { get; set; }

        public LoginDAO()
        {
            context = new ContextHelpers();
        }

        public bool Logar(LoginModel model)
        {
            bool loginOK = true;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if (!model.SENHA.Equals(data["SENHA_CRIPTOGRAFADA"]))
                        loginOK = false;
                }
            }
            else
                loginOK = false;

            return loginOK;
        }

        public bool EsqueciMinhaSenha(string emailUsuario)
        {
            bool envioEmailOk = true;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", emailUsuario);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    envioEmailOk = EmailHelper.EnviarEmail(data["EMAIL_USUARIO"].ToString(), data["NOME_USUARIO"].ToString(), false);
                }
            }
            else
            {
                envioEmailOk = false;
            }

            return envioEmailOk;
        }

        public void Registrar(UsuarioModel model)
        {
            string strQuery = "";

            strQuery = "INSERT" + Environment.NewLine +
                    "INTO TB_USUARIO ( NOME_USUARIO, EMAIL_USUARIO, SENHA_USUARIO, SOBRENOME_USUARIO, RUA_ENDERECO_USUARIO, " + Environment.NewLine +
                    "NUMERO_ENDERECO_USUARIO, BAIRRO_ENDERECO_USUARIO, CIDADE_ENDERECO_USUARIO, ESTADO_ENDERECO_USUARIO" + Environment.NewLine +
                    "CEP_ENDERECO_USUARIO, TIPO_SANGUINEO, DATA_NASCIMENTO, DATA_ULTIMA_DOACAO)" + Environment.NewLine +
                    "VALUES(" + model.NOME_USUARIO + " , "
                     + model.EMAIL_USUARIO + " , " + Environment.NewLine
                     + model.SENHA_USUARIO + " , " + Environment.NewLine
                     + model.SOBRENOME_USUARIO + " , " + Environment.NewLine
                     + model.RUA_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.NUMERO_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.BAIRRO_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.CIDADE_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.ESTADO_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.CEP_ENDERECO_USUARIO + " , " + Environment.NewLine
                     + model.TIPO_SANGUINEO + " , " + Environment.NewLine
                     + model.DATA_NASCIMENTO + " , " + Environment.NewLine
                     + model.DATA_ULTIMA_DOACAO + " , " + Environment.NewLine + " )";


            var a = context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
        }

    }
}
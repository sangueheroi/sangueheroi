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

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if (!model.EMAIL_USUARIO.Equals(data["EMAIL_USUARIO"]) || !model.SENHA.Equals(data["SENHA_CRIPTOGRAFADA"]))
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

            var strQuery = String.Format("SELECT * FROM TB_USUARIO WHERE EMAIL_USUARIO = '{0}'", emailUsuario);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            
            if (dt != null)
            {
                try
                {
                    SmtpClient = new SmtpClient();
                    SmtpClient.Host = "smtp.gmail.com";
                    SmtpClient.Port = 587;
                    SmtpClient.EnableSsl = true;
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpClient.UseDefaultCredentials = false;
                    SmtpClient.Credentials = new NetworkCredential("sangue.heroi@gmail.com", "appsangueheroi10");

                    foreach (DataRow data in dt.Rows)
                    {
                        MailMessage = new MailMessage();
                        MailMessage.From = new MailAddress("sangue.heroi@gmail.com", "Sangue Heroi", Encoding.UTF8);
                        MailMessage.To.Add(new MailAddress(data["EMAIL_USUARIO"].ToString(), "Nome Usuario", Encoding.UTF8));

                        MailMessage.Subject = "Solicitação de Nova Senha";
                        MailMessage.Body = "<div>Email de Troca de Senha </div>";
                        MailMessage.BodyEncoding = Encoding.UTF8;
                        MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                        bool priority = true;

                        if (priority == false)
                        {
                            MailMessage.Priority = MailPriority.Normal;
                        }
                        else
                        {
                            MailMessage.Priority = MailPriority.High;
                        }
                    }

                    SmtpClient.Send(MailMessage);
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

        public void Registrar(UsuarioModel model)
        {
            string strQuery = "";

            strQuery = "INSERT" + Environment.CommandLine +
                    "INTO TB_USUARIO ( NOME_USUARIO, EMAIL_USUARIO, SENHA_USUARIO, SOBRENOME_USUARIO, RUA_ENDEREÇO_USUARIO, " + Environment.CommandLine +
                    "NUMERO_ENDEREÇO_USUARIO, BAIRRO_ENDEREÇO_USUARIO, CIDADE_ENDEREÇO_USUARIO, ESTADO_ENDEREÇO_USUARIO" + Environment.CommandLine +
                    "CEP_ENDEREÇO_USUARIO, TIPO_SANGUINEO, DATA_NASCIMENTO, DATA_ULTIMA_DOACAO)" + Environment.CommandLine +
                    "VALUES(" + model.NOME_USUARIO + " , "
                     + model.EMAIL_USUARIO + " , " + Environment.CommandLine
                     + model.SENHA_USUARIO + " , " + Environment.CommandLine
                     + model.SOBRENOME_USUARIO + " , " + Environment.CommandLine
                     + model.RUA_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.NUMERO_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.BAIRRO_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.CIDADE_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.ESTADO_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.CEP_ENDEREÇO_USUARIO + " , " + Environment.CommandLine
                     + model.TIPO_SANGUINEO + " , " + Environment.CommandLine
                     + model.DATA_NASCIMENTO + " , " + Environment.CommandLine
                     + model.DATA_ULTIMA_DOACAO + " , " + Environment.CommandLine + " )";


            var a = context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
        }
    }
}
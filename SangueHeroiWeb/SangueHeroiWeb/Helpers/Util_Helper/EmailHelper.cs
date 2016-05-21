using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class EmailHelper
    {
        protected static SmtpClient SmtpClient { get; set; }
        protected static MailMessage MailMessage { get; set; }
    
        public static bool EnviarEmail(string _emailDestinatario, string _nomeDestinatario, bool priority)
        {
            bool emailOk = true;

            try
            {
                SmtpClient = new SmtpClient();
                SmtpClient.Host = "smtp.gmail.com";
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = new NetworkCredential("sangue.heroi@gmail.com", "appsangueheroi10");

                MailMessage = new MailMessage();
                MailMessage.From = new MailAddress("sangue.heroi@gmail.com", "Sangue Heroi", Encoding.UTF8);
                MailMessage.To.Add(new MailAddress(_emailDestinatario, _nomeDestinatario, Encoding.UTF8));

                MailMessage.Subject = "Sangue Heroi - SolicitaCão de Nova Senha";
                MailMessage.Body = "Email de Troca de Senha - Sera formatado em breve :D";
                MailMessage.BodyEncoding = Encoding.UTF8;
                MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
               
                if (priority == false)
                {
                    MailMessage.Priority = MailPriority.Normal;
                }
                else
                {
                    MailMessage.Priority = MailPriority.High;
                }
                
                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Mensagem : {0} " + ex.Message);
                emailOk = false;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                emailOk = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                emailOk = false;
            }

            return emailOk;        
        }

        public static bool EnviaEmailParaHemocentro(string _emailDestinatario, string _nomeDestinatario, bool priority)
        {
            bool emailOk = true;

            try
            {
                SmtpClient = new SmtpClient();
                SmtpClient.Host = "smtp.gmail.com";
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = new NetworkCredential("sangue.heroi@gmail.com", "appsangueheroi10");

                MailMessage = new MailMessage();
                MailMessage.From = new MailAddress("sangue.heroi@gmail.com", "Sangue Heroi", Encoding.UTF8);
                MailMessage.To.Add(new MailAddress(_emailDestinatario, _nomeDestinatario, Encoding.UTF8));

                MailMessage.Subject = "Sangue Heroi - Solicitação de Parceria";
                MailMessage.Body = "Atenção! Seu Cadastro Foi Enviado Para nossos Administradores, em até 24horas você receberá um login e uma senha!";
                MailMessage.BodyEncoding = Encoding.UTF8;
                MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                if (priority == false)
                {
                    MailMessage.Priority = MailPriority.Normal;
                }
                else
                {
                    MailMessage.Priority = MailPriority.High;
                }

                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Mensagem : {0} " + ex.Message);
                emailOk = false;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                emailOk = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                emailOk = false;
            }

            return emailOk;
        }

        public static bool EnviarEmailSolicitacaoPareriaHemocentro(string _emailDestinatario, string _nomeDestinatario, bool priority)
        {
            bool emailOk = true;

            try
            {
                SmtpClient = new SmtpClient();
                SmtpClient.Host = "smtp.gmail.com";
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = new NetworkCredential("sangue.heroi@gmail.com", "appsangueheroi10");

                MailMessage = new MailMessage();
                MailMessage.From = new MailAddress("sangue.heroi@gmail.com", "Sangue Heroi", Encoding.UTF8);
                MailMessage.To.Add(new MailAddress(_emailDestinatario, _nomeDestinatario, Encoding.UTF8));

                MailMessage.Subject = "Sangue Heroi - Solicitação de Parceria";
                MailMessage.Body = "Atenção! Existe uma Parceria de um Hemocentro Para ser Validada";
                MailMessage.BodyEncoding = Encoding.UTF8;
                MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                if (priority == false)
                {
                    MailMessage.Priority = MailPriority.Normal;
                }
                else
                {
                    MailMessage.Priority = MailPriority.High;
                }

                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Mensagem : {0} " + ex.Message);
                emailOk = false;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                emailOk = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                emailOk = false;
            }

            return emailOk;
        }
    }
}
using SangueHeroiWeb.Models;
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
        public static bool EnviarEmail(string toEmail, string toName, string body, string subject, bool priority)
        {
            var emailOk = true;

            try
            {
                SmtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("sangue.heroi@gmail.com", "appsangueheroi10")
                };

                MailMessage = new MailMessage
                {
                    From = new MailAddress("sangue.heroi@gmail.com", "Sangue Heroi", Encoding.UTF8),
                    To = { new MailAddress(toEmail, toName, Encoding.UTF8) },
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Priority = priority == false ? MailPriority.Normal : MailPriority.High
                };

                MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

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
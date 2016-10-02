using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using SangueHeroiWeb.Models;

public class AndroidGCMPushNotification
{
    private class Notificacao
    {
        public string Titulo;
        public string Mensagem;
        public string ItemId;
    }

    private class NotificacaoNivelSanguineo
    {
        public string Titulo;
        public string Mensagem;
        public string NOME_HEMOCENTRO;
        public string TIPO_SANGUINEO;
        public int VALOR_TIPO_SANGUINEO;
    }

    private class NotificacaoCompleta
    {
        public string NOME_CAMPANHA;
        public string DESCRICAO_CAMPANHA;
        public string CODIGO_CAMPANHA;
        public string NOME_USUARIO;
        public string EMAIL_USUARIO;
        public string NOME_RECEPTOR;
        public string TIPO_SANGUINEO;
        public string NOME_HOSPITAL;
        public string ESTADO;
        public string CIDADE;
        public string BAIRRO;
        public string LOGRADOURO;
        public string CEP;
        public string DATA_INICIO;
        public string DATA_FIM;
    }

    public AndroidGCMPushNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string EnviarNotificacaoCompleta(List<string> deviceRegIds, string mensagem, string titulo, string id, string nome_usuario, string email_usuario, string nome_receptor, string tipo_sanguineo, string nome_hospital, string estado, string cidade, string bairro, string logradouro, string cep, string data_inicio, string data_fim)
    {
        //try
        //{
        
        string regIds = string.Join("\",\"", deviceRegIds);

        string AppId = "AIzaSyB5oZKX53Uw5z4cUmwEEgefWf8k0PFpwvY";
        var SenderId = 43844248731;

        NotificacaoCompleta n = new NotificacaoCompleta();
 
        n.NOME_CAMPANHA = titulo;
        n.DESCRICAO_CAMPANHA = mensagem;
        n.CODIGO_CAMPANHA = id;
        n.NOME_USUARIO = nome_usuario;
        n.EMAIL_USUARIO = email_usuario;
        n.NOME_RECEPTOR = nome_receptor;
        n.TIPO_SANGUINEO = tipo_sanguineo;
        n.NOME_HOSPITAL = nome_hospital;
        n.ESTADO = estado;
        n.CIDADE = cidade;
        n.BAIRRO = bairro;
        n.LOGRADOURO = logradouro;
        n.CEP = cep;
        n.DATA_INICIO = data_inicio;
        n.DATA_FIM = data_fim;

    var value = new JavaScriptSerializer().Serialize(n);
        WebRequest wRequest;
        wRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        wRequest.Method = "post";
        wRequest.ContentType = " application/json;charset=UTF-8";
        wRequest.Headers.Add(string.Format("Authorization: key={0}", AppId));

        wRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));

        string postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":2419200,\"delay_while_idle\":true,\"data\": { \"message\" : " + value + ",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

        Byte[] bytes = Encoding.UTF8.GetBytes(postData);
        wRequest.ContentLength = bytes.Length;

        Stream stream = wRequest.GetRequestStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();

        WebResponse wResponse = wRequest.GetResponse();

        stream = wResponse.GetResponseStream();

        StreamReader reader = new StreamReader(stream);

        String response = reader.ReadToEnd();

        HttpWebResponse httpResponse = (HttpWebResponse)wResponse;
        string status = httpResponse.StatusCode.ToString();

        reader.Close();
        stream.Close();
        wResponse.Close();

        return response;

        /* if (status == "")
         {
             return response;
         }
         else
         {
             return response;
         }
     }
     catch
     {
         return response;
     }*/
    }

    public string EnviarNotificacaoNiveisSanguineos(List<string> deviceRegIds, string mensagem, string titulo, string nome_hemocentro, string tipo_sanguineo, int valor_tipo_sanguineo)
    {
        //try
        //{
        string regIds = string.Join("\",\"", deviceRegIds);

        string AppId = "AIzaSyB5oZKX53Uw5z4cUmwEEgefWf8k0PFpwvY";
        var SenderId = 43844248731;

        NotificacaoNivelSanguineo n = new NotificacaoNivelSanguineo();
        n.Titulo = titulo;
        n.Mensagem = mensagem;
        n.NOME_HEMOCENTRO = nome_hemocentro;
        n.TIPO_SANGUINEO = tipo_sanguineo;
        n.VALOR_TIPO_SANGUINEO = valor_tipo_sanguineo;

        var value = new JavaScriptSerializer().Serialize(n);
        WebRequest wRequest;
        wRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        wRequest.Method = "post";
        wRequest.ContentType = " application/json;charset=UTF-8";
        wRequest.Headers.Add(string.Format("Authorization: key={0}", AppId));

        wRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));

        string postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":2419200,\"delay_while_idle\":true,\"data\": { \"message\" : " + value + ",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

        Byte[] bytes = Encoding.UTF8.GetBytes(postData);
        wRequest.ContentLength = bytes.Length;

        Stream stream = wRequest.GetRequestStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();

        WebResponse wResponse = wRequest.GetResponse();

        stream = wResponse.GetResponseStream();

        StreamReader reader = new StreamReader(stream);

        String response = reader.ReadToEnd();

        HttpWebResponse httpResponse = (HttpWebResponse)wResponse;
        string status = httpResponse.StatusCode.ToString();

        reader.Close();
        stream.Close();
        wResponse.Close();

        return response;

        /* if (status == "")
         {
             return response;
         }
         else
         {
             return response;
         }
     }
     catch
     {
         return response;
     }*/
    }

    public string EnviarNotificacao(List<string> deviceRegIds, string mensagem, string titulo, string id)
    {
        //try
        //{
            string regIds = string.Join("\",\"", deviceRegIds);

            string AppId = "AIzaSyB5oZKX53Uw5z4cUmwEEgefWf8k0PFpwvY";
            var SenderId = 43844248731;

            Notificacao n = new Notificacao();
            n.Titulo = titulo;
            n.Mensagem = mensagem;
            n.ItemId = id;

            var value = new JavaScriptSerializer().Serialize(n);
            WebRequest wRequest;
            wRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            wRequest.Method = "post";
            wRequest.ContentType = " application/json;charset=UTF-8";
            wRequest.Headers.Add(string.Format("Authorization: key={0}", AppId));

            wRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));

            string postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":2419200,\"delay_while_idle\":true,\"data\": { \"message\" : " + value + ",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

            Byte[] bytes = Encoding.UTF8.GetBytes(postData);
            wRequest.ContentLength = bytes.Length;

            Stream stream = wRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            WebResponse wResponse = wRequest.GetResponse();

            stream = wResponse.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            String response = reader.ReadToEnd();

            HttpWebResponse httpResponse = (HttpWebResponse)wResponse;
            string status = httpResponse.StatusCode.ToString();

            reader.Close();
            stream.Close();
            wResponse.Close();

            return response;

           /* if (status == "")
            {
                return response;
            }
            else
            {
                return response;
            }
        }
        catch
        {
            return response;
        }*/
    }
}
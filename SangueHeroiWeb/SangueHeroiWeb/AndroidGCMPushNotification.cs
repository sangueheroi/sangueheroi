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

    public AndroidGCMPushNotification()
    {
        //
        // TODO: Add constructor logic here
        //
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
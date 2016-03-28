using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using SangueHeroiWeb.Models;
using System.Web.Script.Services;

namespace SangueHeroiWeb
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://sangueheroiweb.azurewebsites.net/WebService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string testarRetorno(string json)
        {
            var retorno_json = "Hugo, envie algo!";

            if (json != "")
            {
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                retorno_json = json;
            }

            return retorno_json;
        }

     }

}

//DataClassesDataContext dc = new DataClassesDataContext();

//[WebMethod]
        /*public string getUsuario(string id)
        {
            var json = "";
        */
        /*var usuario = from resultado in dc.tabela_001_usuarios
                      where resultado.id = int.Parse(id)
                      select resultado;
        */
        /*
            UsuarioModelcs usuario = new UsuarioModelcs();

            usuario.ID_USUARIO = 1;
            usuario.NOME_USUARIO = "Daniel";
            usuario.SOBRENOME_USUARIO = "Costa";
            usuario.ENDEREÇO_USUARIO = "Rua Cel Joao de Oliveira Melo";

            //(1, "Daniel", "Costa", "Rua Cel Joao Oliveira Melo", "565", "Vila Antonieta", "São Paulo", "SP", "03474020", "O+", "daniel@costa.com", 1995 - 02 - 28, 2016 - 02 - 02);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            json = jss.Serialize(usuario);

            return json;
        }*/


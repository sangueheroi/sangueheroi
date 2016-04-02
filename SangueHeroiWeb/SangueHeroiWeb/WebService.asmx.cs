using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.Controllers;
using SangueHeroiWeb.DAO;
using System.Web.Script.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Services.Protocols;

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
        public ValidacaoSoapHeader Autenticacao;
        private const string DEV_TOKEN = "sh10app";

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

        //[SoapHeader("Autenticacao")]
        [WebMethod]
        public bool efetuarLogin(string login, string senha)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                LoginDAO ldao = new LoginDAO();
                LoginModel lmodel = new LoginModel();

                lmodel.EMAIL_USUARIO = login;
                lmodel.SENHA = senha;
                lmodel.LEMBRAR_ME = true;

                var retorno = ldao.Logar(lmodel);

                return retorno;
            //}
            /*else
            {
                throw new Exception("A autenticação falhou");
            }*/
        }

        [WebMethod]
        public void registrarUsuario(string nome, string sobrenome, string login, string senha, string endereco, string numero, string bairro, string cidade, string estado, string cep, string tipo_sanguineo, DateTime dtnascimento, DateTime data_ultima_doacao)
        {
            LoginDAO ldao = new LoginDAO();
            UsuarioModel umodel = new UsuarioModel();

            umodel.EMAIL_USUARIO = login;
            umodel.SENHA_USUARIO = senha;
            umodel.NOME_USUARIO = nome;
            umodel.SOBRENOME_USUARIO = sobrenome;
            umodel.RUA_ENDEREÇO_USUARIO = endereco;
            umodel.NUMERO_ENDEREÇO_USUARIO = numero;
            umodel.BAIRRO_ENDEREÇO_USUARIO = bairro;
            umodel.CIDADE_ENDEREÇO_USUARIO = cidade;
            umodel.ESTADO_ENDEREÇO_USUARIO = estado;
            umodel.CEP_ENDEREÇO_USUARIO = cep;
            umodel.TIPO_SANGUINEO = tipo_sanguineo;
            umodel.DATA_NASCIMENTO = dtnascimento;
            umodel.DATA_ULTIMA_DOACAO = data_ultima_doacao;

            ldao.Registrar(umodel);

            return;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string cadastrarUsuario(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            UsuarioModel usuario = new UsuarioModel();
            //LoginDAO ldao = new LoginDAO();

            //UsuarioModel [] ar_usuario = JsonConvert.DeserializeObject<UsuarioModel[]>(json);
            UsuarioModel ar_usuario = JsonConvert.DeserializeObject<UsuarioModel>(json);

            //JObject instaCall = JObject.Parse(json);
            //UsuarioModel resultado = instaCall["usuario"].ToObject<UsuarioModel>();

            usuario.ID_USUARIO = int.Parse(ar_usuario);
            /*
            usuario.ID_USUARIO = int.Parse(ar_usuario[0]);
            usuario.NOME_USUARIO = ar_usuario[1];
            usuario.SOBRENOME_USUARIO = ar_usuario[2];
            usuario.ESTADO_ENDEREÇO_USUARIO = ar_usuario[3];*/

            return usuario;
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


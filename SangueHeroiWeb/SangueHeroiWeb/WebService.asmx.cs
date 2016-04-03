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
using SangueHeroiWeb.Helpers.Util_Helper;

namespace SangueHeroiWeb
{
    /// <summary>
    /// WebService utilizado para comunicação com a aplicação Android
    /// </summary>
    [WebService(Namespace = "http://sangueheroiweb.azurewebsites.net/WebService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que este Web Service seja chamado de um script, usando ASP.NET AJAX, tire o comentário da próxima linha. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        //public ValidacaoSoapHeader Autenticacao;
        //private const string DEV_TOKEN = "sh10app";

        //Método utilizado para permitir o login pelo app Android, a partir da consulta de login e senha no banco de dados.    
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
            //else
            //{
            //    throw new Exception("A autenticação falhou");
            //}
        }

        //Método utilizado para registrar Usuário no banco de dados a partir do app Android.
        [WebMethod]
        public void registrarUsuario(string nome, string sobrenome, string login, string senha, string endereco, string numero, string bairro, string cidade, string estado, string cep, string tipo_sanguineo, DateTime dtnascimento, DateTime data_ultima_doacao)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
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
            //}
            //else
            //{
            //    throw new Exception("A autenticação falhou");
            //}
        }

    }

}


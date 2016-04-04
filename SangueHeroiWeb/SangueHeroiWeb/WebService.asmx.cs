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
    /// WebService utilizado para comunicaCão com a aplicaCão Android
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
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        //Método utilizado para registrar Usuário no banco de dados a partir do app Android.
        [WebMethod]
        public bool registrarUsuario(string nome, string email, string senha, string logradouro, string bairro, string cidade, string estado, string cep, string tipo_sanguineo, string dtnascimento, string dtultimadoacao, string codigo_heroi)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                LoginDAO ldao = new LoginDAO();
                UsuarioModel umodel = new UsuarioModel();

                umodel.NOME_USUARIO = nome;
                umodel.EMAIL_USUARIO = email;
                umodel.SENHA_USUARIO = senha;
                umodel.LOGRADOURO = logradouro;
                umodel.BAIRRO = bairro;
                umodel.CIDADE = cidade;
                umodel.ESTADO = estado;
                umodel.CEP = cep;
                umodel.TIPO_SANGUINEO = tipo_sanguineo;
                umodel.DATA_NASCIMENTO = Convert.ToDateTime(dtnascimento);
                umodel.DATA_ULTIMA_DOACAO = Convert.ToDateTime(dtultimadoacao);
                umodel.CODIGO_HEROI = Convert.ToInt32(codigo_heroi);

                var retorno = ldao.Registrar(umodel);

                return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

    }

}


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
        public void registrarUsuario(string nome, string documento, string login, string senha, string endereco, string numero, string bairro, string cidade, string estado, string tipo_sanguineo, DateTime dtnascimento)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                LoginDAO ldao = new LoginDAO();
                UsuarioModel umodel = new UsuarioModel();

                umodel.DOCUMENTO_USUARIO = documento;
                umodel.EMAIL_USUARIO = login;
                umodel.SENHA_USUARIO = senha;
                umodel.NOME_USUARIO = nome;
                //umodel.SOBRENOME_USUARIO = sobrenome;
                umodel.LOGRADOURO = endereco;
                umodel.NUMERO = numero;
                umodel.BAIRRO = bairro;
                umodel.CIDADE = cidade;
                umodel.ESTADO = estado;
                //umodel.CEP_ENDERECO_USUARIO = cep;
                umodel.TIPO_SANGUINEO = tipo_sanguineo;
                umodel.DATA_NASCIMENTO = dtnascimento;
                //umodel.DATA_ULTIMA_DOACAO = data_ultima_doacao;

                ldao.Registrar(umodel);

                return;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

    }

}


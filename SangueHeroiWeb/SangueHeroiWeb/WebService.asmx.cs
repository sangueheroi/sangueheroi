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
using System.Web.Mvc;

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
        public int verificarLogin(string login)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                LoginDAO ldao = new LoginDAO();
                LoginUsuarioModel lmodel = new LoginUsuarioModel();

                lmodel.EMAIL_USUARIO = login;

                var retorno = ldao.VerificarLogin(lmodel);

                return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public string testeGCM()
        {
            AndroidGCMPushNotification gcm = new AndroidGCMPushNotification();

            String deviceIdTeste = "c2-R_w1bRvw:APA91bG77mZH8HSs3FQhXYCpk2ESAEIX8m7s1exYAO_DcKN9k9BD5LWyKed10YfnsYL6QbltKTUoyBjJtqtPWW9cD0clrEcnOjslRypt0_sst7LFeYbDPh1CTIjT_y0347wFRSh7srzu";

            var retorno = gcm.SendNotification(deviceIdTeste, "Deu certo");

            return retorno;
        }

        [WebMethod]
        public string[] efetuarLogin(string login, string senha)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                LoginDAO ldao = new LoginDAO();
                LoginUsuarioModel lmodel = new LoginUsuarioModel();

                lmodel.EMAIL_USUARIO = login;
                lmodel.SENHA = senha;
                lmodel.LEMBRAR_ME = true;

                var retorno = ldao.LogarUsuario(lmodel);

                //string json = JsonConvert.SerializeObject(retorno);

                return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        //Método utilizado para registrar Usuário no banco de dados a partir do app Android.
        [WebMethod]
        public int registrarUsuario(string nome, string email, string senha, string sexo, string bairro, string cidade, string estado, string cep, string tipo_sanguineo, string dtnascimento, string dtultimadoacao, int codigo_heroi, bool flagCadastroIsRedeSocial)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
                UsuarioDAO udao = new UsuarioDAO();
                UsuarioModel umodel = new UsuarioModel();

                umodel.NOME_USUARIO = nome;
                umodel.EMAIL_USUARIO = email;
                umodel.SENHA_USUARIO = senha;
                umodel.SEXO = sexo;
                umodel.BAIRRO = bairro;
                umodel.CIDADE = cidade;
                umodel.ESTADO = estado;
                umodel.CEP = cep;
                umodel.TIPO_SANGUINEO = tipo_sanguineo;
                umodel.DATA_NASCIMENTO = Convert.ToDateTime(dtnascimento);

                if(dtultimadoacao == "")
                {
                    DateTime dt = DateTime.Now.AddDays(-90);
                    umodel.DATA_ULTIMA_DOACAO = Convert.ToDateTime(dt);
                }
                else
                    umodel.DATA_ULTIMA_DOACAO = Convert.ToDateTime(dtultimadoacao);

                umodel.CODIGO_HEROI = codigo_heroi;
                umodel.FLAG_CADASTRO_REDE_SOCIAL = flagCadastroIsRedeSocial;

                var retorno = udao.Registrar(umodel);

                return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public string historicoDoacoes(string email)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{

            DoacaoDAO doacao = new DoacaoDAO();

            List<DoacaoModel> lista = doacao.getHistoricoDoacoes(email);

            string json = JsonConvert.SerializeObject(lista, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            
            return json;
            
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public int cadastrarCampanha(string email, string nome_campanha, string descricao, string nome_receptor, string tipo_sanguineo, string dtinicio, string dtfim, string nome_hospital, string logradouro, string bairro, string cidade, string estado, string cep)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
            CampanhaDAO cdao = new CampanhaDAO();
            CampanhaModel cmodel = new CampanhaModel();
            UsuarioModel umodel = new UsuarioModel();

            umodel.EMAIL_USUARIO = email;
            cmodel.NOME_CAMPANHA = nome_campanha;
            cmodel.DESCRICAO_CAMPANHA = descricao;
            cmodel.NOME_RECEPTOR = nome_receptor;
            cmodel.TIPO_SANGUINEO = tipo_sanguineo;
            cmodel.DATA_INICIO_DT = Convert.ToDateTime(dtinicio);
            cmodel.DATA_FIM_DT = Convert.ToDateTime(dtfim);
            cmodel.NOME_HOSPITAL = nome_hospital;
            cmodel.LOGRADOURO = logradouro;
            cmodel.BAIRRO = bairro;
            cmodel.CIDADE = cidade;
            cmodel.ESTADO = estado;
            cmodel.CEP = cep;

            var retorno = cdao.CadastrarCampanha(cmodel, umodel);

            return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public int alterarCampanha(int codigo_campanha, string nome_campanha, string descricao, string nome_receptor, string tipo_sanguineo, string dtinicio, string dtfim, string nome_hospital, string logradouro, string bairro, string cidade, string estado, string cep)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
            CampanhaDAO cdao = new CampanhaDAO();
            CampanhaModel cmodel = new CampanhaModel();
            UsuarioModel umodel = new UsuarioModel();

            cmodel.CODIGO_CAMPANHA = codigo_campanha;
            cmodel.NOME_CAMPANHA = nome_campanha;
            cmodel.DESCRICAO_CAMPANHA = descricao;
            cmodel.NOME_RECEPTOR = nome_receptor;
            cmodel.TIPO_SANGUINEO = tipo_sanguineo;
            if (dtinicio != "")
                cmodel.DATA_INICIO_DT = Convert.ToDateTime(dtinicio);
            if (dtfim != "")
                cmodel.DATA_FIM_DT = Convert.ToDateTime(dtfim);
            cmodel.NOME_HOSPITAL = nome_hospital;
            cmodel.LOGRADOURO = logradouro;
            cmodel.BAIRRO = bairro;
            cmodel.CIDADE = cidade;
            cmodel.ESTADO = estado;
            cmodel.CEP = cep;

            var retorno = cdao.AlterarCampanha(cmodel);

            return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public string minhasCampanhas(string email)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{

            CampanhaDAO campanha = new CampanhaDAO();

            List<CampanhaModel> lista = campanha.getMinhasCampanhas(email);

            string json = JsonConvert.SerializeObject(lista, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            return json;

            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public string consultarCampanhas()
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{

            CampanhaDAO campanha = new CampanhaDAO();

            List<CampanhaModel> lista = campanha.consultarCampanhas();

            string json = JsonConvert.SerializeObject(lista, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            return json;

            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

        [WebMethod]
        public int excluirCampanha(int id, string email)
        {
            //if (Autenticacao != null && Autenticacao.DevToken == DEV_TOKEN)
            //{
            CampanhaDAO cdao = new CampanhaDAO();
            CampanhaModel cmodel = new CampanhaModel();
            UsuarioModel umodel = new UsuarioModel();

            cmodel.CODIGO_CAMPANHA = id;
            umodel.EMAIL_USUARIO = email;

            var retorno = cdao.deletarCampanha(cmodel, umodel);

            return retorno;
            //}
            //else
            //{
            //    throw new Exception("A autenticaCão falhou");
            //}
        }

    }

}


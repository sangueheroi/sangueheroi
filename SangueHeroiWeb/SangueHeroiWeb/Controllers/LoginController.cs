using Microsoft.AspNet.Identity.Owin;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        public ActionResult Login(HemocentroModel model)
        {

            LoginDAO dao = new LoginDAO();
            
            int loginOK = dao.LogarHemocentro(model);

            if (loginOK == (int) SITUACAO.DADOS_INVALIDOS)
            {
                return Json(new
                {
                    msg = "Login ou Senha Incorretos!",
                    isRedirect = false
                });
            }
            else if (loginOK == (int) SITUACAO.NAO_POSSUI_CADASTRO)
            {
                return Json(new
                {
                    msg = "Login Não Cadastrado!",
                    isRedirect = false
                });
            }
            else if (loginOK == Convert.ToInt32(SITUACAO.CADASTRO_BLOQUEADO))
            {
                return Json(new
                {
                    msg = "Atenção! Cadastro Bloqueado, entre em contado com um administrador para o desbloqueio!",
                    isRedirect = false
                });
            }
            else if (loginOK == Convert.ToInt32(SITUACAO.ERRO_DE_SISTEMA))
            {
                return Json(new
                {
                    msg = "Atenção! Erro no Sistema, entre em contado com um administrador reportando o erro!",
                    isRedirect = false
                });
            }
            else
            {
                HemocentroDAO hd = new HemocentroDAO();
                HemocentroModel m =
                    hd.BuscaHemocentro(" WHERE H.LOGIN_HEMOCENTRO = " + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO));

                Session["CODIGO_HEMOCENTRO_PERFIL"] = m.CODIGO_HEMOCENTRO_PERFIL;
                Session["ID_HEMOCENTRO"] = m.CODIGO_HEMOCENTRO;
                ViewBag.NomeHemocentro = m.NOME_HEMOCENTRO;

                Session["NOME_HEMOCENTRO"] = m.NOME_HEMOCENTRO;

                return Json(new
                {
                    isRedirect = true,
                    url = "./Home/Index"
                });
            }
        }

        [HttpGet]
        public ActionResult EsqueciMinhaSenha()
        {
            return PartialView("_EsqueciMinhaSenha");
        }

        [HttpPost]
        public ActionResult EsqueciMinhaSenha(EsqueciMinhaSenhaModel model)
        {
            var lDao = new LoginDAO();
            var hDao = new HemocentroDAO();
            var alteraSenha = lDao.EsqueciMinhaSenha(model.EMAIL);
            var enc = new Encrypt();;

            var hemocentro = hDao.BuscaHemocentro($" WHERE H.EMAIL = '{model.EMAIL}'");

            if (!alteraSenha)
            {
                return Json(new
                {
                    msg = "E-mail Não Encontrado! Digite um E-mail Válido",
                    url = "Index",
                    isRedirect = false
                });
            }
            else
            {
                try
                {
                    string mensagem = $@"<div>
                                    <p><label> Atenção! Foi gerado uma nova senha para você, recomendamos que faça a alteração da mesma.</label></p>
                                    <p><label>Nova Senha: {enc.DecryptoRSA(hemocentro.SENHA_HEMOCENTRO)} </label></p>
                                </div>";

                    using (var sr = new StreamReader(Server.MapPath("\\Views\\Shared\\") + "TemplateEmail.html"))
                    {
                        var body = sr.ReadToEnd();

                        var subject = "Sangue Heroi - Esqueci Minha Senha";

                        EmailHelper.EnviarEmail(hemocentro.EMAIL, hemocentro.NOME_HEMOCENTRO, string.Format(body, mensagem), subject, true);
                    }

                    return Json(new
                    {
                        msg = "Atenção! Foi enviado um e-mail para você com informações para criar uma nova senha",
                        url = "./Home/Index",
                        isRedirect = true
                    });
                }
                catch (Exception exception)
                {
                    return Json(new
                    {
                        msg = $"Atenção! Ocorreu o erro - {exception.Message} - Por gentileza entre em contato com os administradores.",
                        url = "Index",
                        isRedirect = false
                    });

                }
            }
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}
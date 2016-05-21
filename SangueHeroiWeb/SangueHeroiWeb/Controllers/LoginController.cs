using Microsoft.AspNet.Identity.Owin;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
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
  
            if(loginOK == (int) SITUACAO.DADOS_INVALIDOS)
            {
                return Json(new
                {
                    msg = "Login ou Senha Incorretos!",
                    isRedirect = false
                });
            }
            else
            {
                HemocentroDAO hd = new HemocentroDAO();
                //Session["TIPO_HEMOCENTRO_LOGADO"] = hd.BuscaHemocentro(" WHERE H.LOGIN = " + model.LOGIN_HEMOCENTRO).TIPO_PERFIL_HEMOCENTRO;
                Session["TIPO_HEMOCENTRO_LOGADO"] = 0;
                //Session["ID_HEMOCENTRO"] = hd.BuscaHemocentro(" WHERE H.LOGIN = " + model.LOGIN_HEMOCENTRO).CODIGO_HEMOCENTRO;
                Session["ID_HEMOCENTRO"] = 1;

                return Json(new
                {
                    isRedirect = true,
                    url = "Home/Index"
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
            LoginDAO dao = new LoginDAO();

            bool emailOK = dao.EsqueciMinhaSenha(model.EMAIL_HEMOCENTRO);

            if(!emailOK)
            {
                return Json(new
                {
                    msg = "E-mail Não Encontrado! Digite um E-mail Válido",
                    //redirectUrl = "Index",
                    //isRedirect = false
                });
            }
            else
            {
                return Json(new
                {
                    msg = "AtenCão! Foi enviado um e-mail para você com informaCões para criar uma nova senha",
                    //redirectUrl = "Index",
                    //isRedirect = false
                });
            }
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Index", "Login");
        }

    }
}
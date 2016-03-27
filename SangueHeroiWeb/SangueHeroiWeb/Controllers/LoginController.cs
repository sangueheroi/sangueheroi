using Microsoft.AspNet.Identity.Owin;
using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
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
        public ActionResult Login(LoginModel model)
        {
            LoginDAO dao = new LoginDAO();

            bool loginOK = dao.Logar(model);

            if(!loginOK)
            {
                return Json(new
                {
                    msg = "Login ou Senha Incorretos!",
                    //redirectUrl = "Index",
                    //isRedirect = false
                });
            }
            else
            {
                return Json(new
                {
                    msg = "Login Feito com Sucesso"  
                });
            }

            return View();
        }


        [HttpPost]
        public ActionResult LogarFacebook(LoginModel model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogarGmail(LoginModel model)
        {
            return View();
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

            bool emailOK = dao.EsqueciMinhaSenha(model.EMAIL_USUARIO);

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
                    msg = "Atenção! Foi enviado um e-mail para você com informações para criar uma nova senha",
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

        [HttpGet]
        public ActionResult Registrar()
        {
            return PartialView("_Registrar");
        }

        [HttpPost]
        public ActionResult Registrar(String emailUsuario)
        {
            return View();
        }

       
    }
}
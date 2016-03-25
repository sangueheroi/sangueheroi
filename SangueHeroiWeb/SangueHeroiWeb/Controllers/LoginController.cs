using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var a = "";

            return PartialView("_Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
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
        public ActionResult EsqueciMinhaSenha(String emailUsuario)
        {
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
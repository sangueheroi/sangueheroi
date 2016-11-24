using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["ID_HEMOCENTRO"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }            
        }
    }
}
using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.Helpers.Constantes_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class HemocentroController : Controller
    {
        // GET: Hemocentro
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ParceriaHemocentro()
        {
            HemocentroDAO ud = new HemocentroDAO();
            HemocentroModel model = new HemocentroModel();

            ViewBag.NomeEstados = model.ListaEstados;

            return PartialView("_CadastroParceriaHemocentro");
        }

        [HttpPost]
        public ActionResult ParceriaHemocentro(HemocentroModel model)
        {
            HemocentroDAO dao = new HemocentroDAO();
            String msg = "Conta realizada com sucesso!";

            if (model != null)
            {
                model.LOGIN_HEMOCENTRO = model.EMAIL_HEMOCENTRO.Split('@')[0];
                model.SENHA_HEMOCENTRO = Helpers.Util_Helper.GeraSenha.CriaSenha();
                model.CODIGO_STATUS = Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Ativo;
               
                if (dao.ParceriaHemocentro(model) == 0)
                {
                    msg = "Erro na realização do cadastro!";
                }
            }

            return Json(new
            {
                msg = msg
                //redirectUrl = "Index",
                //isRedirect = false
            });

            //return View();
        }
    }
}
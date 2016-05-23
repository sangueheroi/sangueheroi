using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListHemocentrosBloqueados()
        {
            HemocentroDAO hd = new HemocentroDAO();
            List<HemocentroModel> list = hd.Lista(" WHERE H.CODIGO_STATUS = " + Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AtivarHemocentro(int IdHemocentro)
        {
            HemocentroDAO hd = new HemocentroDAO();
            hd.AtivarHemocentro(IdHemocentro);
            HemocentroModel m = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + IdHemocentro);

            EmailHelper.EnviaEmailParaHemocentroPerfilAtivo(m, true);

            List<HemocentroModel> list = hd.Lista(" WHERE H.CODIGO_STATUS = " + Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
    }
}
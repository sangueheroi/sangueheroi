using SangueHeroiWeb.DAO;
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

            list.Add(new HemocentroModel()
            {
                CODIGO_HEMOCENTRO = 1,
                NOME_HEMOCENTRO = "Hemocentro",
                CNPJ = "11111111",
                CEP = "111111",
                CIDADE_HEMOCENTRO = "São Paulo",
                ESTADO_HEMOCENTRO = "SP",
                TELEFONE_HEMOCENTRO = "11-11111111"
            });

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AtivarHemocentro(int IdHemocentro)
        {
            HemocentroDAO hd = new HemocentroDAO();
            hd.AtivarHemocentro(IdHemocentro);

            List<HemocentroModel> list = hd.Lista(" WHERE H.CODIGO_STATUS = " + Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
    }
}
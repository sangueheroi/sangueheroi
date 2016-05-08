using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.Helpers;
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
                model.CODIGO_STATUS = Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado;
               
                if (dao.ParceriaHemocentro(model) == 0)
                {
                    //Enviar email para admns, avisando que existe cadastro para ser validado
                    //Envio email para o hemocentro falando que em 24h o cadastro sera validado
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
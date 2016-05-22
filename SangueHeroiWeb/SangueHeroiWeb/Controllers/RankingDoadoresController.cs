using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class RankingDoadoresController : Controller
    {
        // GET: Ranking de Doadores
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListRankingDoadores()
        {
            DoacaoDAO dd = new DoacaoDAO();
            List<DoacaoModel> list = dd.getRankingDoadores();

            return Json(list, JsonRequestBehavior.AllowGet);
        }    
    }
}
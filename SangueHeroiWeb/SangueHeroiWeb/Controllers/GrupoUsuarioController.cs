using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class GrupoUsuarioController : Controller
    {
        // GET: GrupoUsuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListGrupoUsuarios()
        {
            GrupoUsuarioDAO grupoUsuarioDAO = new GrupoUsuarioDAO();
            List<GrupoUsuarioModel> list = grupoUsuarioDAO.GetListGrupoUsuario();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UsuariosGrupo(int idGrupoUsuario)
        {
            GrupoUsuarioDAO grupoUsuarioDAO = new GrupoUsuarioDAO();
            GrupoUsuarioModel model = grupoUsuarioDAO.GetListGrupoUsuario(" WHERE UG.CODIGO_GRUPO = " + idGrupoUsuario).FirstOrDefault();

            return PartialView("_UsuariosGrupo", model);
        }

        public JsonResult GetListUsuariosDoGrupo(int idGrupo)
        {
            GrupoUsuarioDAO grupoUsuarioDAO = new GrupoUsuarioDAO();
            List<UsuarioModel> list = grupoUsuarioDAO.GetListUsuarioPorGrupo(" WHERE UG.CODIGO_GRUPO = " + idGrupo);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
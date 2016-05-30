using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Helpers.Constantes_Helper;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RelatorioTipoSanguineoLocalidade()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NiveisSanguineos()
        {
            HemocentroDAO hd = new HemocentroDAO();
            List<HemocentroModel> list = hd.Lista(" WHERE H.CODIGO_STATUS = " + Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Ativo  +"AND CODIGO_HEMOCENTRO_PERFIL = " + Constantes.TIPO_PERFIL_HEMOCENTRO.HEMOCENTRO);

            ViewBag.ListaHemocentros = list;

            return View();
        }

        public ActionResult GetDadosNiveisSanguineosGrafico(int idHemocentro)
        {
            HemocentroDAO hd = new HemocentroDAO();
            List<HemocentroNiveisSanguineosModelGrafico> model = hd.GetNiveisSanguineos(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
            IEnumerable<HemocentroNiveisSanguineosModelGrafico> data = model;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TiposSanguineos()
        {
            return View();
        }

        public ActionResult GetNiveisSanguineosPorUsuario()
        {
            UsuarioDAO ud = new UsuarioDAO();
            //List<UsuarioGraficoModel> ugm = ud.GetTipoSanguineoPorUsuario();
            List<UsuarioGraficoModel> ugm = new List<UsuarioGraficoModel>();

            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "O-", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 35 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "O+", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 47 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "A-", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 12 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "A+", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 21 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "B-", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 66 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "B+", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 10 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "AB-", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 19 });
            ugm.Add(new UsuarioGraficoModel() { NOME_TIPO_SANGUINEO_USUARIO = "AB+", QUANTIDADE_USUARIOS_TIPO_SANGUINEO = 7 });

            IEnumerable<UsuarioGraficoModel> data = ugm;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
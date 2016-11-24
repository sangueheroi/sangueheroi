using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Helpers.Constantes_Helper;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SangueHeroiWeb.Controllers
{
    public class CampanhaController : Controller
    {
        // GET: Campanha
        public ActionResult Index()
        {
            return View();
        }

       public JsonResult GetListCampanhas()
        {
            var campanhaDao = new CampanhaDAO();
            var list = campanhaDao.consultarCampanhas("  WHERE C.DATA_FIM >= " + UtilHelper.DateTimeParaSQLDate(DateTime.Now));

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Editar(int idCampanha)
        {
            var campanhaDao = new CampanhaDAO();
            var model = campanhaDao.BuscaCampanha(" WHERE C.CODIGO_CAMPANHA = " + idCampanha);
            ViewBag.TipoSanguineo = TiposSanguineos.GetTiposSanguineos();

            Session["NOME_CAMPANHA"] = model.NOME_CAMPANHA;
            List<String> Estados = new List<string>();
            Estados.Add("AC");
            Estados.Add("AL");
            Estados.Add("AP");
            Estados.Add("AM");
            Estados.Add("BA");
            Estados.Add("CE");
            Estados.Add("DF");
            Estados.Add("ES");
            Estados.Add("GO");
            Estados.Add("MA");
            Estados.Add("MT");
            Estados.Add("MS");
            Estados.Add("MG");
            Estados.Add("PA");
            Estados.Add("PB");
            Estados.Add("PR");
            Estados.Add("PE");
            Estados.Add("PI");
            Estados.Add("RR");
            Estados.Add("RO");
            Estados.Add("RJ");
            Estados.Add("RN");
            Estados.Add("RS");
            Estados.Add("SC");
            Estados.Add("SP");
            Estados.Add("SE");
            Estados.Add("TO");

            ViewBag.Estados = Estados;

            return PartialView("_Editar", model);
        }

        [HttpPost]
        public ActionResult Editar(CampanhaModel model)
        {
            var campanhaDao = new CampanhaDAO();
            var msg = "Edição Realizada com sucesso";
            var uDao = new UsuarioDAO();

            var lstUsuario = uDao.consultarUsuarios();
            var usuario = new UsuarioModel { DESTINATARIOS = new List<UsuarioModel>() };

            foreach (var usu in lstUsuario)
            {
                usuario.DESTINATARIOS.Add(usu);
            }

            var destinatarios = JsonConvert.SerializeObject(usuario, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            if (campanhaDao.AlterarCampanha(model, destinatarios) == (int)SITUACAO.SUCESSO)
                return Json(new
                {
                    msg,
                    isRedirect = true
                });

            msg = "Atenção! Ocorreu um Erro ao realizar a edição, favor contatar um administrador";

            return Json(new
            {
                msg,
                isRedirect = false
            });
        }

        [HttpGet]
        public ActionResult CriarCampanha()
        {
            CampanhaModel model = new CampanhaModel();

            ViewBag.TipoSanguineo = TiposSanguineos.GetTiposSanguineos();

            List<String> Estados = new List<string>();
            Estados.Add("AC");
            Estados.Add("AL");
            Estados.Add("AP");
            Estados.Add("AM");
            Estados.Add("BA");
            Estados.Add("CE");
            Estados.Add("DF");
            Estados.Add("ES");
            Estados.Add("GO");
            Estados.Add("MA");
            Estados.Add("MT");
            Estados.Add("MS");
            Estados.Add("MG");
            Estados.Add("PA");
            Estados.Add("PB");
            Estados.Add("PR");
            Estados.Add("PE");
            Estados.Add("PI");
            Estados.Add("RR");
            Estados.Add("RO");
            Estados.Add("RJ");
            Estados.Add("RN");
            Estados.Add("RS");
            Estados.Add("SC");
            Estados.Add("SP");
            Estados.Add("SE");
            Estados.Add("TO");

            ViewBag.Estados = Estados;
            return View(model);
        }

        [HttpPost]
        public ActionResult CriarCampanha(CampanhaModel model)
        {
            var msg = "Campanha Cadastrada com sucesso";
            var redirect = true;

            var cDao = new CampanhaDAO();
            var hDao = new HemocentroDAO();
            var dDo = new DispositivoDAO();
            var uDao = new UsuarioDAO();

            var hemocentro = hDao.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + Convert.ToInt32(Session["ID_HEMOCENTRO"].ToString()));
            model.CODIGO_HEMOCENTRO = hemocentro.CODIGO_HEMOCENTRO;
            model.NOME_USUARIO = hemocentro.NOME_HEMOCENTRO;

            if (model.NOME_RECEPTOR == null)
                model.NOME_RECEPTOR = "";
            else
                model.TIPO_SANGUINEO = "";

            var retorno = cDao.CadastrarCampanhaHemocentro(model);

            var lstUsuario = uDao.consultarUsuarios();
            var lstDestinatarios = new UsuarioModel { DESTINATARIOS = new List<UsuarioModel>() };
            

            foreach (var usu in lstUsuario)
            {
                lstDestinatarios.DESTINATARIOS.Add(usu);
            }

            var destinatarios = JsonConvert.SerializeObject(lstDestinatarios, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            if (retorno == (int)SITUACAO.SUCESSO)
            {
                dDo.DispararNotificacao(model, true, destinatarios);
            }
            else
            {
                redirect = false;
                msg = "Erro a criar campanha, contate um administrador!";
            }


            return Json(new
            {
                msg,
                isRedirect = redirect,
                url = Url.Action("Index")
            });

        }

        [HttpPost]
        public ActionResult EncerrarCampanha(int idCampanha)
        {
            CampanhaDAO campanhaDAO = new CampanhaDAO();
            String msg = "Exclusão Realizada com sucesso";
            bool redirect = true;

            CampanhaModel model = campanhaDAO.BuscaCampanha(" WHERE C.CODIGO_CAMPANHA = " + idCampanha);

            if (campanhaDAO.deletarCampanhaHemocentro(model) != (int)SITUACAO.SUCESSO)
            {
                msg = "Atenção! Ocorreu um Erro ao realizar a edição, favor contatar um administrador";
                redirect = false;
            }

            return Json(new
            {
                msg = msg,
                isRedirect = redirect
            });
        }
    }
}
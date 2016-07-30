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
    public class CampanhaController : Controller
    {
        // GET: Campanha
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListCampanhas()
        {
            CampanhaDAO campanhaDAO = new CampanhaDAO();
            HemocentroDAO hemocentroDAO = new HemocentroDAO();
            List<CampanhaModel> list = new List<CampanhaModel>();
            HemocentroModel hemocentroModel = new HemocentroModel();

            list = campanhaDAO.consultarCampanhas("  WHERE C.DATA_FIM >= " + UtilHelper.DateTimeParaSQLDate(DateTime.Now));

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Editar(int idCampanha)
        {
            CampanhaDAO campanhaDAO = new CampanhaDAO();
            CampanhaModel model = campanhaDAO.BuscaCampanha(" WHERE C.CODIGO_CAMPANHA = " + idCampanha);

            Session["NOME_CAMPANHA"] = model.NOME_CAMPANHA;

            return PartialView("_Editar",model);
        }

        [HttpPost]
        public ActionResult Editar(CampanhaModel model)
        {
            CampanhaDAO campanhaDAO = new CampanhaDAO();
            String msg = "Edição Realizada com sucesso";
            bool redirect = true;

            
            if (campanhaDAO.AlterarCampanha(model) != (int)SITUACAO.SUCESSO)
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
           
            String msg = "Campanha Cadastrada com sucesso";
            bool redirect = true;

            CampanhaDAO campanhaDAO = new CampanhaDAO();
            HemocentroDAO hemocentroDAO = new HemocentroDAO();
            HemocentroModel modelHemocentro = hemocentroDAO.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + Convert.ToInt32(Session["ID_HEMOCENTRO"].ToString()));
            DispositivoDAO ddao = new DispositivoDAO();

            model.CODIGO_HEMOCENTRO = modelHemocentro.CODIGO_HEMOCENTRO;
            model.NOME_USUARIO = modelHemocentro.NOME_HEMOCENTRO;

            if (model.NOME_RECEPTOR == null)
                model.NOME_RECEPTOR = "";
            else
                model.TIPO_SANGUINEO = "";

            var retorno = campanhaDAO.CadastrarCampanhaHemocentro(model);

            if (retorno == (int)SITUACAO.SUCESSO)
            {
                ddao.DispararNotificacao(model,true);
            }
            else
            {
                redirect = false;
                msg = "Erro a criar campanha, contate um administrador!";
            }


            return Json(new
            {
                msg = msg,
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
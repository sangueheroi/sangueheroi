using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SangueHeroiWeb.Helpers.Util_Helper;

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
            String msg = "Solicitação de Parceira realizada com sucesso!";
            bool redirect = true;

            if (model != null)
            {
                model.LOGIN_HEMOCENTRO = model.EMAIL_HEMOCENTRO.Split('@')[0];
                model.SENHA_HEMOCENTRO = Helpers.Util_Helper.GeraSenha.CriaSenha();
                model.CODIGO_STATUS = Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado;
               
                if (dao.ParceriaHemocentro(model))
                {
                    //Enviar email para admns, avisando que existe cadastro para ser validado
                    List<Helpers.Constantes_Helper.EmailAdministradorescs> list = new List<Helpers.Constantes_Helper.EmailAdministradorescs>();

                    foreach (var email in list)
                    {
                        var emailAdm = email.ToString().Split('|')[0];
                        var nomeAdm = email.ToString().Split('|')[1];

                        EmailHelper.EnviarEmailSolicitacaoPareriaHemocentro(emailAdm, nomeAdm, true);
                    }

                    //Envio email para o hemocentro falando que em 24h o cadastro sera validado

                    EmailHelper.EnviaEmailParaHemocentro(model.EMAIL_HEMOCENTRO, model.NOME_HEMOCENTRO, true);
                }
                else
                {
                    msg = "Erro ao Solicitar Parceria, contate os administradores do sistema!";
                    redirect = false;
                }
            }

            return Json(new
            {
                msg = msg,
                redirectUrl = "Index",
                isRedirect = redirect
            });
        }

        [HttpGet]
        public ActionResult Editar()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            //HemocentroModel model = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);

            HemocentroModel model = new HemocentroModel()
            {
                CODIGO_HEMOCENTRO = 1,
                NOME_HEMOCENTRO = "Hemocentro",
                CNPJ = "11111111",
                CEP = "111111",
                CIDADE_HEMOCENTRO = "São Paulo",
                ESTADO_HEMOCENTRO = "SP",
                TELEFONE_HEMOCENTRO = "11-11111111"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(HemocentroModel model)
        {
            HemocentroDAO hd = new HemocentroDAO();
            
            if(hd.Editar(model))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Inativar()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            //HemocentroModel model = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);

            HemocentroModel model = new HemocentroModel()
            {
                CODIGO_HEMOCENTRO = 1,
                NOME_HEMOCENTRO = "Hemocentro",
                CNPJ = "11111111",
                CEP = "111111",
                CIDADE_HEMOCENTRO = "São Paulo",
                ESTADO_HEMOCENTRO = "SP",
                TELEFONE_HEMOCENTRO = "11-11111111"
            };

            return View(model);
        }
    }
}
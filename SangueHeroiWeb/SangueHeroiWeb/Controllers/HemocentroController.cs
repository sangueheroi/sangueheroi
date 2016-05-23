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
                    List<String> list = Helpers.Constantes_Helper.EmailAdministradorescs.Email();

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
                redirectUrl = "Login/Index",
                isRedirect = redirect
            });
        }

        [HttpGet]
        public ActionResult Editar()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            HemocentroModel model = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(HemocentroModel model)
        {
            HemocentroDAO hd = new HemocentroDAO();
            String msg = "Edição Realizada com sucesso";
            
            if(!(hd.Editar(model)))
            {
                msg = "Atenção! Ocorreu um Erro ao realizar a edição, favor contatar um administrador";
            }

            ViewBag.Msg = msg;
            return View(model);
        }

        [HttpGet]
        public ActionResult Inativar()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];
            HemocentroDAO hd = new HemocentroDAO();

            if (idHemocentro == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                HemocentroModel model = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
                return PartialView("_Inativar", model);
            }
        }

        [HttpPost]
        public ActionResult InativarHemocentro()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            hd.Inativar(Convert.ToInt32(idHemocentro));

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
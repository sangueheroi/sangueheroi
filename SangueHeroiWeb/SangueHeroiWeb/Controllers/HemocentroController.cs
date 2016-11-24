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
            var dao = new HemocentroDAO();
            var msg = "Solicitação de Parceira realizada com sucesso!";
            var redirect = true;
            var enc = new Encrypt();

            if (model != null)
            {
                model.LOGIN_HEMOCENTRO = model.EMAIL.Split('@')[0];
                model.SENHA_HEMOCENTRO = enc.Encryption(GeraSenha.CriaSenha());
                model.CODIGO_STATUS = Constantes.CADASTRO_STATUS.Bloqueado;

                if (dao.ParceriaHemocentro(model) == Constantes.PARCERIA_HEMOCENTRO.SUCESSO)
                {
                    //Enviar email para admns, avisando que existe cadastro para ser validado
                    var listEmailAdm = Helpers.Constantes_Helper.EmailAdministradorescs.Email();
                    var body = "Atenção! Existe uma Parceria de um Hemocentro Para ser Validada";
                    const string subject = "Sangue Heroi - Solicitação de Parceria";

                    foreach (var email in listEmailAdm)
                    {
                        var emailAdm = email.Split('|')[0];
                        var nomeAdm = email.Split('|')[1];

                        EmailHelper.EnviarEmail(emailAdm, nomeAdm, body, subject, true);
                    }

                    //Envio email para o hemocentro falando que em 24h o cadastro sera validado
                    body =
                        "Atenção! Seu Cadastro Foi Enviado Para nossos Administradores, em até 24horas você receberá um login e uma senha!";
                    EmailHelper.EnviarEmail(model.EMAIL, model.NOME_HEMOCENTRO, body, subject, true);
                }
                else if (dao.ParceriaHemocentro(model) == Constantes.PARCERIA_HEMOCENTRO.USUARIO_EXISTENTE)
                {
                    msg = "Atenção! Já existe cadastro com esses dados, para solicitar sua senha, vá em login, depois em 'esqueci minha senha', caso encontre dificuldades entre em contato com um administrador";
                    redirect = false;
                }
                else if (dao.ParceriaHemocentro(model) == Constantes.PARCERIA_HEMOCENTRO.ERRO)
                {
                    msg = "Atenção! Ocorreu um Erro, Por gentileza entre em contato com os administradores do sistema";
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
            var enc = new Encrypt();

            HemocentroDAO hd = new HemocentroDAO();
            HemocentroModel model = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
            model.SENHA_HEMOCENTRO = enc.DecryptoRSA(model.SENHA_HEMOCENTRO);
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(HemocentroModel model)
        {
            HemocentroDAO hd = new HemocentroDAO();
            String msg = "Edição Realizada com sucesso";

            if (!(hd.Editar(model)))
            {
                msg = "Atenção! Ocorreu um Erro ao realizar a edição, favor contatar um administrador";
            }

            Session["NOME_HEMOCENTRO"] = model.NOME_HEMOCENTRO;

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

        [HttpGet]
        public ActionResult ConsultaNiveisSanguineos()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            HemocentroNiveisSanguineosModel model = hd.BuscaNiveisPorHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);

            ViewBag.model = model;

            return View(model);
        }

        [HttpGet]   
        public ActionResult EditarNiveisSanguineos()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            HemocentroModel hm = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
            HemocentroNiveisSanguineosModel model = hd.BuscaNiveisPorHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
           
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarNiveisSanguineos(HemocentroNiveisSanguineosModel model)
        {
            HemocentroDAO hd = new HemocentroDAO();
            String msg = "Alteração Realizada com Sucesso";

            if (model.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS != 0)
            {
                if (!(hd.EditarNiveisSanguineos(model)))
                {
                    msg = "Atenção! Ocorreu um Erro ao realizar a edição, favor contatar um administrador";
                }
            }

            model = hd.BuscaNiveisPorHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + model.CODIGO_HEMOCENTRO);

            ViewBag.Msg = msg;
            return View(model);
        }

        [HttpGet]
        public ActionResult CadastrarNiveisSanguineos()
        {
            var idHemocentro = Session["ID_HEMOCENTRO"];

            HemocentroDAO hd = new HemocentroDAO();
            HemocentroModel hm = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + idHemocentro);
            HemocentroNiveisSanguineosModel model = new HemocentroNiveisSanguineosModel();

            model.CODIGO_HEMOCENTRO = hm.CODIGO_HEMOCENTRO;
            model.NOME_HEMOCENTRO = hm.NOME_HEMOCENTRO;

            return View(model);
        }

        [HttpPost]
        public ActionResult CadastrarNiveisSanguineos(HemocentroNiveisSanguineosModel model)
        {
            HemocentroDAO hd = new HemocentroDAO();
            String msg = "Cadastro Realizado com Sucesso";

            if (model.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS == 0)
            {
                if (!(hd.CadastrarNiveisSanguineos(model)))
                {
                    msg = "Atenção! Ocorreu um Erro ao realizar o cadastro, favor contatar um administrador";
                }
            }
           

            ViewBag.Msg = msg;
            return View(model);
        }
    }
}
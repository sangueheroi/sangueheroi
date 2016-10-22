using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
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

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AtivarHemocentro(int IdHemocentro)
        {
            var hd = new HemocentroDAO();
            hd.AtivarHemocentro(IdHemocentro);
            var hemocentro = hd.BuscaHemocentro(" WHERE H.CODIGO_HEMOCENTRO = " + IdHemocentro);
            var enc = new Encrypt();

            string mensagem = $@"<div>
                                    <p><label> Atenção! Seu Cadastro Foi Ativado com sucesso, foi gerado um login e senha automaticos, recomendamos que a senha seja alterada.</label></p>
                                    <p><label>Usuario: {hemocentro.LOGIN_HEMOCENTRO} </label></p>
                                    <p><label>Senha: {enc.DecryptoRSA(hemocentro.SENHA_HEMOCENTRO)} </label></p>
                                </div>";

            using (var sr = new StreamReader(Server.MapPath("\\Views\\Shared\\") + "TemplateEmail.html"))
            {
                var body = sr.ReadToEnd();

                var subject = "Sangue Heroi - Solicitação de Parceria";

                EmailHelper.EnviarEmail(hemocentro.EMAIL, hemocentro.NOME_HEMOCENTRO, string.Format(body,mensagem), subject, true);

            }

            var list = hd.Lista(" WHERE H.CODIGO_STATUS = " + Constantes.CADASTRO_STATUS.Bloqueado);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FaleConosco(string txtSeuNome, string txtSeuEmail, string txtSuaMensagem)
        {
            var list = Helpers.Constantes_Helper.EmailAdministradorescs.Email();
            string mensagem = $@"<div>
                                    <p><label> Olá, um usuario enviou uma mensagem, verifique se algum administrador já respondeu o usuario, ou você mesmo pode responder!</label></p>
                                    <p><label>Usuario {txtSeuNome} </label></p>
                                    <p><label>Email usuario {txtSeuEmail} </label></p>
                                    <p><label>Mensagem do usuario - {txtSuaMensagem} </label></p>
                                </div>";

            using (var sr = new StreamReader(Server.MapPath("\\Views\\Shared\\") + "TemplateEmail.html"))
            {
                var body = sr.ReadToEnd();

                try
                {
                    foreach (var email in list)
                    {
                        var emailAdm = email.Split('|')[0];
                        var nomeAdm = email.Split('|')[1];
                        
                        EmailHelper.EnviarEmail(emailAdm, nomeAdm, string.Format(body, mensagem),"Sangue Herói - Fale Conosco", true);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            

            
            
            return null;
        }

    }
}
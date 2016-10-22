using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using SangueHeroiWeb.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class LoginDAO
    {
        ContextHelpers context;

        public LoginDAO()
        {
            context = new ContextHelpers();
        }

        public int VerificarLogin(LoginUsuarioModel model)
        {
            int loginOK = (int) SITUACAO.SUCESSO;

            var strQuery = $"SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{model.EMAIL_USUARIO}'";

            DataTable dt = new DataTable();

            dt =
                (DataTable)
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
                loginOK = (int) SITUACAO.SUCESSO;
            else
                loginOK = (int) SITUACAO.NAO_POSSUI_CADASTRO;

            return loginOK;
        }

        public string[] LogarUsuario(LoginUsuarioModel model)
        {
            Encrypt enc = new Encrypt();

            string[] usuario = new string[2];

            string nome = "";

            string senha_banco = "";

            int loginOK = (int) SITUACAO.SUCESSO;

            var strQuery = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt =
                (DataTable)
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        senha_banco = data["SENHA_USUARIO"].ToString();
                        nome = data["NOME_USUARIO"].ToString();

                        model.SENHA = enc.DecryptoRSA(model.SENHA);
                        senha_banco = enc.DecryptoRSA(senha_banco);

                        if (!model.SENHA.Equals(senha_banco))
                            loginOK = (int) SITUACAO.DADOS_INVALIDOS;
                    }
                }
                else
                {
                    nome = "";
                    loginOK = (int) SITUACAO.NAO_POSSUI_CADASTRO;
                }

                usuario[0] = nome;
                usuario[1] = loginOK.ToString();
            }
            catch (Exception exception)
            {
                usuario[0] = exception.Message;
                usuario[1] = exception.Message;
            }

            return usuario;
        }

        public int LogarHemocentro(HemocentroModel model)
        {
            var loginOk = (int) SITUACAO.SUCESSO;
            var hDao = new HemocentroDAO();
            var enc = new Encrypt();
            try
            {
                var hemocentro = hDao.BuscaHemocentro($" WHERE LOGIN_HEMOCENTRO = '{model.LOGIN_HEMOCENTRO}'");
                if (hemocentro != null)
                {
                    if (Convert.ToInt32(hemocentro.CODIGO_STATUS) != Constantes.CADASTRO_STATUS.Bloqueado)
                    {
                        

                        var senhaCadastrada = enc.DecryptoRSA(hemocentro.SENHA_HEMOCENTRO);
                        if (!senhaCadastrada.Equals(model.SENHA_HEMOCENTRO))
                            loginOk = (int)SITUACAO.DADOS_INVALIDOS;
                    }
                    else
                    {
                        loginOk = Convert.ToInt32(SITUACAO.CADASTRO_BLOQUEADO);
                    }
                }
                else
                {
                    loginOk = (int)SITUACAO.NAO_POSSUI_CADASTRO;
                }
            }
            catch (Exception)
            {
                loginOk = (int) SITUACAO.ERRO_DE_SISTEMA;
            }
           
            return loginOk;
        }

        public bool EsqueciMinhaSenha(string emailHemocentro)
        {
            var envioEmailOk = true;
            var hDao = new HemocentroDAO();
            var hemocentro = hDao.BuscaHemocentro($" WHERE H.EMAIL = '{emailHemocentro}'");
            var enc = new Encrypt();

            try
            {
                if (hemocentro != null)
                {
                    hemocentro.SENHA_HEMOCENTRO = enc.Encryption(GeraSenha.CriaSenha());
                    hDao.Editar(hemocentro);
                }
                else
                {
                    envioEmailOk = false;
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Mensagem : {0} " + ex.Message);
                envioEmailOk = false;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                envioEmailOk = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                envioEmailOk = false;
            }

            return envioEmailOk;
        }

    }
}
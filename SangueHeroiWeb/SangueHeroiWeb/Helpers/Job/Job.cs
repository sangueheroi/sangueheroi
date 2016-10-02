using Quartz;
using SangueHeroiWeb.DAO;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Helpers.Job
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            VerificaNiveisSanguineos();
            VerificaUltimaDataDoacaoUsuario();
        }

        private static void VerificaUltimaDataDoacaoUsuario()
        {
            var uDao = new UsuarioDAO();
            var dDao = new DoacaoDAO();

            var lstUsuario = uDao.consultarUsuarios();

            foreach (var usuario in lstUsuario)
            {
                
            }
        }

        private static void VerificaNiveisSanguineos()
        {
            var hDao = new HemocentroDAO();
            var lstHemocentros = hDao.Lista();

            foreach (var hemocentro in lstHemocentros)
            {
                var lstNiveisSanguineos = hDao.GetNiveisSanguineos($" WHERE HNS.CODIGO_HEMOCENTRO =  {hemocentro.CODIGO_HEMOCENTRO}");

                foreach (var tipoSanguineo in lstNiveisSanguineos)
                {
                    if (tipoSanguineo.VALOR_TIPO_SANGUINEO <= 10)
                    {
                        //Chama MEtodo de envio de notificacao
                    }
                }
            }
        }

    }
}
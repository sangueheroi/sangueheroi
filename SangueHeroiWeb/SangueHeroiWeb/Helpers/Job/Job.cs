using Newtonsoft.Json;
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
            var disDao = new DispositivoDAO();

            var lstUsuario = uDao.consultarUsuarios();

            foreach (var usuario in lstUsuario)
            {
                var diferenca = usuario.DATA_PROXIMA_DOACAO - DateTime.Now;

                //Se a diferenca for 0 significa que a data da proxima doacao é o próximo dia então envia a notificacao
                if (diferenca.Days == 0)
                {
                    string destinatarios = JsonConvert.SerializeObject(usuario.EMAIL_USUARIO, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                    disDao.DispararNotificacaoProximaDoacao(usuario.DATA_PROXIMA_DOACAO, "O dia de sua doação está próximo!", "Identificamos que a data que você pode realizar sua doação está bem próxima! Doe sangue, salve vidas!", destinatarios);
                }

            }
        }

        private static void VerificaNiveisSanguineos()
        {
            var hDao = new HemocentroDAO();
            var uDao = new UsuarioDAO();
            var dDao = new DispositivoDAO();

            var lstHemocentros = hDao.Lista();

            foreach (var hemocentro in lstHemocentros)
            {
                var lstNiveisSanguineos = hDao.GetNiveisSanguineos($" WHERE HNS.CODIGO_HEMOCENTRO =  {hemocentro.CODIGO_HEMOCENTRO}");

                foreach (var tipoSanguineo in lstNiveisSanguineos)
                {
                    if (tipoSanguineo.VALOR_TIPO_SANGUINEO <= 10)
                    {
                        var lstUsuarios = uDao.consultarEmailUsuarioPorTipoSanguineo(tipoSanguineo.NOME_TIPO_SANGUINEO);

                        string destinatarios = JsonConvert.SerializeObject(lstUsuarios, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                        dDao.DispararNotificacaoNiveisSanguineos(hemocentro.NOME_HEMOCENTRO, "Baixo nível sanguíneo!", "Identificamos que você possui o tipo sanguíneo que está abaixo do nível esperado neste hemocentro. Por favor, doe e salve vidas!", tipoSanguineo.NOME_TIPO_SANGUINEO, tipoSanguineo.VALOR_TIPO_SANGUINEO, destinatarios);
                    }
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class GrupoUsuarioModel
    {
        public int CODIGO_GRUPO { get; set; }

        public string NOME_GRUPO { get; set; }

        public string DESCRICAO_GRUPO { get; set; }

        public DateTime DATA_CRIACAO = DateTime.Now;

        public string EMAIL_USUARIO { get; set; }

        public List<UsuarioModel> INTEGRANTES { get; set; }

        public int QUANTIDADE_INTEGRANTES
        {
            get
            {
                return INTEGRANTES.Count();
            }
        }

        public string DATA_CRIACAO_STR { get { return DATA_CRIACAO.ToString("dd/MM/yyyy"); } }
    }
}
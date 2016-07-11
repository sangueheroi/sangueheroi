using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class CampanhaModel
    {
        public int CODIGO_CAMPANHA { get; set; }

        public string CODIGO_CAMPANHA_STR { get; set; }

        public string NOME_USUARIO { get; set; }

        public string EMAIL_USUARIO { get; set; }

        public string NOME_CAMPANHA { get; set; }

        public string DESCRICAO_CAMPANHA { get; set; }

        public string NOME_RECEPTOR { get; set; }

        public string TIPO_SANGUINEO { get; set; }

        public string NOME_HOSPITAL { get; set; }

        public string LOGRADOURO { get; set; }

        public string BAIRRO { get; set; }

        public string CIDADE { get; set; }

        public string ESTADO { get; set; }

        public string CEP { get; set; }

        public DateTime DATA_INICIO_DT { get; set; }

        public DateTime DATA_FIM_DT { get; set; }

        public string DATA_INICIO { get { return DATA_INICIO_DT.ToString("dd/MM/yyyy"); } }

        public string DATA_FIM { get { return DATA_FIM_DT.ToString("dd/MM/yyyy"); } }
    }
}
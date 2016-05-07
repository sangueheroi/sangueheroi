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

        public string NOME_CAMPANHA { get; set; }

        public string DESCRICAO_CAMPANHA { get; set; }

        public DateTime DATA_INICIO_DT { get; set; }

        public DateTime DATA_FIM_DT { get; set; }

        public string DATA_INICIO { get { return DATA_INICIO_DT.ToString("dd/MM/yy"); } }

        public string DATA_FIM { get { return DATA_FIM_DT.ToString("dd/MM/yy"); } }
    }
}
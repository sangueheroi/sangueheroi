using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class DoacaoModel
    {
        public int CODIGO_DOACAO { get; set; }

        public int CODIGO_USUARIO { get; set; }

        public DateTime DATA_DOACAO { get; set; }

        public string LOGRADOURO_ENDERECO_DOACAO { get; set; }

        public string CEP_ENDERECO_DOACAO { get; set; }
    }
}
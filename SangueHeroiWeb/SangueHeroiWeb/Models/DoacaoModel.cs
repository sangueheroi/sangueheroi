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

        public string NOME_USUARIO { get; set; }

        public string EMAIL_USUARIO { get; set; }

        public DateTime DATA_DOACAO_DT { get; set; }

        public string LOGRADOURO_ENDERECO_DOACAO { get; set; }

        public string CEP_ENDERECO_DOACAO { get; set; }

        public string TIPO_SANGUINEO { get; set; }

        public string NOME_HEMOCENTRO { get; set; }

        public int QTD_DOACOES { get; set; }

        public int PONTUACAO { get; set; }

        public int QTD_VIDAS_SALVAS { get; set; }

        public string DATA_DOACAO { get { return DATA_DOACAO_DT.ToString("dd/MM/yy"); } }
    }
}
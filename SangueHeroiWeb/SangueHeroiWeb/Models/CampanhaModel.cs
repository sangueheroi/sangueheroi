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
        [Required]
        [Display(Name = "Código Campanha")]
        public int CODIGO_CAMPANHA { get; set; }

        public string CODIGO_CAMPANHA_STR { get; set; }

        [Required]
        [Display(Name = "Nome Usuário")]
        public string NOME_USUARIO { get; set; }

        [Required]
        [Display(Name = "E-mail Usuário")]
        public string EMAIL_USUARIO { get; set; }

        [Required]
        [Display(Name = "Nome Campanha")]
        public string NOME_CAMPANHA { get; set; }

        [Required]
        [Display(Name = "Descrição Campanha")]
        public string DESCRICAO_CAMPANHA { get; set; }

        [Required]
        [Display(Name = "Nome Receptor")]
        public string NOME_RECEPTOR { get; set; }

        [Required]
        [Display(Name = "Tipo Sanguíneo")]
        public string TIPO_SANGUINEO { get; set; }

        [Required]
        [Display(Name = "Nome Hospital")]
        public string NOME_HOSPITAL { get; set; }

        [Required]
        [Display(Name = "Logradouro")]
        public string LOGRADOURO { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string BAIRRO { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string CIDADE { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string ESTADO { get; set; }

        [Required]
        [Display(Name = "Cep")]
        public string CEP { get; set; }

        [Required]
        [Display(Name = "Data Inicio")]
        public DateTime DATA_INICIO_DT { get; set; }

        [Required]
        [Display(Name = "Data Fim")]
        public DateTime DATA_FIM_DT { get; set; }

        public string DATA_INICIO { get { return DATA_INICIO_DT.ToString("dd/MM/yyyy"); } }

        public string DATA_FIM { get { return DATA_FIM_DT.ToString("dd/MM/yyyy"); } }

        public int BTN_EXCLUI { get; set; }

        public int CODIGO_HEMOCENTRO { get; set; }

        public List<GrupoUsuarioModel> DESTINATARIOS { get; set; }

    }
}
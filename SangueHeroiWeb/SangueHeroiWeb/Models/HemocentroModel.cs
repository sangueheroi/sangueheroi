using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class HemocentroModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CODIGO_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public String NOME_HEMOCENTRO { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [Display(Name = "CNPJ")]
        public String CNPJ { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        public String RAZAO_SOCIAL { get; set; }

        [Display(Name = "CEP")]
        public String CEP { get; set; }

        [Display(Name = "Cidade")]
        public String CIDADE_HEMOCENTRO { get; set; }

        [Display(Name = "Estado")]
        public String ESTADO_HEMOCENTRO { get; set; }

        [Display(Name = "Telefone")]
        public String TELEFONE_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public String EMAIL_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "Período de Funcionamento")]
        public String PERIODO_FUNCIONAMENTO_HEMOCENTRO { get; set; }
        
        public int CODIGO_STATUS { get; set; }

        [Required]
        [Display(Name = "Login")]
        public String LOGIN_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string SENHA_HEMOCENTRO { get; set; }

        public DateTime DATA_CRIACAO = DateTime.Now;

        public List<String> ListaEstados = new List<String>() { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        public int TIPO_PERFIL_HEMOCENTRO { get; set; }
    }
}
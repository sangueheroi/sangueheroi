using SangueHeroiWeb.Helpers.Util_Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace SangueHeroiWeb.Models
{
    public class UsuarioModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CODIGO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string NOME_USUARIO { get; set; }

        [Required]
        [Display(Name = "Documento")]
        public string DOCUMENTO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Status")]
        public static bool STATUS_USUARIO { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} Deve Conter ao Menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string SENHA_USUARIO { get; set; }

        public DateTime DATA_CRIACAO = DateTime.Now;

        [Required]
        [Display(Name = "Sobrenome")]
        public string SOBRENOME_USUARIO { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EMAIL_USUARIO { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string LOGRADOURO { get; set; }

        [Required]
        [Display(Name = "Nº")]
        public string NUMERO { get; set; }

        [Required]
        [Display(Name = "Complemento")]
        public string COMPLEMENTO { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string BAIRRO { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string CIDADE { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string ESTADO { get; set; }

        public List<String> ListaEstados = new List<String>() { "Acre", "Alagoa", "Amápa", "Amazonas", "Bahia", "Ceará", "Distrito Federal", "Espírito Santo", "Goiás", "Maranhão", "Mato Grosso do Sul", "Minas Gerais", "Pará", "Paraíba", "Paraná", "Pernambuco", "Piauí", "Rio de Janeiro", "Rio Grande do Norte", "Rio Grande do Sul", "Rondônia", "Roraima", "Santa Catarina", "São Paulo", "Sergipe", "Tocantins" };

        [Required]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Required]
        [Display(Name = "Tipo Sanguíneo")]
        public string TIPO_SANGUINEO { get; set; }

        
        [Required]
        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_NASCIMENTO { get; set; }

        [Required]
        [Display(Name = "Data Ultima DoaCão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_ULTIMA_DOACAO { get; set; }

        public List<String> ListaTipoSanguineo = new List<String>() { "A+", "A-", "AB+", "AB-", "B+", "B-", "O-", "O+" };

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("SENHA_USUARIO", ErrorMessage = "Erro! Digite a senha Corretamente")]
        public string ConfirmaSenha { get; set; }

        public List<UsuarioHeroiModel> ListaHerois = new List<UsuarioHeroiModel>();

        public int CODIGO_HEROI { get; set; }

    }

    public class UsuarioHeroiModel
    {
        public string NOME_HEROI { get; set; }
        public string CARACTERISTICA_HEROI { get; set; }
        public string DESCRICAO_HEROI { get; set; }
        public int CODIGO_HEROI { get; set; }
    }
}
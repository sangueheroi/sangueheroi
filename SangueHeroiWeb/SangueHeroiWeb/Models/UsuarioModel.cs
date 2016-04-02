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

        public int CODIGO_STATUS = UtilHelper.BoolParaInt(STATUS_USUARIO);

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
        public string RUA_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Nº")]
        public string NUMERO_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string BAIRRO_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string CIDADE_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string ESTADO_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public string CEP_ENDEREÇO_USUARIO { get; set; }

        [Required]
        [Display(Name = "Tipo Sanguíneo")]
        public string TIPO_SANGUINEO { get; set; }

        
        [Required]
        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_NASCIMENTO { get; set; }

        [Required]
        [Display(Name = "Data Ultima Doação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_ULTIMA_DOACAO { get; set; }

        public List<String> ListaTipoSanguineo = new List<String>() { "A+", "A-", "AB+", "AB-", "B+", "B-", "O-", "O+" };

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("SENHA_USUARIO", ErrorMessage = "Erro! Digite a senha Corretamente")]
        public string ConfirmaSenha { get; set; }
        
    }
}
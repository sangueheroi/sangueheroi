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
        public int ID_USUARIO { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string NOME_USUARIO { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        public string SOBRENOME_USUARIO { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string ENDEREÇO_USUARIO { get; set; }

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
        [Display(Name = "E-mail")]
        public string EMAIL_USUARIO { get; set; }

        [Required]
        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_NASCIMENTO { get; set; }

        [Required]
        [Display(Name = "Data Ultima Doação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DATA_ULTIMA_DOACAO { get; set; }


    }
}
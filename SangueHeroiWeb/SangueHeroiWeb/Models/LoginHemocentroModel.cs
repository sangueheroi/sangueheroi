using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class LoginHemocentroModel
    {
        [Required]
        [Display(Name = "Login")]
        public string LOGIN_HEMOCENTRO { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string SENHA { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool LEMBRAR_ME { get; set; }

    }

    public class EsqueciMinhaSenhaModel
    {
        [Required]
        [Display(Name = "Email")]
        public string EMAIL { get; set; }
    }
}
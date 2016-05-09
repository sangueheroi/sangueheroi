using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class LoginUsuarioModel
    {
        public string EMAIL_USUARIO { get; set; }
        public string SENHA { get; set; }

       
        public bool LEMBRAR_ME { get; set; }
    }
}
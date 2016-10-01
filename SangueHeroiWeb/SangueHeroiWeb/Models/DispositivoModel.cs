using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class DispositivoModel
    {
        public int CODIGO_DISPOSITIVO { get; set; }

        public string EMAIL_USUARIO { get; set; }

        public string TOKEN { get; set; }
    }
}
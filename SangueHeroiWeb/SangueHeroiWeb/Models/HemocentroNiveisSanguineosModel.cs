using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Models
{
    public class HemocentroNiveisSanguineosModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS { get; set; }

        [Required]
        [Display(Name = "Código Hemocentro")]
        public int CODIGO_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "Nome Hemocentro")]
        public String NOME_HEMOCENTRO { get; set; }

        [Required]
        [Display(Name = "O-")]
        public int On { get; set; }

        [Required]
        [Display(Name = "O+")]
        public int Op { get; set; }

        [Required]
        [Display(Name = "A-")]
        public int An { get; set; }

        [Required]
        [Display(Name = "A+")]
        public int Ap { get; set; }

        [Required]
        [Display(Name = "B-")]
        public int Bn { get; set; }

        [Required]
        [Display(Name = "B+")]
        public int Bp { get; set; }

        [Required]
        [Display(Name = "AB-")]
        public int ABn { get; set; }

        [Required]
        [Display(Name = "AB+")]
        public int ABp { get; set; }

        public HemocentroNiveisSanguineosModelGrafico grafico { get; set; }
    }

    public class HemocentroNiveisSanguineosModelGrafico
    {
        public String NOME_TIPO_SANGUINEO { get; set; }
        public int VALOR_TIPO_SANGUINEO { get; set; }
        public string NOME_HEMOCENTRO { get; set; }
    }
}
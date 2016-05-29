using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Helpers.Constantes_Helper
{
    public class TiposSanguineos
    {
        public int CODIGO_TIPO_SANGUINEO { get; set; }
        public String NOME_TIPO_SANGUINEO { get; set; }

        public static List<TiposSanguineos> GetTiposSanguineos()
        {
            List<TiposSanguineos> list = new List<TiposSanguineos>();
            TiposSanguineos On = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 1, NOME_TIPO_SANGUINEO = "O-" };
            TiposSanguineos Op = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 2, NOME_TIPO_SANGUINEO = "O+" };
            TiposSanguineos An = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 3, NOME_TIPO_SANGUINEO = "A-" };
            TiposSanguineos Ap = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 4, NOME_TIPO_SANGUINEO = "A+" };
            TiposSanguineos Bn = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 5, NOME_TIPO_SANGUINEO = "B-" };
            TiposSanguineos Bp = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 6, NOME_TIPO_SANGUINEO = "B+" };
            TiposSanguineos ABp = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 7, NOME_TIPO_SANGUINEO = "AB+" };
            TiposSanguineos ABn = new TiposSanguineos() { CODIGO_TIPO_SANGUINEO = 8, NOME_TIPO_SANGUINEO = "AB-" };

            list.Add(On);
            list.Add(Op);
            list.Add(An);
            list.Add(Ap);
            list.Add(Bn);
            list.Add(Bp);
            list.Add(ABp);
            list.Add(ABn);

            return list;
        }
    }
}
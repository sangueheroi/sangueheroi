using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class UtilHelper
    {
        public static string QuotedText(string pTexto)
        {
            string Result = "";

            Result = pTexto.Replace("'", "''");

            return "'" + Result + "'";
        }
        
        public static string QuotedStr(string pStr)
        {
            string Result = "";
            for (int vIndex = 0; vIndex <= pStr.Length - 1; vIndex++)
            {
                if (pStr[vIndex].ToString() == "'")
                    Result = Result + "''";
                else
                    Result = Result + pStr[vIndex].ToString();
            }
            return "'" + Result + "'";
        }

        public static string DateTimeParaSQLDate(DateTime dt)
        {
            return "'" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "'";

        }

        public static string DateTimeParaBrDate(DateTime dt)
        {
            return String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt);
        }

        public static int BoolParaInt(bool _bool)
        {
            int retorno = 1;

            if (!_bool)
                retorno = 2;

            return retorno;
        }

        
    }
}
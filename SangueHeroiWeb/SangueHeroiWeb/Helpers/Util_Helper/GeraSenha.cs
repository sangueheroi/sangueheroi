using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class GeraSenha
    {
        private const string SenhaCaracteresValidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVXYZ1234567890@#!?";

        public static string CriaSenha()
        {

            int tamanho = 8;
            
            int valormaximo = SenhaCaracteresValidos.Length;
            
            Random random = new Random(DateTime.Now.Millisecond);
            
            StringBuilder senha = new StringBuilder(tamanho);
            
            for (int i = 0; i < tamanho; i++)
                senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);

            return senha.ToString();
        }
    }
}
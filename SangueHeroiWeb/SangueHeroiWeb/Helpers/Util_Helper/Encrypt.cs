using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class Encrypt
    {
        public static String Encrypto(string value)
        {
            MD5 md5 = MD5.Create();

            value = value + Constantes.PALAVRA_CRIPTOGRAFIA;

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);

            byte[] hash = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();

        }
    }
}
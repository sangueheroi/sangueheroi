﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Diagnostics;
using System.Text;
using SangueHeroiWeb.DAO;
using System.Data;
using SangueHeroiWeb.Helpers.Util_Helper;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class Encrypt
    {
        ContextHelpers context;

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

        private string[] ConsultarChaves()
        {
            context = new ContextHelpers();

            string[] chaves = new string[2];

            string cv_publica = "";
            string cv_privada = "";

            var strQuery = String.Format("SELECT CV_PUBLICA, CV_PRIVADA FROM AUTENTICACAO");

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    cv_publica = data["CV_PUBLICA"].ToString();
                    cv_privada = data["CV_PRIVADA"].ToString();
                }
            }
            else
            {
                cv_publica = "";
                cv_privada = "";
            }

            chaves[0] = cv_publica;
            chaves[1] = cv_privada;

            return chaves;
        }

        public string DecryptoRSA(string valor)
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "Tracker";

            CspParameters cspParams;
            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider(cspParams);

            String[] chaves = ConsultarChaves();

            rsa1.FromXmlString(chaves[1]);

            string data2Decrypt = valor;

            byte[] encryptedBytes = Convert.FromBase64String(data2Decrypt);

            byte[] plain = rsa1.Decrypt(encryptedBytes, false);
            string decryptedString = System.Text.Encoding.UTF8.GetString(plain);

            return decryptedString;
        }

        public string GetTokenAutenticacao(System.ServiceModel.OperationContext operationContext)
        {
            string token = null;

            // Look at headers on incoming message.
            for (int i = 0; i < System.ServiceModel.OperationContext.Current.IncomingMessageHeaders.Count; i++)
            {
                MessageHeaderInfo h = System.ServiceModel.OperationContext.Current.IncomingMessageHeaders[i];

                // For any reference parameters with the correct name.
                if (h.Name == "Autenticacao")
                { 
                    // Read the value of that header. 
                    XmlReader xr = System.ServiceModel.OperationContext.Current.IncomingMessageHeaders.GetReaderAtHeader(i);
                    //apiKey = xr.ReadElementContentAsString(); 
                    xr.ReadToFollowing("Autenticacao"); 
                    token = xr.ReadElementContentAsString();
                }
            }

            // Return the API key (if present, null if not).
            return token;
        }

        public bool CompararToken(string token_externo)
        {
            context = new ContextHelpers();

            token_externo = DecryptoRSA(token_externo);

            var strQuery = String.Format("SELECT CODIGO_AUTENTICACAO FROM AUTENTICACAO WHERE PWDCOMPARE('{0}', TOKEN) = 1", token_externo);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }

}
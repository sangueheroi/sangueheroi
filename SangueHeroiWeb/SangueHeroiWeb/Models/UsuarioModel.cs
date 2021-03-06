﻿using SangueHeroiWeb.Helpers.Util_Helper;
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
        public int CODIGO_USUARIO { get; set; }

        public string NOME_USUARIO { get; set; }

        public static bool STATUS_USUARIO { get; set; }

        public string SENHA_USUARIO { get; set; }

        public DateTime DATA_CRIACAO = DateTime.Now;

        public string EMAIL_USUARIO { get; set; }

        public string SEXO { get; set; }

        public string BAIRRO { get; set; }

        public string CIDADE { get; set; }

        public string ESTADO { get; set; }

        public string CEP { get; set; }

        public string TIPO_SANGUINEO { get; set; }

        public DateTime DATA_NASCIMENTO { get; set; }

        public DateTime DATA_ULTIMA_DOACAO { get; set; }

        public DateTime DATA_PROXIMA_DOACAO { get; set; }

        public int CODIGO_HEROI { get; set; }

        public bool FLAG_CADASTRO_REDE_SOCIAL { get; set; }

        public List<UsuarioModel> DESTINATARIOS { get; set; }

        public string DATA_CRIACAO_STR => $"{DATA_CRIACAO.Day}-{DATA_CRIACAO.Month}-{DATA_CRIACAO.Year}";
        public string DATA_NASCIMENTO_STR => $"{DATA_NASCIMENTO.Day}-{DATA_NASCIMENTO.Month}-{DATA_NASCIMENTO.Year}";
        public string DATA_ULTIMA_DOACAO_STR => $"{DATA_ULTIMA_DOACAO.Day}-{DATA_ULTIMA_DOACAO.Month}-{DATA_ULTIMA_DOACAO.Year}";
        public string DATA_PROXIMA_DOACAO_STR => $"{DATA_PROXIMA_DOACAO.Day}-{DATA_PROXIMA_DOACAO.Month}-{DATA_PROXIMA_DOACAO.Year}";
    }

    public class UsuarioHeroiModel
    {
        public string NOME_HEROI { get; set; }
        public string CARACTERISTICA_HEROI { get; set; }
        public string DESCRICAO_HEROI { get; set; }
        public int CODIGO_HEROI { get; set; }
    }

    public class UsuarioGrupoModel
    {
        public int CODIGO_GRUPO { get; set; }

        public string NOME_GRUPO { get; set; }

        public string DESCRICAO_GRUPO { get; set; }

        public DateTime DATA_CRIACAO = DateTime.Now;

        public string EMAIL_USUARIO { get; set; }

        public List<UsuarioModel> INTEGRANTES { get; set; }

    }


}
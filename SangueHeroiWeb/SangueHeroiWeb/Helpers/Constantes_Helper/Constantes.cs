using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Helpers.Util_Helper
{
    public class Constantes
    {
        public const string PALAVRA_CRIPTOGRAFIA = "usjtsangueheroi";


        public class HeroiUsuario
        {
            public const int HEROI_AMARELO = 1;
            public const int HEROI_AZUL = 2;
            public const int HEROI_LARANJA = 3;
            public const int HEROI_ROXO = 4;
            public const int HEROI_VERDE = 5;
            public const int HEROI_VERMELHO = 6;
        }

        public class CADASTRO_STATUS
        {
            public const int Ativo = 1;
            public const int Bloqueado = 2;
            public const int Excluido = 3;
        }

    }
}
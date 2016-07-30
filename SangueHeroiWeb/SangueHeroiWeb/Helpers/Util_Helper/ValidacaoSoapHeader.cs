using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;

namespace SangueHeroiWeb
{
    /// <summary>
    /// Credencial de acesso aos métodos do Webservice
    /// </summary>
    public class ValidacaoSoapHeader : SoapHeader
    {
        private string _token;
        public ValidacaoSoapHeader()
        {
        }
        public ValidacaoSoapHeader(string token)
        {
            this._token = token;
        }
        public string token
        {
            get { return this._token; }
            set { this._token = value; }
        }
    }
}


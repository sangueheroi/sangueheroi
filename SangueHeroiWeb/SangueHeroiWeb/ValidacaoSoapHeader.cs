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
        private string _devToken;
        public ValidacaoSoapHeader()
        {
        }
        public ValidacaoSoapHeader(string devToken)
        {
            this._devToken = devToken;
        }
        public string DevToken
        {
            get { return this._devToken; }
            set { this._devToken = value; }
        }
    }
}


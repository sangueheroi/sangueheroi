using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SangueHeroiWeb.DAO
{
    public class CampanhaDAO
    {
        ContextHelpers context;
        protected SmtpClient SmtpClient { get; set; }
        protected MailMessage MailMessage { get; set; }

        public CampanhaDAO()
        {
            context = new ContextHelpers();
        }

        public int CadastrarCampanha(CampanhaModel cmodel, UsuarioModel umodel, string destinatario)
        {
            int cadastroOK = (int)SITUACAO.DADOS_INVALIDOS;

            string strQueryInsert = "";
            string strQueryInsert2 = "";

            string strQueryConsultaCodigo = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", umodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                int codigo_usuario = 0;


                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryInsert = "EXECUTE frmCadastrarCampanha " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.DESCRICAO_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_RECEPTOR) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_INICIO_DT) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_FIM_DT) + " , " + Environment.NewLine
                 + codigo_usuario + " , " + Environment.NewLine
                 + 1 + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_HOSPITAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.LOGRADOURO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.BAIRRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.ESTADO) + " ; ";

                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    cadastroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

                string strQueryConsultaCodigoCampanha = String.Format("SELECT CODIGO_CAMPANHA FROM CAMPANHA WHERE NOME_CAMPANHA = '{0}'", cmodel.NOME_CAMPANHA);

                DataTable dt2 = new DataTable();

                dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt2.Rows.Count != 0)
                {
                    int codigo_campanha = 0;

                    foreach (DataRow data in dt2.Rows)
                        codigo_campanha = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());

                    GrupoUsuarioModel destinatarios = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<GrupoUsuarioModel>(destinatario);

                    foreach (var item in destinatarios.DESTINATARIOS)
                    {
                        string strQueryConsultaCodigoGrupo = String.Format("SELECT CODIGO_GRUPO FROM GRUPO WHERE CODIGO_GRUPO = '{0}'", item.CODIGO_GRUPO);

                        DataTable dt3 = new DataTable();

                        dt3 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoGrupo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                        if (dt3.Rows.Count != 0)
                        {
                            int codigo_grupo = 0;

                            foreach (DataRow data in dt3.Rows)
                                codigo_grupo = Convert.ToInt32(data["CODIGO_GRUPO"].ToString());

                            strQueryInsert2 = "INSERT INTO CAMPANHA_GRUPO (CODIGO_GRUPO, CODIGO_CAMPANHA) VALUES (" + codigo_grupo + ", " + codigo_campanha + ");";

                            try
                            {
                                var b = context.ExecuteCommand(strQueryInsert2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                                cadastroOK = (int)SITUACAO.SUCESSO;
                            }
                            catch (Exception)
                            {
                                cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                            }
                        }
                        else if (dt3.Rows.Count == 0)
                        {
                            cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
                        }
                    }
                }
                else if (dt2.Rows.Count == 0)
                {
                    cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
                }           
            }
            else if (dt.Rows.Count == 0)
            {
                cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return cadastroOK;
        }

        public int CadastrarCampanhaHemocentro(CampanhaModel model)
        {
            int cadastroOK = (int)SITUACAO.SUCESSO;

            string strQueryInsert = "";

            strQueryInsert = "EXECUTE frmCadastrarCampanha " + Environment.NewLine
             + UtilHelper.TextForSql(model.NOME_CAMPANHA) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.DESCRICAO_CAMPANHA) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.NOME_RECEPTOR) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
             + UtilHelper.DateTimeParaSQLDate(model.DATA_INICIO_DT) + " , " + Environment.NewLine
             + UtilHelper.DateTimeParaSQLDate(model.DATA_FIM_DT) + " , " + Environment.NewLine
             + 143 + " , " + Environment.NewLine
             + model.CODIGO_HEMOCENTRO + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.NOME_HOSPITAL) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.LOGRADOURO) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.BAIRRO) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
             + UtilHelper.TextForSql(model.ESTADO) + " ; ";

            try
            {
                context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception)
            {
                cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
            }

            return cadastroOK;
        }

        public int AlterarCampanha(CampanhaModel cmodel, string destinatario)
        {
            int alteracaoOK = (int)SITUACAO.DADOS_INVALIDOS;

            string strQueryUpdate = "";
            string strQueryInsert = "";
            string strQueryDelete = "";
            int codigo_campanha = 0;

            string strQueryConsultaCampanha = String.Format("SELECT * FROM CAMPANHA C INNER JOIN CAMPANHA_ENDERECO CE ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA WHERE C.CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

            DataTable dt = new DataTable();
            DataTable dt4 = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    if (cmodel.NOME_CAMPANHA == "")
                        cmodel.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    if (cmodel.DESCRICAO_CAMPANHA == "")
                        cmodel.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();
                    if (string.IsNullOrEmpty(cmodel.NOME_RECEPTOR))
                        cmodel.NOME_RECEPTOR = data["NOME_RECEPTOR"].ToString();
                    if (string.IsNullOrEmpty(cmodel.TIPO_SANGUINEO))
                        cmodel.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                    if (cmodel.DATA_INICIO_DT == null)
                        cmodel.DATA_INICIO_DT = Convert.ToDateTime(data["DATA_INICIO"].ToString());
                    if (cmodel.DATA_FIM_DT == null)
                        cmodel.DATA_FIM_DT = Convert.ToDateTime(data["DATA_FIM"].ToString());
                    if (cmodel.NOME_HOSPITAL == "")
                        cmodel.NOME_HOSPITAL = data["NOME_HOSPITAL"].ToString();
                    if (cmodel.LOGRADOURO == "")
                        cmodel.LOGRADOURO = data["LOGRADOURO"].ToString();
                    if (cmodel.BAIRRO == "")
                        cmodel.BAIRRO = data["BAIRRO"].ToString();
                    if (cmodel.CIDADE == "")
                        cmodel.CIDADE = data["CIDADE"].ToString();
                    if (cmodel.ESTADO == "")
                        cmodel.ESTADO = data["ESTADO"].ToString();
                    if (cmodel.CEP == "")
                        cmodel.CEP = data["CEP"].ToString();
                }

                strQueryUpdate = "EXECUTE frmAtualizarCampanha " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.DESCRICAO_CAMPANHA) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_RECEPTOR) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_INICIO_DT) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(cmodel.DATA_FIM_DT) + " , " + Environment.NewLine
                 + cmodel.CODIGO_CAMPANHA + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.NOME_HOSPITAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.LOGRADOURO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.BAIRRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(cmodel.ESTADO) + ";";

                strQueryDelete = String.Format("DELETE CG FROM CAMPANHA_GRUPO CG INNER JOIN CAMPANHA C ON CG.CODIGO_CAMPANHA = C.CODIGO_CAMPANHA WHERE C.NOME_CAMPANHA = '{0}'", cmodel.NOME_CAMPANHA);

                try
                {
                    var c = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    alteracaoOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    alteracaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

                GrupoUsuarioModel destinatarios = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<GrupoUsuarioModel>(destinatario);

                foreach (var item in destinatarios.DESTINATARIOS)
                {
                    string strQueryConsultaExistenciaCampanha = String.Format("SELECT CG.CODIGO_CAMPANHA FROM CAMPANHA C INNER JOIN CAMPANHA_GRUPO CG ON CG.CODIGO_CAMPANHA = C.CODIGO_CAMPANHA WHERE CG.CODIGO_GRUPO = {0} AND C.NOME_CAMPANHA = '{1}'", item.CODIGO_GRUPO, cmodel.NOME_CAMPANHA);
                    string strQueryConsultaCodigoCampanha = String.Format("SELECT C.CODIGO_CAMPANHA FROM CAMPANHA C WHERE C.NOME_CAMPANHA = '{0}'", cmodel.NOME_CAMPANHA);

                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();

                    dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaExistenciaCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
                    dt3 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoCampanha, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count == 0)
                    {
                        foreach (DataRow data in dt3.Rows)
                            codigo_campanha = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());

                        strQueryInsert = "INSERT INTO CAMPANHA_GRUPO (CODIGO_GRUPO, CODIGO_CAMPANHA) VALUES (" + item.CODIGO_GRUPO + ", " + codigo_campanha + ");";

                        try
                        {
                            var b = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                            alteracaoOK = (int)SITUACAO.SUCESSO;
                        }
                        catch (Exception)
                        {
                            alteracaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                        }
                    }
                    else if (dt2.Rows.Count != 0)
                    {
                        alteracaoOK = (int)SITUACAO.JA_POSSUI_CADASTRO;
                    }
                }
            }
            else if (dt.Rows.Count == 0)
            {
                alteracaoOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return alteracaoOK;
        }

        public List<CampanhaModel> consultarCampanhas(String where = "")
        {
            string strQuery = "";
            string strQuerySelect2 = "";

            strQuery = String.Format("  SELECT U.NOME_USUARIO, " + Environment.NewLine
                                    + " U.EMAIL_USUARIO, C.CODIGO_CAMPANHA," + Environment.NewLine
                                    + " C.NOME_CAMPANHA, C.DESCRICAO_CAMPANHA, " + Environment.NewLine
                                    + " C.NOME_RECEPTOR, C.TIPO_SANGUINEO, C.DATA_INICIO, " + Environment.NewLine
                                    + " C.DATA_FIM, CE.NOME_HOSPITAL, CE.LOGRADOURO, CE.BAIRRO, " + Environment.NewLine
                                    + " CE.CIDADE, CE.ESTADO, CE.CEP, C.CODIGO_HEMOCENTRO, H.NOME_HEMOCENTRO, H.EMAIL " + Environment.NewLine
                                    + " FROM CAMPANHA C " + Environment.NewLine
                                    + " INNER JOIN CAMPANHA_ENDERECO CE ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA " + Environment.NewLine
                                    + " INNER JOIN HEMOCENTRO H ON H.CODIGO_HEMOCENTRO = C.CODIGO_HEMOCENTRO " + Environment.NewLine
                                    + " INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO ");

            if (where.Count() > 0)
            {
                strQuery += where;
            }

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            List<CampanhaModel> list = new List<CampanhaModel>();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    CampanhaModel campanha = new CampanhaModel();
                    List<GrupoUsuarioModel> lista_grupos = new List<GrupoUsuarioModel>();

                    campanha.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                    campanha.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    campanha.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();

                    if(Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString()) > 1)
                    {
                        campanha.NOME_USUARIO = data["NOME_HEMOCENTRO"].ToString();
                        campanha.EMAIL_USUARIO = data["EMAIL"].ToString();
                    }
                    else
                    {
                        campanha.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                        campanha.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();
                    }
                    campanha.NOME_RECEPTOR = data["NOME_RECEPTOR"].ToString();
                    campanha.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                    campanha.NOME_HOSPITAL = data["NOME_HOSPITAL"].ToString();
                    campanha.LOGRADOURO = data["LOGRADOURO"].ToString();
                    campanha.BAIRRO = data["BAIRRO"].ToString();
                    campanha.CIDADE = data["CIDADE"].ToString();
                    campanha.CEP = data["CEP"].ToString();
                    campanha.ESTADO = data["ESTADO"].ToString();
                    campanha.DATA_INICIO_DT = Convert.ToDateTime(data["DATA_INICIO"].ToString());
                    campanha.DATA_FIM_DT = Convert.ToDateTime(data["DATA_FIM"].ToString());
                    campanha.CODIGO_HEMOCENTRO = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());

                    strQuerySelect2 = String.Format("SELECT CG.CODIGO_GRUPO, G.NOME_GRUPO FROM CAMPANHA C INNER JOIN CAMPANHA_GRUPO CG ON C.CODIGO_CAMPANHA = CG.CODIGO_CAMPANHA INNER JOIN GRUPO G ON G.CODIGO_GRUPO = CG.CODIGO_GRUPO WHERE C.NOME_CAMPANHA = '{0}'", campanha.NOME_CAMPANHA);
                    dt2 = (DataTable)context.ExecuteCommand(strQuerySelect2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow data2 in dt2.Rows)
                        {
                            GrupoUsuarioModel grupo = new GrupoUsuarioModel();
                            List<UsuarioModel> usuarios = new List<UsuarioModel>();

                            grupo.CODIGO_GRUPO = Convert.ToInt32(data2["CODIGO_GRUPO"].ToString());
                            grupo.NOME_GRUPO = data2["NOME_GRUPO"].ToString();
                            grupo.INTEGRANTES = usuarios;
                            grupo.DATA_CRIACAO = Convert.ToDateTime(null);
                            lista_grupos.Add(grupo);
                        }
                    }

                    campanha.DESTINATARIOS = lista_grupos; 
                    list.Add(campanha);

                }
            }

            return list;
        }

        public CampanhaModel BuscaCampanha(String where = "")
        {
            return consultarCampanhas(where).FirstOrDefault();
        }

        public List<CampanhaModel> getMinhasCampanhas(string email)
        {
            string strQuery = "";
            string strQuerySelect2 = "";

            strQuery = String.Format("SELECT U.NOME_USUARIO, U.EMAIL_USUARIO, C.CODIGO_CAMPANHA, C.NOME_CAMPANHA, C.DESCRICAO_CAMPANHA, C.NOME_RECEPTOR, C.TIPO_SANGUINEO, C.DATA_INICIO, C.DATA_FIM, CE.NOME_HOSPITAL, CE.LOGRADOURO, CE.BAIRRO, CE.CIDADE, CE.ESTADO, CE.CEP FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO INNER JOIN CAMPANHA_ENDERECO CE ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA WHERE U.EMAIL_USUARIO = '{0}' ", email);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            List<CampanhaModel> list = new List<CampanhaModel>();

            dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    CampanhaModel campanha = new CampanhaModel();
                    List<GrupoUsuarioModel> lista_grupos = new List<GrupoUsuarioModel>();

                    campanha.CODIGO_CAMPANHA = Convert.ToInt32(data["CODIGO_CAMPANHA"].ToString());
                    campanha.NOME_CAMPANHA = data["NOME_CAMPANHA"].ToString();
                    campanha.DESCRICAO_CAMPANHA = data["DESCRICAO_CAMPANHA"].ToString();
                    campanha.NOME_USUARIO = data["NOME_USUARIO"].ToString();
                    campanha.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();
                    campanha.NOME_RECEPTOR = data["NOME_RECEPTOR"].ToString();
                    campanha.TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString();
                    campanha.NOME_HOSPITAL = data["NOME_HOSPITAL"].ToString();
                    campanha.LOGRADOURO = data["LOGRADOURO"].ToString();
                    campanha.BAIRRO = data["BAIRRO"].ToString();
                    campanha.CIDADE = data["CIDADE"].ToString();
                    campanha.CEP = data["CEP"].ToString();
                    campanha.ESTADO = data["ESTADO"].ToString();
                    campanha.DATA_INICIO_DT = Convert.ToDateTime(data["DATA_INICIO"].ToString());
                    campanha.DATA_FIM_DT = Convert.ToDateTime(data["DATA_FIM"].ToString());
                    
                    strQuerySelect2 = String.Format("SELECT CG.CODIGO_GRUPO, G.NOME_GRUPO FROM CAMPANHA C INNER JOIN CAMPANHA_GRUPO CG ON C.CODIGO_CAMPANHA = CG.CODIGO_CAMPANHA INNER JOIN GRUPO G ON G.CODIGO_GRUPO = CG.CODIGO_GRUPO WHERE C.NOME_CAMPANHA = '{0}'", campanha.NOME_CAMPANHA);
                    dt2 = (DataTable)context.ExecuteCommand(strQuerySelect2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow data2 in dt2.Rows)
                        {
                            GrupoUsuarioModel grupo = new GrupoUsuarioModel();
                            List<UsuarioModel> usuarios = new List<UsuarioModel>();

                            grupo.CODIGO_GRUPO = Convert.ToInt32(data2["CODIGO_GRUPO"].ToString());
                            grupo.NOME_GRUPO = data2["NOME_GRUPO"].ToString();
                            grupo.INTEGRANTES = usuarios;
                            grupo.DATA_CRIACAO = Convert.ToDateTime(null);
                            lista_grupos.Add(grupo);
                        }
                    }

                    campanha.DESTINATARIOS = lista_grupos;
                    list.Add(campanha);
                }
            }

            return list;
        }

        public int deletarCampanha(CampanhaModel cmodel, UsuarioModel umodel)
        {
            int delete_campanha = (int)SITUACAO.SUCESSO;

            var strQuerySelect = String.Format("SELECT * FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE C.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);
            var strQueryDelete = String.Format("DELETE CE FROM CAMPANHA_ENDERECO CE INNER JOIN CAMPANHA C ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE CE.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);
            var strQueryDelete2 = String.Format("DELETE C FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE C.CODIGO_CAMPANHA = '{0}' AND U.EMAIL_USUARIO = '{1}'", cmodel.CODIGO_CAMPANHA, umodel.EMAIL_USUARIO);
            var strQueryDelete3 = String.Format("DELETE CG FROM CAMPANHA_GRUPO CG INNER JOIN CAMPANHA C ON C.CODIGO_CAMPANHA = CG.CODIGO_CAMPANHA WHERE C.NOME_CAMPANHA = {0}", cmodel.NOME_CAMPANHA);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    var c = context.ExecuteCommand(strQueryDelete3, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var a = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var b = context.ExecuteCommand(strQueryDelete2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    delete_campanha = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    delete_campanha = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count <= 0)
                delete_campanha = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return delete_campanha;
        }

        public int deletarCampanhaHemocentro(CampanhaModel cmodel)
        {
            int delete_campanha = (int)SITUACAO.SUCESSO;

            var strQueryDelete = String.Format("DELETE CE FROM CAMPANHA_ENDERECO CE INNER JOIN CAMPANHA C ON C.CODIGO_CAMPANHA = CE.CODIGO_CAMPANHA INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE CE.CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);
            var strQueryDelete2 = String.Format("DELETE C FROM CAMPANHA C INNER JOIN USUARIO U ON C.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE C.CODIGO_CAMPANHA = '{0}'", cmodel.CODIGO_CAMPANHA);

            try
            {
                context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                context.ExecuteCommand(strQueryDelete2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception)
            {
                delete_campanha = (int)SITUACAO.ERRO_DE_SISTEMA;
            }

            return delete_campanha;
        }

    }
}
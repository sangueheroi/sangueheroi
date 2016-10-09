using SangueHeroiWeb.Helpers.Util_Helper;
using SangueHeroiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.DAO
{

    public class UsuarioDAO
    {
        ContextHelpers context;

        public UsuarioDAO()
        {
            context = new ContextHelpers();

        }

        public List<UsuarioHeroiModel> GetInformacoesHerois()
        {
            var strQuery = String.Format("SELECT * FROM USUARIO_HEROI");

            List<UsuarioHeroiModel> lst = new List<UsuarioHeroiModel>();

            DataTable dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    UsuarioHeroiModel md = new UsuarioHeroiModel();

                    md.CODIGO_HEROI = Convert.ToInt32(data["CODIGO_HEROI"].ToString());
                    md.NOME_HEROI = data["NOME_HEROI"].ToString();
                    md.CARACTERISTICA_HEROI = data["CARACTERISTICA_HEROI"].ToString();
                    md.DESCRICAO_HEROI = data["DESCRICAO_HEROI"].ToString();
                    lst.Add(md);
                }
            }

            return lst;
        }

        public int Registrar(UsuarioModel model)
        {
            int registroOK = (int)SITUACAO.SUCESSO;
            bool flag = false;

            string strQuery = "";
            string strQueryUpdate = "";
            string strQueryInsert = "";

            string strQueryConsultaEmail = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", model.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaEmail, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            foreach (DataRow data in dt.Rows)
                flag = Convert.ToBoolean(data["FLAG_CADASTRO_REDE_SOCIAL"].ToString());

            if (dt.Rows.Count != 0 && flag == true)
            {
                int codigo_usuario = 0;

                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                strQueryUpdate = "EXECUTE frmAtualizarUsuario " + Environment.NewLine
                 + codigo_usuario + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SEXO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.BAIRRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_NASCIMENTO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_ULTIMA_DOACAO) + " , " + Environment.NewLine
                 + model.CODIGO_HEROI + " , " + Environment.NewLine
                 + false + " ;";

                try
                {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    registroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    registroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

            }
            if (dt.Rows.Count == 0)
            {
                strQueryInsert = "EXECUTE frmRegistrarUsuario " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SENHA_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL_USUARIO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.SEXO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.BAIRRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TIPO_SANGUINEO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_NASCIMENTO) + " , " + Environment.NewLine
                 + UtilHelper.DateTimeParaSQLDate(model.DATA_ULTIMA_DOACAO) + " , " + Environment.NewLine
                 + model.CODIGO_HEROI + " , " + Environment.NewLine
                 + model.FLAG_CADASTRO_REDE_SOCIAL + " ;";

                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    registroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    registroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count != 0 && flag == false)
                registroOK = (int)SITUACAO.JA_POSSUI_CADASTRO;

            return registroOK;
        }

        public List<UsuarioModel> consultarUsuarios()
        {
            var strQuery = "";

            strQuery =  " SELECT * " + Environment.NewLine
                      + " FROM USUARIO U " + Environment.NewLine
                      + " INNER JOIN USUARIO_PERFIL UP " + Environment.NewLine
                      + " ON U.CODIGO_USUARIO = UP.CODIGO_USUARIO " + Environment.NewLine
                      + " INNER JOIN USUARIO_ENDERECO UE " + Environment.NewLine
                      + " ON U.CODIGO_USUARIO = UE.CODIGO_USUARIO " + Environment.NewLine
                      + " WHERE U.CODIGO_USUARIO = 117";       

            var dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            var list = new List<UsuarioModel>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    list.Add(new UsuarioModel
                    {
                        CODIGO_USUARIO = Convert.ToInt32(data["CODIGO_USUARIO"].ToString()),
                        NOME_USUARIO = data["NOME_USUARIO"].ToString(),
                        EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString(),
                        TIPO_SANGUINEO = data["TIPO_SANGUINEO"].ToString(),
                        DATA_NASCIMENTO = Convert.ToDateTime(data["DATA_NASCIMENTO"].ToString()),
                        DATA_ULTIMA_DOACAO = Convert.ToDateTime(data["DATA_ULTIMA_DOACAO"].ToString()),
                        SEXO = data["SEXO"].ToString(),
                        DATA_PROXIMA_DOACAO = Convert.ToDateTime(data["DATA_PROXIMA_DOACAO"].ToString()),
                        BAIRRO = data["BAIRRO"].ToString(),
                        CIDADE = data["CIDADE"].ToString(),
                        ESTADO = data["ESTADO"].ToString(),
                        CEP = data["CEP"].ToString(),
                    });
                }
            }

            return list;
        }

        public List<UsuarioModel> consultarEmailUsuarioPorTipoSanguineo(string tipo_sanguineo)
        {
            var strQuery = "";

            strQuery = " SELECT EMAIL_USUARIO " + Environment.NewLine
                      + " FROM USUARIO U " + Environment.NewLine
                      + " INNER JOIN USUARIO_PERFIL UP " + Environment.NewLine
                      + " ON U.CODIGO_USUARIO = UP.CODIGO_USUARIO " + Environment.NewLine
                      + " WHERE UP.TIPO_SANGUINEO = '" + tipo_sanguineo + "';";
                                  
            var dt = (DataTable)context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            var list = new List<UsuarioModel>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    UsuarioModel usuario = new UsuarioModel();
                    usuario.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();
                    list.Add(usuario);
                }
            }

            return list;
        }

        public List<UsuarioGraficoModel> GetTipoSanguineoPorUsuario(string where = "")
        {

            string strSQL = " SELECT COUNT (U.NOME_USUARIO) AS QUANTIDADE_USUARIOS_TIPO_SANGUINEO, " + Environment.NewLine
                          + " UP.TIPO_SANGUINEO FROM USUARIO U " + Environment.NewLine
                          + " INNER JOIN USUARIO_PERFIL UP " + Environment.NewLine
                          + " ON U.CODIGO_USUARIO = UP.CODIGO_USUARIO " + Environment.NewLine
                          + " INNER JOIN USUARIO_ENDERECO UE " + Environment.NewLine
                          + " ON U.CODIGO_USUARIO = UE.CODIGO_USUARIO " + Environment.NewLine;

            if (where.Trim() != "")
            {
                strSQL = strSQL + where + Environment.NewLine + "GROUP BY UP.TIPO_SANGUINEO";
            }
            else
            {
                strSQL = strSQL + Environment.NewLine + "GROUP BY UP.TIPO_SANGUINEO";
            }



            List<UsuarioGraficoModel> lista = new List<UsuarioGraficoModel>();

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            try
            {
                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                        UsuarioGraficoModel ugm = new UsuarioGraficoModel();

                        ugm.NOME_TIPO_SANGUINEO_USUARIO = data["TIPO_SANGUINEO"].ToString();
                        ugm.QUANTIDADE_USUARIOS_TIPO_SANGUINEO = Convert.ToInt32(data["QUANTIDADE_USUARIOS_TIPO_SANGUINEO"].ToString());

                        lista.Add(ugm);
                    }

            }
            catch (Exception ex)
            {

            }

            return lista;
        }

        public int CadastrarGrupo(UsuarioGrupoModel ugmodel, string json)
        {
            int cadastroOK = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_grupo = 0;

            int codigo_usuario = 0;

            string strQueryInsert = "";
            string strQueryInsert2 = "";

            string strQueryConsultaCodigo = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", ugmodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                strQueryInsert = "INSERT INTO GRUPO (NOME_GRUPO, DESCRICAO_GRUPO, EMAIL_USUARIO, DATA_CRIACAO) VALUES ('" + ugmodel.NOME_GRUPO + "', '" + ugmodel.DESCRICAO_GRUPO + "', '" + ugmodel.EMAIL_USUARIO + "', GETDATE());";

                try
                {
                    var a = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    cadastroOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

                string strQueryConsultaCodigoGrupo = String.Format("SELECT CODIGO_GRUPO FROM GRUPO WHERE NOME_GRUPO = '{0}'", ugmodel.NOME_GRUPO);

                DataTable dt2 = new DataTable();

                dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoGrupo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt2.Rows.Count != 0)
                {

                    foreach (DataRow data in dt2.Rows)
                        codigo_grupo = Convert.ToInt32(data["CODIGO_GRUPO"].ToString());

                    UsuarioGrupoModel integrantes = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioGrupoModel>(json);

                    foreach (var item in integrantes.INTEGRANTES)
                    {

                        string strQueryConsultaCodigoUsuario = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", item.EMAIL_USUARIO);

                        DataTable dt3 = new DataTable();

                        dt3 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                        if (dt3.Rows.Count != 0)
                        {

                            foreach (DataRow data in dt3.Rows)
                                codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                            strQueryInsert2 = "INSERT INTO USUARIO_GRUPO (CODIGO_GRUPO, CODIGO_USUARIO) VALUES (" + codigo_grupo + ", " + codigo_usuario + ");";

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

        public int AlterarGrupo(UsuarioGrupoModel ugmodel, string json)
        {
            int alteracaoOK = (int)SITUACAO.DADOS_INVALIDOS;

            string strQueryUpdate = "";
            string strQueryInsert = "";
            string email_usuario = "";
            int codigo_usuario = 0;

            string strQueryConsultaCodigo = String.Format("SELECT * FROM GRUPO WHERE CODIGO_GRUPO = {0} AND EMAIL_USUARIO = '{1}'", ugmodel.CODIGO_GRUPO, ugmodel.EMAIL_USUARIO);
            string strQueryConsultaEmailBD = String.Format("SELECT UG.CODIGO_USUARIO, U.EMAIL_USUARIO FROM USUARIO U INNER JOIN USUARIO_GRUPO UG ON U.CODIGO_USUARIO = UG.CODIGO_USUARIO WHERE UG.CODIGO_GRUPO = {0}", ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();
            DataTable dt4 = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            dt4 = (DataTable)context.ExecuteCommand(strQueryConsultaEmailBD, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                strQueryUpdate = String.Format("UPDATE GRUPO SET NOME_GRUPO = '" + ugmodel.NOME_GRUPO + "', DESCRICAO_GRUPO = '" + ugmodel.DESCRICAO_GRUPO + "', EMAIL_USUARIO = '" + ugmodel.EMAIL_USUARIO + "' WHERE CODIGO_GRUPO = " + ugmodel.CODIGO_GRUPO + "");

                try
                {
                    var a = context.ExecuteCommand(strQueryUpdate, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    alteracaoOK = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    alteracaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                }

                UsuarioGrupoModel integrantes = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioGrupoModel>(json);

                foreach (var item in integrantes.INTEGRANTES)
                {
                    string strQueryConsultaEmailUsuario = String.Format("SELECT UG.CODIGO_USUARIO, U.EMAIL_USUARIO FROM USUARIO U INNER JOIN USUARIO_GRUPO UG ON UG.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}' AND UG.CODIGO_GRUPO = {1}", item.EMAIL_USUARIO, ugmodel.CODIGO_GRUPO);
                    string strQueryConsultaCodigoUsuario = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", item.EMAIL_USUARIO);

                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();

                    dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaEmailUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
                    dt3 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count == 0)
                    {

                        foreach (DataRow data in dt3.Rows)
                        {
                            codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());
                            //email_usuario = data["EMAIL_USUARIO"].ToString();
                        }

                        strQueryInsert = "INSERT INTO USUARIO_GRUPO (CODIGO_GRUPO, CODIGO_USUARIO) VALUES (" + ugmodel.CODIGO_GRUPO + ", " + codigo_usuario + ");";

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
                    else if (dt4.Rows.Count != 0)
                    {
                        /*for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            for (int j = 0; j < integrantes.INTEGRANTES.Count; i++)
                            {
                                if(dt4.Rows.)
                            }
                        }*/

                        foreach (DataRow data in dt4.Rows)
                        {
                            email_usuario = data["EMAIL_USUARIO"].ToString();

                            if (!(email_usuario.Equals(item.EMAIL_USUARIO)))
                            {
                                try
                                {
                                    var strQueryDelete = String.Format("DELETE UG FROM USUARIO_GRUPO UG INNER JOIN USUARIO U ON UG.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE UG.CODIGO_GRUPO = {0} AND U.EMAIL_USUARIO = '{1}'", ugmodel.CODIGO_GRUPO, email_usuario);
                                    var b = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                                    alteracaoOK = (int)SITUACAO.SUCESSO;
                                }
                                catch (Exception)
                                {
                                    alteracaoOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                                }
                            }
                        }
                    }
                }
            }
            else if (dt.Rows.Count == 0)
            {
                alteracaoOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }
            return alteracaoOK;
        }

        public List<UsuarioGrupoModel> consultarMeusGrupos(UsuarioGrupoModel ugmodel)
        {
            string strQuerySelect = "";
            string strQuerySelect2 = "";

            strQuerySelect = String.Format("SELECT DISTINCT G.CODIGO_GRUPO, G.NOME_GRUPO, G.DESCRICAO_GRUPO, G.EMAIL_USUARIO FROM GRUPO G INNER JOIN USUARIO_GRUPO UG ON G.CODIGO_GRUPO = UG.CODIGO_GRUPO INNER JOIN USUARIO U ON UG.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}' OR G.EMAIL_USUARIO = '{1}'", ugmodel.EMAIL_USUARIO, ugmodel.EMAIL_USUARIO);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            List<UsuarioGrupoModel> list = new List<UsuarioGrupoModel>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    UsuarioGrupoModel grupo = new UsuarioGrupoModel();
                    List<UsuarioModel> lista_usuarios = new List<UsuarioModel>();

                    grupo.DATA_CRIACAO = Convert.ToDateTime(null);
                    grupo.CODIGO_GRUPO = Convert.ToInt32(data["CODIGO_GRUPO"].ToString());
                    grupo.NOME_GRUPO = data["NOME_GRUPO"].ToString();
                    grupo.DESCRICAO_GRUPO = data["DESCRICAO_GRUPO"].ToString();
                    grupo.EMAIL_USUARIO = data["EMAIL_USUARIO"].ToString();

                    strQuerySelect2 = String.Format("SELECT U.NOME_USUARIO, U.EMAIL_USUARIO FROM USUARIO U INNER JOIN USUARIO_GRUPO UG ON U.CODIGO_USUARIO = UG.CODIGO_USUARIO WHERE UG.CODIGO_GRUPO = {0}", grupo.CODIGO_GRUPO);
                    dt2 = (DataTable)context.ExecuteCommand(strQuerySelect2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    foreach (DataRow data2 in dt2.Rows)
                    {
                        UsuarioModel usuario = new UsuarioModel();

                        usuario.DATA_CRIACAO = Convert.ToDateTime(null);
                        usuario.NOME_USUARIO = data2["NOME_USUARIO"].ToString();
                        usuario.EMAIL_USUARIO = data2["EMAIL_USUARIO"].ToString();
                        lista_usuarios.Add(usuario);
                        grupo.INTEGRANTES = lista_usuarios;
                    }

                    list.Add(grupo);
                }
            }
            return list;
        }

        public int deletarGrupo(UsuarioGrupoModel ugmodel)
        {
            int delete_grupo = (int)SITUACAO.DADOS_INVALIDOS;

            var strQuerySelect = String.Format("SELECT * FROM GRUPO WHERE CODIGO_GRUPO = {0} AND EMAIL_USUARIO = '{1}'", ugmodel.CODIGO_GRUPO, ugmodel.EMAIL_USUARIO);
            var strQueryDelete = String.Format("DELETE G FROM GRUPO G WHERE G.CODIGO_GRUPO = {0} AND G.EMAIL_USUARIO = '{1}'", ugmodel.CODIGO_GRUPO, ugmodel.EMAIL_USUARIO);
            var strQueryDelete2 = String.Format("DELETE UG FROM USUARIO_GRUPO UG WHERE UG.CODIGO_GRUPO = {0}", ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    var a = context.ExecuteCommand(strQueryDelete2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    var b = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    delete_grupo = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    delete_grupo = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count <= 0)
                delete_grupo = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return delete_grupo;
        }

        public int desvincularGrupo(UsuarioGrupoModel ugmodel)
        {
            int desvincula_grupo = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_usuario = 0;

            var strQuerySelect = String.Format("SELECT * FROM USUARIO_GRUPO UG INNER JOIN USUARIO U ON UG.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE U.EMAIL_USUARIO = '{0}' AND UG.CODIGO_GRUPO = {1}", ugmodel.EMAIL_USUARIO, ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                try
                {
                    var strQueryDelete = String.Format("DELETE UG FROM USUARIO_GRUPO UG INNER JOIN USUARIO U ON UG.CODIGO_USUARIO = U.CODIGO_USUARIO WHERE UG.CODIGO_GRUPO = {0} AND UG.CODIGO_USUARIO = " + codigo_usuario + "", ugmodel.CODIGO_GRUPO);
                    var b = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                    desvincula_grupo = (int)SITUACAO.SUCESSO;
                }
                catch (Exception)
                {
                    desvincula_grupo = (int)SITUACAO.ERRO_DE_SISTEMA;
                }
            }
            else if (dt.Rows.Count <= 0)
                desvincula_grupo = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return desvincula_grupo;
        }

        public int desvincularGrupoEmMassa(UsuarioGrupoModel ugmodel, string json)
        {
            int deleteOK = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_usuario = 0;

            string strQueryDelete = "";

            string strQueryConsultaCodigo = String.Format("SELECT * FROM GRUPO WHERE EMAIL_USUARIO = '{0}' AND CODIGO_GRUPO = {1}", ugmodel.EMAIL_USUARIO, ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                UsuarioGrupoModel integrantes = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioGrupoModel>(json);

                foreach (var item in integrantes.INTEGRANTES)
                {
                    string strQueryConsultaCodigoUsuario = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", item.EMAIL_USUARIO);

                    DataTable dt2 = new DataTable();

                    dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt2.Rows)
                            codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                        strQueryDelete = String.Format("DELETE FROM USUARIO_GRUPO WHERE CODIGO_GRUPO = {0} AND CODIGO_USUARIO = {1}", ugmodel.CODIGO_GRUPO, codigo_usuario);

                        try
                        {
                            var b = context.ExecuteCommand(strQueryDelete, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                            deleteOK = (int)SITUACAO.SUCESSO;
                        }
                        catch (Exception)
                        {
                            deleteOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                        }
                    }
                    else if (dt2.Rows.Count == 0)
                    {
                        deleteOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
                    }
                }
            }
            else if (dt.Rows.Count == 0)
            {
                deleteOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return deleteOK;
        }

        public int vincularGrupo(UsuarioGrupoModel ugmodel)
        {
            int vincula_grupo = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_usuario = 0;
            int codigo_grupo = 0;

            var strQuerySelect = String.Format("SELECT * FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", ugmodel.EMAIL_USUARIO);
            var strQuerySelect2 = String.Format("SELECT * FROM GRUPO WHERE CODIGO_GRUPO = {0}", ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            dt2 = (DataTable)context.ExecuteCommand(strQuerySelect2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                    codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                foreach (DataRow data in dt2.Rows)
                    codigo_grupo = Convert.ToInt32(data["CODIGO_GRUPO"].ToString());

                var strQuerySelect3 = String.Format("SELECT * FROM USUARIO_GRUPO WHERE CODIGO_GRUPO = {0} AND CODIGO_USUARIO = {1}", codigo_grupo, codigo_usuario);
                dt3 = (DataTable)context.ExecuteCommand(strQuerySelect3, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt3.Rows.Count <= 0)
                {
                    try
                    {
                        var strQueryInsert = String.Format("INSERT INTO USUARIO_GRUPO (CODIGO_GRUPO, CODIGO_USUARIO) VALUES (" + codigo_grupo + "," + codigo_usuario + ")");
                        var b = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                        vincula_grupo = (int)SITUACAO.SUCESSO;
                    }
                    catch (Exception)
                    {
                        vincula_grupo = (int)SITUACAO.ERRO_DE_SISTEMA;
                    }
                }
                else if (dt3.Rows.Count > 0)
                    vincula_grupo = (int)SITUACAO.JA_POSSUI_CADASTRO;
            }
            else if (dt.Rows.Count <= 0)
                vincula_grupo = (int)SITUACAO.NAO_POSSUI_CADASTRO;

            return vincula_grupo;
        }

        public int vincularGrupoEmMassa(UsuarioGrupoModel ugmodel, string json)
        {
            int cadastroOK = (int)SITUACAO.DADOS_INVALIDOS;

            int codigo_usuario = 0;

            string strQueryInsert = "";

            string strQueryConsultaCodigo = String.Format("SELECT * FROM GRUPO WHERE EMAIL_USUARIO = '{0}' AND CODIGO_GRUPO = {1}", ugmodel.EMAIL_USUARIO, ugmodel.CODIGO_GRUPO);

            DataTable dt = new DataTable();

            dt = (DataTable)context.ExecuteCommand(strQueryConsultaCodigo, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count != 0)
            {
                UsuarioGrupoModel integrantes = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<UsuarioGrupoModel>(json);

                foreach (var item in integrantes.INTEGRANTES)
                {
                    string strQueryConsultaCodigoUsuario = String.Format("SELECT CODIGO_USUARIO FROM USUARIO WHERE EMAIL_USUARIO = '{0}'", item.EMAIL_USUARIO);

                    DataTable dt2 = new DataTable();

                    dt2 = (DataTable)context.ExecuteCommand(strQueryConsultaCodigoUsuario, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    if (dt2.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt2.Rows)
                            codigo_usuario = Convert.ToInt32(data["CODIGO_USUARIO"].ToString());

                        strQueryInsert = String.Format("INSERT INTO USUARIO_GRUPO (CODIGO_GRUPO, CODIGO_USUARIO) VALUES ({0}, {1})", ugmodel.CODIGO_GRUPO, codigo_usuario);

                        try
                        {
                            var b = context.ExecuteCommand(strQueryInsert, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                            cadastroOK = (int)SITUACAO.SUCESSO;
                        }
                        catch (Exception)
                        {
                            cadastroOK = (int)SITUACAO.ERRO_DE_SISTEMA;
                        }
                    }
                    else if (dt2.Rows.Count == 0)
                    {
                        cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
                    }
                }
            }
            else if (dt.Rows.Count == 0)
            {
                cadastroOK = (int)SITUACAO.NAO_POSSUI_CADASTRO;
            }

            return cadastroOK;
        }

    }
}
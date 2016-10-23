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
    public class HemocentroDAO
    {
        ContextHelpers context;

        public HemocentroDAO()
        {
            context = new ContextHelpers();
        }

        public int ParceriaHemocentro(HemocentroModel model)
        {
            var registroOk = Constantes.PARCERIA_HEMOCENTRO.SUCESSO;

            try
            {
                var dt =
                    (DataTable)
                    context.ExecuteCommand(
                        $"SELECT * FROM HEMOCENTRO WHERE CNPJ = '{model.CNPJ}' OR EMAIL = '{model.EMAIL}'",
                        CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt.Rows.Count == 0)
                {
                    var strQuery = "EXECUTE frmCadastrarHemocentro " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                                      + model.CODIGO_STATUS + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.SENHA_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.EMAIL) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                                      + UtilHelper.TextForSql(model.CEP);

                    context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
                }
                else
                {
                    registroOk = Constantes.PARCERIA_HEMOCENTRO.USUARIO_EXISTENTE;
                }
            }
            catch (Exception)
            {
                registroOk = Constantes.PARCERIA_HEMOCENTRO.ERRO;
            }

           

            return registroOk;
        }

        public void AtivarHemocentro(int _idHemocentro)
        {
            string strSQL = $" UPDATE HEMOCENTRO SET CODIGO_STATUS = {Constantes.CADASTRO_STATUS.Ativo} WHERE CODIGO_HEMOCENTRO = {_idHemocentro} ";

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
        }

        public bool Editar(HemocentroModel model)
        {
            bool registroOK = true;
            string strQuery = "";
            var enc = new Encrypt();

            strQuery = "EXECUTE frmAtualizarHemocentro " + Environment.NewLine
                 + model.CODIGO_HEMOCENTRO + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.NOME_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CNPJ) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.RAZAO_SOCIAL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.LOGIN_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(enc.Encryption(model.SENHA_HEMOCENTRO)) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.EMAIL) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.TELEFONE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.PERIODO_FUNCIONAMENTO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CIDADE_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.ESTADO_HEMOCENTRO) + " , " + Environment.NewLine
                 + UtilHelper.TextForSql(model.CEP);

            try
            {
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception ex)
            {
                registroOK = false;
            }

            return registroOK;
        }

        public void Inativar(int _idHemocentro)
        {
            string strSQL = string.Format(" UPDATE HEMOCENTRO SET CODIGO_STATUS = {0} WHERE CODIGO_HEMOCENTRO = {1} ", Helpers.Util_Helper.Constantes.CADASTRO_STATUS.Bloqueado, _idHemocentro);

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
        }

        public List<HemocentroModel> Lista(string where = "")
        {

            var strSQL = " SELECT * FROM HEMOCENTRO H " + Environment.NewLine
                            + " INNER JOIN HEMOCENTRO_ENDERECO HE" + Environment.NewLine
                            + " ON H.CODIGO_HEMOCENTRO = HE.CODIGO_HEMOCENTRO ";

            if (where.Trim() != "")
                strSQL = strSQL + where;

            List<HemocentroModel> lista = new List<HemocentroModel>();

            try
            {
                DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                        HemocentroModel hm = new HemocentroModel();

                        hm.CODIGO_HEMOCENTRO = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());
                        hm.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                        hm.CNPJ = data["CNPJ"].ToString();
                        hm.RAZAO_SOCIAL = data["RAZAO_SOCIAL"].ToString();
                        hm.CODIGO_STATUS = Convert.ToInt32(data["CODIGO_STATUS"].ToString());
                        hm.LOGIN_HEMOCENTRO = data["LOGIN_HEMOCENTRO"].ToString();
                        hm.SENHA_HEMOCENTRO = data["SENHA_HEMOCENTRO"].ToString();
                        hm.CODIGO_HEMOCENTRO_PERFIL = Convert.ToInt32(data["CODIGO_HEMOCENTRO_PERFIL"].ToString());
                        hm.EMAIL = data["EMAIL"].ToString();
                        hm.TELEFONE_HEMOCENTRO = data["TELEFONE_HEMOCENTRO"].ToString();
                        hm.PERIODO_FUNCIONAMENTO_HEMOCENTRO = data["PERIODO_FUNCIONAMENTO"].ToString();
                        hm.CIDADE_HEMOCENTRO = data["CIDADE"].ToString();
                        hm.ESTADO_HEMOCENTRO = data["ESTADO"].ToString();
                        hm.CEP = data["CEP"].ToString();

                        lista.Add(hm);
                    }

            }
            catch (Exception ex)
            {

            }

            return lista;
        }

        public bool CadastrarNiveisSanguineos(HemocentroNiveisSanguineosModel model)
        {
            bool registroOK = true;

            string strQuery = "";

            strQuery = "EXECUTE frmCadastrarHemocentroNiveisSanguineos " + Environment.NewLine
                 + model.CODIGO_HEMOCENTRO + " , " + Environment.NewLine
                 + model.On + " , " + Environment.NewLine
                 + model.Op + " , " + Environment.NewLine
                 + model.An + " , " + Environment.NewLine
                 + model.Ap + " , " + Environment.NewLine
                 + model.Bn + " , " + Environment.NewLine
                 + model.Bp + " , " + Environment.NewLine
                 + model.ABn + " , " + Environment.NewLine
                 + model.ABp;

            try
            {
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception ex)
            {
                registroOK = false;
            }

            return registroOK;
        }

        public bool EditarNiveisSanguineos(HemocentroNiveisSanguineosModel model)
        {
            bool registroOK = true;

            string strQuery = "";

            strQuery = "EXECUTE frmAtualizarHemocentroNiveisSanguineos " + Environment.NewLine
                 + model.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS + " , " + Environment.NewLine
                 + model.CODIGO_HEMOCENTRO + " , " + Environment.NewLine
                 + model.On + " , " + Environment.NewLine
                 + model.Op + " , " + Environment.NewLine
                 + model.An + " , " + Environment.NewLine
                 + model.Ap + " , " + Environment.NewLine
                 + model.Bn + " , " + Environment.NewLine
                 + model.Bp + " , " + Environment.NewLine
                 + model.ABn + " , " + Environment.NewLine
                 + model.ABp;

            try
            {
                context.ExecuteCommand(strQuery, CommandType.Text, ContextHelpers.TypeCommand.ExecuteReader);
            }
            catch (Exception ex)
            {
                registroOK = false;
            }

            return registroOK;
        }

        public HemocentroModel BuscaHemocentro(string where = "")
        {
            return Lista(where).FirstOrDefault();
        }

        public HemocentroNiveisSanguineosModel BuscaNiveisPorHemocentro(string where = "")
        {
            string strSQL = " SELECT H.CODIGO_HEMOCENTRO, " + Environment.NewLine
                          + " H.NOME_HEMOCENTRO, " + Environment.NewLine
                          + " HNS.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS, " + Environment.NewLine
                          + " HNS.One, " + Environment.NewLine
                          + " HNS.Opo, " + Environment.NewLine
                          + " HNS.Ane, " + Environment.NewLine
                          + " HNS.Apo, " + Environment.NewLine
                          + " HNS.Bne, " + Environment.NewLine
                          + " HNS.Bpo, " + Environment.NewLine
                          + " HNS.ABne, " + Environment.NewLine
                          + " HNS.ABpo " + Environment.NewLine
                          + " FROM HEMOCENTRO_NIVEIS_SANGUINEOS HNS     " + Environment.NewLine
                          + " LEFT JOIN HEMOCENTRO H (NOLOCK)  " + Environment.NewLine
                          + " ON H.CODIGO_HEMOCENTRO = HNS.CODIGO_HEMOCENTRO ";

            if (where.Trim() != "")
                strSQL = strSQL + where;

            HemocentroNiveisSanguineosModel hnsm = new HemocentroNiveisSanguineosModel();

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            try
            {
                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                       
                        hnsm.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS = Convert.ToInt32(data["CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS"].ToString());
                        hnsm.CODIGO_HEMOCENTRO = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());
                        hnsm.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                        hnsm.On = Convert.ToInt32(data["One"].ToString());
                        hnsm.Op = Convert.ToInt32(data["Opo"].ToString());
                        hnsm.An = Convert.ToInt32(data["Ane"].ToString());
                        hnsm.Ap = Convert.ToInt32(data["Apo"].ToString());
                        hnsm.Bn = Convert.ToInt32(data["Bne"].ToString());
                        hnsm.Bp = Convert.ToInt32(data["Bpo"].ToString());
                        hnsm.ABp = Convert.ToInt32(data["ABpo"].ToString());
                        hnsm.ABn = Convert.ToInt32(data["ABne"].ToString());
                    }

            }
            catch (Exception ex)
            {

            }

            return hnsm;
        }

        public List<HemocentroModel> ConsultarNiveisSanguineosTodosOsHemocentros()
        {
            string strQuerySelect = "";
            string strQuerySelect2 = "";

            strQuerySelect = String.Format("SELECT * FROM HEMOCENTRO");

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            List<HemocentroModel> list = new List<HemocentroModel>();

            dt = (DataTable)context.ExecuteCommand(strQuerySelect, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    HemocentroModel hemocentro = new HemocentroModel();
                    List<HemocentroNiveisSanguineosModel> lista_tipos_sanguineos = new List<HemocentroNiveisSanguineosModel>();

                    hemocentro.ListaEstados = null;
                    hemocentro.CODIGO_HEMOCENTRO = Convert.ToInt32(data["CODIGO_HEMOCENTRO"].ToString());
                    hemocentro.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                    hemocentro.CNPJ = null;
                    hemocentro.RAZAO_SOCIAL = null;
                    hemocentro.CODIGO_STATUS = Convert.ToInt32(null);
                    hemocentro.LOGIN_HEMOCENTRO = null;
                    hemocentro.SENHA_HEMOCENTRO = null;
                    hemocentro.CODIGO_HEMOCENTRO_PERFIL = Convert.ToInt32(null);
                    hemocentro.EMAIL = null;
                    hemocentro.DATA_CRIACAO = Convert.ToDateTime(null);
                    hemocentro.TELEFONE_HEMOCENTRO = null;
                    hemocentro.PERIODO_FUNCIONAMENTO_HEMOCENTRO = null;

                    strQuerySelect2 = String.Format("SELECT HNS.One, HNS.Opo, HNS.Ane, HNS.Apo, HNS.Bne, HNS.Bpo, HNS.ABne, HNS.ABpo FROM HEMOCENTRO_NIVEIS_SANGUINEOS HNS INNER JOIN HEMOCENTRO H ON HNS.CODIGO_HEMOCENTRO = H.CODIGO_HEMOCENTRO WHERE HNS.CODIGO_HEMOCENTRO = {0}", hemocentro.CODIGO_HEMOCENTRO);
                    dt2 = (DataTable)context.ExecuteCommand(strQuerySelect2, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);

                    foreach (DataRow data2 in dt2.Rows)
                    {
                        HemocentroNiveisSanguineosModel niveis_sanguineos = new HemocentroNiveisSanguineosModel();

                        niveis_sanguineos.CODIGO_HEMOCENTRO_NIVEIS_SANGUINEOS = Convert.ToInt32(null);
                        niveis_sanguineos.CODIGO_HEMOCENTRO = Convert.ToInt32(null);
                        niveis_sanguineos.NOME_HEMOCENTRO = null;
                        niveis_sanguineos.On = Convert.ToInt32(data2["One"].ToString());
                        niveis_sanguineos.Op = Convert.ToInt32(data2["Opo"].ToString());
                        niveis_sanguineos.An = Convert.ToInt32(data2["Ane"].ToString());
                        niveis_sanguineos.Ap = Convert.ToInt32(data2["Apo"].ToString());
                        niveis_sanguineos.Bn = Convert.ToInt32(data2["Bne"].ToString());
                        niveis_sanguineos.Bp = Convert.ToInt32(data2["Bpo"].ToString());
                        niveis_sanguineos.ABn = Convert.ToInt32(data2["ABne"].ToString());
                        niveis_sanguineos.ABp = Convert.ToInt32(data2["ABpo"].ToString());

                        lista_tipos_sanguineos.Add(niveis_sanguineos);      
                    }

                    hemocentro.TIPOS_SANGUINEOS = lista_tipos_sanguineos;
                    list.Add(hemocentro);
                }
            }
            return list;
        }

        public List<HemocentroNiveisSanguineosModelGrafico> GetNiveisSanguineos(string where = "")
        {
            string strSQL = " SELECT " + Environment.NewLine
                          + " H.NOME_HEMOCENTRO,"
                          + " HNS.One, " + Environment.NewLine
                          + " HNS.Opo, " + Environment.NewLine
                          + " HNS.Ane, " + Environment.NewLine
                          + " HNS.Apo, " + Environment.NewLine
                          + " HNS.Bne, " + Environment.NewLine
                          + " HNS.Bpo, " + Environment.NewLine
                          + " HNS.ABne, " + Environment.NewLine
                          + " HNS.ABpo " + Environment.NewLine
                          + " FROM HEMOCENTRO_NIVEIS_SANGUINEOS HNS     " + Environment.NewLine
                          + " LEFT JOIN HEMOCENTRO H (NOLOCK)  " + Environment.NewLine
                          + " ON H.CODIGO_HEMOCENTRO = HNS.CODIGO_HEMOCENTRO ";

            if (where.Trim() != "")
                strSQL = strSQL + where;

            List<HemocentroNiveisSanguineosModelGrafico> list = new List<HemocentroNiveisSanguineosModelGrafico>();
            HemocentroNiveisSanguineosModel hnsm = new HemocentroNiveisSanguineosModel();

            DataTable dt = (DataTable)context.ExecuteCommand(strSQL, CommandType.Text, ContextHelpers.TypeCommand.ExecuteDataTable);
            try
            {
                if (dt.Rows.Count > 0)
                    foreach (DataRow data in dt.Rows)
                    {
                        hnsm.NOME_HEMOCENTRO = data["NOME_HEMOCENTRO"].ToString();
                        hnsm.On = Convert.ToInt32(data["One"].ToString());
                        hnsm.Op = Convert.ToInt32(data["Opo"].ToString());
                        hnsm.An = Convert.ToInt32(data["Ane"].ToString());
                        hnsm.Ap = Convert.ToInt32(data["Apo"].ToString());
                        hnsm.Bn = Convert.ToInt32(data["Bne"].ToString());
                        hnsm.Bp = Convert.ToInt32(data["Bpo"].ToString());
                        hnsm.ABp = Convert.ToInt32(data["ABpo"].ToString());
                        hnsm.ABn = Convert.ToInt32(data["ABne"].ToString());
                    }

            }
            catch (Exception ex)
            {

            }

            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "O-", VALOR_TIPO_SANGUINEO = hnsm.On, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "O+", VALOR_TIPO_SANGUINEO = hnsm.Op, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "A-", VALOR_TIPO_SANGUINEO = hnsm.An, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "A+", VALOR_TIPO_SANGUINEO = hnsm.Ap, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "B-", VALOR_TIPO_SANGUINEO = hnsm.Bn, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "B+", VALOR_TIPO_SANGUINEO = hnsm.Bp, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "AB+", VALOR_TIPO_SANGUINEO = hnsm.ABp, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });
            list.Add(new HemocentroNiveisSanguineosModelGrafico() { NOME_TIPO_SANGUINEO = "AB-", VALOR_TIPO_SANGUINEO = hnsm.ABn, NOME_HEMOCENTRO = hnsm.NOME_HEMOCENTRO });

            return list;
        }
    }
}
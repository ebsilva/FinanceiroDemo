using Metasoft.Models;
using Metasoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Metasoft.Infrastructure
{
    public class RepoLib
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="de"> Data de Inicio</param>
        /// <param name="ate"></param>
        /// <param name="tipodata"></param>
        /// <param name="query"></param>
        /// <param name="useUsDateFormat"></param>
        /// <returns></returns>
        /// 

        #region Contas
        public static string Get_Correct_Year(string ano, string de = "", string ate = "", string situacao = "", bool useUsDateFormat = true)
        {
            if (situacao != "7") return "0";
            if (ano != "0") return ano;

            return "0";

        }
        public static IEnumerable<ContaPrViewModel> Get_ContaPrs_Data(ApplicationDbContext context, string year, string tipo)
        {
            var tipoParameter = new SqlParameter("@tipo", tipo);
             if (year == "0")
                return context.Database.SqlQuery<ContaPrViewModel>("P_Contas @tipo", tipoParameter).AsEnumerable<ContaPrViewModel>();
            else
                return context.Database.SqlQuery<ContaPrViewModel>("P_Contas_" + year + " @tipo", tipoParameter).AsEnumerable<ContaPrViewModel>();
        }
        public static IEnumerable<ContaPrViewModel> Get_Contas_By_Period(IEnumerable<ContaPrViewModel> result, string tipodata, string de, string ate, bool useUsDateFormat, bool archived = false)
        {

            if (archived) result = from a in result where a.archive == true select a;

            if (de != "" && ate == "")
            {
                if (useUsDateFormat)
                {
                    if (tipodata == "vencimento")
                    {
                        result = from a in result where DateTime.Parse(a.vencimento) >= DateTime.Parse(de) select a;
                    }
                    else
                    {
                        if (tipodata == "dtcad")
                            result = from a in result where DateTime.Parse(usdate(a.dtcad)) >= DateTime.Parse(usdate(de)) select a;
                        else
                            result = from a in result where a.dtpagto != null && DateTime.Parse(usdate(a.dtpagto)) >= DateTime.Parse(usdate(de)) select a;
                    }
                }
                else
                {
                    if (tipodata == "vencimento") { result = from a in result where DateTime.Parse(a.vencimento) >= DateTime.Parse(de) select a; }
                    else
                    {
                        if (tipodata == "dtcad") { result = from a in result where DateTime.Parse(a.dtcad) >= DateTime.Parse(de) select a; }
                        else
                        {
                            result = from a in result where a.dtpagto != null select a;
                            result = from a in result where a.dtpagto != null && DateTime.Parse(a.dtpagto) >= DateTime.Parse(de) select a;
                        }
                    }
                }
            }
            else
            {
                if (de == "" && ate != "")
                {
                    if (useUsDateFormat)
                    {
                        if (tipodata == "vencimento")
                            result = from a in result where DateTime.Parse(usdate(a.vencimento)) <= DateTime.Parse(usdate(ate)) select a;
                        else
                            if (tipodata == "dtcad")
                            result = from a in result where DateTime.Parse(usdate(a.dtcad)) <= DateTime.Parse(usdate(ate)) select a;
                        else
                            result = from a in result where DateTime.Parse(usdate(a.dtpagto)) <= DateTime.Parse(usdate(ate)) select a;
                    }
                    else
                    {
                        if (tipodata == "vencimento") { result = from a in result where DateTime.Parse(a.vencimento) <= DateTime.Parse(ate) select a; }
                        else
                        {
                            if (tipodata == "dtcad") { result = from a in result where DateTime.Parse(a.dtcad) <= DateTime.Parse(ate) select a; }
                            else
                            {
                                result = from a in result where a.dtpagto != null select a;
                                result = from a in result where DateTime.Parse(a.dtpagto) <= DateTime.Parse(ate) select a;
                            }
                        }
                    }
                }
                else
                {
                    if (de != "" && ate != "")
                    {
                        if (useUsDateFormat)
                        {
                            string d = usdate(de);
                            string at = usdate(ate);

                            if (tipodata == "vencimento")
                                result = from a in result where DateTime.Parse(a.vencimento) >= DateTime.Parse(de) && DateTime.Parse(a.vencimento) <= DateTime.Parse(ate) select a;
                            else
                              if (tipodata == "dtcad")
                                result = from a in result where DateTime.Parse(usdate(a.dtcad)) >= DateTime.Parse(usdate(de)) && DateTime.Parse(usdate(a.dtcad)) <= DateTime.Parse(usdate(ate)) select a;
                            else
                                result = from a in result where DateTime.Parse(usdate(a.dtpagto)) >= DateTime.Parse(usdate(de)) && DateTime.Parse(usdate(a.dtpagto)) <= DateTime.Parse(usdate(ate)) select a;
                        }
                        else
                        {
                            if (tipodata == "vencimento") { result = from a in result where DateTime.Parse(a.vencimento) >= DateTime.Parse(de) && DateTime.Parse(a.vencimento) <= DateTime.Parse(ate) select a; }
                            else
                            {
                                if (tipodata == "dtcad") { result = from a in result where DateTime.Parse(a.dtcad) >= DateTime.Parse(de) && DateTime.Parse(a.dtcad) <= DateTime.Parse(ate) select a; }
                                else
                                {
                                    result = from a in result where a.dtpagto != null select a;
                                    result = from a in result where DateTime.Parse(a.dtpagto) >= DateTime.Parse(de) && DateTime.Parse(a.dtpagto) <= DateTime.Parse(ate) select a;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static String Set_De_Ate_Filter(string tableprefix, string de, string ate, string tipodata, string query, Boolean useUsDateFormat = true)
        {
            if (de != "" && ate == "")
            {
                if (useUsDateFormat) de = usdate(de);

                query += "and CAST(" + tableprefix + tipodata + " as DATE) = CAST('" + de + "' as DATE) ";
            }
            if (de == "" && ate != "")
            {
                if (useUsDateFormat) ate = usdate(ate);

                query += "and CAST(" + tableprefix + tipodata + " as DATE) <= CAST('" + ate + "' as DATE) ";
            }
            if (de != "" && ate != "")
            {
                if (useUsDateFormat) { de = usdate(de); ate = usdate(ate); }
                query += "and (CAST(" + tableprefix + tipodata + " as DATE) >= CAST('" + de + "' as DATE) and " + tableprefix + tipodata + " <= CAST('" + ate + "' as DATE)) ";
            }

            return query;
        }
        public static IEnumerable<ContaPrViewModel> Apply_Situacao_ContaPr_Filter(IEnumerable<ContaPrViewModel> result, string situacao, string tipodata, string de, string ate, bool useUsDateFormat = true)
        {
            result = Get_Contas_By_Period(result, tipodata, de, ate, useUsDateFormat, false);
            if (situacao != "")
            {
                switch (situacao)
                {

                    // Em atraso
                    case "2":
                        if (useUsDateFormat)
                            result = from a in result where a.situacao == "A" && DateTime.Parse(usdate(a.vencimento)) < DateTime.Parse(usdate(DateTime.Now.ToShortDateString())) select a;
                        else
                            result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento) < DateTime.Parse(DateTime.Now.ToShortDateString()) select a;

                        break;
                    //Hoje
                    case "3":
                        if (useUsDateFormat)
                            result = from a in result where a.situacao == "A" && DateTime.Parse(usdate(a.vencimento)) == DateTime.Parse(usdate(DateTime.Now.ToShortDateString())) select a;
                        else
                            result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento) == DateTime.Parse(DateTime.Now.ToShortDateString()) select a;
                        break;
                    // Este mês
                    case "4":
                        if (tipodata == "vencimento")
                        {
                            if (useUsDateFormat)
                                result = from a in result where a.situacao == "A" && DateTime.Parse(usdate(a.vencimento)).Month == DateTime.Parse(usdate(DateTime.Now.ToShortDateString())).Month select a;
                            else
                                result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento).Month == DateTime.Parse(DateTime.Now.ToShortDateString()).Month select a;

                        }
                        else
                        {
                            if (tipodata == "dtcad")
                            {
                                if (useUsDateFormat)
                                    result = from a in result where a.situacao == "A" && DateTime.Parse(usdate(a.dtcad)).Month == DateTime.Parse(usdate(DateTime.Now.ToShortDateString())).Month select a;
                                else
                                    result = from a in result where a.situacao == "A" && DateTime.Parse(a.dtcad).Month == DateTime.Parse(DateTime.Now.ToShortDateString()).Month select a;

                            }
                            else
                            {
                                if (tipodata == "dtpagto")
                                {
                                    if (useUsDateFormat)
                                        result = from a in result where a.situacao == "A" && DateTime.Parse(usdate(a.dtpagto)).Month == DateTime.Parse(usdate(DateTime.Now.ToShortDateString())).Month select a;
                                    else
                                        result = from a in result where a.situacao == "A" && DateTime.Parse(a.dtpagto).Month == DateTime.Parse(DateTime.Now.ToShortDateString()).Month select a;

                                }
                            }
                        }
                        break;
                    // Recorrentes
                    case "5":
                        result = from a in result where a.recorrente == true select a;
                        break;
                    // Pagas
                    case "6":
                        result = from a in result where a.situacao == "P" select a;
                        break;
                    // Arquivadas
                    case "7":
                        //result = RepoLib.Get_Contas_By_Period(result,  tipodata, de, ate, useUsDateFormat,  true);
                        break;

                }
            }
            return result;
        }

        public static IEnumerable<ContaPrViewModel> Get_Contas_Vencidas(IEnumerable<ContaPrViewModel> result, string tipodata, string de, string ate, bool useUsDateFormat, bool archived = true)
        {
            if (de != "" && ate == "")
            {
                if (useUsDateFormat) de = usdate(de);

                result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento) < DateTime.Parse(DateTime.Now.ToShortDateString()) select a;
            }
            else
            {
                if (de == "" && ate != "")
                {
                    if (useUsDateFormat) ate = usdate(ate);

                    result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento) < DateTime.Parse(DateTime.Now.ToShortDateString()) select a;
                }
                else
                {
                    if (useUsDateFormat)
                    {
                        de = usdate(de);
                        ate = usdate(ate);
                    }
                    result = from a in result where a.situacao == "A" && DateTime.Parse(a.vencimento) >= DateTime.Parse(de) && DateTime.Parse(a.vencimento) <= DateTime.Parse(ate) select a;
                }
            }


            return result;
        }

        public static IEnumerable<ContaPrViewModel> Set_Contas_Category_Filter(IEnumerable<ContaPrViewModel> result, int categoria)
        {
            if (categoria != 0)
            {
                result = from a in result where a.categoriaid == categoria select a;
            }
            return result;
        }

        public static IEnumerable<ContaPrViewModel> Set_Contas_CliFor_Filter(IEnumerable<ContaPrViewModel> result, int cliforid)
        {
            if (cliforid != 0)
            {
                result = from a in result where a.cliforid == cliforid select a;
            }
            return result;
        }


        public static string Set_ContaPr_Situacao_Filter(string query, string situacao, string tipodata, string de, string ate)

        {
            if (situacao != "")
            {
                switch (situacao)
                {
                    // Todas
                    case "1": break;
                    // Em atraso
                    case "2": query += " and CAST(getdate() AS DATE) > CAST(cpr." + tipodata + " as DATE)  and cpr.situacao = 'A' "; break;
                    //Hoje
                    case "3": query += " and CAST(cpr." + tipodata + " as DATE) = CAST(getdate() as DATE) "; break;
                    // Este mês
                    case "4": query += " and month(cpr." + tipodata + ") = month(getdate())  and year(cpr." + tipodata + ") = year(getdate())"; break;
                    // Recorrentes
                    case "5": query += " and recorrente = 1 "; break;
                    // Pagas
                    case "6": query += " and cpr.situacao='P' "; break;
                    // Arquivadas
                    case "7":
                        string ArchiveYear = "";
                        if (de != "" && ate == "")
                        {
                            ArchiveYear = DateTime.Parse(usdate(de)).Year.ToString();

                            query += "and cpr." + tipodata + " = CAST('" + usdate(de) + "' as DATE) ";
                            break;
                        }
                        else
                        {
                            if (de == "" && ate != "")
                            {
                                ArchiveYear = DateTime.Parse(usdate(ate)).Year.ToString();
                                query += "and cpr." + tipodata + " = CAST('" + usdate(ate) + "' as DATE) ";

                            }
                            else
                            {
                                // Se De e Ate estiverem em branco pega arquivados do ano anterior
                                if (de == "" && ate == "")
                                {
                                    ArchiveYear = (DateTime.Now.Year).ToString();
                                    query += "and cpr." + tipodata + " >= CAST('" + usdate(de) + "' as DATE)  " + "and cpr." + tipodata + " <= CAST('" + usdate(ate) + "' as DATE) ";

                                }
                                else
                                {
                                    ArchiveYear = DateTime.Parse(usdate(de)).Year.ToString();
                                    query += "and cpr." + tipodata + " >= CAST('" + usdate(de) + "' as DATE)  " + "and cpr." + tipodata + " <= CAST('" + usdate(ate) + "' as DATE) ";
                                }

                            }

                        }
                        query = query.Replace("contaPRs", "contaPRs_" + ArchiveYear);
                        break;
                }
            }
            return query;
        }
        public static string Set_Order(string mode, string query, string sort, string tipodata)
        {
            if (mode == "categoria")
            {
                query += " order by ca.nome,c.nome";
            }
            else
            {

                switch (sort)
                {
                    case "nome":
                        query += " order by c.nome"; break;
                    case "categoria":
                        query += " order by ca.nome"; break;
                    case "valor":
                        query += " order by cpr.valor"; break;
                    case "vencimento":
                    case "dtpagto":
                    case "dtvencto":
                        query += " order by cpr." + tipodata + ""; break;
                    default:
                        query += " order by cpr." + tipodata + " ,cliforid,cpr.contagrupo desc"; break;
                }


            }

            return query;
        }
        #endregion

        #region Propostas
        public static IEnumerable<PropostaViewModel> Get_Propostas_Data(ApplicationDbContext context, int isproject, string ano = "0")
        {
            var tipoParameter = new SqlParameter("@isproject", isproject);
            if (ano == "0")
                return context.Database.SqlQuery<PropostaViewModel>("spPropostas @isproject", tipoParameter).AsEnumerable<PropostaViewModel>();
            else
                return context.Database.SqlQuery<PropostaViewModel>("spPropostas_" + ano + " @isproject", tipoParameter).AsEnumerable<PropostaViewModel>();
        }
        public static IEnumerable<PropostaViewModel> Get_Propostas_By_Period(IEnumerable<PropostaViewModel> result, string de, string ate, bool useUsDateFormat = false)
        {

            if (de != "" && ate == "")
            {
                if (useUsDateFormat)
                    result = from a in result where DateTime.Parse(a.dtcad) >= DateTime.Parse(de) select a;
                else
                    result = from a in result where DateTime.Parse(a.dtcad) >= DateTime.Parse(de) select a;
            }
            else
            {
                if (de == "" && ate != "")
                {
                    if (useUsDateFormat)
                        result = from a in result where DateTime.Parse(usdate(a.dtcad)) <= DateTime.Parse(usdate(ate)) select a;
                    else
                        result = from a in result where DateTime.Parse(a.dtcad) <= DateTime.Parse(ate) select a;
                }
                else
                {
                    if (de != "" && ate != "")
                    {
                        if (useUsDateFormat)
                        {
                            result = from a in result where DateTime.Parse(usdate(a.dtcad)) >= DateTime.Parse(usdate(de)) && DateTime.Parse(usdate(a.dtcad)) <= DateTime.Parse(usdate(ate)) select a;
                        }
                        else
                        {
                            result = from a in result where DateTime.Parse(a.dtcad) >= DateTime.Parse(de) && DateTime.Parse(a.dtcad) <= DateTime.Parse(ate) select a;
                        }
                    }
                }
            }

            return result;
        }
        public static IEnumerable<PropostaViewModel> Set_Propostas_Category_Filter(IEnumerable<PropostaViewModel> result, int categoria)
        {
            if (categoria != 0)
            {
                result = from a in result where a.categoriaid == categoria select a;
            }
            return result;
        }
        public static IEnumerable<PropostaViewModel> Set_Propostas_Cliente_Filter(IEnumerable<PropostaViewModel> result, int cliente)
        {
            if (cliente != 0) result = from a in result where a.clienteid == cliente select a;

            return result;
        }
        public static IEnumerable<PropostaViewModel> Set_Propostas_Situacao_Filter(IEnumerable<PropostaViewModel> result, int situacao)

        {
            if (situacao != 0) return from a in result where a.situacaoid == situacao select a; ;

            return result;
        }
        public static IEnumerable<PropostaViewModel> Set_Propostas_Reponsavel_Filter(IEnumerable<PropostaViewModel> result, string responsavelid)
        {
            if (!String.IsNullOrEmpty(responsavelid)) return from a in result where a.responsavelid == responsavelid select a; ;

            return result;
        }
        public static string usdate(string text)
        {

            return text.Substring(3, 2) + "/" + text.Substring(0, 2) + "/" + text.Substring(6, 4);
        } 
        #endregion

        public static IEnumerable<OrcamentoVm> ListaOrcamento(ApplicationDbContext context,string ano, string tipo ="P")
        {
            string query = "P_AtualizaOrcamento";
            if (!String.IsNullOrWhiteSpace(ano))
            {
                if(ano== DateTime.Now.Year.ToString())
                    query = String.Format("{0} {1},'{2}'", query, ano, tipo);
                else
                    query = String.Format("{0}{1} {2},'{3}'", query, ano, ano, tipo);

                //var  x = context.Database.SqlQuery<OrcamentoVm>(query).AsEnumerable<OrcamentoVm>();
                return context.Database.SqlQuery<OrcamentoVm>(query).AsEnumerable<OrcamentoVm>();

            }
            else
            {
                return null;
            }
           
        }
    }

}
using Metasoft.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Metasoft.ViewModels;
using Metasoft.Models.Extensions;

namespace Metasoft.Infrastructure
{
    public class Repository : IRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ApplicationDbContext Dbcontext()
        {
            return new ApplicationDbContext();
        }
        public ClienteContaPrViewModel ClienteContaPr(int? contaprid)
        {
            try
            {

                string query = "Select " +
                                "cr.contaprid,cr.cliforid,cr.valor," +
                                "convert(varchar(25), cr.vencimento, 103) vencimento," +
                                "convert(varchar(25), getdate(), 103) emissao," +
                                "cr.descricao,cr.noordem," +
                                "c.nome clientenome,isnull(c.cnpj,'') cnpj ,isnull(c.ie,'') ie,c.endereco,c.bairro,c.cidade,c.estado,c.cep " +
                                "from contaprs cr " +
                                "inner join clientes c on cr.cliforid = c.clienteid " +
                                "where cr.contaprid = " + contaprid.ToString();
                IEnumerable<ClienteContaPrViewModel> x = context.Database.SqlQuery<ClienteContaPrViewModel>(query);
                return x.ToList().FirstOrDefault();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public IEnumerable<NfForCrViewModel> GetDetalhes_e_Nfs_Proposta(int propostaid)
        {
            string query = "select c.nf,convert(char(10), c.dtcad,103) emissao,convert(char(10), c.vencimento,103) vencimento," +
                           "isnull(convert(char(10),c.dtpagto,103),'') dtpagto,convert(decimal(15,2),c.valor) valor," +
                           "isnull(convert(char(10),dtpagto,103),'') dtpagto," +
                           "c.descricao,c.noordem " +
                            "from contaprs c " +
                           "where c.propostaid = " + propostaid.ToString();
            IEnumerable<NfForCrViewModel> x = context.Database.SqlQuery<NfForCrViewModel>(query);
            return x.ToList();
        }

        #region Cientes
        public IEnumerable<ClienteViewModel> ClientesAll(string tipo, string situacao = "")
        {

            string pagorrec = (tipo == "C") ? "R" : "P";
            try
            {
                string query = "Select c.clienteid,c.bairro,c.cidade,c.estado,c.cep,c.cnpj, c.complemento," +
                                "c.endereco,c.nome, c.dtcad,c.email,c.fone,c.site,c.tipo,c.f0800," +
                                "(Select count(*) from contaprs where cliforid = c.clienteid and tipo = '" + pagorrec + "' and situacao = 'A' " +
                                "and cast(vencimento  as date) < cast(getdate() as date) ) atrasados, " +
                                "(Select count(*) from contaprs where cliforid = c.clienteid and tipo = '" + pagorrec + "' and situacao = 'A' " +
                                "and cast(vencimento  as date) >= cast(getdate() as date)) pagamentos " +
                                "from clientes c " +
                                "where c.tipo = '" + tipo + "'";
                query = "Select clienteid,bairro,cidade,estado,cep,isnull(cnpj,'') cnpj,isnull(complemento,'') complemento,endereco,nome,dtcad,email,fone, isnull(site,'') site, tipo,f0800,atrasados,pagamentos from(" + query + ") Clientes ";

                if (!String.IsNullOrEmpty(situacao))
                {
                    switch (situacao)
                    {
                        case "A":
                            query += " where atrasados > 0 ";
                            break;
                        case "D":
                            query += " where atrasados = 0 ";
                            break;
                        default:
                            query += "";
                            break;
                    }

                }

                query += " order by nome";

                IEnumerable<ClienteViewModel> x = context.Database.SqlQuery<ClienteViewModel>(query);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public IEnumerable<CliForViewModel> CliForAll(string tipo = "")
        {
            if (String.IsNullOrEmpty(tipo))
            {
                return from c in context.Clientes orderby c.nome select new CliForViewModel { cliforid = c.clienteid, nome = c.nome };
            }
            else
            {
                return from c in context.Clientes where c.tipo == tipo orderby c.nome select new CliForViewModel { cliforid = c.clienteid, nome = c.nome };
            }

        }
        public Cliente GetCliFor(int? id)
        {
            var cliente = context.Clientes.Find(id);
            return cliente;

        }
        public Boolean AlterarCliente(Cliente cliente)
        {
            try
            {
                context.Entry(cliente).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public bool ExcluirCliente(int id)
        {

            var cliente = context.Clientes.Find(id);
            context.Clientes.Remove(cliente);
            context.SaveChanges();
            return true;
        }
        public bool ContatoDelete(int id)
        {
            var contato = context.Contatoes.Find(id);
            context.Contatoes.Remove(contato);
            context.SaveChanges();
            return true;
        }
        int IRepository.NovoCliente(Cliente cliente, string tipo)
        {
            try
            {
                cliente.tipo = tipo;
                context.Clientes.Add(cliente);
                context.SaveChanges();
                return cliente.clienteid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public IEnumerable<Contato> GetContatos(int clienteid)
        {
            string query = "Select contatoid,clienteid,nome,fone,celular,email,sexo from contatoes where clienteid = " + clienteid.ToString();
            IEnumerable<Contato> x = context.Database.SqlQuery<Contato>(query);
            return x.ToList();
        }

        public IEnumerable<ContaPrViewModel> GetContas(int clienteid, string tipo, string atrasadas = "false")
        {
            string query = "Select " +
                           "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                           "c.nome clifornome, " +
                           "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                           "cpr.npar,cpr.valor, " +
                           "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                           "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                           "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                           "from contaPRs cpr " +
                           "inner join Clientes c on c.clienteid = cpr.cliforid " +
                           "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                           "left join Propostas p on p.propostaid = cpr.propostaid " +
                           "where cpr.cliforid = " + clienteid + " and cpr.situacao = 'A' ";
            if (atrasadas == "true")
            {
                query += "and (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE)) and cpr.tipo = '" + tipo + "'";
            }
            else
            {
                query += "and CAST(cpr.vencimento as DATE) >= (CAST(getdate() AS DATE))   and cpr.tipo = '" + tipo + "'";
            }
            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();
        }
        public bool FindClienteByName(string name)
        {
            var x = from c in context.Clientes where c.nome.Equals(name) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FindClienteByCnpj(string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj)) { return false; }
            var x = from c in context.Clientes where c.cnpj.Equals(cnpj) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<CliForViewModel> GetClientesWithPropostas()
        {
            //string query = "Select clienteid cliforid,nome from clientes " +
            //                "where tipo = 'C' " +
            //                "and clienteid in (" +
            //                "select clienteid from propostas where dtaceite is not null and isproject = 0" +
            //                ") order by nome;";

            string query = "Select clienteid cliforid,nome from clientes where tipo = 'C' order by nome;";

            IEnumerable<CliForViewModel> x = context.Database.SqlQuery<CliForViewModel>(query);
            return x.ToList();
        }
        public ClienteViewModel GetClienteDetail(int clienteid)
        {
            string query = "select nome,cnpj,ie,endereco,complemento,bairro,cidade,cep,estado,fone,f0800,email,[site] " +
                           "from Clientes " +
                           "where clienteid=" + clienteid.ToString();
            IEnumerable<ClienteViewModel> x = context.Database.SqlQuery<ClienteViewModel>(query);
            return x.ToList().FirstOrDefault();
        }

        int IRepository.NovoContato(Contato contato)
        {
            try
            {
                context.Contatoes.Add(contato);
                context.SaveChanges();
                return contato.contatoid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        #endregion

        #region Contratos
        public IEnumerable<ContratosViewModel> ContratosAll(string tipodata, string de, string ate, int tipocontrato, int clifor, int situacao)
        {
            string query = "select co.id,c.clienteid,c.nome clientenome,i.id indiceid,i.nome indice," +
                            "convert(varchar(25), r.dtinicio, 103) dtinicio," +
                            "convert(varchar(25), r.dttermino, 103) dttermino," +
                            "convert(varchar(25), r.proxreajuste, 103) proxreajuste," +
                            "convert(varchar(25), r.iniciosv, 103) iniciosv," +
                            "r.valor,r.observacao,r.prazo," +
                            "s.id situacaoid, s.nome situacao, s.cor,tc.nome tipocontrato, " +
                            "(Select count(*) from contaprs where contratoid = r.id) contas " +
                            "from contratoes co " +
                            "inner join Clientes c on c.clienteid = co.clienteid " +
                            "inner join Indices i on i.id = co.indice " +
                            "inner join Renovacaos r on r.contratoid = co.id and r.situacaoid = 1 " +
                            "inner join Categorias tc on tc.categoriaid = co.tipocontrato " +
                            "inner join SituacaoRenovacaos s on s.id = r.situacaoid " +
                            "where co.situacaoid<>99 ";

            if (de != "" && ate == "") { query += "and CAST(r." + tipodata + " as DATE) = CAST('" + usdate(de) + "' as DATE) "; }
            if (de == "" && ate != "") { query += "and CAST(r." + tipodata + " as DATE) <= CAST('" + usdate(ate) + "' as DATE) "; }
            if (de != "" && ate != "") { query += "and (CAST(r." + tipodata + " as DATE) >= CAST('" + usdate(de) + "' as DATE) and r." + tipodata + " <= CAST('" + usdate(ate) + "' as DATE)) "; }


            if (tipocontrato != 0) { query += " and co.tipocontrato=" + tipocontrato.ToString(); }
            if (clifor != 0) { query += " and c.clienteid=" + clifor.ToString(); }
            //if (situacao != 0) { query += " and s.situacaoid=" + situacao.ToString(); }

            IEnumerable<ContratosViewModel> x = context.Database.SqlQuery<ContratosViewModel>(query);
            return x.ToList();
        }
        public int NovoContrato(ContratoViewModel contrato)
        {
            try
            {
                /* Primeiro adiciona contatro mae */
                Contrato newcontrato = new Contrato
                {
                    clienteid = contrato.clienteid,
                    descricao = contrato.descricao,
                    situacaoid = 1,
                    tipocontrato = contrato.tipocontrato,
                    indice = contrato.indice
                };

                /*  Agora adiciona primeiroi historico com situacao = 1 ( ativo )*/

                contrato.proxreajuste = null;
                if (contrato.iniciosv != null)
                {
                    contrato.proxreajuste = contrato.iniciosv.Value.AddMonths(contrato.prazo);
                }
                context.Contratoes.Add(newcontrato);
                context.SaveChanges();

                Renovacao renovacao = new Renovacao
                {
                    contratoid = newcontrato.id,
                    dtinicio = contrato.dtinicio,
                    dttermino = contrato.dttermino,
                    iniciosv = contrato.iniciosv,
                    observacao = contrato.observacao,
                    prazo = contrato.prazo,
                    proxreajuste = contrato.proxreajuste,
                    valor = contrato.valor,
                    situacaoid = 1,
                    dtcad = DateTime.Today
                };


                context.Renovacaos.Add(renovacao);
                context.SaveChanges();

                return newcontrato.id;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }

        public ContratoViewModel GetContrato(int id)
        {
            Contrato contrato = (from c in context.Contratoes where c.id == id select c).FirstOrDefault();
            Renovacao renovacao = (from r in context.Renovacaos where r.contratoid == id && r.situacaoid == 1 select r).FirstOrDefault();
            ContratoViewModel acontrato = new ContratoViewModel
            {
                id = contrato.id,
                dtcad = contrato.dtcad,
                renovacaoid = renovacao.id,
                clienteid = contrato.clienteid,
                descricao = contrato.descricao,
                tipocontrato = contrato.tipocontrato,
                indice = contrato.indice,
                dtinicio = renovacao.dtinicio,
                dttermino = renovacao.dttermino,
                iniciosv = renovacao.iniciosv,
                observacao = renovacao.observacao,
                prazo = renovacao.prazo,
                proxreajuste = renovacao.proxreajuste,
                valor = renovacao.valor,
                situacaoid = contrato.situacaoid,
                situacaorenovacaoid = renovacao.situacaoid
            };

            return acontrato;
        }
        public Boolean AlterarContrato(ContratoViewModel contrato)
        {

            Contrato altcontrato = new Contrato
            {
                id = contrato.id,
                dtcad = contrato.dtcad,
                descricao = contrato.descricao,
                clienteid = contrato.clienteid,
                indice = contrato.indice,
                tipocontrato = contrato.tipocontrato,
                situacaoid = contrato.situacaoid
            };

            try
            {
                context.Entry(altcontrato).State = EntityState.Modified;
                context.SaveChanges();

                try
                {

                    contrato.proxreajuste = null;
                    if (contrato.iniciosv != null)
                    {
                        contrato.proxreajuste = contrato.iniciosv.Value.AddMonths(contrato.prazo);
                    }
                    Renovacao renovacao = new Renovacao
                    {
                        id = contrato.renovacaoid,
                        dtcad = contrato.dtcad,
                        contratoid = contrato.id,
                        dtinicio = contrato.dtinicio,
                        dttermino = contrato.dttermino,
                        iniciosv = contrato.iniciosv,
                        observacao = contrato.observacao,
                        prazo = contrato.prazo,
                        proxreajuste = contrato.proxreajuste,
                        valor = contrato.valor,
                        situacaoid = contrato.situacaorenovacaoid
                    };
                    context.Entry(renovacao).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception exc)
                {
                    var msg = exc.Message;
                    return false;

                }

            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public Boolean ExcluirContrato(int id)
        {
            var item = context.Contratoes.Find(id);
            if (item != null)
            {
                try
                {
                    UpdateTable("Delete from Renovacaos  where contratoid=" + item.id.ToString());
                    context.Contratoes.Remove(item);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception exc)
                {
                    var msg = exc.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<HistoricoContratoViewModel> GetHistoricoContrato(int id)
        {
            string query = "Select r.id," +
                            "convert(varchar(25), r.dtcad, 103) dtcad," +
                            "convert(varchar(25), r.dtinicio, 103) inicio," +
                            "convert(varchar(25), r.dttermino, 103) termino," +
                            "convert(varchar(25),  r.proxreajuste, 103) reajuste," +
                            "r.valor,r.observacao,sr.nome situacao " +
                            "from Renovacaos r " +
                            "inner join Contratoes co on co.id = r.contratoid " +
                            "inner join Clientes c on c.clienteid = co.clienteid " +
                            "inner join SituacaoRenovacaos sr on sr.id = r.situacaoid " +
                            "where r.contratoid=" + id.ToString();
            IEnumerable<HistoricoContratoViewModel> x = context.Database.SqlQuery<HistoricoContratoViewModel>(query);
            return x.ToList();
        }

        public int CountContasReceberForContrato(int id)
        {
            /* Pega contas pelo numero da renovacao */
            Renovacao renovacao = (from r in context.Renovacaos where r.contratoid == id && r.situacaoid == 1 select r).FirstOrDefault();
            return (from c in context.ContaPRs where c.contratoid.Equals(renovacao.id) select c).Count();
        }

        public IEnumerable<ContaPrViewModel> GetContasContrato(int id)
        {

            /* Primeiro pega numero da renovacao */
            Renovacao renovacao = (from r in context.Renovacaos where r.contratoid == id && r.situacaoid == 1 select r).FirstOrDefault();
            string query = "Select " +
                           "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                           "c.nome clifornome, " +
                           "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                           "cpr.npar,cpr.valor, " +
                           "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                           "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                           "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                           "from contaPRs cpr " +
                           "inner join Clientes c on c.clienteid = cpr.cliforid " +
                           "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                           "left join Propostas p on p.propostaid = cpr.propostaid " +
                           "where cpr.contratoid = " + renovacao.id;

            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();
        }
        #endregion

        #region Categorias

        public IEnumerable<Categoria> CategoriasAll()
        {
            return from c in context.Categorias orderby c.nome select c;
        }
        public IEnumerable<Categoria> CategoriasByTipo(string tipo)
        {
            return from c in context.Categorias where c.tipo == tipo orderby c.nome select c;
        }
        public Categoria CategoriaById(int id)
        {
            return (from c in context.Categorias where c.categoriaid == id orderby c.nome select c).FirstOrDefault();
        }
        public int NovaCategoria(Categoria categoria)
        {
            try
            {
                context.Categorias.Add(categoria);
                context.SaveChanges();
                return categoria.categoriaid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public bool ExcluirCategoria(int id)
        {
            var categoria = context.Categorias.Find(id);
            if (categoria != null)
            {
                context.Categorias.Remove(categoria);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public Categoria GetCategoria(int id)
        {
            Categoria categoria = context.Categorias.Find(id);
            return categoria;
        }
        public Boolean AlterarCategoria(Categoria categoria)
        {
            try
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public Boolean CategoriaExists(string nome)
        {
            return (from c in context.Categorias where c.nome.Equals(nome) select c).Count() > 0;
        }
        public Boolean CategoriaJaExiste(string nome)
        {
            var x = from c in context.Categorias where c.nome.ToLower().Equals(nome.ToLower()) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Comercial
        public IEnumerable<ComercialViewModel> ComercialAll(int mes, int ano)
        {
            string query = "Select c.id,c.nome,c.email,isnull(r.faturado,0) faturado " +
                            "from comercials c " +
                            "left Join (" +
                            "Select  r.comercialid,cast(sum(((p.valor-r.insumos) * .2) * (r.percentual/100)) as decimal(15,2)) faturado " +
                            "from Remuneracaos r " +
                            "inner join Propostas p on p.propostaid = r.propostaid " +
                            " where month(dtlan) = " + mes.ToString() + " and year(dtlan)= " + ano.ToString() + " " +
                            "group by r.comercialid " +
                            ") r on r.comercialid = c.id";
            IEnumerable<ComercialViewModel> x = context.Database.SqlQuery<ComercialViewModel>(query);
            return x.ToList();
        }
        public int NovoComercial(Comercial item)
        {
            try
            {
                context.Comercials.Add(item);
                context.SaveChanges();
                return item.id;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean ComercialExists(Comercial comercial)
        {
            Boolean nomeexists = (from c in context.Comercials where c.nome.Equals(comercial.nome) select c).Count() > 0;
            Boolean emailexists = false;
            if (!String.IsNullOrEmpty(comercial.email))
            {
                emailexists = (from c in context.Comercials where c.email.Equals(comercial.email) select c).Count() > 0;
            }
            if (nomeexists || emailexists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Comercial GetComercial(int id)
        {
            var x = (from c in context.Comercials where c.id == id select c).FirstOrDefault();
            return x;
        }
        public Boolean AlterarComercial(Comercial item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public Boolean ExcluirComercial(int id)
        {
            var item = context.Comercials.Find(id);
            if (item != null)
            {
                context.Comercials.Remove(item);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Contas Pagar Receber

        #region Refactoring
        public IEnumerable<ContaPrViewModel> GetAllContas(string tipo, string tipodata, string de = "", string ate = "", string ano = "", int categoria = 0, int cliforid = 0, string situacao = "", string mode = "", string sort = "", string responsavel = "")
        {
            bool useUsDateFormat = false;

            string fromYear = Get_Correct_Year(ano, de, ate, situacao, false);
            var tipoParameter = new SqlParameter("@tipo", tipo);
            IEnumerable<ContaPrViewModel> main = RepoLib.Get_ContaPrs_Data(context, fromYear, tipo);
            var y = (from b in main select b).AsEnumerable<ContaPrViewModel>();
            if (!String.IsNullOrEmpty(ano) && ano != "0")
            {
                de = "01/01/" + fromYear; ate = "31/12/" + fromYear;
            }

            // Aplicar filtros a dataset basico
            y = RepoLib.Apply_Situacao_ContaPr_Filter(y, situacao, tipodata, de, ate, useUsDateFormat);
            y = RepoLib.Get_Contas_By_Period(y, tipodata, de, ate, useUsDateFormat, false);
            if (categoria != 0) y = RepoLib.Set_Contas_Category_Filter(y, categoria);
            if (cliforid != 0) y = RepoLib.Set_Contas_CliFor_Filter(y, cliforid);
            // if  (!String.IsNullOrEmpty(responsavel))  y = RepoLib.Set_Responsavel_Filter(y, responsavel);
            return y.ToList();
        }
        public string Get_Correct_Year(string ano, string de = "", string ate = "", string situacao = "", bool useUsDateFormat = true)
        {
            if (situacao != "7") return "0";
            if (ano != "0") return ano;

            return "0";

        }
        public int GetNumeroNotaFiscal(int nfid) => context.NfNumeracaos.Where(n => n.NfIdReceber == nfid).Select(nf => nf.NfIdGerado).FirstOrDefault();

        #endregion


        #region Contas
        public IEnumerable<ContaPrViewModel> ContaPRsAll(string tipo, string tipodata, string de = "", string ate = "", string ano = "", int categoria = 0, int cliforid = 0, string situacao = "", string mode = "", string sort = "", string responsavel = "")
        {
            bool useUsDateFormat = false;
            string fromYear = RepoLib.Get_Correct_Year(ano, de, ate, situacao, false);
            var tipoParameter = new SqlParameter("@tipo", tipo);
            IEnumerable<ContaPrViewModel> main = RepoLib.Get_ContaPrs_Data(context, fromYear, tipo);
            var y = (from b in main select b).AsEnumerable<ContaPrViewModel>();
            if (!String.IsNullOrEmpty(ano) && ano != "0")
            {
                de = "01/01/" + fromYear; ate = "31/12/" + fromYear;
            }

            // Aplicar filtros a dataset basico
            y = RepoLib.Apply_Situacao_ContaPr_Filter(y, situacao, tipodata, de, ate, useUsDateFormat);
            y = RepoLib.Get_Contas_By_Period(y, tipodata, de, ate, useUsDateFormat, false);
            if (categoria != 0) y = RepoLib.Set_Contas_Category_Filter(y, categoria);
            if (cliforid != 0) y = RepoLib.Set_Contas_CliFor_Filter(y, cliforid);
            // if  (!String.IsNullOrEmpty(responsavel))  y = RepoLib.Set_Responsavel_Filter(y, responsavel);
            return y.ToList();
        }

        public IEnumerable<ContaPrExcelViewModel> ContaPRExcel(string tipo, string de = "", string ate = "", int categoria = 0, int fornecedor = 0, string situacao = "", string mode = "", string sort = "")
        {
            string query = "Select cpr.contaprid conta_id,convert(varchar(25), cpr.dtcad, 103) dtcad,cpr.npar,cpr.cliforid,cpr.propostaid proposta_id," +
                "c.nome clifor_nome, cpr.descricao,ca.nome categoria_nome,cpr.valor, " +
                "convert(varchar(25), cpr.vencimento, 103) vencimento, isnull(convert(varchar(25), cpr.dtpagto, 103),'') dtpagto, " +
                "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                "cpr.situacao,isnull(cpr.noordem,'01/01') noordem, isnull(n.nfid,'0') nf_id, isnull(nf,'') nf " +
                "from contaPRs cpr " +
                "inner join Clientes c on c.clienteid = cpr.cliforid " +
                "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                "left join Propostas p on p.propostaid = cpr.propostaid " +
                "left join Nfs n on n.contaprid = cpr.contaprid " +
                "where cpr.tipo = '" + tipo + "'  and cpr.archive = 0 ";

            if (de != "" && ate == "") { query += "and CAST(cpr.vencimento as DATE) = CAST('" + usdate(de) + "' as DATE) "; }
            if (de == "" && ate != "") { query += "and CAST(cpr.vencimento as DATE) <= CAST('" + usdate(ate) + "' as DATE) "; }
            if (de != "" && ate != "") { query += "and (CAST(cpr.vencimento as DATE) >= CAST('" + usdate(de) + "' as DATE) and cpr.vencimento <= CAST('" + usdate(ate) + "' as DATE)) "; }
            if (categoria != 0) { query += " and ca.categoriaid=" + categoria.ToString(); }
            if (fornecedor != 0) { query += " and cpr.cliforid=" + fornecedor.ToString(); }
            if (situacao != "")
            {
                switch (situacao)
                {
                    case "1": break;
                    case "2": query += " and CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE)  and cpr.situacao = 'A' "; break;
                    case "3": query += " and CAST(cpr.vencimento as DATE) = CAST(getdate() as DATE) "; break;
                    case "4": query += " and month(cpr.vencimento) = month(getdate())  and year(cpr.vencimento) = year(getdate())"; break;
                    case "5": query += " and recorrente = 1 "; break;
                    case "6": query += " and cpr.situacao='P' "; break;
                    case "7": query += " and cpr.archive = 1  "; break;
                }
            }
            if (mode == "categoria") { query += " order by ca.nome,c.nome"; }
            else
            {
                switch (sort)
                {
                    case "nome": query += " order by c.nome"; break;
                    case "categoria": query += " order by ca.nome"; break;
                    case "valor": query += " order by cpr.valor"; break;
                    case "vencimento": query += " order by cpr.vencimento"; break;
                    default: query += " order by cpr.vencimento ,cliforid,cpr.contagrupo desc"; break;
                }
            }
            IEnumerable<ContaPrExcelViewModel> x = context.Database.SqlQuery<ContaPrExcelViewModel>(query);
            return x.ToList();

        }
        IEnumerable<ContaPrViewModel> IRepository.ContaPRsPorCategoria(string tipo, string mes, string ano)
        {

            string query = "Select " +
                 "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                 "c.nome clifornome, " +
                 "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                 "cpr.npar,cpr.valor, " +
                 "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                 "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                 "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                 "from contaPRs cpr " +
                 "inner join Clientes c on c.clienteid = cpr.cliforid " +
                 "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                 "left join Propostas p on p.propostaid = cpr.propostaid " +
                 "where cpr.tipo = '" + tipo + "' ";

            query += " and ((month(vencimento)=" + mes + " and year(vencimento)=" + ano + "  ) or ( month(cpr.dtpagto)=" + mes + " and year(cpr.dtpagto)=" + ano + "))  ";

            //if (mes != "") { query += " and (month(vencimento)=" + mes + " or month(cpr.dtpagto)=" +mes + ") "; }
            //if (ano != "") { query += " and (year(vencimento)=" + ano + " or year(cpr.dtpagto)=" + ano + ") "; }

            query += " order by ca.nome,c.nome";

            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();

        }
        public int NovaContaPR(ContaPR contapr)
        {
            try
            {
                contapr.situacao = "A";
                contapr.dtcad = DateTime.Now;
                int parcelas = contapr.npar;

                if (contapr.recorrente)
                {
                    ContaAddMany(parcelas, contapr);
                }
                else
                {
                    context.ContaPRs.Add(contapr);
                    context.SaveChanges();
                    UpdateTable("Update contaprs set contagrupo =" + contapr.contaprid.ToString() + " where contaprid=" + contapr.contaprid.ToString());
                }
                return contapr.contaprid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        private void ContaAddMany(int qtd, ContaPR contapr)
        {
            string[] no = contapr.noordem.Split('/');

            int first = Int32.Parse(no[0]);
            int primeiraparcela = first;
            int last = Int32.Parse(no[1]);

            int contagrupo = 0;
            for (int i = first; i <= last; i++)
            {
                contapr.npar = i;
                contapr.noordem = first.ToString().PadLeft(2, '0') + "/" + last.ToString().PadLeft(2, '0');
                context.ContaPRs.Add(contapr);
                context.SaveChanges();
                if (i == primeiraparcela)
                {
                    contagrupo = contapr.contaprid;
                    contapr.contagrupo = contagrupo;
                    context.Entry(contapr).State = EntityState.Modified;
                    context.SaveChanges();
                }
                contapr.vencimento = contapr.vencimento.AddMonths(1);
                contapr.contagrupo = contagrupo;
                first += 1;

            }
        }
        public int NovaParcelaPR(ContaPR contapr)
        {
            try
            {
                contapr.situacao = "A";
                contapr.dtcad = DateTime.Now;
                context.ContaPRs.Add(contapr);
                context.SaveChanges();
                return contapr.contaprid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean AlterarContaApagar(ContaPR conta)
        {
            try
            {
                context.Entry(conta).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public Boolean AlterarConta(ContaPR conta)
        {
            try
            {
                context.Entry(conta).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public ContaPR FindConta(int? id)
        {

            var x = (from c in context.ContaPRs where c.contaprid == id select c).FirstOrDefault();
            var z = (from y in context.ContaPRs where y.contagrupo == x.contagrupo orderby y.noordem descending select y).FirstOrDefault();
            return z;
        }
        public bool NfJaExiste(int contaid)
        {
            var result = (from n in context.NfRecebers where n.ContaId == contaid select new { nfid = n.NfId }).FirstOrDefault();
            if (result != null)
            {
                return result.nfid > 0;
            }
            return false;
        }
        public NfReceberViewModel NovaNfAreceber(int contaid)
        {
            var NotaVm = CriaNfReceberViewModel(contaid);

            return NotaVm;
        }
        public ClienteContaNota ImprimirNotaFiscal(int contaprid)
        {

            ClienteContaNota ccn = new ClienteContaNota();
            ccn.Conta = context.ContaPRs.Where(c => c.contaprid == contaprid).Select(c => c).FirstOrDefault();
            ccn.Cliente = context.Clientes.Where(c => c.clienteid == ccn.Conta.cliforid).Select(c => c).FirstOrDefault();
            NfReceber nota = context.NfRecebers.Where(n => n.ContaId == contaprid).Select(n => n).FirstOrDefault();
            if (nota == null)
            {
                nota = new NfReceber
                {
                    ContaId = ccn.Conta.contaprid,
                    NumeroOrdem = ccn.Conta.noordem,
                    CliForId = ccn.Conta.cliforid,
                    Vencimento = ccn.Conta.vencimento,
                    NumPrint = 0,
                    Situacao = "A"
                };
                var r = context.NfRecebers.Add(nota);
                context.SaveChanges();
            }

            ccn.Nota = nota;

            ccn.Detalhes = context.NfReceberDetalhes.Where(d => d.Nfid == ccn.Nota.NfId).Select(dt => dt).ToList();
            /*Nota fiscal já existe recupera detalhes */
            if (ccn.Detalhes.Count == 0)
            {
                /*Cria registro da NotaFiscal & detalhes*/
                string descricao ; 
                int quantidade ;
                decimal valor; ;

                for (int i = 0; i < 19; i++)
                {
                    // coloca dados da conta
                    descricao = "";
                    quantidade = 0;
                    valor = 0;

                    if (i == 0) {
                       quantidade = 1;
                       descricao = ccn.Conta.descricao;
                       valor = ccn.Conta.valor;
                    }
  
                    var d = new NfReceberDetalhe { 
                        Nfid = ccn.Nota.NfId, Ordem = i + 1, 
                        Quantidade = quantidade, 
                        Unitario = valor, 
                        Descricao = descricao };
                    context.NfReceberDetalhes.Add(d);

                }
                context.SaveChanges();
                ccn.Detalhes = context.NfReceberDetalhes.Where(d => d.Nfid == ccn.Nota.NfId).Select(dt => dt).ToList();
            }



            return ccn;
        }
        public int CriaProximoNumeroNotaFiscal(int nfid)
        {
            var numeracao = new NfNumeracao { NfIdReceber = nfid, Criacao = DateTime.Now, Status = 1 };
            try
            {
                context.NfNumeracaos.Add(numeracao);
                context.SaveChanges();
                var nfreceber = context.NfRecebers.Where(nr => nr.NfId == nfid).Select(nr => nr).FirstOrDefault();
                nfreceber.NumeroNf = numeracao.NfIdGerado;
                context.SaveChanges();

                ContaPR conta = context.ContaPRs.Where(c => c.contaprid == nfreceber.ContaId).FirstOrDefault();
                conta.nfid = nfreceber.NfId;
                context.SaveChanges();

                return numeracao.NfIdGerado;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        private NfReceber GetNota(int nfid) => context.NfRecebers.Where(n => n.NfId == nfid).Select(n => n).FirstOrDefault();
        public ContaPR GetConta(int id) => context.ContaPRs.Where(c => c.contaprid == id).Select(c => c).FirstOrDefault();
        private Cliente GetCliente(int id) => context.Clientes.Where(c => c.clienteid == id).Select(c => c).FirstOrDefault();
        private List<NfReceberDetalhe> GetDetalhes(int nfid) => context.NfReceberDetalhes.Where(d => d.Nfid == nfid).Select(d => d).ToList();
        private void AtualizaNfIdDaConta(ContaPR conta, int nfid)
        {
            conta.nfid = nfid;
            Grava();
        }
        public bool ConfirmaRecebimento(int id, string dataquitacao)
        {
            if (QuitaConta(id, "R", dataquitacao))
                return true;

            return false;
        }
        public bool ConfirmaPagamento(int id, string dataquitacao)
        {
            if (QuitaConta(id, "P", dataquitacao))
                return true;

            return false;
        }
        private bool QuitaConta(int id,string tipo, string data)
        {
            try
            {
                var conta = context.ContaPRs.Where(c => c.contaprid == id).FirstOrDefault();
                if (conta != null)
                {
                    var _data = DateTime.Parse(data);
                    conta.dtpagto = _data;
                    conta.situacao = "P";
                    //AtualizaSaldoMes(tipo, conta.valor);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                var mes = exc.InnerException;
                return false;
            }
        }
        private void AtualizaSaldoMes(string tipo, decimal valor)
        {
            var saldo =  context.SaldoAnuals.Where(s =>  s.Ano == DateTime.Now.Year).FirstOrDefault();
            if (saldo != null)
                if(tipo == "R")
                   saldo.Saldo += valor;
                else
                   saldo.Saldo -= valor;

        }
        #endregion

        #region Extrato
        public IQueryable<LinhaExtrato> LancamentosPagosNoAno(int ano, string inicioEm)
        {


            string dinicio = "01/01/2021";
            if (!String.IsNullOrWhiteSpace(inicioEm))
                dinicio = inicioEm;


            var query =
                "select contaprid ContaId,vencimento Vencimento, dtpagto Pagamento, tipo Tipo, descricao Descricao, valor Valor " +
                "from ContaPrs " +
                "where situacao = 'P' " + "" +
                "and  YEAR(dtpagto) = " + ano.ToString() + " " +
                "and dtpagto > CAST('" + dinicio + "' as Date)";
            IEnumerable<LinhaExtrato> baselancamentos = context.Database.SqlQuery<LinhaExtrato>(query);


            return baselancamentos
                    .Select(c => new LinhaExtrato
                    {
                        ContaId = c.ContaId,
                        Vencimento = c.Vencimento,
                        Pagamento = (DateTime)c.Pagamento,
                        Tipo = c.Tipo,
                        Descricao = c.Descricao,
                        Valor = c.Valor
                    }).AsQueryable().OrderBy(ord => ord.Pagamento);

        }
        public bool SaveNewDataInicio(int ano,int id, string datainicio)
        {

            var saldo = context.SaldoAnuals.Where(s => s.Ano == ano).FirstOrDefault();
            if (saldo != null)
            {
                var _data = DateTime.Parse(datainicio);
                saldo.Inicio = _data;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public SaldoAnual SaldoAnterior(int ano)
        {
            var _saldoAnoAnterior = SaldoInicialAno(ano);
            var inicio = "";

            if (_saldoAnoAnterior.Inicio != null)
                inicio = _saldoAnoAnterior.Inicio.ToShortDateString();

            return _saldoAnoAnterior;
        }

        public ExtratoViewModel BaseLancamentos(int mes, int ano)
        {
            var _saldoAnterior = SaldoAnterior(ano);
            var _linhas = LancamentosPagosNoAno(ano, _saldoAnterior.Inicio.ToShortDateString());
            var _extrato = new ExtratoViewModel();

            _extrato.Linhas = new List<LinhaExtrato>();
            _extrato.Linhas = _linhas.OrderBy(ord => ord.Pagamento).ToList();
            _extrato.SaldoInicial= _saldoAnterior.Saldo;
            _extrato.Id = _saldoAnterior.Id;

            return _extrato;
        }

        public ExtratoViewModel Extrato(int mes,int ano)
        {
            // Calcula o saldo atual deste o inicio
            var _extrato = BaseLancamentos(mes, ano);
            decimal saldodia = _extrato.SaldoInicial;
            foreach (var linha in _extrato.Linhas)
            {

                if (linha.Tipo == "R")
                    saldodia += linha.Valor;
                else
                    saldodia -= linha.Valor;

                linha.Saldo = saldodia;

               
            }
            _extrato.SaldoDia = saldodia;
            var _extratoMes = _extrato.Linhas.Where(m => m.Pagamento.Month == mes).OrderBy(ord => ord.Pagamento).AsQueryable().ToList();
            _extrato.Linhas = _extratoMes;
            return _extrato;
        }

        public SaldoAnual SaldoInicialAno(int ano)
        {
            var s = context.SaldoAnuals
                   .Where(l => l.Ano == ano).FirstOrDefault();
            return s;
        }


        private DateTime UltimoDiaDoMes(int mes, int ano)
        {
           
            int lastDay =  DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var dia = DateTime.Parse(string.Format("{0}/{1}/{2}", lastDay, mes, ano));
            return dia;
        }
        #endregion

        private NfReceberViewModel CriaNfReceberViewModel(int contaid)
        {
            var NotaVm = new NfReceberViewModel();
            var conta = GetConta(contaid);
            NotaVm.Detalhes = new List<NfReceberDetalhe>();
            NotaVm.Clifor = GetCliente(conta.cliforid);

            if (conta.nfid < 1)
            {
                try
                {
                    NotaVm.Nota = CriaNota(conta);
                    conta.nfid = NotaVm.Nota.NfId;
                    context.SaveChanges();
                    AtualizaNfIdDaConta(conta, NotaVm.Nota.NfId);
                    NotaVm.Detalhes = CriaDetalhesNf(conta.nfid);
                                   }
                catch (Exception exc)
                {
                    var msg = exc.Message;
                    return null;
                }
            }
            else
            {
                NotaVm.Nota = GetNota(conta.nfid);
                NotaVm.Detalhes = GetDetalhes(NotaVm.Nota.NfId);
            }
            return NotaVm;
        }

        private NfReceber CriaNota(ContaPR conta)
        {
            try
            {
                var nota = new NfReceber()
                {
                    NumeroOrdem = conta.noordem,
                    NumeroNf = 0,
                    ContaId = conta.contaprid,
                    CliForId = conta.cliforid,
                    Emissao = DateTime.Now,
                    Vencimento = conta.vencimento,
                    NumPrint = 0,
                    Situacao = conta.situacao
                };

                /// Cadastra Nota
                context.NfRecebers.Add(nota);
                Grava();
                return nota;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        private List<NfReceberDetalhe> CriaDetalhesNf(int nfid)
        {
            var detalhe = new NfReceberDetalhe();
            var detalhes = new List<NfReceberDetalhe>();
            for (int i = 1; i < 20; i++)
            {
                detalhe = new NfReceberDetalhe() { Nfid = nfid };
                detalhe.Ordem = i;
                detalhes.Add(detalhe);
                context.NfReceberDetalhes.Add(detalhe);
            }
            context.SaveChanges();
            return detalhes;

        }
        public void NotaFiscalUpdateItemQtd(int nfid, int line, int qtde)
        {
            var nf = (from i in context.NfReceberDetalhes where i.Nfid == nfid && i.Ordem == line select i).FirstOrDefault();
            if (nf != null)
            {
                nf.Quantidade = qtde;
                context.SaveChanges();
            }

        }
        public void NotaFiscalUpdateItemDescricao(int nfid, int line, string descricao)
        {
            var nf = (from i in context.NfReceberDetalhes where i.Nfid == nfid && i.Ordem == line select i).FirstOrDefault();
            if (nf != null)
            {
                nf.Descricao =descricao;
                context.SaveChanges();
            }
        }
        public void NotaFiscalUpdateItemUnitario(int nfid, int line, string unitario)
        {

            try
            {
                if (!String.IsNullOrEmpty(unitario))
                {
                    var valor = Decimal.Parse(unitario.Replace("R$",""));
                    var nf = (from i in context.NfReceberDetalhes where i.Nfid == nfid && i.Ordem == line select i).FirstOrDefault();
                    if (nf != null)
                    {
                        nf.Unitario = valor;
                        context.SaveChanges();
                    }
                }
               }
            catch (Exception)
            {

               
            }
        }

        public int AddNfDetail(NFDetail nfdetail)
        {
            try
            {
                context.Nfdetails.Add(nfdetail);
                context.SaveChanges();
                return nfdetail.nfdetailid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }

        
        public ClienteContaPrViewModel ImprimirNotaFiscal_old(int contaprid)
        {



            try
            {
                string query = "Select nfid,n.no noordem,n.cliforid,convert(varchar(25), n.vencimento, 103) vencimento ,na.nome, c.nome clientenome,c.cnpj,isnull(c.ie,'') ie,c.endereco," +
                 "isnull(c.complemento,'') complemento,c.bairro,c.cidade,c.estado,c.cep,c.fone,isnull(c.email,'') email,convert(varchar(25), n.dtemissao, 103) emissao " +
                 "from nfs n " +
                 "inner join contaprs cp on cp.contaprid = n.contaprid " +
                 "inner join clientes c on c.clienteid = n.cliforid " +
                 "inner join Naturezas na on na.naturezaid = n.natureza " +
                 "where  n.situacao = 'A' and cp.contaprid =" + contaprid.ToString();

                ClienteContaPrViewModel x = context.Database.SqlQuery<ClienteContaPrViewModel>(query).FirstOrDefault();

                string detailsquery = "Select n.nfid, n.produtoid quantidade,isnull(p.nome,isnull(n.descricao,'')) produtonome, n.unitario, n.quantidade * n.unitario total " +
                                      "from nfdetails n " +
                                      "left join produtoes p on p.produtoid = n.produtoid " +
                                      "where nfid =" + x.nfid.ToString();

                IEnumerable<NfDetailViewModel> y = context.Database.SqlQuery<NfDetailViewModel>(detailsquery);
                x.nfdetails = y.ToList();

                //foreach (NfDetailViewModel item in y){
                //    x.nfdetails.ToList().Add(item);

                //}

                return x;
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }



        }
        public bool ExcluirPagamento(int id)
        {
            var conta = context.ContaPRs.Find(id);
            if (conta != null)
            {
                context.ContaPRs.Remove(conta);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public Boolean ExcluirNotaFiscalAreceber(int id)
        {
            var nf = context.Nfs.Find(id);
            if (nf != null)
            {
                context.Nfs.Remove(nf);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<PropostaContaRViewModel> GetPropostasCliente(int situacaoid, int clienteid)
        {
            string query = "Select propostaid, cast(year(dtcad) as char(4))+ '/' + REPLICATE('0', 4 - LEN(propostaid)) " +
                            "+ cast(propostaid as char(4)) + ' - '  + descricao as descricao " +
                            "from propostas where situacaoid in(4,5) and clienteid = " + clienteid.ToString();
            //"from propostas where situacaoid = " + situacaoid.ToString() + " and clienteid = " + clienteid.ToString();
            IEnumerable<PropostaContaRViewModel> x = context.Database.SqlQuery<PropostaContaRViewModel>(query);
            return x.ToList();
        }

        public IEnumerable<PropostaRemuneracaoViewModel> GetPropostasContrato(int clienteid)
        {

            string sql = "Select p.propostaid,c.nome cliente,p.descricao,ca.nome categoria,isnull(p.valor,0) valor " +
                         "from propostas p " +
                         "inner join clientes c on c.clienteid = p.clienteid " +
                         "inner join Categorias ca on ca.categoriaid = p.categoriaid " +
                         "where p.dtaceite is not null and p.isproject = 0 and p.clienteid=" + clienteid.ToString();
            IEnumerable<PropostaRemuneracaoViewModel> x = context.Database.SqlQuery<PropostaRemuneracaoViewModel>(sql);
            return x.ToList();
        }
        /// <summary>
        /// Lista lancamento para Ano/mes e categoria informados
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <param name="id"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public IEnumerable<ContaPrViewModel> OrcamentoContasMes(string tipo, string ano, int mes, int id, bool all)
        {
            var iAno = Int32.Parse(ano);
            var result = from c in context.ContaPRs
                         join cl in context.Clientes on c.cliforid equals cl.clienteid
                         where c.tipo == tipo &&
                               c.vencimento.Year == iAno
                               && c.vencimento.Month == mes
                               && c.vencimento < DateTime.Now
                         select new ContaPrViewModel
                         {
                             categoriaid = c.categoriaid,
                             contaprid = c.contaprid,
                             cliforid = c.cliforid,
                             propostaid = c.propostaid,
                             clifornome = cl.nome,
                             dtcad = c.dtcad.ToString(),
                             npar = c.npar,
                             valor = c.valor,
                             vencimento = c.vencimento.ToString(),
                             dtpagto = c.dtpagto.ToString(),
                             situacao = c.situacao,
                             recorrente = c.recorrente,
                             categorianome = "",
                         };
            if (id != 0)
            {
                result = result.Where(r => r.categoriaid == id).Select(r => r);
                //result = from c in result where c.categoriaid == id select c;
            }
            if (all == false)
            {
                result = result.Where(r => r.situacao == "A").Select(r => r);
            }
            
            return result;
        }
        /// <summary>
        /// Atualiza valor da provisao para ano/mes/categoria
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <param name="id"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public CaixaItemVm OrcamentoUpdateCaixa(string ano, string mes, string id, string v)
        {
            int m = int.Parse(mes);
            int ct = int.Parse(id);
            int a = int.Parse(ano);
            decimal valor = decimal.Parse(v.Replace(".",","));

            Caixa o = GetCaixaByAnoCategoria(a, ct);

            if (o == null)
                o = SeNaoTemAdiciona(a, ct);

            o.AtualizaValorDoMes(m, valor);
            Grava();
            return new CaixaItemVm { Valor = valor };
         }
        public SaldoAnual ContasUpdateSaldoMes(int id, string valor)
        {
            var saldo = context.SaldoAnuals.Where(s => s.Id == id).FirstOrDefault();
            if(saldo != null)
            {
                var v = Decimal.Parse(valor.Replace(".",","));
                saldo.Saldo = v;
                context.SaveChanges();
            }

            return saldo;

        }
        private Caixa  SeNaoTemAdiciona(int a, int ct) {
            context.Caixas.Add(new Caixa { Ano = a, Cat = ct });
            context.SaveChanges();
            return GetCaixaByAnoCategoria(a, ct);
        }
        private Caixa GetCaixaByAnoCategoria(int a, int ct) => context.Caixas.Where(c => c.Cat == ct && c.Ano == a).Select(c => c).FirstOrDefault();
        private bool Grava()
        {

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public decimal GetSaldoAtual()
        {
           var r = context.SaldoAnuals
                   .Where(s => s.Ano == DateTime.Now.Year)
                   .FirstOrDefault();
            if (r == null)
            {
                r = new SaldoAnual { Ano = DateTime.Now.Year};
                context.SaldoAnuals.Add(r);
                context.SaveChanges();
            }
            var e = Extrato(DateTime.Now.Month, DateTime.Now.Year);
            return e.SaldoDia;
        }
        public SaldoAnual GetSaldoByAno(int mes,int ano)
        {
            var r = context.SaldoAnuals
                              .Where(s => s.Ano == ano)
                              .FirstOrDefault();
            if (r == null)
            {
                r = new SaldoAnual { Ano = ano,Inicio = DateTime.Now };
                context.SaldoAnuals.Add(r);
                context.SaveChanges();
            }
            return r;
        }
        #endregion

        #region Formas de Pagamento
        public IEnumerable<Fp> FpsAll()
        {
            try
            {
                var x = (from c in context.Fps select c);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }

    	#endregion
      
        public void Dispose()
        {
            context.Dispose();
        }

        #region Indices
        public IEnumerable<Indice> IndicesAll()
        {
            return from c in context.Indices orderby c.nome select c;
        }

        public int NovoIndice(Indice indice)
        {
            try
            {
                context.Indices.Add(indice);
                context.SaveChanges();
                return indice.id;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean IndiceJaExiste(string nome)
        {
            var x = from c in context.Indices where c.nome.ToLower().Equals(nome.ToLower()) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean AlterarIndice(Indice item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }

        public bool ExcluirIndice(int id)
        {
            var item = context.Indices.Find(id);
            if (item != null)
            {
                try
                {
                    context.Indices.Remove(item);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception exc)
                {
                    var msg = exc.Message;
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        public Indice GetIndice(int id)
        {
            Indice item = context.Indices.Find(id);
            return item;
        }
        #endregion

        #region Propostas
        IEnumerable<PropostaViewModel> IRepository.PropostasAll(string de, string ate, string ano, int categoria, int cliente, int situacao, string responsavel)
        {
            try
            {

                string fromYear = RepoLib.Get_Correct_Year(ano, de, ate, "", false);
                IEnumerable<PropostaViewModel> main = RepoLib.Get_Propostas_Data(context, 0, fromYear);
                main = RepoLib.Get_Propostas_By_Period(main, de, ate, false);
                main = RepoLib.Set_Propostas_Category_Filter(main, categoria);
                main = RepoLib.Set_Propostas_Cliente_Filter(main, cliente);
                main = RepoLib.Set_Propostas_Situacao_Filter(main, situacao);
                main = RepoLib.Set_Propostas_Reponsavel_Filter(main, responsavel);

                return main.ToList();

                //if (!String.IsNullOrEmpty(responsavel)) { query += " and p.responsavel='" +responsavel.ToString() + "'"; }

                //query += " order by p.propostaid desc";

            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }

        }
        public int NovaProposta(Proposta proposta)
        {
            try
            {
                proposta.situacaoid = 1;
                proposta.isproject = 0;

                proposta.dtcad = DateTime.Now;
                context.Propostas.Add(proposta);
                context.SaveChanges();
                return proposta.propostaid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public int NovaVisita(Visita visita)
        {
            try
            {
                context.Visitas.Add(visita);
                context.SaveChanges();
                return visita.VisitaId;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }

        public IEnumerable<Proposta> GetPropostasCliente(int clienteid)
        {
            string sql = "Select propostaid,envio,numpo,numnf,isnull(valor,0) valor,he,hu,previsao " +
                         "from propostas " +
                         "where isproject=0 and situacaoid = 2 and recebido = 0 and clienteid = " + clienteid;
            IEnumerable<Proposta> x = context.Database.SqlQuery<Proposta>(sql);
            return x.ToList();
        }
        /* 2018/06/28 */
        public IEnumerable<Responsavel> GetResponsaveis()
        {
            string sql = "Select Id, Nome  " +
                                "from AspNetUsers " +
                                "where  email <> 'administrador@metasoft.com'  order by Nome";
            IEnumerable<Responsavel> x = context.Database.SqlQuery<Responsavel>(sql);
            return x.ToList();
        }

        public IEnumerable<PropostaRemuneracaoViewModel> GetPropostasRemuneracao()
        {
            string sql = "Select p.propostaid,c.nome cliente,p.descricao,isnull(p.valor,0) valor " +
                         "from propostas p " +
                         "inner join clientes c on c.clienteid = p.clienteid " +
                         "where p.dtaceite is not null and p.isproject = 0 " +
                         "order by c.nome ";
            IEnumerable<PropostaRemuneracaoViewModel> x = context.Database.SqlQuery<PropostaRemuneracaoViewModel>(sql);
            return x.ToList();
        }
        public Proposta GetProposta(int id)
        {
            var x = (from c in context.Propostas where c.propostaid == id select c).FirstOrDefault();
            return x;
        }
        public Boolean AlterarProposta(Proposta proposta)
        {
            try
            {
                context.Entry(proposta).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public Boolean ExcluirProposta(int id)
        {
            var proposta = context.Propostas.Find(id);
            if (proposta != null)
            {
                context.Propostas.Remove(proposta);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<ContaPrViewModel> GetContasProposta(int propostaid)
        {
            string query = "Select " +
                            "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                            "c.nome clifornome, " +
                            "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                            "cpr.npar,cpr.valor, " +
                            "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                            "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                            "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                            "from contaPRs cpr " +
                            "inner join Clientes c on c.clienteid = cpr.cliforid " +
                            "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                            "left join Propostas p on p.propostaid = cpr.propostaid " +
                            "where cpr.propostaid = " + propostaid.ToString() + " and cpr.situacao = 'A' ";

            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();
        }
        public IEnumerable<Visita> ShowVisitas(int propostaid)
        {
            string query = "Select VisitaId,DateCad,VisitaEm, Descricao,Contato,Fone,Celular,propostaid,[Local] from visitas where propostaid = " + propostaid.ToString() + " order by VisitaEm";
            IEnumerable<Visita> x = context.Database.SqlQuery<Visita>(query);
            return x.ToList();
        }
        #endregion

        #region Produtos
        public IEnumerable<Produto> ProdutosAll()
        {
            return from c in context.Produtoes orderby c.nome select c;
        }
        public int NovoProduto(Produto produto)
        {
            try
            {
                context.Produtoes.Add(produto);
                context.SaveChanges();
                return produto.produtoid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean ProdutoJaExiste(string nome)
        {
            var x = from c in context.Produtoes where c.nome.ToLower().Equals(nome.ToLower()) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean ProdutoJaExiste(string nome, int id = 0)
        {

            if (id > 0)
            {
                var x = from c in context.Produtoes where c.nome.ToLower().Equals(nome.ToLower()) && c.produtoid != id select c;
                if (x.Count() > 0) { return true; } else { return false; }
            }
            else
            {
                var x = from c in context.Produtoes where c.nome.ToLower().Equals(nome.ToLower()) select c;
                if (x.Count() > 0) { return true; } else { return false; }
            }

        }
        public bool ExcluirProduto(int id)
        {
            var item = context.Produtoes.Find(id);
            if (item != null)
            {
                context.Produtoes.Remove(item);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool AlterarProduto(Produto item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public Produto GetProduto(int id)
        {
            Produto item = context.Produtoes.Find(id);
            return item;
        }
        #endregion

        #region Perfis
        public IEnumerable<Perfil> PerfisAll(Boolean inclueadmin = false)
        {
            string sql = "Select id,name from aspnetroles ";
            if (!inclueadmin)
            {
                sql += "where name <> 'Administrador'";
            }

            IEnumerable<Perfil> x = context.Database.SqlQuery<Perfil>(sql);
            return x.ToList();
        }
        public Boolean AlterarNomePerfil(string id, string name)
        {

            if (UpdateTable("Update AspNetRoles set name ='" + name + "' where id='" + id + "'"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Boolean AlterarPermissoes(Permissao permissoes)
        {
            try
            {
                context.Entry(permissoes).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public Boolean PerfilJaExiste(string name, string id = "")
        {

            if (!String.IsNullOrEmpty(id))
            {
                string sql = "Select id,name from aspnetroles where name = '" + name + "' and id<>'" + id + "'"; ;
                IEnumerable<Perfil> x = context.Database.SqlQuery<Perfil>(sql);
                if (x.Count() > 0) { return true; } else { return false; }
            }
            else
            {
                string sql = "Select id,name from aspnetroles where name = '" + name + "'"; ;
                IEnumerable<Perfil> x = context.Database.SqlQuery<Perfil>(sql);
                if (x.Count() > 0) { return true; } else { return false; }
            }

        }
        public PerfilPermissaoViewModel GetPerfilPermissoes(string id)
        {

            //string sql = "select roleid,r.name,pro_edit,pro_view,rec_edit,rec_view," +
            //             "pag_edit,pag_view,cad_edit,cad_view,rep_edit, rep_view," +
            //             "ree_edit,ree_view " +
            //             "from Aspnetroles r " +
            //             "inner join permissaos p on p.roleid = r.id " +
            //             "where r.id='" + id +"'";

            /* ALTERADO EM 31/03/2017 */
            string sql = "select roleid,r.name,pro_edit,pro_view,rec_edit,rec_view," +
             "pag_edit,pag_view,cad_edit,cad_view,rep_edit, rep_view," +
             "ree_edit,ree_view ,prj_edit,prj_view,ctt_edit,ctt_view,rem_edit,rem_view " +
             "from Aspnetroles r " +
             "inner join permissaos p on p.roleid = r.id " +
             "where r.id='" + id + "'";

            IEnumerable<PerfilPermissaoViewModel> x = context.Database.SqlQuery<PerfilPermissaoViewModel>(sql);
            return x.ToList().FirstOrDefault();
        }
        #endregion

        #region Reemboso
        public IEnumerable<ReembolsoViewModel> ReembolsosAll(int mes, int ano)
        {
            try
            {
                string query = "Select r.reembolsoid,r.tipoid,r.userid,tr.nome tiponome,r.valor," +
                               "isnull(convert(varchar(25), r.vencimento, 103),'')  vencimento,isnull(convert(varchar(25), r.dtpagto, 103),'') dtpagto," +
                               "isnull(convert(varchar(25), r.dtcad, 103),'') dtcad," +
                               "r.situacao,u.nome usernome,isnull(r.descricao,'') descricao " +
                               "from reembolsoes r " +
                               "inner join AspNetUsers u on u.id = r.userid " +
                               "inner join TiposReembolsoes tr on tr.tiporeembolsoid = r.tipoid " +
                               "where month(vencimento) = " + mes.ToString() + " and year(vencimento) = " + ano.ToString();

                IEnumerable<ReembolsoViewModel> x = context.Database.SqlQuery<ReembolsoViewModel>(query);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public IEnumerable<ReembolsoViewModel> ReembolsosUsuario(int mes, int ano, string userid, string tipo = "")
        {
            try
            {
                string query = "Select r.reembolsoid,r.tipoid,r.userid,tr.nome tiponome,r.valor," +
                               "isnull(convert(varchar(25), r.vencimento, 103),'')  vencimento,isnull(convert(varchar(25), r.dtpagto, 103),'') dtpagto," +
                               "isnull(convert(varchar(25), r.dtcad, 103),'') dtcad," +
                               "r.situacao,u.nome usernome,isnull(r.descricao,'') descricao " +
                               "from reembolsoes r " +
                               "inner join AspNetUsers u on u.id = r.userid " +
                               "inner join TiposReembolsoes tr on tr.tiporeembolsoid = r.tipoid " +
                               "where month(vencimento) = " + mes.ToString() + " and year(vencimento) = " + ano.ToString() + " ";

                if (!String.IsNullOrEmpty(userid)) { query += " and u.id = '" + userid + "'"; }
                if (!String.IsNullOrEmpty(tipo)) { query += " and r.tipoid = " + tipo + ""; }


                IEnumerable<ReembolsoViewModel> x = context.Database.SqlQuery<ReembolsoViewModel>(query);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public int NovoReembolso(Reembolso reembolso)
        {
            try
            {
                reembolso.situacao = "A";
                reembolso.dtcad = DateTime.Now;
                //reembolso.vencimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                context.Reembolsoes.Add(reembolso);
                context.SaveChanges();
                return reembolso.reembolsoid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public bool ExcluirReembolso(int id)
        {
            var reembolso = context.Reembolsoes.Find(id);
            if (reembolso != null)
            {
                context.Reembolsoes.Remove(reembolso);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Reembolso GetReembolso(int id)
        {
            return (from c in context.Reembolsoes where c.reembolsoid == id select c).FirstOrDefault();
        }
        public Boolean AlterarReembolso(Reembolso reembolso)
        {
            try
            {
                context.Entry(reembolso).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        #endregion

        #region Remuneracao
        public IEnumerable<RemuneracaoViewModel> RemuneracaoAll(int mes, int ano)
        {
            string query = "Select " +
                           "cast(year(p.dtcad) as varchar(4)) + '/' + cast(p.propostaid as varchar) propostaid," +
                           "r.id,month(dtlan) mes,year(dtlan) ano,isnull(co.nome,'') comercial,c.nome cliente,p.descricao," +
                           "p.valor,r.insumos," +
                           "cast(p.valor-r.insumos as decimal(15,2)) liqsi," +
                           "cast((p.valor-r.insumos)-(p.valor-r.insumos) * .2 as decimal(15,2)) liqci," +
                           "p.situacaoid faturada, s.cor, s.nome situacaonome,cast(r.percentual as int) percentual," +
                           "cast(((p.valor-r.insumos)-(p.valor-r.insumos) * .2) * (r.percentual/100) as decimal(15,2)) remuneracao " +
                           "from Remuneracaos r " +
                           "inner join Propostas p on p.propostaid = r.propostaid " +
                           "inner join situacaos s on s.situacaoid = p.situacaoid " +
                           "inner join Clientes c on c.clienteid = p.clienteid " +
                           "inner join Comercials co on co.id = r.comercialid " +
                           "where month(r.dtlan)= " + mes.ToString() + " and year(r.dtlan) = " + ano.ToString();
            IEnumerable<RemuneracaoViewModel> x = context.Database.SqlQuery<RemuneracaoViewModel>(query);
            return x.ToList();
        }

        public IEnumerable<ComercialRemuneracaoViewModel> GetComercialRemuneracao()
        {
            string query = "Select id,nome from Comercials order by nome";
            IEnumerable<ComercialRemuneracaoViewModel> x = context.Database.SqlQuery<ComercialRemuneracaoViewModel>(query);
            return x.ToList();

        }
        public int NovaRemuneracao(Remuneracao item)
        {
            try
            {
                context.Remuneracaos.Add(item);
                context.SaveChanges();
                return item.id;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean ExcluirRemuneracao(int id)
        {
            var item = context.Remuneracaos.Find(id);
            if (item != null)
            {
                context.Remuneracaos.Remove(item);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Remuneracao GetRemuneracao(int id)
        {
            return (from c in context.Remuneracaos where c.id == id select c).FirstOrDefault();
        }
        public Boolean AlterarRemuneracao(Remuneracao item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        #endregion

        #region TipoReembolso
        public IEnumerable<TiposReembolso> TiposReembolsosAll()
        {
            return from c in context.TiposReembolsoes orderby c.nome select c;
        }
        public int NovoTipoReembolso(TiposReembolso tiporeembolso)
        {
            try
            {
                context.TiposReembolsoes.Add(tiporeembolso);
                context.SaveChanges();
                return tiporeembolso.tiporeembolsoid;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean ExcluirTipoReembolso(int id)
        {
            var item = context.TiposReembolsoes.Find(id);
            if (item != null)
            {
                context.TiposReembolsoes.Remove(item);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public TiposReembolso GetTipoReembolso(int id)
        {
            TiposReembolso item = context.TiposReembolsoes.Find(id);
            return item;
        }
        public Boolean AlterarTipoReembolso(TiposReembolso item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public TiposReembolso TipoReembolsoById(int id)
        {
            return (from c in context.TiposReembolsoes where c.tiporeembolsoid == id orderby c.nome select c).FirstOrDefault();
        }
        public Boolean TipoReembolsoJaExiste(string nome)
        {

            var item = from c in context.TiposReembolsoes where c.nome.ToLower().Equals(nome.ToLower()) select c;
            if (item.Count() > 0) { return true; }
            else { return false; }
        }
        #endregion

        #region TipoContrato
        public IEnumerable<TipoContrato> TiposContratoAll()
        {
            return from c in context.TipoContratoes orderby c.nome select c;
        }
        public int NovoTipoContrato(TipoContrato item)
        {
            try
            {
                context.TipoContratoes.Add(item);
                context.SaveChanges();
                return item.id;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return 0;
            }
        }
        public Boolean TipoContratoJaExiste(string nome)
        {
            var x = from c in context.TipoContratoes where c.nome.ToLower().Equals(nome.ToLower()) select c;
            if (x.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean AlterarTipoContrato(TipoContrato item)
        {
            try
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public bool ExcluirTipoContrato(int id)
        {
            var item = context.TipoContratoes.Find(id);
            if (item != null)
            {
                context.TipoContratoes.Remove(item);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public TipoContrato GetTipoContrato(int id)
        {
            TipoContrato item = context.TipoContratoes.Find(id);
            return item;
        }
        #endregion

        #region Usuarios
        IEnumerable<UsuarioViewModel> IRepository.UsuariosAll()
        {
            string sql = "select u.id,u.Nome nome,u.Email  email, r.Name perfil,case when u.active=1 then 'A' else 'I' end status," +
                         "isnull(convert(varchar(20), LockoutEndDateUtc, 120),'') LockDate from AspNetUsers u " +
                         "inner join AspNetUserRoles ur on ur.UserId = u.Id " +
                         "inner join AspNetRoles r on r.Id = ur.RoleId  " +
                         "where email<>'administrador@metasoft.com' order by nome";

            IEnumerable<UsuarioViewModel> x = context.Database.SqlQuery<UsuarioViewModel>(sql);
            return x.ToList();
        }
        public Boolean IsUserInRole(string id, string role)
        {
            try
            {
                string query = "Select r.name from aspnetusers u " +
                               "inner join AspNetUserRoles ur on ur.UserId = u.id " +
                               "inner join AspNetroles r on r.Id = ur.RoleId " +
                               "where u.id='" + id + "' and r.Name='" + role + "'";

                IEnumerable<RoleViewModel> x = context.Database.SqlQuery<RoleViewModel>(query);
                if (x.ToList().Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public AlterarPerfilViewModel GetUserRoleInfo(string id)
        {
            string query = "select  r.id,u.nome  username,r.id roleid,r.Name rolename  from aspnetusers u " +
                           "inner join aspnetuserroles ur on ur.UserId = u.id " +
                           "inner join aspnetroles r on r.Id = ur.RoleId " +
                           "where u.id = '" + id + "'";

            IEnumerable<AlterarPerfilViewModel> x = context.Database.SqlQuery<AlterarPerfilViewModel>(query);
            return x.ToList().FirstOrDefault();

        }
        public Permissao GetPermissoes(string email)
        {

            /* ALTERADO EM 31/03/2017 */
            /* ALTERADO EM 30/05/2017 */
            string sql = "select 0 id,r.id roleid, r.Name perfil," +
             "pro_edit,pro_view,rec_edit,rec_view,pag_edit,pag_view,cad_edit,cad_view,rep_edit,rep_view,ree_edit,ree_view,prj_edit,prj_view, " +
             "ctt_edit, ctt_view,rem_edit,rem_view " +
             "from AspNetUsers u " +
             "inner join AspNetUserRoles ur on ur.UserId = u.Id " +
             "inner join AspNetRoles r on r.Id = ur.RoleId  " +
             "inner join Permissaos p on p.RoleId = r.Id  " +
             "where email = '" + email + "'";



            IEnumerable<Permissao> permissoes = context.Database.SqlQuery<Permissao>(sql);
            return permissoes.ToList().FirstOrDefault();
        }
        public string GetRoleIdByName(string rolename)
        {
            string query = "select id from aspnetroles where name = '" + rolename + "'";
            IEnumerable<String> x = context.Database.SqlQuery<String>(query);
            return x.ToList().FirstOrDefault();
        }
        public IEnumerable<UsuarioViewModel> GetUsuarios()
        {
            string sql = "select u.id,u.Nome nome  from AspNetUsers u " +
                         "where email<>'administrador@metasoft.com' order by nome";

            IEnumerable<UsuarioViewModel> x = context.Database.SqlQuery<UsuarioViewModel>(sql);
            return x.ToList();
        }
        #endregion


        #region Helpers
        public bool UpdateTable(string cmd)
        {
            try
            {

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                using (var command = new SqlCommand(cmd, conn))
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }

            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }

        }
        public Cidade GetCidade(int id)
        {
            Cidade cidade = context.Cidades.Find(id);
            return cidade;
        }
        public string usdate(string text)
        {

            return text.Substring(3, 2) + "/" + text.Substring(0, 2) + "/" + text.Substring(6, 4);
        }
        #endregion

        #region Financeiro

        public ResumoDiarioViewModel ResumoDiario()
        {
            try
            {

                string query = "select  " +
                                "isnull(sum(case when (tipo= 'P' and cast(vencimento as date) = CAST(getdate() AS DATE) and situacao = 'A') then valor else 0 end),0) as apagarhoje," +
                                "isnull(sum(case when (tipo= 'R' and cast(vencimento as date) = CAST(getdate() AS DATE) and situacao = 'A') then valor else 0 end),0) as areceberhoje," +
                                "isnull(sum(case when (tipo= 'P' and cast(vencimento as date) < CAST(getdate() AS DATE) and situacao = 'A') then valor else 0 end),0) as apagaratrasado," +
                                "isnull(sum(case when (tipo= 'R' and cast(vencimento as date) < CAST(getdate() AS DATE) and situacao = 'A') then valor else 0 end),0) as areceberatrasado, " +
                                "isnull(sum(case when (tipo= 'P' and month(cast(vencimento as date)) = month(CAST(getdate() AS DATE)) and year(cast(vencimento as date)) = year(CAST(getdate() AS DATE)) " +
                                "and situacao  = 'A') then valor else 0 end),0) as apagarmes," +
                                "isnull(sum(case when(tipo = 'R' and month(cast(vencimento as date)) = month(CAST(getdate() AS DATE))  and year(cast(vencimento as date)) = year(CAST(getdate() AS DATE)) " +
                                "and situacao = 'A') then valor else 0 end), 0) as arecebermes," +
                                "(" +
                                "Select pagano-recano liquido from " +
                                "(Select isnull(sum(case when (tipo= 'P' and cast(vencimento as date) <= CAST(getdate() AS DATE) and situacao = 'P') then valor else 0 end),0) as pagano, " +
                                "isnull(sum(case when (tipo= 'R' and cast(vencimento as date) <= CAST(getdate() AS DATE) and situacao = 'P') then valor else 0 end),0) as recano " +
                                "from contaprs) resumo " +
                                ") liquido," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 1) then ceiling(valor) else 0 end),0) pjan," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 1) then ceiling(valor) else 0 end),0) rjan," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 2) then ceiling(valor) else 0 end),0) pfev," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 2) then ceiling(valor) else 0 end),0) rfev," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 3) then ceiling(valor) else 0 end),0) pmar," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 3) then ceiling(valor) else 0 end),0) rmar," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 4) then ceiling(valor) else 0 end),0) pabr," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 4) then ceiling(valor) else 0 end),0) rabr," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 5) then ceiling(valor) else 0 end),0) pmai," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 5) then ceiling(valor) else 0 end),0) rmai," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 6) then ceiling(valor) else 0 end),0) pjun," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 6) then ceiling(valor) else 0 end),0) rjun," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 7) then ceiling(valor) else 0 end),0) pjul," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 7) then ceiling(valor) else 0 end),0) rjul," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 8) then ceiling(valor) else 0 end),0) pago," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 8) then ceiling(valor) else 0 end),0) rago," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 9) then ceiling(valor) else 0 end),0) pset," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 9) then ceiling(valor) else 0 end),0) rset," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 10) then ceiling(valor) else 0 end),0) pout," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 10) then ceiling(valor) else 0 end),0) rout," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 11) then ceiling(valor) else 0 end),0) pnov," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 11) then ceiling(valor) else 0 end),0) rnov," +
                                "isnull(sum(case when (tipo= 'P' and month(vencimento) = 12) then ceiling(valor) else 0 end),0) pdez," +
                                "isnull(sum(case when (tipo= 'R' and month(vencimento) = 12) then ceiling(valor) else 0 end),0) rdez " +
                                "from contaprs ";
                //"where year(vencimento) = year(getdate())";

                IEnumerable<ResumoDiarioViewModel> x = context.Database.SqlQuery<ResumoDiarioViewModel>(query);

                var result = x.FirstOrDefault();
                result.liquido = GetSaldoAtual();
                return result;

            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        #endregion
        #region Relatorios
        public Boolean AddNewHistory(Historico historico)
        {
            try
            {
                context.Historicoes.Add(historico);
                context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
        public IEnumerable<DiarioConsolidadoViewModel> DiarioConsolidado(string mes, string ano)
        {
            try
            {

                string query = "Select d.dia, isnull(ap.valor,0) pagar, isnull(ar.valor,0) receber from dias d " +
                                "left join ( " +
                                "select day(vencimento) dia, sum(valor) valor  " +
                                "from contaprs where tipo = 'P' and year(vencimento) = " + ano + " and month(vencimento) = " + mes + "  " +
                                "group by day(vencimento) " +
                                ") ap on ap.dia = d.dia " +
                                "left join ( " +
                                "select day(vencimento) dia, sum(valor) valor " +
                                "from contaprs where tipo = 'R' and year(vencimento) = " + ano + " and month(vencimento) = " + mes + " " +
                                "group by day(vencimento) " +
                                ") ar on ar.dia = d.dia	";
                IEnumerable<DiarioConsolidadoViewModel> x = context.Database.SqlQuery<DiarioConsolidadoViewModel>(query);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public IEnumerable<ContaPrViewModel> GetLancamentos(string dia, string mes, string ano, string tipo)
        {
            string query = "Select " +
                            "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                            "c.nome clifornome, " +
                            "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                            "cpr.npar,cpr.valor, " +
                            "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                            "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                            "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                            "from contaPRs cpr " +
                            "inner join Clientes c on c.clienteid = cpr.cliforid " +
                            "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                            "left join Propostas p on p.propostaid = cpr.propostaid " +
                            "where day(vencimento) = " + dia + " and month(vencimento) = " + mes + " and year(vencimento) = " + ano + " and cpr.tipo = '" + tipo + "'";
            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();

        }
        IEnumerable<MensalConsolidadoViewModel> IRepository.MensalConsolidado(string ano)
        {
            try
            {

                string query = "Select m.mes, isnull(ap.valor,0) pagar, isnull(ar.valor,0) receber from meses m " +
                                "left join ( " +
                                "select month(vencimento) mes, sum(valor) valor  " +
                                "from contaprs where tipo = 'P' and year(vencimento) = " + ano + " " +
                                "group by month(vencimento) " +
                                ") ap on ap.mes = m.mes " +
                                "left join ( " +
                                "select month(vencimento) mes, sum(valor) valor " +
                                "from contaprs where tipo = 'R' and year(vencimento) = " + ano + " " +
                                "group by month(vencimento) " +
                                ") ar on ar.mes = m.mes	";
                IEnumerable<MensalConsolidadoViewModel> x = context.Database.SqlQuery<MensalConsolidadoViewModel>(query);
                return x.ToList();
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return null;
            }
        }
        public IEnumerable<ContaPrViewModel> GetLancamentos(string mes, string ano, string tipo)
        {
            string query = "Select " +
                            "cpr.contaprid,cpr.cliforid,cpr.propostaid, " +
                            "c.nome clifornome, " +
                            "convert(varchar(25), cpr.dtcad, 103) dtcad, " +
                            "cpr.npar,cpr.valor, " +
                            "convert(varchar(25), cpr.vencimento, 103) vencimento,convert(varchar(25), cpr.dtpagto, 103) dtpagto, " +
                            "case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado, " +
                            "cpr.situacao,cpr.recorrente,ca.nome categorianome " +
                            "from contaPRs cpr " +
                            "inner join Clientes c on c.clienteid = cpr.cliforid " +
                            "inner join Categorias ca on ca.categoriaid = cpr.categoriaid " +
                            "left join Propostas p on p.propostaid = cpr.propostaid " +
                            "where  month(vencimento) = " + mes + " and year(vencimento) = " + ano + " and cpr.tipo = '" + tipo + "'";
            IEnumerable<ContaPrViewModel> x = context.Database.SqlQuery<ContaPrViewModel>(query);
            return x.ToList();

        }
        public IEnumerable<Historico> GetHistory(int fid, string tipo)
        {
            string query = "select historicoid,fid,tipo,data,texto from Historicoes where fid=" + fid + " and tipo ='" + tipo + "' ";
            IEnumerable<Historico> x = context.Database.SqlQuery<Historico>(query);
            return x.ToList();
        }
        public IEnumerable<GraphData> ResumoPorCategoria(string tipo, string mes, string ano)
        {

            if (string.IsNullOrEmpty(mes)) { mes = DateTime.Now.Month.ToString(); }
            if (string.IsNullOrEmpty(ano)) { ano = DateTime.Now.Year.ToString(); }

            String query = "Select ca.nome x,sum(valor) y  from contaprs c " +
                            "inner join categorias ca on ca.categoriaid = c.categoriaid " +
                            "where month(c.vencimento) = " + mes + " and year(c.vencimento) = " + ano + " and c.tipo='" + tipo + "' " +
                            "group by ca.nome";

            IEnumerable<GraphData> gd = context.Database.SqlQuery<GraphData>(query);
            return gd.ToList();
        }

        public IEnumerable<OrcamentoVm> OrcamentoAnual(string ano, string tipo)
        {
            var main = RepoLib.ListaOrcamento(context,ano,tipo);
            return main.ToList();
        }



        #endregion

    }
}

//private  DateTime GetLastDayOfMonth(this DateTime dateTime)
//{
//    return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
//}
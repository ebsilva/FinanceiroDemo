using Metasoft.Models;
using Metasoft.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metasoft.Infrastructure
{
    public interface IRepository : IDisposable
    {

        ApplicationDbContext Dbcontext();


        #region Clientes
        IEnumerable<ClienteViewModel> ClientesAll(string tipo, string situacao = "");
        Boolean ExcluirCliente(int id);
        Boolean ContatoDelete(int id);
        IEnumerable<CliForViewModel> CliForAll(string tipo);
        int NovoCliente(Cliente cliente, string tipo);

        IEnumerable<Contato> GetContatos(int clienteid);
        Boolean FindClienteByName(string name);
        Boolean FindClienteByCnpj(string cnpj);
        IEnumerable<ContaPrViewModel> GetContas(int clienteid, string tipo, string atrasadas);

        ClienteViewModel GetClienteDetail(int clienteid);

        Cliente GetCliFor(int? id);
        Boolean AlterarCliente(Cliente cliente);
        /* Contatos */
        int NovoContato(Contato contato);
        IEnumerable<CliForViewModel> GetClientesWithPropostas();
        ClienteContaPrViewModel ClienteContaPr(int? contaprid);
        IEnumerable<NfForCrViewModel> GetDetalhes_e_Nfs_Proposta(int propostaid);
        #endregion

        #region Comercial
        IEnumerable<ComercialViewModel> ComercialAll(int mes, int ano);
        int NovoComercial(Comercial item);
        Boolean ComercialExists(Comercial comercial);
        Comercial GetComercial(int id);
        Boolean AlterarComercial(Comercial item);
        Boolean ExcluirComercial(int id);
        #endregion

        #region Contratos
        IEnumerable<ContratosViewModel> ContratosAll(string tipodata, string de, string ate, int tipocontrato, int clifor, int situacao);
        int NovoContrato(ContratoViewModel contrato);
        ContratoViewModel GetContrato(int id);
        Boolean AlterarContrato(ContratoViewModel contrato);
        Boolean ExcluirContrato(int id);
        IEnumerable<HistoricoContratoViewModel> GetHistoricoContrato(int id);
        int CountContasReceberForContrato(int id);
        IEnumerable<ContaPrViewModel> GetContasContrato(int id);
        #endregion

        #region Categorias
        IEnumerable<Categoria> CategoriasAll();
        IEnumerable<Categoria> CategoriasByTipo(string tipo);
        int NovaCategoria(Categoria categoria);
        Boolean ExcluirCategoria(int id);
        Categoria GetCategoria(int id);
        Boolean AlterarCategoria(Categoria categoria);
        Categoria CategoriaById(int id);
        Boolean CategoriaExists(string nome);
        Boolean CategoriaJaExiste(string nome); 
        #endregion


        /* Fp */

        IEnumerable<Fp> FpsAll();

        #region Contas
        IEnumerable<ContaPrViewModel> ContaPRsAll(string tipo, string de, string tipodata, string ate, string ano, int categoria, int fornecedor, string situacao, string mode, string sort = "", string responsavel = "");
        IEnumerable<ContaPrExcelViewModel> ContaPRExcel(string tipo, string de, string ate, int categoria, int fornecedor, string situacao, string mode, string sort = "");
        IEnumerable<ContaPrViewModel> ContaPRsPorCategoria(string tipo, string mes, string ano);
        int NovaContaPR(ContaPR contapr);
        Boolean UpdateTable(string cmd);
        int NovaParcelaPR(ContaPR contapr);
        Boolean AlterarConta(ContaPR conta);
        NfReceberViewModel NovaNfAreceber(int contaid);
        bool NfJaExiste(int contaid);
        int AddNfDetail(NFDetail nfdetail);
        ContaPR GetConta(int contaprid);
        ClienteContaNota ImprimirNotaFiscal(int contaprid);
        Boolean ExcluirPagamento(int id);
        ContaPR FindConta(int? id);
        Boolean ExcluirNotaFiscalAreceber(int id);
        IEnumerable<PropostaContaRViewModel> GetPropostasCliente(int clienteid, int situacaoid);
        IEnumerable<ContaPrViewModel> OrcamentoContasMes(string tipo, string ano, int mes, int id, bool all);
        CaixaItemVm OrcamentoUpdateCaixa(string ano, string mes, string id, string valor);
        void NotaFiscalUpdateItemQtd(int nfid, int line, int qtde);
        void NotaFiscalUpdateItemDescricao(int nfid, int line, string descricao);
        void NotaFiscalUpdateItemUnitario(int nfid, int line, string unitario);
        int CriaProximoNumeroNotaFiscal(int nfid);
        int GetNumeroNotaFiscal(int nfid);
        decimal GetSaldoAtual();
        SaldoAnual GetSaldoByAno(int mes,int ano);
        SaldoAnual ContasUpdateSaldoMes(int id, string valor);
        bool ConfirmaRecebimento(int id, string datarecebimento);
        bool ConfirmaPagamento(int id, string dtapagamento);
        bool SaveNewDataInicio(int ano,int id, string datainicio);

        ExtratoViewModel Extrato(int mes,int ano);

        #endregion

        #region Indices
        IEnumerable<Indice> IndicesAll();
        int NovoIndice(Indice indice);
        Boolean IndiceJaExiste(string nome);
        Boolean AlterarIndice(Indice item);
        bool ExcluirIndice(int id);
        Indice GetIndice(int id); 
        #endregion

       

        

        #region Propostas
        IEnumerable<PropostaViewModel> PropostasAll(string de, string ate, string ano, int categoria, int clifor, int situacao, string responsavel);
        IEnumerable<Proposta> GetPropostasCliente(int clienteid);
        IEnumerable<PropostaRemuneracaoViewModel> GetPropostasContrato(int clienteid);
        int NovaProposta(Proposta proposta);
        int NovaVisita(Visita visita);
        Proposta GetProposta(int id);
        Boolean AlterarProposta(Proposta proposta);
        Boolean ExcluirProposta(int id);
        IEnumerable<ContaPrViewModel> GetContasProposta(int propostaid);
        /* 2018/06/28 */
        IEnumerable<Responsavel> GetResponsaveis();
        IEnumerable<Visita> ShowVisitas(int propostaid);
        #endregion

        #region Perfis
        IEnumerable<Perfil> PerfisAll(Boolean inclueadmin = false);
        Boolean PerfilJaExiste(string nome, string id = "");
        PerfilPermissaoViewModel GetPerfilPermissoes(string id);
        Boolean AlterarNomePerfil(string id, string name);
        Boolean AlterarPermissoes(Permissao permissoes);
        #endregion

        #region Produtos
        IEnumerable<Produto> ProdutosAll();
        int NovoProduto(Produto produto);
        Boolean ProdutoJaExiste(string nome, int id);
        Boolean ExcluirProduto(int id);
        Boolean AlterarProduto(Produto produto);
        Produto GetProduto(int id);
        #endregion

        #region Relatorios
        ResumoDiarioViewModel ResumoDiario();
        IEnumerable<DiarioConsolidadoViewModel> DiarioConsolidado(string mes, string ano);
        IEnumerable<MensalConsolidadoViewModel> MensalConsolidado(string ano);
        IEnumerable<ContaPrViewModel> GetLancamentos(string dia, string mes, string ano, string tipo);
        IEnumerable<ContaPrViewModel> GetLancamentos(string mes, string ano, string tipo);
        IEnumerable<GraphData> ResumoPorCategoria(string tipo, string mes, string ano);
        IEnumerable<OrcamentoVm> OrcamentoAnual(string ano, string tipo);

        #endregion

        #region Remuneracao
        IEnumerable<RemuneracaoViewModel> RemuneracaoAll(int mes, int ano);
        int NovaRemuneracao(Remuneracao item);

        IEnumerable<PropostaRemuneracaoViewModel> GetPropostasRemuneracao();
        IEnumerable<ComercialRemuneracaoViewModel> GetComercialRemuneracao();
        Boolean ExcluirRemuneracao(int id);
        Remuneracao GetRemuneracao(int id);
        Boolean AlterarRemuneracao(Remuneracao item);
        #endregion

        #region Reembolso
        IEnumerable<ReembolsoViewModel> ReembolsosAll(int mes, int ano);
        IEnumerable<ReembolsoViewModel> ReembolsosUsuario(int mes, int ano, string userid, string tipo = "");

        int NovoReembolso(Reembolso reembolso);
        Reembolso GetReembolso(int id);
        Boolean AlterarReembolso(Reembolso reembolso);
        #endregion

        #region TiposReembolso
        IEnumerable<TiposReembolso> TiposReembolsosAll();
        int NovoTipoReembolso(TiposReembolso tiporeembolso);
        Boolean ExcluirTipoReembolso(int id);
        TiposReembolso GetTipoReembolso(int id);
        Boolean AlterarTipoReembolso(TiposReembolso tiporeembolso);
        TiposReembolso TipoReembolsoById(int id);
        Boolean TipoReembolsoJaExiste(string nome);
        #endregion

        #region Contrato
        IEnumerable<TipoContrato> TiposContratoAll();
        int NovoTipoContrato(TipoContrato item);
        Boolean TipoContratoJaExiste(string nome);
        Boolean AlterarTipoContrato(TipoContrato item);
        bool ExcluirTipoContrato(int id);
        TipoContrato GetTipoContrato(int id);
        #endregion

        #region Usuarios
        IEnumerable<UsuarioViewModel> UsuariosAll();
        IEnumerable<UsuarioViewModel> GetUsuarios();
        AlterarPerfilViewModel GetUserRoleInfo(string id);
        string GetRoleIdByName(string rolename);
        Permissao GetPermissoes(string email);
        //Boolean AcessoPermitido(String modulo, Permissao permissoes); 
        #endregion

        #region Helpers
        Cidade GetCidade(int id);
        Boolean AddNewHistory(Historico historico);
        IEnumerable<Historico> GetHistory(int fid, string tipo);
        Boolean ExcluirReembolso(int id);
       
        #endregion












    }
}

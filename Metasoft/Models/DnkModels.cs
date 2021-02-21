using Metasoft.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Metasoft.Models
{

    public class Proposta
    {
        [Key]
        public int propostaid { get; set; }
        public DateTime dtcad { get; set; }

        [Display(Name = "Descrição"), Required(ErrorMessage = "Descrição é obrigatória")]
        public string descricao { get; set; }

        [Display(Name = "Razão Social Cliente"), Required(ErrorMessage = "Razão Social do Cliente é obrigatório")]
        public int clienteid { get; set; }

        [Display(Name = "Responsável")]
        public string responsavel { get; set; }

        [Display(Name = "Categoria"), Required(ErrorMessage = "Categoria é obrigatório")]
        public int categoriaid { get; set; }

        [Display(Name = "Envio")]
        public string envio { get; set; }

        [Display(Name = "Data Aceite")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtaceite { get; set; }

        [Display(Name = "Data Fim Projeto (prev)")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtfimprvprj { get; set; }

        [Display(Name = "Data Inicio Projeto")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtiniprj { get; set; }

        [Display(Name = "Data Finalização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtfinalizacao { get; set; }

        [Display(Name = "Nº PO")]
        public string numpo { get; set; }
        [Display(Name = "Nº NF")]
        public string numnf { get; set; }

        [Display(Name = "R$ Valor"), Required(ErrorMessage = "Valor é obrigatório")]
        [Range(1, 99999999)]
        public decimal valor { get; set; }

        [Display(Name = "Situação")]

        public Boolean recebido { get; set; }

        [Display(Name = "HE")]
        public int? he { get; set; }

        [Display(Name = "HU")]
        public int? hu { get; set; }

        [Display(Name = "Previsão")]
        public int? previsao { get; set; }

        [Display(Name = "Situação")]
        public int situacaoid { get; set; }

        public int isproject { get; set; }

        public int fp { get; set; }

    }

    public class PropostaViewModel
    {

        public int propostaid { get; set; }
        public int clienteid { get; set; }

        public int categoriaid { get; set; }
        [Display(Name = "Categoria")]
        public string categorianome { get; set; }

        [Display(Name = "Cliente")]
        public string clientenome { get; set; }

        public string responsavelid { get; set; }

        [Display(Name = "Responsavel")]
        public string responsavel { get; set; }


        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Cadastrado em")]
        public string dtcad { get; set; }

        [Display(Name = "Envio")]
        public string envio { get; set; }

        [Display(Name = "Data Aceite")]
        public string dtaceite { get; set; }


        [Display(Name = "Data Finalização")]
        public string dtfinalizacao { get; set; }

        [Display(Name = "Nº PO")]
        public string numpo { get; set; }

        [Display(Name = "Nº NF")]
        public string numnf { get; set; }

        [Display(Name = "R$")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Decimal valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Decimal faturado { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Decimal saldo { get; set; }

        [Display(Name = "F.Pagto")]

        public int? fpid { get; set; }

        public string fpnome { get; set; }

        [Display(Name = "Recebido")]
        public Boolean recebido { get; set; }

        [Display(Name = "HE")]
        public int horasestimadas { get; set; }

        [Display(Name = "HU")]
        public int horasutilizadas { get; set; }

        [Display(Name = "Nº Par")]
        public int np { get; set; }

        [Display(Name = "Previsão")]
        public string previsao { get; set; }

        public int situacaoid { get; set; }
        [Display(Name = "Situação")]
        public string situacao { get; set; }
        public string cor { get; set; }

        public string motivo { get; set; }
    }

    public class NfForCrViewModel
    {
        public string nf { get; set; }
        public string emissao { get; set; }
        public string vencimento { get; set; }
        public string dtpagto { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal valor { get; set; }

        public string descricao { get; set; }
        public string noordem { get; set; }
    }

    public class PropostaRemuneracaoViewModel
    {
        public int propostaid { get; set; }
        public string descricao { get; set; }
        public string cliente { get; set; }
        public string categoria { get; set; }
        public decimal valor { get; set; }
    }

    public class Cliente
    {
        [Key]
        public int clienteid { get; set; }
        public DateTime dtcad { get; set; }

        [Required(ErrorMessage = "Razão social é obrigatória")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }

        [Display(Name = "IE")]
        public string ie { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Complemento")]
        public string complemento { get; set; }

        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Cep")]
        public string cep { get; set; }


        [Display(Name = "Fone")]
        public string fone { get; set; }

        [Display(Name = "0800")]
        public string f0800 { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Site")]
        public string site { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

    }

    public class Responsavel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
    }

    public class ClienteViewModel
    {
        public int clienteid { get; set; }
        [Display(Name = "Cadastro")]
        public DateTime dtcad { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }
        [Display(Name = "IE")]
        public string ie { get; set; }
        [Display(Name = "Endereço")]
        public string endereco { get; set; }
        [Display(Name = "Complemento")]
        public string complemento { get; set; }
        [Display(Name = "Bairro")]
        public string bairro { get; set; }
        [Display(Name = "Cep")]
        public string cep { get; set; }
        [Display(Name = "Município")]
        public string cidade { get; set; }

        [Display(Name = "UF")]
        public string estado { get; set; }

        [Display(Name = "Fone")]
        public string fone { get; set; }

        [Display(Name = "0800")]
        public string f0800 { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Site")]
        public string site { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Atrasados")]
        public int atrasados { get; set; }

        [Display(Name = "Pagamentos")]
        public int pagamentos { get; set; }

    }

    public class CliForViewModel
    {
        public int cliforid { get; set; }
        public string nome { get; set; }
    }

    public class Fp
    {
        [Key]
        public int fpid { get; set; }

        public string nome { get; set; }
    }

    public class Cidade
    {
        [Key]
        public int cidadeid { get; set; }
        public string uf { get; set; }
        public string nome { get; set; }
    }
    public class CidadesViewModel
    {
        public int cidadeid { get; set; }
        public string nome { get; set; }
    }

    public class Contato
    {

        public int contatoid { get; set; }
        public int clienteid { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string fone { get; set; }
        public string celular { get; set; }
        public string sexo { get; set; }

    }

    public class ContaPR
    {
        [Key]
        public int contaprid { get; set; }
        public DateTime? dtcad { get; set; }
        [Required(ErrorMessage = "Nº de parcelas é obrigatório")]
        public int npar { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int cliforid { get; set; }

        [Display(Name = "Nº Proposta")]
        public int propostaid { get; set; }

        public int contratoid { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Categoria é obrigatório")]
        public int categoriaid { get; set; }

        [Display(Name = "Nº Ordem")]
        public string noordem { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string descricao { get; set; }


        [Required(ErrorMessage = "Valor do pagamento e obrigatório")]
        [Range(1, 999999999)]
        [Display(Name = "R$ Valor")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "Vencimento é obrigatório")]
        [Range(typeof(DateTime), "1/1/2016", "01/01/2050", ErrorMessage = "Vencimento não permitido")]
        [Display(Name = "Vencimento")]


        public DateTime vencimento { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Dt. Pagto")]
        public DateTime? dtpagto { get; set; }

        [Display(Name = "Situação")]

        public string situacao { get; set; }

        [Display(Name = "Repetir")]
        public Boolean recorrente { get; set; }

        public int contagrupo { get; set; }
        [Display(Name = "Nº NF")]
        public string nf { get; set; }
        public int nfid { get; set; }

        public bool archive { get; set; }


    }
    public class PropostaContaRViewModel {

        public int propostaid { get; set; }
        public string descricao { get; set; }

    }

    public class DiarioConsolidadoViewModel
    {
        public int dia { get; set; }
        public decimal receber { get; set; }
        public decimal pagar { get; set; }
    }

    public class MensalConsolidadoViewModel
    {
        public int mes { get; set; }
        public decimal receber { get; set; }
        public decimal pagar { get; set; }
    }

    public class ContaPrViewModel
    {
        public int contaprid { get; set; }

        [Display(Name = "Lançamento")]
        public string dtcad { get; set; }

        [Display(Name = "Par Nº")]
        public int npar { get; set; }

        public int cliforid { get; set; }

        [Display(Name = "Fornecedor")]
        public string clifornome { get; set; }

        public int propostaid { get; set; }

        public int categoriaid { get; set; }

        [Display(Name = "Categoria")]
        public string categorianome { get; set; }

        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Valor R$")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal valor { get; set; }

        [Display(Name = "Vencimento")]
        public string vencimento { get; set; }

        public string tipo { get; set; }

        [Display(Name = "Dt. Pagto")]
        public string dtpagto { get; set; } = "";

        [Display(Name = "Situação")]
        public string situacao { get; set; }

        [Display(Name = "Situação")]
        public string atrasado { get; set; }

        [Display(Name = "Repetir")]
        public Boolean recorrente { get; set; }
        [Display(Name = "Nº Ord.")]
        public string noordem { get; set; }

        public int contagrupo { get; set; }
        public int allowpar { get; set; }

        [Display(Name = "Nº NF.")]
        public int nfid { get; set; }
        public int numprint { get; set; }

        [Display(Name = "Nº NF.")]
        public string nf { get; set; }

        public Boolean archive { get; set; }
        public int addressok { get; set; }
    }


    public class ContaPrExcelViewModel
    {
        public int conta_id { get; set; }
        public string dtcad { get; set; }

        public int npar { get; set; }

        public int cliforid { get; set; }

        public string clifor_nome { get; set; }

        public int proposta_id { get; set; }

        public int categoria_id { get; set; }

        public string categoria_nome { get; set; }

        public string descricao { get; set; }

        public decimal valor { get; set; }

        public string vencimento { get; set; }

        public string dtpagto { get; set; }

        public string situacao { get; set; }

        public string atrasado { get; set; }

        public string noordem { get; set; }

        public int nfid { get; set; }

        public string nf { get; set; }

    }


    public class Categoria
    {

        public int categoriaid { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
    }

    public class PorCategoriaViewModel
    {
        public string categoria { get; set; }
        public string cliente { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
        public string situacao { get; set; }
        public DateTime dtpagto { get; set; }
    }
    public class ResumoDiarioViewModel
    {
        [Display(Name = "A pagar hoje")]
        public decimal areceberhoje { get; set; }

        [Display(Name = "A receber hoje")]
        public decimal apagarhoje { get; set; }

        [Display(Name = "A receber em atraso")]
        public decimal areceberatrasado { get; set; }

        [Display(Name = "A pagar em atraso")]
        public decimal apagaratrasado { get; set; }
        public decimal arecebermes { get; set; }
        public decimal apagarmes { get; set; }

        public decimal liquido { get; set; }

        public decimal pjan { get; set; }
        public decimal rjan { get; set; }
        public decimal pfev { get; set; }
        public decimal rfev { get; set; }
        public decimal pmar { get; set; }
        public decimal rmar { get; set; }
        public decimal pabr { get; set; }
        public decimal rabr { get; set; }
        public decimal pmai { get; set; }
        public decimal rmai { get; set; }
        public decimal pjun { get; set; }
        public decimal rjun { get; set; }
        public decimal pjul { get; set; }
        public decimal rjul { get; set; }
        public decimal pago { get; set; }
        public decimal rago { get; set; }
        public decimal pset { get; set; }
        public decimal rset { get; set; }
        public decimal pout { get; set; }
        public decimal rout { get; set; }
        public decimal pnov { get; set; }
        public decimal rnov { get; set; }
        public decimal pdez { get; set; }
        public decimal rdez { get; set; }

        public decimal rames { get; set; }
        public decimal pames { get; set; }


    }



    public class UsuarioViewModel
    {
        [Key]
        public string id { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "Perfil")]
        public string perfil { get; set; }

        public int active { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        public string LockDate { get; set; }


    }
    public class UsuarioRoleViewModel
    {
        public string userid { get; set; }
        public string roleid { get; set; }
        public string perfil { get; set; }
    }

    public class CreateUserModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }


        [Display(Name = "email")]
        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Senha é obrigatória")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Confirmação de senha é obrigatória")]

        [Display(Name = "Confirmação da senha")]
        public string ComfirmPassword { get; set; }
    }

    public class AlterarPerfilViewModel {

        public string id { get; set; }

        [Display(Name = "Nome")]
        public string username { get; set; }
        public string roleid { get; set; }
        [Display(Name = "Perfil atual")]
        public string rolename { get; set; }

    }

    public class EditUserModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        public string ComfirmPassword { get; set; }
    }

    public class Perfil
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Historico
    {
        [Key]
        public int historicoid { get; set; }
        public int fid { get; set; }
        public string tipo { get; set; }
        public DateTime data { get; set; }
        public string texto { get; set; }
    }
    public class Reembolso
    {
        [Key]
        public int reembolsoid { get; set; }
        public DateTime dtcad { get; set; }
        public int tipoid { get; set; }
        public string userid { get; set; }
        public Decimal valor { get; set; }
        public string descricao { get; set; }
        public DateTime vencimento { get; set; }
        public DateTime dtpagto { get; set; }
        public string situacao { get; set; }
    }
    public class ReembolsoItem
    {
        public ReembolsoItem() { }
        private readonly IRepository repository;
        public ReembolsoItem(IRepository objIrepository) { repository = objIrepository; }
        [Key]
        public int reembolsoid { get; set; }

        public DateTime dtcad { get; set; }
        [Display(Name = "Tipo de Reembolso"), Required(ErrorMessage = "Campo obrigatório")]
        public int tipoid { get; set; }
        public string userid { get; set; }
        [Display(Name = "R$ Valor"), Required(ErrorMessage = "Campo obrigatório")]
        public Decimal valor { get; set; }
        [Display(Name = "Descrição"), Required(ErrorMessage = "Campo obrigatório")]
        public string descricao { get; set; }
        [Display(Name = "Referência"), Required(ErrorMessage = "Campo é obrigatório")]
        public DateTime vencimento { get; set; }
        public DateTime dtpagto { get; set; }
        public string situacao { get; set; }

        public IList<ReembolsoViewModel> reembolsosdosusuario { get; set; }
        public void GetReembLancados(int mes, int ano, string userid)
        {

            var ru = repository.ReembolsosUsuario(mes, ano, userid);
            reembolsosdosusuario = ru.ToList();
        }
    }

    public class ReembolsoViewModel
    {

        [Key]
        public int reembolsoid { get; set; }
        public int tipoid { get; set; }
        public string userid { get; set; }
        [Display(Name = "Cadastro")]
        public string dtcad { get; set; }
        [Display(Name = "Tipo Reembolso")]
        public string tiponome { get; set; }
        [Display(Name = "Funcionário")]
        public string usernome { get; set; }
        [Display(Name = "Descrição")]
        public string descricao { get; set; }
        [Display(Name = "R$ Valor")]
        public Decimal valor { get; set; }
        [Display(Name = "Vencimento")]
        public string vencimento { get; set; }
        [Display(Name = "Pagamento")]
        public string dtpagto { get; set; }
        [Display(Name = "Situação")]
        public string situacao { get; set; }
    }

    public class TiposReembolso
    {
        [Key]
        public int tiporeembolsoid { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string nome { get; set; }
    }

    public class RoleViewModel
    {
        string roleid { get; set; }
    }

    public class Produto
    {
        [Key]
        public int produtoid { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "R$ Preço")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }
    }

    public class NF
    {
        [Key]
        public int nfid { get; set; }
        public int contaprid { get; set; }
        public int cliforid { get; set; }
        public DateTime dtcad { get; set; }
        public string no { get; set; }
        public int natureza { get; set; }
        public DateTime vencimento { get; set; }
        public int impressoes { get; set; }
        public string situacao { get; set; }
    }

    public class NFDetail
    {
        [Key]
        public int nfdetailid { get; set; }
        public int nfid { get; set; }
        public int produtoid { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public Decimal unitario { get; set; }
    }

    public class ClienteContaPrViewModel
    {
        public int nfid { get; set; }
        public int contaprid { get; set; }
        public int cliforid { get; set; }
        public string vencimento { get; set; }
        public string emissao { get; set; }
        public string noordem { get; set; }
        public string clientenome { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string endereco { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }

        public IEnumerable<NfDetailViewModel> nfdetails { get; set; }


    }

    public class ClienteContaNota
    {
        public Cliente Cliente { get; set; }
        public ContaPR Conta { get; set; }
        public NfReceber Nota { get; set; }
        public List<NfReceberDetalhe> Detalhes { get; set; }

    }



    public class NfDetailViewModel
    {
        public int nfid { get; set; }
        public int quantidade { get; set; }
        public string produtonome { get; set; }
        public Decimal unitario { get; set; }
        public Decimal total { get; set; }
    }

    public class GraphData
    {
        public string x { get; set; }
        public decimal y { get; set; }
    }


}
using Metasoft.Models;
using Metasoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Metasoft.Interfaces
{
    interface IContasReceber
    {
        #region Listas
        ActionResult Analitico(string fromresumodiario = "", string sort = "");
        [HttpPost]
        ActionResult Analitico(FormCollection fc);

        ActionResult PorCategoria();
        [HttpPost]
        ActionResult PorCategoria(FormCollection fc);
        #endregion

        #region CRUD
        ActionResult Alterar(int id);
        [HttpPost]
        ActionResult Alterar(ContaPR contapr, FormCollection fc);

        ActionResult Nova();
        [HttpPost]
        ActionResult Nova(ContaPR contapr, FormCollection fc);

        ActionResult NotaFiscal(int id);
        [HttpPost]
        ActionResult NotaFiscal(NfReceberViewModel notavm, FormCollection fc);

        ActionResult NovaParcela(int? id);
        [HttpPost]
        ActionResult NovaParcela(ContaPR contapr, FormCollection fc);

        ActionResult CancelarNotaFiscal(int id);
        #endregion

        #region Pagamentos
        Boolean AddParcela(string tipo, int cliforid, ContaPR contapr);

        ActionResult ConfirmaPagamento(int id, string dtpagto);
        ActionResult ArquivarPagamento(int id);
        ActionResult ExcluirPagamento(int id);
        void ImprimirNotaFiscal(int? id);
        #endregion

        #region Relatorios
        ActionResult MensalConsolidado();
        [HttpPost]
        ActionResult MensalConsolidado(FormCollection fc);

        ActionResult OrcamentoAnual();
        [HttpPost]
        ActionResult OrcamentoAnual(FormCollection fc);

        ActionResult OrcamentoContasMes(string tipo, string ano, int mes, int id, bool all = false);
        ActionResult OrcamentoUpdateCaixa(string ano, string mes, string id, string valor);
        #endregion

        #region Helpers
        ActionResult ShowLancamentos(string dia, string mes, string ano, string tipo);
        ActionResult ShowLancamentosMes(string mes, string ano, string tipo);
        ActionResult ShowHistory(int fid, string tipo);
        ActionResult PopulateCategorias(string tipo = "", int id = 0);
        ActionResult PopulatePropostasCliente(int situacaoid, int clienteid);
        ActionResult GetCliFor(string tipo = null);
        ActionResult GetPropostasCliente(int clienteid);
        ActionResult AddNewHistory(int fid, string tipo, string texto);
        void Dispose(bool disposing);

        void DownloadExcel(string tipo, string de, string ate, int categoria, int clifor, string situacao); 
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{


    public class ExtratoViewModel
    {
        public int Id { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoDia { get; set; }
        public List<LinhaExtrato> Linhas { get; set; }
    }

    public class LinhaExtrato
    {
        public int ContaId { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Pagamento { get; set; }
        public string Descricao { get; set; }
        public string NumOrdem { get; set; }
        public string Tipo { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Valor { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Saldo { get; set; }

    }
}
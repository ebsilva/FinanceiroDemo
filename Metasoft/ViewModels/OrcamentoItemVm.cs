using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metasoft.ViewModels
{
    public class OrcamentoItemVm
    {
        public string Ano { get; set; }
        public int Mes { get; set; }
        public int  Id { get; set;}
        public decimal Caixa { get; set; }
        public decimal O { get; set; } = 0;
        public decimal R { get; set; } = 0;
        public decimal Tto { get; set; } = 0;
        public decimal Ttr { get; set; } = 0;
    }
}
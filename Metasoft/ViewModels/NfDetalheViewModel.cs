using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metasoft.ViewModels
{
    public class NfDetalheViewModel
    {
        public class NfReceberDetalheViewModel
        {
            public int NfDetailId { get; set; }
            public int Ordem { get; set; }
            public int Nfid { get; set; }
            public string Descricao { get; set; } = "";
            public int Quantidade { get; set; } = 0;
            public decimal Unitario { get; set; } = 0;
            public decimal Total { get { return this.Quantidade * this.Unitario; } }


        }
    }
}
using Metasoft.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.ViewModels
{
    public class NfReceberViewModel
    {
        public Cliente  Clifor { get; set; }
        public NfReceber Nota { get; set; }
        public List<NfReceberDetalhe> Detalhes { get; set; }
    }
}
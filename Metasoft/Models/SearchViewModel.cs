using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class SearchViewModel
    {
        public string DivClass { get; set; }
        public string Tabela { get; set; }
        public string Id { get; set; }
        public string Mtop { get; set; }
        public string  Height { get; set; }
        public int StarCol { get; set; }
        public int EndCol { get; set; }
    }
}
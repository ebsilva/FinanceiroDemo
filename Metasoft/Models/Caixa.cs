using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class Caixa
    {
        [Key]
        public int Id { get; set; }
        public int Cat { get; set; }
        public int Ano { get; set; }
        public decimal Cja { get; set; }
        public decimal Cfv { get; set; }
        public decimal Cmr { get; set; }
        public decimal Cab { get; set; }
        public decimal Cma { get; set; }
        public decimal Cju { get; set; }
        public decimal Cjl { get; set; }
        public decimal Cag { get; set; }
        public decimal Cst { get; set; }
        public decimal Cou { get; set; }
        public decimal Cno { get; set; }
        public decimal Cde { get; set; }

    }
}
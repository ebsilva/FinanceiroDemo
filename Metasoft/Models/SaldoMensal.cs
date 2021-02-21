using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class SaldoMensal
    {
        [Key]
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }     // Not Used
        public string Tipo { get; set; } // Not used
        public decimal Saldo { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class SaldoAnual
    {
        [Key]
        public int Id { get; set; }
        public int Ano { get; set; }
        public DateTime Inicio { get; set; } 
        public Decimal Saldo { get; set; } = 0;
    }
}
using Metasoft.Infrastructure;
using Metasoft.Models;
using System;

namespace Metasoft.ViewModels
{
    public class SaldoMensalViewModel 
    {
        private readonly IRepository _repo;
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }

        public SaldoMensalViewModel()
        {
            _repo = new Repository();
            var r = _repo.GetSaldoAtual();
            this.Ano = DateTime.Now.Year;
            this.Mes = 0;
            this.Saldo = r;
            this.Tipo = "";
 
        }
    }

}
using Metasoft.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using Metasoft.Models;

namespace Metasoft.Utilities
{
    /// <summary>
    /// Linq custom extension
    /// </summary>
    public static class Lce
    {

        public static bool IsLate(this ContaFilter c,DateTime vencimento, string situacao)
        {
            if (DateTime.Now > vencimento && situacao == "A")
                return true;

            return false;
        }

        /// A ideia e filtrar a partir desta classe estatica

        //public static ContaPrViewModel  GetData (ThisLiteral ContaFilter cf)
        //{
        //    var r = from c in ContaPrs
        //            join cl in Clientes on c.Cliforid equals cl.Clienteid
        //            join ca in Categorias on c.Categoriaid equals ca.Categoriaid
        //            join p in Propostas on c.Propostaid equals p.Propostaid into pp
        //            from cp in pp.DefaultIfEmpty()
        //            select new { c, clienteNome = cl.Nome, categoriaNome = ca.Nome, cp.Propostaid, atrasado = IsLate(c.Vencimento, c.Situacao) };

        //    return null;
        //}


    }

    public class ContaFilter
    {

        public string Tipo { get; set; } = "";
        public string TipoData { get; set; } = "vencimento";
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Categoria { get; set; } = 0;
        public int Cliente { get; set; } = 0;
        public string Situacao { get; set; } = "";
        public string Mode { get; set; } = "";
        public string Sort { get; set; } = "";
        public string Respontavel { get; set; } = "";


    };
}
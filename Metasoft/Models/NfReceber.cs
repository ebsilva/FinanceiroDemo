using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class NfReceber
    {
        [Key]
        public int NfId { get; set; }
        public int NumeroNf { get; set; }
        public string NumeroOrdem { get; set; }
        public int ContaId { get; set; }
        public int CliForId { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public int NumPrint { get; set; }
        public string OpPag { get; set; }
        public string Situacao { get; set; }
    }

    public class NfReceberDetalhe
    {
        [Key]
        public int NfDetailId { get; set; }
        public int Nfid { get; set; }
        public int Ordem { get; set; }
        public string Descricao { get; set; } = "";
        public int Quantidade { get; set; } = 0;
        public decimal Unitario { get; set; } = 0;


    }

    public class NfNumeracao
    {
        [Key]
        public int NfIdGerado { get; set; }
        public int NfIdReceber { get; set; }
        public DateTime Criacao { get; set; }
        public int Status { get; set; }
    }
}
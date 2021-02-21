using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class Visita
    {
        public int VisitaId { get; set; }
        public int propostaid { get; set; }
        public DateTime DateCad { get; set; } = DateTime.Now;
        public DateTime VisitaEm { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }
        public string Contato { get; set; }
        public string Fone { get; set; }
        public string Celular { get; set; }
        public string Local { get; set; }
    }
}
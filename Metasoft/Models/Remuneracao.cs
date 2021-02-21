using System;
using System.ComponentModel.DataAnnotations;

namespace Metasoft.Models
{

    public class Remuneracao
    {
        [Key]
        public int id { get; set; }
        public DateTime dtcad { get; set; }
        public DateTime dtlan { get; set; }
        public int propostaid { get; set; }
        public int comercialid { get; set; }
        public decimal? insumos { get; set; }
        public decimal percentual { get; set; }

    }

    public class RemuneracaoViewModel
    {
        public string propostaid { get; set; }
        public int id { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }

        public string comercial { get; set; }
        public string cliente { get; set; }
        public string descricao { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal insumos { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal liqsi { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal liqci { get; set; }

        public int percentual { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public int faturada { get; set; }
        public string situacaonome { get; set; }
        public string cor { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal remuneracao { get; set; }
    }

    public class NovaRemuneracaoViewModel
    {

        [Required(ErrorMessage = "Proposta é obrigatório")]
        public int proposta { get; set; }

        [Required(ErrorMessage = "Comercial é obrigatório")]
        public int comercial { get; set; }

        public decimal? insumos { get; set; }

        [Required(ErrorMessage = "Percentual é obrigatório")]
        public decimal percentual { get; set; }
        
    }

    public class ComercialRemuneracaoViewModel
    {
        public int id { get; set; }
        public string nome { get; set; }
    }
}
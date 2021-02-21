using System;
using System.ComponentModel.DataAnnotations;

namespace Metasoft.Models
{
    public class Comercial
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nome"), Required(ErrorMessage = "Nome é obrigatória")]
        public string nome { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
    }
    public class ComercialViewModel
    {
        public int id { get; set; }
        [Display(Name = "Nome"), Required(ErrorMessage = "Nome é obrigatória")]
        public string nome { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Decimal faturado { get; set; }
    }


}
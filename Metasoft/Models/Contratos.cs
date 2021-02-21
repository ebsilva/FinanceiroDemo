using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{

    public class Contrato
    {
        public int id { get; set; }
        public DateTime dtcad { get; set; }
        public int clienteid { get; set; }
        public string descricao { get; set; }
        public int tipocontrato { get; set; }
        public int indice { get; set; }
        public int situacaoid { get; set; }

    }

    public class Renovacao
    {
        public int id { get; set; }
        public DateTime dtcad { get; set; }

        public int? contratoid { get; set; }

        public DateTime dtinicio { get; set; }

        public DateTime dttermino { get; set; }

        public DateTime? iniciosv { get; set; }


        public decimal valor { get; set; }

        public DateTime? proxreajuste { get; set; }

        public int prazo { get; set; }

        public string observacao { get; set; }

        public int situacaoid { get; set; }

    }

    public class ContratoViewModel
    {
        public int id { get; set; }
        public int renovacaoid { get; set; }

        public DateTime dtcad { get; set; }

        [Display(Name ="Cliente")]
        [Required(ErrorMessage = "Cliente é obrigatório")]
        public int clienteid { get; set; }

        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Tipo de contrato")]
        [Required(ErrorMessage = "Tipo de contrato é obrigatório")]
        public int tipocontrato { get; set; }

        [Display(Name = "Indice")]
        [Required(ErrorMessage = "Índice é obrigatório")]
        public int indice { get; set; }

        //public int contratoid { get; set; }

        [Display(Name = "Início")]
        [Required(ErrorMessage = "Data início de contrato é obrigatória")]
        public DateTime dtinicio { get; set; }

        [Display(Name = "Termino")]
        [Required(ErrorMessage = "Data término de contrato é obrigatória")]
        public DateTime dttermino { get; set; }

        [Display(Name = "Início Serviço")]
        public DateTime? iniciosv { get; set; }

        [Display(Name = "R$ Valor")]
        [Required(ErrorMessage = "Valor do contrato é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        [Display(Name = "Dt. Reaj.")]
        public DateTime? proxreajuste { get; set; }

        [Display(Name = "Prazo")]
        [Required(ErrorMessage = "Prazo do contrato é obrigatória")]
        public int prazo { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }

        public int situacaoid { get; set; }
        public int situacaorenovacaoid { get; set; }

    }


    public class ContratosViewModel
    {
        public int id { get; set; }
        public DateTime dtcad { get; set; }
        public int clienteid { get; set; }

        public string clientenome { get; set; }
        
        public int indiceid { get; set; }
        public string indice { get; set; }

        public string tipocontrato { get; set; }

        public string dtinicio { get; set; }
        public string dttermino { get; set; }
        public string iniciosv { get; set; }

        [Display(Name = "R$ Valor")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }
        public string proxreajuste { get; set; }

        public int prazo { get; set; }
        public string observacao { get; set; }
        public int situacaoid { get; set; }
        public string situacao { get; set; }
        public string cor { get; set; }
        public int contas { get; set; }
    }

    public class ContratosExcelViewModel
    {
        public int Numero { get; set; }

        public string Cliente { get; set; }

        public string Indice { get; set; }

        public string Tipo_Contrato { get; set; }

        public string Inicio { get; set; }
        public string Termino { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        public string Reajuste { get; set; }

        public int Prazo { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }
    }


    public class Indice
    {
        public int id { get; set; }
        public string nome { get; set; }
    }

    public class TipoContrato
    {
        public int id { get; set; }
        public string nome { get; set; }
    }

    public class HistoricoContratoViewModel
    {
        public int id { get; set; }
        public string dtcad { get; set; }
        public string inicio { get; set; }
        public string termino { get; set; }
        public string reajuste { get; set; }
        public decimal valor { get; set; }
        public string observacao { get; set; }
        public string situacao { get; set; }
    }
}
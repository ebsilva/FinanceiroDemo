using System;
using System.ComponentModel.DataAnnotations;

namespace Metasoft.Models
{

    public class Permissao
    {
        [Key]
        public string roleid { get; set; }
        public Boolean pro_edit { get; set; }
        public Boolean pro_view { get; set; }

        public Boolean rec_edit { get; set; }
        public Boolean rec_view { get; set; }

        public Boolean pag_edit { get; set; }
        public Boolean pag_view { get; set; }

        public Boolean cad_edit { get; set; }
        public Boolean cad_view { get; set; }

        public Boolean rep_edit { get; set; }
        public Boolean rep_view { get; set; }

        public Boolean ree_edit { get; set; }
        public Boolean ree_view { get; set; }

        /* Adicionado em 31/03/2017 */
        public Boolean prj_edit { get; set; }
        public Boolean prj_view { get; set; }

        /* Adicionado em 30/05/2017 */
        public Boolean ctt_edit { get; set; }
        public Boolean ctt_view { get; set; }

        public Boolean rem_edit { get; set; }
        public Boolean rem_view { get; set; }
    }

    public class PerfilPermissaoViewModel
    {
        [Key]
        public string roleid { get; set; }
        public string name { get; set; }
        public Boolean pro_edit { get; set; }
        public Boolean pro_view { get; set; }

        public Boolean rec_edit { get; set; }
        public Boolean rec_view { get; set; }

        public Boolean pag_edit { get; set; }
        public Boolean pag_view { get; set; }

        public Boolean cad_edit { get; set; }
        public Boolean cad_view { get; set; }

        public Boolean rep_edit { get; set; }
        public Boolean rep_view { get; set; }

        public Boolean ree_edit { get; set; }
        public Boolean ree_view { get; set; }

        /* Adicionado em 31/03/2017 */
        public Boolean prj_edit { get; set; }
        public Boolean prj_view { get; set; }

        /* Adicionado em 30/05/2017 */
        public Boolean ctt_edit { get; set; }
        public Boolean ctt_view { get; set; }

        public Boolean rem_edit { get; set; }
        public Boolean rem_view { get; set; }

    }


    public class CreatePerfilViewModel
    {
        [Key]
        public string roleid { get; set; }
        [Required(ErrorMessage = "Nome do perfil é obrigatório")]
        public string name { get; set; }
        public int? propostas { get; set; }
        public int? receber { get; set; }
        public int? pagar { get; set; }
        public int? cadcadastros { get; set; }
        public int? relatorios { get; set; }
        public int? reembolso { get; set; }
        public int? projetos { get; set; }

        /* Adicionado em 30/05/2017 */
        public int? contratos { get; set; }

        public int? remuneracoes { get; set; }
    }

}
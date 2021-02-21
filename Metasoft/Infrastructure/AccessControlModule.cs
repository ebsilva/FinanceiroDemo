using System;

using System.Web;
using Metasoft.Models;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Metasoft.Infrastructure
{
    public class AccessControlModule : IHttpModule {

        public void Init(HttpApplication app)
        {
            app.PostRequestHandlerExecute += HandleEvent;
        }

        private void HandleEvent(object src,EventArgs args)
        {
            string url = System.Web.HttpContext.Current.Request.RawUrl;
            HttpContext context = HttpContext.Current;
            Permissao permissoes = null;

            if (HttpContext.Current.Session != null)
            {

                permissoes = permissoes = (Permissao)HttpContext.Current.Session["permissoes"];
                var ca = context.Request.RequestContext.RouteData;
                string action = ca.GetRequiredString("action");
                string controller = ca.GetRequiredString("controller");
                switch (action)
                {
                    case "Login":
                    case "AcessoNaoPermitido": break;
                    case "LogOff": break;
                    default:
                        if (!AcessoPermitido(action, permissoes))
                        {
                            context.Response.Redirect("~/Home/AcessoNaoPermitido");
                        }
                        break;
                }



            }
            //else
            //{
            //    //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //    context.Response.Redirect("~/Account/Login"); ;
            //}

        }

        protected Boolean AcessoPermitido(String modulo, Permissao permissoes)
        {

            if(permissoes == null)
            {
                return false;
            }
            switch (modulo.ToLower())
            {
                case "login": return true;
                /* Propostas */
                case "resumodiario": if (permissoes.rep_view || permissoes.rep_edit) { return true; }; break;
                case "listapropostas": if (permissoes.pro_view || permissoes.pro_edit) { return true; }; break;
                case "novaproposta": if (permissoes.pro_edit) { return true; }; break;
                case "alterarproposta": if (permissoes.pro_edit) { return true; }; break;
                case "excluirproposta": if (permissoes.pro_edit) { return true; }; break;
                /* A pagar */
                case "apagaranalitico": if (permissoes.pag_view || permissoes.pag_edit) { return true; }; break;
                case "apagarporcategoria": if (permissoes.rep_view) { return true; }; break;
                case "novacontaapagar": if (permissoes.pag_edit) { return true; }; break;
                case "alterarcontaapagar": if (permissoes.pag_edit) { return true; }; break;
                case "novaparcelaapagar": if (permissoes.pag_edit) { return true; }; break;
                /* A receber/pagar */
                case "excluirpagamento": if (permissoes.pag_edit || permissoes.rec_edit) { return true; }; break;
                case "excluirnotafiscalareceber": if (permissoes.rec_edit) { return true; }; break;
                case "confirmapagamento": if (permissoes.rec_edit || permissoes.pag_edit) { return true; }; break;
                case "diarioconsolidado": if (permissoes.rep_edit) { return true; }; break;
                case "mensalconsolidado": if (permissoes.rep_edit) { return true; }; break;
                case "addNewhistory": if (permissoes.rep_edit || permissoes.pag_edit) { return true; }; break;
                case "showhistory": if (permissoes.rep_edit) { return true; }; break;
                /* A receber */
                case "areceberanalitico": if (permissoes.rec_view || permissoes.rec_edit) { return true; }; break;
                case "areceberaporcategoria": if (permissoes.rec_view) { return true; }; break;
                case "novacontaareceber": if (permissoes.rec_edit) { return true; }; break;
                case "alterarcontareceber": if (permissoes.rec_edit) { return true; }; break;
                case "novaparcelaareceber": if (permissoes.rec_edit) { return true; }; break;
                case "notafiscalareceber": if (permissoes.rec_edit) { return true; }; break;
                case "imprimirnotafiscalareceber": if (permissoes.rec_edit) { return true; }; break;
                case "cancelarnotaFiscalareceber": if (permissoes.rec_edit) { return true; }; break;
                /* Categorias */
                case "listacategorias": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novacategoria": if (permissoes.cad_edit) { return true; }; break;
                case "alterarcategoria": if (permissoes.cad_edit) { return true; }; break;
                case "excluircategoria": if (permissoes.cad_edit) { return true; }; break;
                case "saveNewcategoria": if (permissoes.cad_edit) { return true; }; break;
                /* Clientes */
                case "listaclientes": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novocliente": if (permissoes.cad_edit) { return true; }; break;
                case "alterarcliente": if (permissoes.cad_edit) { return true; }; break;
                case "excluircliente": if (permissoes.cad_edit) { return true; }; break;
                /* Fornecedores */
                case "listafornecedores": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novofornecedor": if (permissoes.cad_edit) { return true; }; break;
                case "alterarfornecedor": if (permissoes.cad_edit) { return true; }; break;
                case "excluirfornecedor": if (permissoes.cad_edit) { return true; }; break;
                /*  Produtos */
                case "listaprodutos": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novoproduto": if (permissoes.cad_edit) { return true; }; break;
                case "alterarproduto": if (permissoes.cad_edit) { return true; }; break;
                case "excluirproduto": if (permissoes.cad_edit) { return true; }; break;
                /* Reembolso */
                case "reembolsousuario": if (permissoes.ree_edit || permissoes.ree_view) { return true; }; break;
                case "listareembolso": if (permissoes.ree_edit || permissoes.ree_view) { return true; }; break;
                case "alterarreembolso": if (permissoes.ree_edit) { return true; }; break;
                case "cadastrarreembolso": if (permissoes.ree_edit) { return true; }; break;
                case "excluirreembolso": if (permissoes.ree_edit) { return true; }; break;
                /*  Tipos Reembolso */
                case "listaTiposreembolso": if (permissoes.ree_edit || permissoes.ree_view) { return true; }; break;
                case "novotiporeembolso": if (permissoes.ree_edit) { return true; }; break;
                case "alterartiporeembolso": if (permissoes.ree_edit) { return true; }; break;
                case "excluirriporeembolso": if (permissoes.ree_edit) { return true; }; break;
                /* Perfis */
                case "listaperfis": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novoperfil": if (permissoes.cad_edit) { return true; }; break;
                case "alterarperfil": if (permissoes.cad_edit) { return true; }; break;
                /* Usuarios */
                case "listausuarios": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novousuario": if (permissoes.cad_edit) { return true; }; break;
                case "resetpwd": if (permissoes.cad_edit) { return true; }; break;
                case "excluirusuario": if (permissoes.cad_edit) { return true; }; break;
                case "bloquear": if (permissoes.cad_edit) { return true; }; break;
                case "desbloquear": if (permissoes.cad_edit) { return true; }; break;
                /* Contratos*/
                case "listacontratos": if (permissoes.ctt_edit || permissoes.ctt_view) { return true; }; break;
                case "novocontrato": if (permissoes.ctt_edit) { return true; }; break;
                case "alterarcontrato": if (permissoes.ctt_edit) { return true; }; break;
                case "lancarcr": if (permissoes.ctt_edit) { return true; }; break;
                //case "populatecategorias": if (permissoes.ctt_edit) { return true; }; break;
                //case "getclifor": if (permissoes.ctt_edit) { return true; }; break;
                //case "populateclienteswithproposta": if (permissoes.ctt_edit) { return true; }; break;
                //case "populateindices": if (permissoes.ctt_edit) { return true; }; break;

                /* Remuneracoes */
                case "listaremuneracao": if (permissoes.rem_edit || permissoes.rem_view) { return true; }; break;
                case "novaremuneracao": if (permissoes.rem_edit) { return true; }; break;
                case "alterarremuneracao": if (permissoes.rem_edit) { return true; }; break;
                /* Comercial */
                case "listacomercial": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "novocomercial": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                case "alterarcomercial": if (permissoes.cad_edit || permissoes.cad_view) { return true; }; break;
                
                default: return true;
            }
            return false;
        }


        public void Dispose()
        {
            //do nothing
        }
    }
}
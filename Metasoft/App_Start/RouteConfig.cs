using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Metasoft
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            /* CLIENTES */
            routes.MapRoute("GetMunicipios",
                         "Clientes/GetMunicipios/",
                         new { controller = "Clientes", action = "GetMunicipios" },
                         new[] { "Metasoft.Controllers" });
            routes.MapRoute("SaveNewContato",
                        "Clientes/SaveNewContato",
                         new { controller = "Clientes", action = "SaveNewContato" },
                         new[] { "Metasoft.Controllers" });
            routes.MapRoute("ContatoDelete",
                       "Clientes/ContatoDelete",
                       new { controller = "Clientes", action = "ContatoDelete" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetContatos",
                       "Clientes/GetContatos",
                       new { controller = "Clientes", action = "GetContatos" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("ShowContatos",
                       "Clientes/ShowContatos",
                       new { controller = "Clientes", action = "ShowContatos" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("ShowContas",
                       "Clientes/ShowContas",
                       new { controller = "Clientes", action = "ShowContas" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirContato",
                      "Clientes/ExcluirContato",
                      new { controller = "Clientes", action = "ExcluirContato" },
                      new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirCliente",
                      "Clientes/ExcluirCliente",
                      new { controller = "Cliente", action = "ExcluirCliente" },
                      new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirFornecedor",
                      "Clientes/ExcluirFornecedor",
                      new { controller = "Cliente", action = "ExcluirFornecedor" },
                      new[] { "Metasoft.Controllers" });

            /* CATEGORIAS */
            routes.MapRoute("SaveNewCategoria",
                     "Categorias/SaveNewCategoria",
                     new { controller = "Categorias", action = "SaveNewCategoria" },
                     new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirCategoria",
                         "Categorias/ExcluirCategoria",
                         new { controller = "Categorias", action = "ExcluirCategoria" },
                         new[] { "Metasoft.Controllers" });


            /* PROPOSTAS */
            routes.MapRoute("SavePagamentosProposta",
                     "Propostas/SavePagamentosProposta",
                     new { controller = "Propostas", action = "SavePagamentosProposta" },
                     new[] { "Metasoft.Controllers" });
            routes.MapRoute("LiberarProposta",
                     "Propostas/LiberarProposta",
                     new { controller = "Propostas", action = "LiberarProposta" },
                     new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetClientes",
                     "Propostas/GetClientes",
                     new { controller = "Propostas", action = "GetClientes" },
                     new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirProposta",
                     "Propostas/ExcluirProposta",
                     new { controller = "Propostas", action = "ExcluirProposta" },
                     new[] { "Metasoft.Controllers" });
            routes.MapRoute("ShowContasProposta",
                       "Propostas/ShowContasProposta",
                       new { controller = "Propostas", action = "ShowContasProposta" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("VoltarFluxo",
                       "Propostas/VoltarFluxo",
                       new { controller = "Propostas", action = "VoltarFluxo" },
                       new[] { "Metasoft.Controllers" });
            routes.MapRoute("AvancarFluxo",
                       "Propostas/AvancarFluxo",
                       new { controller = "Propostas", action = "AvancarFluxo" },
                       new[] { "Metasoft.Controllers" });

            /* CONTAPRS */
            routes.MapRoute("PopulateCategorias",
                   "ContaPRs/PopulateCategorias",
                   new { controller = "ContaPRs", action = "PopulateCategorias" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetCliFor",
                   "ContaPRs/GetCliFor",
                   new { controller = "ContaPRs", action = "GetCliFor" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("ConfirmaPagamento",
                   "ContaPRs/ConfirmaPagamento",
                   new { controller = "ContaPRs", action = "ConfirmaPagamento" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("ShowLancamentos",
                   "ContaPRs/ShowLancamentos",
                   new { controller = "ContaPRs", action = "ShowLancamentos" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetPropostasCliente",
                   "ContaPRs/GetPropostasCliente",
                   new { controller = "ContaPRs", action = "GetPropostasCliente" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("AddNewHistory",
                  "ContaPRs/AddNewHistory",
                  new { controller = "ContaPRs", action = "AddNewHistory" },
                  new[] { "Metasoft.Controllers" });
            routes.MapRoute("ShowHistory",
                 "ContaPRs/ShowHistory",
                 new { controller = "ContaPRs", action = "ShowHistory" },
                 new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetHistory",
                 "ContaPRs/GetHistory",
                 new { controller = "ContaPRs", action = "GetHistory" },
                 new[] { "Metasoft.Controllers" });
            routes.MapRoute("PopulatePropostasCliente",
                 "ContaPrs/PopulatePropostasCliente",
                 new { controller = "ContaPrs", action = "PopulatePropostasCliente" },
                 new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirNotaFiscalAreceber",
                 "ContaPRs/ExcluirNotaFiscalAreceber",
                 new { controller = "ContaPRs", action = "ExcluirNotaFiscalAreceber" },
                 new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirPagamento",
                         "ContaPRs/ExcluirPagamento",
                         new { controller = "ContaPRs", action = "ExcluirPagamento" },
                         new[] { "Metasoft.Controllers" });
            routes.MapRoute("ArquivarPagamento",
                   "ContaPRs/ArquivarPagamento",
                   new { controller = "ContaPRs", action = "ArquivarPagamento" },
                   new[] { "Metasoft.Controllers" });

            routes.MapRoute("ListaIndices",
               "Indices/ListaIndices",
               new { controller = "Indices", action = "ListaIndices" },
               new[] { "Metasoft.Controllers" });

            /* REEMBOLSOS */
            routes.MapRoute("PopulateTiposReembolso",
                   "Reembolsoes/PopulateTiposReembolso",
                   new { controller = "Reembolsoes", action = "PopulateTiposReembolso" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirReembolso",
                   "Reembolsoes/ExcluirReembolso",
                   new { controller = "Reembolsoes", action = "ExcluirReembolso" },
                   new[] { "Metasoft.Controllers" });

            /* REMUNERACAO */
            routes.MapRoute("PopulatePropostasRemuneracao",
                   "Remuneracao/PopulatePropostasRemuneracao",
                   new { controller = "Remuneracao", action = "PopulatePropostasRemuneracao" },
                   new[] { "Metasoft.Controllers" });

            routes.MapRoute("PopulateComercialRemuneracao",
                   "Remuneracao/PopulateComercialRemuneracao",
                   new { controller = "Remuneracao", action = "PopulateComercialRemuneracao" },
                   new[] { "Metasoft.Controllers" });

            

            /* PRODUTOS */
            routes.MapRoute("PopulateProdutos",
                   "Produtos/PopulateProdutos",
                   new { controller = "Reembolsoes", action = "PopulateProdutos" },
                   new[] { "Metasoft.Controllers" });


            /* USUARIOS */
            routes.MapRoute("PopulatePerfis",
                   "Usuarios/PopulatePerfis",
                   new { controller = "Usuarios", action = "PopulatePerfis" },
                   new[] { "Metasoft.Controllers" });
            routes.MapRoute("GetUsuarios",
                  "Usuarios/GetUsuarios",
                  new { controller = "Usuarios", action = "GetUsuarios" },
                  new[] { "Metasoft.Controllers" });
            routes.MapRoute("ExcluirUsuario",
                  "Usuarios/ExcluirUsuario",
                  new { controller = "Usuarios", action = "ExcluirUsuario" },
                  new[] { "Metasoft.Controllers" });
            routes.MapRoute("BloquearUsuario",
                  "Usuarios/BloquearUsuario",
                  new { controller = "Usuarios", action = "BloquearUsuario" },
                  new[] { "Metasoft.Controllers" });
            routes.MapRoute("DesbloquearUsuario",
                  "Usuarios/DesbloquearUsuario",
                  new { controller = "Usuarios", action = "DesbloquearUsuario" },
                  new[] { "Metasoft.Controllers" });
            routes.MapRoute("ResetPwd",
                  "Usuarios/ResetPwd",
                  new { controller = "Usuarios", action = "ResetPwd" },
                  new[] { "Metasoft.Controllers" });
        }
    }
}

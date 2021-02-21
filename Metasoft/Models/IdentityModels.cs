using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Metasoft.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string nome { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime lastpwdupdate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create() {return new ApplicationDbContext();}
        public System.Data.Entity.DbSet<Proposta> Propostas { get; set; }
        public System.Data.Entity.DbSet<Cliente> Clientes { get; set; }
        public System.Data.Entity.DbSet<Cidade> Cidades { get; set; }
        public System.Data.Entity.DbSet<Fp>Fps { get; set; }
        public System.Data.Entity.DbSet<Contato> Contatoes { get; set; }
        public System.Data.Entity.DbSet<Categoria> Categorias { get; set; }
        public System.Data.Entity.DbSet<ContaPR> ContaPRs { get; set; }
        public System.Data.Entity.DbSet<Historico> Historicoes { get; set; }
        public System.Data.Entity.DbSet<Reembolso> Reembolsoes { get; set; }
        public System.Data.Entity.DbSet<TiposReembolso> TiposReembolsoes { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Produto> Produtoes { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.NF> Nfs { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.NFDetail> Nfdetails { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Permissao> Permissoes { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Comercial> Comercials { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Remuneracao> Remuneracaos { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Caixa> Caixas { get; set; }

        public System.Data.Entity.DbSet<Metasoft.Models.Contrato> Contratoes { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.Renovacao> Renovacaos { get; set; }

        public System.Data.Entity.DbSet<Metasoft.Models.Indice> Indices { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.TipoContrato> TipoContratoes { get; set; }
        /* 24/07/2018*/
        public System.Data.Entity.DbSet<Metasoft.Models.Visita> Visitas { get; set; }
        /* 06/10/2020*/
        public System.Data.Entity.DbSet<Metasoft.Models.NfReceber> NfRecebers { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.NfReceberDetalhe> NfReceberDetalhes { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.NfNumeracao> NfNumeracaos { get; set; }
        /* 04 Janeiro 2021*/
        public System.Data.Entity.DbSet<Metasoft.Models.SaldoMensal> SaldoMensals { get; set; }
        public System.Data.Entity.DbSet<Metasoft.Models.SaldoAnual> SaldoAnuals { get; set; }


    }
}
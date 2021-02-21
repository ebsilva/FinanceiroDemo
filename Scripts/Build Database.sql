SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------------------------------------------------------
-- AspNetUsers
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[nome] [varchar](60) NULL,
	[active] [smallint] NOT NULL,
	[lastpwdupdate] [datetime] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((1)) FOR [active]

----------------------------------------------------------------------------------------------------------------------
-- AspNetRoles
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- AspNetUserRoles
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
go
go

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
----------------------------------------------------------------------------------------------------------------------
-- AspNetUserLogins
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

----------------------------------------------------------------------------------------------------------------------
-- AspNetUserClaims
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD  CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
----------------------------------------------------------------------------------------------------------------------
-- dias
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[dias](
	[dia] [int] NOT NULL
)
GO
----------------------------------------------------------------------------------------------------------------------
-- Meses
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Meses](
	[mes] [int] NOT NULL,
	[nome] [varchar](20) NOT NULL
)
GO

----------------------------------------------------------------------------------------------------------------------
-- Fps
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Fps](
	[fpid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL
)
GO
----------------------------------------------------------------------------------------------------------------------
-- Indices
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Indices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-112638] ON [dbo].[Indices]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- Naturezas
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Naturezas](
	[naturezaid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NOT NULL
)
GO
----------------------------------------------------------------------------------------------------------------------
-- Comercials
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Comercials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
 CONSTRAINT [PK_dbo.Comercials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170504-ComercialEmail] ON [dbo].[Comercials]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170504-ComercialNome] ON [dbo].[Comercials]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- Permissaos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Permissaos](
	[roleid] [nvarchar](128) NOT NULL,
	[pro_edit] [bit] NULL,
	[pro_view] [bit] NULL,
	[rec_edit] [bit] NULL,
	[rec_view] [bit] NULL,
	[pag_edit] [bit] NULL,
	[pag_view] [bit] NULL,
	[cad_edit] [bit] NULL,
	[cad_view] [bit] NULL,
	[rep_edit] [bit] NULL,
	[rep_view] [bit] NULL,
	[ree_edit] [bit] NULL,
	[ree_view] [bit] NULL,
	[prj_edit] [bit] NULL,
	[prj_view] [bit] NULL,
	[ctt_edit] [bit] NULL,
	[ctt_view] [bit] NULL,
	[rem_edit] [bit] NULL,
	[rem_view] [bit] NULL,
 CONSTRAINT [PK_dbo.Permissoes] PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [pro_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [pro_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rec_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rec_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [pag_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [pag_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [cad_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [cad_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rep_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rep_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [ree_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [ree_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [ctt_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [ctt_view]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rem_edit]
GO
ALTER TABLE [dbo].[Permissaos] ADD  DEFAULT ((0)) FOR [rem_view]
GO
ALTER TABLE [dbo].[Permissaos]  WITH CHECK ADD  CONSTRAINT [FK_1_Permissoes] FOREIGN KEY([roleid])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permissaos] CHECK CONSTRAINT [FK_1_Permissoes]
GO
ALTER TABLE [dbo].[Permissaos]  WITH CHECK ADD  CONSTRAINT [FK_1_Permissoes] FOREIGN KEY([roleid])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO




----------------------------------------------------------------------------------------------------------------------
-- Atividades
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Atividades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [datetime] NOT NULL,
	[nome] [varchar](100) NULL
)

GO

ALTER TABLE [dbo].[Atividades] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170419-175545] ON [dbo].[Atividades]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

Select * from atividades
insert atividades (nome) values('Codificação')
insert atividades (nome) values('Fraseologia')
insert atividades (nome) values('Implantação')
insert atividades (nome) values('Testes Internos')
insert atividades (nome) values('Homologação')
insert atividades (nome) values('Suporte')
insert atividades (nome) values('Reunião')
insert atividades (nome) values('Análise documentação')

----------------------------------------------------------------------------------------------------------------------
-- Lancamentoes
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Lancamentoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](128) NOT NULL,
	[projetoid] [int] NOT NULL,
	[atividadeid] [int] NOT NULL,
	[dia] [date] NOT NULL,
	[horas] [int] NULL
)

GO

ALTER TABLE [dbo].[Lancamentoes] ADD  DEFAULT ((0)) FOR [horas]
GO

ALTER TABLE [dbo].[Lancamentoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Lancamentoes_dbo.Atividades_id] FOREIGN KEY([atividadeid])
REFERENCES [dbo].[Atividades] ([id])
GO

ALTER TABLE [dbo].[Lancamentoes] CHECK CONSTRAINT [FK_dbo.Lancamentoes_dbo.Atividades_id]
GO

CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170403-181353] ON [dbo].[Lancamentoes]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181419] ON [dbo].[Lancamentoes]
(
	[projetoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181439] ON [dbo].[Lancamentoes]
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181850] ON [dbo].[Lancamentoes]
(
	[dia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


----------------------------------------------------------------------------------------------------------------------
-- Situacaos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Situacaos](
	[situacaoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[cor] [varchar](20) NULL
)

GO
ALTER TABLE [dbo].[Situacaos] ADD  DEFAULT ('white') FOR [cor]
CREATE UNIQUE CLUSTERED INDEX [Pk_Situacoes] ON [dbo].[Situacaos]
(
	[situacaoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- SituacaoProjetos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[SituacaoProjetos](
	[situacaoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[cor] [varchar](20) NOT NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170421-095119] ON [dbo].[SituacaoProjetos]
(
	[situacaoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


----------------------------------------------------------------------------------------------------------------------
-- TipoContratoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[TipoContratoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-095217] ON [dbo].[TipoContratoes]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO




----------------------------------------------------------------------------------------------------------------------
--  Historicoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Historicoes](
	[historicoid] [int] IDENTITY(1,1) NOT NULL,
	[fid] [int] NOT NULL,
	[tipo] [char](1) NOT NULL,
	[texto] [varchar](2000) NULL
)
GO
CREATE NONCLUSTERED INDEX [Historico_fid] ON [dbo].[Historicoes]
(
	[fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


----------------------------------------------------------------------------------------------------------------------
--  Propostas
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Propostas](
	[propostaid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[clienteid] [int] NOT NULL,
	[envio] [date] NULL,
	[numpo] [nvarchar](max) NULL,
	[numnf] [nvarchar](max) NULL,
	[fp] [int] NOT NULL,
	[recebido] [bit] NOT NULL,
	[he] [int] NULL,
	[hu] [int] NULL,
	[previsao] [int] NULL,
	[situacaoid] [int] NOT NULL,
	[descricao] [varchar](200) NULL,
	[dtaceite] [date] NULL,
	[dtfinalizacao] [date] NULL,
	[valor] [decimal](12, 2) NOT NULL,
	[categoriaid] [int] NULL,
	[dtiniprj] [date] NULL,
	[dtfimprj] [date] NULL,
	[dtfimprvprj] [date] NULL,
	[situacaoprojetoid] [int] NOT NULL,
	[observacao] [varchar](200) NULL,
	[isproject] [int] NULL
)

GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT ((0)) FOR [fp]
GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT ((1)) FOR [situacaoid]
GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT ((0)) FOR [valor]
GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT ((2)) FOR [situacaoprojetoid]
GO

ALTER TABLE [dbo].[Propostas] ADD  DEFAULT ((0)) FOR [isproject]
CREATE UNIQUE CLUSTERED INDEX [Pk_Propostas] ON [dbo].[Propostas]
(
	[propostaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [Propostas_clienteid] ON [dbo].[Propostas]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [SituacaoId-NC-Index-20170502-145519] ON [dbo].[Propostas]
(
	[situacaoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [SituacaoProjeto-NC-Index-20170502-145545] ON [dbo].[Propostas]
(
	[situacaoprojetoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO




----------------------------------------------------------------------------------------------------------------------
--  AtividadeProjetoes
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[AtividadeProjetoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [datetime] NOT NULL,
	[projetoid] [int] NOT NULL,
	[atividadeid] [int] NOT NULL,
	[he] [int] NULL,
	[situacaoid] [int] NOT NULL
)

GO

ALTER TABLE [dbo].[AtividadeProjetoes] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[AtividadeProjetoes] ADD  DEFAULT ((0)) FOR [he]
GO

ALTER TABLE [dbo].[AtividadeProjetoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtividadeProjetoes.Propostas_propostaid] FOREIGN KEY([projetoid])
REFERENCES [dbo].[Propostas] ([propostaid])
GO

ALTER TABLE [dbo].[AtividadeProjetoes] CHECK CONSTRAINT [FK_dbo.AtividadeProjetoes.Propostas_propostaid]
GO

ALTER TABLE [dbo].[AtividadeProjetoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtividadeProjetoes.SituacaoProjetos_id] FOREIGN KEY([situacaoid])
REFERENCES [dbo].[SituacaoProjetos] ([situacaoid])
GO

ALTER TABLE [dbo].[AtividadeProjetoes] CHECK CONSTRAINT [FK_dbo.AtividadeProjetoes.SituacaoProjetos_id]
GO

ALTER TABLE [dbo].[AtividadeProjetoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AtividadeProjetoes_dbo.Atividades_id] FOREIGN KEY([atividadeid])
REFERENCES [dbo].[Atividades] ([id])
GO

ALTER TABLE [dbo].[AtividadeProjetoes] CHECK CONSTRAINT [FK_dbo.AtividadeProjetoes_dbo.Atividades_id]
GO

----------------------------------------------------------------------------------------------------------------------
-- AtividadesLancamentos
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[AtividadesLancamentos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [datetime] NOT NULL,
	[referencia] [datetime] NOT NULL,
	[projetoid] [int] NOT NULL,
	[clienteid] [int] NOT NULL,
	[atividadeid] [int] NOT NULL,
	[userid] [nvarchar](128) NOT NULL,
	[horas] [int] NOT NULL
)

GO

ALTER TABLE [dbo].[AtividadesLancamentos] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

----------------------------------------------------------------------------------------------------------------------
-- Produtoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Produtoes](
	[produtoid] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](150) NOT NULL,
	[valor] [decimal](12, 2) NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [Pk_Produtos] ON [dbo].[Produtoes]
(
	[produtoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
--  Clientes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Clientes](
	[clienteid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[nome] [varchar](100) NULL,
	[cnpj] [nvarchar](max) NULL,
	[ie] [nvarchar](max) NULL,
	[endereco] [nvarchar](max) NULL,
	[complemento] [nvarchar](max) NULL,
	[bairro] [nvarchar](max) NULL,
	[cep] [nvarchar](max) NULL,
	[fone] [char](20) NULL,
	[email] [varchar](200) NULL,
	[site] [varchar](100) NULL,
	[tipo] [char](1) NULL,
	[f0800] [varchar](15) NULL,
	[cidade] [varchar](100) NULL,
	[estado] [char](2) NULL,
 CONSTRAINT [PK_dbo.Clientes] PRIMARY KEY CLUSTERED 
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [dtcad]
GO


CREATE NONCLUSTERED INDEX [ClientesNome] ON [dbo].[Clientes]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

----------------------------------------------------------------------------------------------------------------------
-- Contatoes
----------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Contatoes](
	[contatoid] [int] IDENTITY(1,1) NOT NULL,
	[clienteid] [int] NOT NULL,
	[nome] [varchar](100) NOT NULL,
	[fone] [char](20) NULL,
	[celular] [char](20) NULL,
	[email] [varchar](200) NULL,
	[sexo] [char](1) NOT NULL
)

GO

ALTER TABLE [dbo].[Contatoes]  WITH CHECK ADD  CONSTRAINT [FK_Contatos_Clientes_ID] FOREIGN KEY([clienteid])
REFERENCES [dbo].[Clientes] ([clienteid])
GO

ALTER TABLE [dbo].[Contatoes] CHECK CONSTRAINT [FK_Contatos_Clientes_ID]
GO
CREATE CLUSTERED INDEX [ContatoId] ON [dbo].[Contatoes]
(
	[contatoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [ContatoClienteID] ON [dbo].[Contatoes]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO



----------------------------------------------------------------------------------------------------------------------
-- Contratoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Contratoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[clienteid] [int] NOT NULL,
	[descricao] [varchar](200) NULL,
	[observacao] [varchar](200) NULL,
	[tipocontrato] [int] NOT NULL,
	[indice] [int] NOT NULL,
	[situacaoid] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Contratoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Contratoes] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[Contratoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id] FOREIGN KEY([clienteid])
REFERENCES [dbo].[Clientes] ([clienteid])
GO

ALTER TABLE [dbo].[Contratoes] CHECK CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id]
GO

ALTER TABLE [dbo].[Contratoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contratoes_Indices] FOREIGN KEY([indice])
REFERENCES [dbo].[Indices] ([id])
GO

ALTER TABLE [dbo].[Contratoes] CHECK CONSTRAINT [FK_dbo.Contratoes_Indices]
GO


----------------------------------------------------------------------------------------------------------------------
-- Categorias
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Categorias](
	[categoriaid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NULL,
	[tipo] [char](1) NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [Pk_Categorias] ON [dbo].[Categorias]
(
	[categoriaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO



----------------------------------------------------------------------------------------------------------------------
-- ContaPrs
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[ContaPrs](
	[contaprid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[categoriaid] [int] NULL,
	[npar] [int] NOT NULL,
	[cliforid] [int] NOT NULL,
	[propostaid] [int] NULL,
	[valor] [decimal](12, 2) NOT NULL,
	[vencimento] [date] NOT NULL,
	[tipo] [char](1) NOT NULL,
	[dtpagto] [date] NULL,
	[situacao] [char](1) NOT NULL,
	[recorrente] [bit] NULL,
	[descricao] [varchar](200) NULL,
	[noordem] [varchar](5) NULL,
	[contagrupo] [int] NULL,
	[archive] [bit] NULL,
	[contratoid] [int] NULL
)

GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ((1)) FOR [npar]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ((1)) FOR [propostaid]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ('R') FOR [tipo]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ('A') FOR [situacao]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ((0)) FOR [recorrente]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ('01/01') FOR [noordem]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ((0)) FOR [archive]
GO

ALTER TABLE [dbo].[ContaPrs] ADD  DEFAULT ((0)) FOR [contratoid]
GO

ALTER TABLE [dbo].[ContaPrs]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Contaprs] FOREIGN KEY([categoriaid])
REFERENCES [dbo].[Categorias] ([categoriaid])
GO

ALTER TABLE [dbo].[ContaPrs] CHECK CONSTRAINT [FK_Categorias_Contaprs]
GO

ALTER TABLE [dbo].[ContaPrs]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_UserID] FOREIGN KEY([cliforid])
REFERENCES [dbo].[Clientes] ([clienteid])
GO

ALTER TABLE [dbo].[ContaPrs] CHECK CONSTRAINT [FK_Clientes_UserID]

CREATE CLUSTERED INDEX [ContaPrsId] ON [dbo].[ContaPrs]
(
	[contaprid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [ContasCliForId] ON [dbo].[ContaPrs]
(
	[cliforid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[ContaPrs]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_UserID] FOREIGN KEY([cliforid])
REFERENCES [dbo].[Clientes] ([clienteid])
GO

ALTER TABLE [dbo].[ContaPrs] CHECK CONSTRAINT [FK_Clientes_UserID]
GO


----------------------------------------------------------------------------------------------------------------------
-- UsuarioProjetoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[UsuarioProjetoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](128) NOT NULL,
	[projetoid] [int] NOT NULL
)

GO

ALTER TABLE [dbo].[UsuarioProjetoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsuarioProjetoes.Propostas_propostaid] FOREIGN KEY([projetoid])
REFERENCES [dbo].[Propostas] ([propostaid])
GO

ALTER TABLE [dbo].[UsuarioProjetoes] CHECK CONSTRAINT [FK_dbo.UsuarioProjetoes.Propostas_propostaid]
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170406-135422] ON [dbo].[UsuarioProjetoes]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170406-135449] ON [dbo].[UsuarioProjetoes]
(
	[userid] ASC,
	[projetoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- NFs
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[NFs](
	[nfid] [int] IDENTITY(1,1) NOT NULL,
	[no] [char](5) NULL,
	[cliforid] [int] NOT NULL,
	[dtcad] [date] NOT NULL,
	[vencimento] [date] NULL,
	[natureza] [int] NULL,
	[impressoes] [int] NULL,
	[situacao] [char](1) NULL,
	[contaprid] [int] NULL,
	[dtemissao] [date] NULL
)

GO

ALTER TABLE [dbo].[NFs] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[NFs] ADD  DEFAULT ((0)) FOR [natureza]
GO

ALTER TABLE [dbo].[NFs] ADD  DEFAULT ((0)) FOR [impressoes]
GO

ALTER TABLE [dbo].[NFs] ADD  DEFAULT ('A') FOR [situacao]
GO

ALTER TABLE [dbo].[NFs] ADD  DEFAULT (getdate()) FOR [dtemissao]
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170105-095738] ON [dbo].[NFs]
(
	[nfid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


----------------------------------------------------------------------------------------------------------------------
-- NFDetails
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[NFDetails](
	[nfdetailid] [int] IDENTITY(1,1) NOT NULL,
	[nfid] [int] NOT NULL,
	[produtoid] [int] NULL,
	[descricao] [varchar](100) NULL,
	[quantidade] [int] NOT NULL,
	[unitario] [decimal](12, 2) NULL
)

GO

ALTER TABLE [dbo].[NFDetails] ADD  DEFAULT ((0)) FOR [quantidade]
GO

ALTER TABLE [dbo].[NFDetails] ADD  DEFAULT ((0)) FOR [unitario]
GO

ALTER TABLE [dbo].[NFDetails]  WITH CHECK ADD  CONSTRAINT [FK_NfDetailslNfs] FOREIGN KEY([nfid])
REFERENCES [dbo].[NFs] ([nfid])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[NFDetails] CHECK CONSTRAINT [FK_NfDetailslNfs]
GO
CREATE CLUSTERED INDEX [Pk_NfDetails] ON [dbo].[NFDetails]
(
	[nfid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [Pk_NfDetailsNonClustered] ON [dbo].[NFDetails]
(
	[nfdetailid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO



----------------------------------------------------------------------------------------------------------------------
-- [UsuarioProjetoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[UsuarioProjetoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](128) NOT NULL,
	[projetoid] [int] NOT NULL
)

GO

ALTER TABLE [dbo].[UsuarioProjetoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsuarioProjetoes.Propostas_propostaid] FOREIGN KEY([projetoid])
REFERENCES [dbo].[Propostas] ([propostaid])
GO

ALTER TABLE [dbo].[UsuarioProjetoes] CHECK CONSTRAINT [FK_dbo.UsuarioProjetoes.Propostas_propostaid]
GO
----------------------------------------------------------------------------------------------------------------------
-- Reembolsoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Reembolsoes](
	[reembolsoid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[tipoid] [int] NULL,
	[userid] [nvarchar](128) NOT NULL,
	[valor] [decimal](12, 2) NOT NULL,
	[vencimento] [date] NOT NULL,
	[dtpagto] [date] NULL,
	[situacao] [char](1) NOT NULL,
	[descricao] [varchar](200) NULL
)

GO

ALTER TABLE [dbo].[Reembolsoes] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[Reembolsoes] ADD  DEFAULT ('A') FOR [situacao]
GO

ALTER TABLE [dbo].[Reembolsoes]  WITH CHECK ADD  CONSTRAINT [fk_Reembolsoes_TipoReembolso] FOREIGN KEY([tipoid])
REFERENCES [dbo].[TiposReembolsoes] ([tiporeembolsoid])
GO

ALTER TABLE [dbo].[Reembolsoes] CHECK CONSTRAINT [fk_Reembolsoes_TipoReembolso]
GO

----------------------------------------------------------------------------------------------------------------------
-- TiposReembolsoes
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[TiposReembolsoes](
	[tiporeembolsoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL
)
GO
CREATE UNIQUE CLUSTERED INDEX [Pk_Reembolsoes] ON [dbo].[Reembolsoes]
(
	[reembolsoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [ReembolsoesUserId_NonClustered] ON [dbo].[Reembolsoes]
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[Reembolsoes]  WITH CHECK ADD  CONSTRAINT [fk_Reembolsoes_TipoReembolso] FOREIGN KEY([tipoid])
REFERENCES [dbo].[TiposReembolsoes] ([tiporeembolsoid])
GO

ALTER TABLE [dbo].[Reembolsoes] CHECK CONSTRAINT [fk_Reembolsoes_TipoReembolso]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_TipoReembolsoId] ON [dbo].[TiposReembolsoes]
(
	[tiporeembolsoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
----------------------------------------------------------------------------------------------------------------------
-- Remuneracaos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Remuneracaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[dtlan] [date] NOT NULL,
	[propostaid] [int] NOT NULL,
	[comercialid] [int] NOT NULL,
	[insumos] [decimal](12, 2) NULL,
	[percentual] [decimal](5, 2) NULL,
	[faturada] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Remuneracaos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Remuneracaos] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[Remuneracaos] ADD  DEFAULT ((0)) FOR [insumos]
GO

ALTER TABLE [dbo].[Remuneracaos] ADD  DEFAULT ((0)) FOR [percentual]
GO

ALTER TABLE [dbo].[Remuneracaos] ADD  DEFAULT ((0)) FOR [faturada]
GO

ALTER TABLE [dbo].[Remuneracaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id] FOREIGN KEY([comercialid])
REFERENCES [dbo].[Comercials] ([id])
GO

ALTER TABLE [dbo].[Remuneracaos] CHECK CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id]
GO

ALTER TABLE [dbo].[Remuneracaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Propostas_propostaid] FOREIGN KEY([propostaid])
REFERENCES [dbo].[Propostas] ([propostaid])
GO

ALTER TABLE [dbo].[Remuneracaos] CHECK CONSTRAINT [FK_dbo.Remuneracaos_dbo.Propostas_propostaid]
GO

ALTER TABLE [dbo].[Remuneracaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id] FOREIGN KEY([comercialid])
REFERENCES [dbo].[Comercials] ([id])
GO

ALTER TABLE [dbo].[Remuneracaos] CHECK CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id]
GO
ALTER TABLE [dbo].[Remuneracaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Propostas_propostaid] FOREIGN KEY([propostaid])
REFERENCES [dbo].[Propostas] ([propostaid])
GO

ALTER TABLE [dbo].[Remuneracaos] CHECK CONSTRAINT [FK_dbo.Remuneracaos_dbo.Propostas_propostaid]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-DtCad] ON [dbo].[Remuneracaos]
(
	[dtcad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

----------------------------------------------------------------------------------------------------------------------
-- SituacaoRenovacaos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[SituacaoRenovacaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[cor] [varchar](20) NULL
)

GO
ALTER TABLE [dbo].[SituacaoRenovacaos] ADD  DEFAULT ('white') FOR [cor]
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-095049] ON [dbo].[SituacaoRenovacaos]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


----------------------------------------------------------------------------------------------------------------------
-- Renovacaos
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Renovacaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contratoid] [int] NOT NULL,
	[dtcad] [date] NOT NULL,
	[dtinicio] [date] NOT NULL,
	[dttermino] [date] NOT NULL,
	[valor] [decimal](15, 2) NOT NULL,
	[proxreajuste] [date] NULL,
	[prazo] [int] NOT NULL,
	[observacao] [varchar](200) NULL,
	[situacaoid] [int] NOT NULL,
	[iniciosv] [date] NULL,
 CONSTRAINT [PK_dbo.Renovacaos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Renovacaos] ADD  DEFAULT (getdate()) FOR [dtcad]
GO

ALTER TABLE [dbo].[Renovacaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Renovacaos_dbo.Contrato_id] FOREIGN KEY([contratoid])
REFERENCES [dbo].[Contratoes] ([id])
GO

ALTER TABLE [dbo].[Renovacaos] CHECK CONSTRAINT [FK_dbo.Renovacaos_dbo.Contrato_id]
GO

ALTER TABLE [dbo].[Renovacaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Renovacaos_SituacaoRenovacaos] FOREIGN KEY([situacaoid])
REFERENCES [dbo].[SituacaoRenovacaos] ([id])
GO

ALTER TABLE [dbo].[Renovacaos] CHECK CONSTRAINT [FK_dbo.Renovacaos_SituacaoRenovacaos]
GO


----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------
-- 
----------------------------------------------------------------------------------------------------------------------


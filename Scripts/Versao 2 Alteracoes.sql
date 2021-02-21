Select * from situacaos
--alter table situacaos add cor varchar(20) Default (('white'))
--update situacaos set cor = 'white' where situacaoid = 1
--update situacaos set cor = 'yellow' where situacaoid = 2
--update situacaos set cor = 'red' where situacaoid = 3
--update situacaos set cor = 'blue' where situacaoid = 4
--update situacaos set cor = 'green' where situacaoid = 5
--update situacaos set cor = 'white' where situacaoid =6
--update situacaos set cor = 'orange' where situacaoid =7
CREATE TABLE [dbo].[Comercials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[email] [varchar] (100) null,
) ON [PRIMARY]


ALTER TABLE [dbo].[Comercials] ADD  CONSTRAINT [PK_dbo.Comercials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170504-ComercialNome] ON [dbo].[Comercials]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170504-ComercialEmail] ON [dbo].[Comercials]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


-- DROP TABLE [dbo].[Remuneracaos]
CREATE TABLE [dbo].[Remuneracaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL DEFAULT getdate(),
	[dtlan] [date] NOT NULL ,
	[propostaid] int NOT NULL,
	[comercialid] int NOT NULL,
	[insumos] decimal (12,2) null default ((0)),
	[percentual] decimal (5,2) null default ((0)),
	[faturada] int not null default (0)

) ON [PRIMARY]

ALTER TABLE [dbo].[Remuneracaos] ADD  CONSTRAINT [PK_dbo.Remuneracaos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [NonClusteredIndex-DtCad] ON [dbo].[Remuneracaos]
(
	[dtcad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE [dbo].[Remuneracaos]  
WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Propostas_propostaid] 
FOREIGN KEY([propostaid])
REFERENCES [dbo].[Propostas] ([propostaid])

ALTER TABLE [dbo].[Remuneracaos]  
WITH CHECK ADD  CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id] 
FOREIGN KEY([comercialid])
REFERENCES [dbo].[Comercials] ([id])
ALTER TABLE [dbo].[Remuneracaos] CHECK CONSTRAINT [FK_dbo.Remuneracaos_dbo.Comercials_id]


--Reembolsoes- Aspnetusers
ALTER TABLE [dbo].[Reembolsoes]  
WITH CHECK ADD  CONSTRAINT [FK_dbo.Reembolsoes_dbo.AspNetUsers_id] 
FOREIGN KEY([userid])
REFERENCES [dbo].[AspNetUsers] ([id])
ALTER TABLE [dbo].[Reembolsoes] CHECK CONSTRAINT [FK_dbo.Reembolsoes_dbo.AspNetUsers_id]

CREATE TABLE [dbo].[Contratoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL DEFAULT (getdate()),
	[clienteid] [int] NOT NULL,
	[dtinicio] [date] NULL,
	[dtfim] [date] NULL,
	[prazo] int null,
	[servicos] [varchar](200) NULL,
	[valor] [decimal](12, 2) NOT NULL DEFAULT ((0)),
	[dtreajuste] [date] NULL,
	[observacao] [varchar](200) NULL,
	[situacaoid] [int] NOT NULL DEFAULT ((1))
) ON [PRIMARY] 
/* PRIMARY KEY */
ALTER TABLE [dbo].[Contratoes] ADD  CONSTRAINT [PK_dbo.Contratoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/* FOREIGN KEY */
ALTER TABLE [dbo].[Contratoes]  
WITH CHECK ADD  CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id] 
FOREIGN KEY([clienteid])
REFERENCES [dbo].[Clientes] ([clienteid])
ALTER TABLE [dbo].[Contratoes] CHECK CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id]


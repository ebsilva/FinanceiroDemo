--DROP TABLE CONTRATOES
CREATE TABLE [dbo].[Contratoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL default (( getdate())),
	[clienteid] [int] NOT NULL,
	[descricao] varchar(200) null,
	[observacao] varchar(200) null,
	[tipocontrato] [int] not null,
	[indice] [int] not null,
	[situacaoid] [int] NOT NULL,
	
 CONSTRAINT [PK_dbo.Contratoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[Contratoes] ADD  DEFAULT (getdate()) FOR [dtcad]
ALTER TABLE [dbo].[Contratoes] ADD  DEFAULT ((1)) FOR [situacaoid]
ALTER TABLE [dbo].[Contratoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id] FOREIGN KEY([clienteid])
REFERENCES [dbo].[Clientes] ([clienteid])
ALTER TABLE [dbo].[Contratoes] CHECK CONSTRAINT [FK_dbo.Contratoes_dbo.Cliente_id]
ALTER TABLE [dbo].[Contratoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contratoes_Indices] FOREIGN KEY([indice])
REFERENCES [dbo].[Indices] ([id])
ALTER TABLE [dbo].[Contratoes] CHECK CONSTRAINT [FK_dbo.Contratoes_Indices]



--DROP TABLE RENOVACAOS
CREATE TABLE [dbo].[Renovacaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contratoid] [int] NOT NULL,
	[dtcad] [date] NOT NULL default (( getdate())),
	[dtinicio] [date] NOT NULL,
	[dttermino] [date] NOT NULL,
	[valor] decimal(15,2) not null,
	[proxreajuste] [date] NOT NULL,
	[prazo] int not null,
	[observacao] varchar(200) null,
	[situacaoid] [int] NOT NULL,
	
 CONSTRAINT [PK_dbo.Renovacaos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Renovacaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Renovacaos_dbo.Contrato_id] FOREIGN KEY([contratoid])
REFERENCES [dbo].[Contratoes] ([id])
ALTER TABLE [dbo].[Renovacaos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Renovacaos_SituacaoRenovacaos] FOREIGN KEY([situacaoid])
REFERENCES [dbo].[SituacaoRenovacaos] ([id])


--Drop table [dbo].[Indices]
CREATE TABLE [dbo].[Indices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL
) ON [PRIMARY]
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-112638] ON [dbo].[Indices]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


CREATE TABLE [dbo].[SituacaoRenovacaos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[cor] [varchar](20) NULL DEFAULT ('white')
) ON [PRIMARY]
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-095049] ON [dbo].[SituacaoRenovacaos]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TiposContratoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
) ON [PRIMARY]
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170522-095217] ON [dbo].[TiposContratoes]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



-- select * from clientes
-- select * from contratoes
-- select * from situacaocontratoes
-- select * from tiposreajuste
insert SituacaoRenovacaos (nome,cor) values ('Ativo','Green')
insert SituacaoRenovacaos (nome,cor) values ('Concluido','Blue')

select co.id,c.clienteid,c.nome clientenome,tr.id tiporeajusteid,tr.nome tiporeajuste,
       r.dtinicio,r.dttermino,r.valor,r.observacao,r.prazo,r.proxreajuste,
	   s.id situacaoid, s.nome situacao, s.cor 
from contratoes co
inner join Clientes c on c.clienteid = co.clienteid 
inner join TiposReajuste tr on tr.id = co.tiporeajuste
inner join Renovacaos r on r.contratoid = co.id and r.situacaoid = 1
inner join SituacaoRenovacaos s on s.id = r.situacaoid 


CREATE TABLE [dbo].[ContaPrs_2023](
	[contaprid] [int] NOT NULL,
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
	[contratoid] [int] NULL,
	[nf] [char](10) NULL
) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [ContaPrsId_2023] ON [dbo].[ContaPrs_2023]
(
	[contaprid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ContasCliForId_2023] ON [dbo].[ContaPrs_2023]
(
	[cliforid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ContasCategoria_2023] ON [dbo].[ContaPrs_2023]
(
	[categoriaid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ContasSituacao_2023] ON [dbo].[ContaPrs_2023]
(
	[situacao] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO



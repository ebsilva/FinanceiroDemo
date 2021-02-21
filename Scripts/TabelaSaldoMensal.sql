USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[SaldoMensals]    Script Date: 04/01/2021 20:37:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SaldoMensals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cat] [int] NULL,
	[Ano] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[Tipo] char(1),
	[Saldo] [decimal](15, 2) NOT NULL Default 0
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SaldoMensals] ADD  DEFAULT (datepart(year,getdate())) FOR [Ano]
GO

ALTER TABLE [dbo].[SaldoMensals] ADD  DEFAULT (datepart(month,getdate())) FOR [Mes]
GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20210104-195950] ON [dbo].[SaldoMensals]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



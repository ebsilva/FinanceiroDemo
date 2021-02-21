USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[SaldoMensals]    Script Date: 15/01/2021 00:02:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop table [dbo].[SaldoAnuals]
CREATE TABLE [dbo].[SaldoAnuals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ano] [int] NOT NULL,
	[Inicio][Date] NULL,
	[Saldo] [decimal](15, 2) Default 0 NOT NULL
) ON [PRIMARY]
GO

select * from SaldoAnuals




ALTER TABLE [dbo].[SaldoAnuals] ADD  DEFAULT ((0)) FOR [Saldo]
GO



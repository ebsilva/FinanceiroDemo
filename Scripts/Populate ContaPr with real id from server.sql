USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[ContaPrs]    Script Date: 21/06/2019 12:35:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContaPrs2](
	[contaprid] [int] identity(1,1),
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
SET IDENTITY_INSERT [dbo].[ContaPrs2] ON;
INSERT INTO [dbo].[ContaPrs2]
 (contaprid,dtcad,categoriaid,npar,cliforid,propostaid,valor,vencimento,tipo,dtpagto,situacao,recorrente,descricao,noordem,
  contagrupo,archive,contratoid,nf)

SELECT contaprid,dtcad,categoriaid,npar,cliforid,propostaid,valor,vencimento,tipo,dtpagto,situacao,recorrente,descricao,noordem,
       contagrupo,archive,contratoid,nf
  FROM [dbo].[ContaPrs];


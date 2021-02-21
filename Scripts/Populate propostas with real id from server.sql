USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[Propostas]    Script Date: 16/06/2019 17:59:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop table [dbo].[Propostas2];
CREATE TABLE [dbo].[Propostas2](
	[propostaid] [int] identity(1,1),
	[dtcad] [date] NOT NULL,
	[clienteid] [int] NOT NULL,
	[envio] [date] NULL,
	[numpo] [nvarchar](max) NULL,
	[numnf] [nvarchar](max) NULL,
	[fp] [int] default 0 NULL,
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
	[situacaoprojetoid] [int] default 0,
	[observacao] [varchar](200) NULL,
	[isproject] [int] NULL,
	[motivo] [varchar](200) NULL,
	[Responsavel] [nvarchar](128) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT dbo.Propostas2 ON;  

INSERT INTO dbo.Propostas2
 (propostaid,dtcad,clienteid,envio,numpo,numnf,fp,recebido ,he ,hu, 
	   previsao,situacaoid,descricao,dtaceite,dtfinalizacao,valor,categoriaid,
	   dtiniprj,dtfimprj,dtfimprvprj,situacaoprojetoid,observacao,isproject, 
	   motivo,Responsavel)

SELECT propostaid,dtcad,clienteid,envio,numpo,numnf,fp,recebido ,he ,hu, 
	   previsao,situacaoid,descricao,dtaceite,dtfinalizacao,valor,categoriaid,
	   dtiniprj,dtfimprj,dtfimprvprj,situacaoprojetoid,observacao,isproject, 
	   motivo,Responsavel
  FROM dbo.Propostas;

  Select *
  from propostas2 p2
  inner join propostas p on p2.propostaid = p.propostaid
  where p2.dtcad <> p.dtcad and p2.clienteid <> p.clienteid


SET IDENTITY_INSERT [dbo].[Propostas2] OFF;  
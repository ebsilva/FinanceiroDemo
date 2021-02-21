USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[Propostas]    Script Date: 16/06/2019 17:59:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop table [dbo].[Propostas2];
CREATE TABLE [dbo].[Clientes2](
	[clienteid] [int] identity(1,1),
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
	[nomefantasia] [varchar](100) NULL,
 CONSTRAINT [PK_dbo.Clientes2] PRIMARY KEY CLUSTERED 
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Clientes2] ON;  

INSERT INTO [dbo].[Clientes2]
 (clienteid,dtcad,nome,cnpj,ie,endereco, complemento, bairro,cep,fone, email, site, tipo,f0800,cidade, estado,nomefantasia)
SELECT clienteid,dtcad,nome,cnpj,ie,endereco, complemento, bairro,cep,fone, email, site, tipo,f0800,cidade, estado,nomefantasia
  FROM dbo.Clientes;



SET IDENTITY_INSERT [dbo].[Propostas2] OFF;  
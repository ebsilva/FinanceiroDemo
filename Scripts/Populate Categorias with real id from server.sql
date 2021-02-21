USE [DnkLocalDb]
GO

/****** Object:  Table [dbo].[Propostas]    Script Date: 16/06/2019 17:59:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop table [dbo].[Categorias2];
CREATE TABLE [dbo].[Categorias2](
	[categoriaid] [int] identity(1,1),
	[nome] [varchar](100) NULL,
	[tipo] [char](1) NULL
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Categorias2] ON;  

INSERT INTO [dbo].[Categorias2] (categoriaid,nome, tipo) SELECT categoriaid,nome, tipo  FROM [dbo].[Categorias]



SET IDENTITY_INSERT [dbo].[Categorias2] OFF;  

CREATE UNIQUE CLUSTERED INDEX [Pk_Categorias] ON [dbo].[Categorias]
(
	[categoriaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

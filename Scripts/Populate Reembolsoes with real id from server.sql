USE [DnkLocalDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--drop table [dbo].[Reembolsoes2];
CREATE TABLE [dbo].[Reembolsoes2](
	[reembolsoid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL,
	[tipoid] [int] NULL,
	[userid] [nvarchar](128) NOT NULL,
	[valor] [decimal](12, 2) NOT NULL,
	[vencimento] [date] NOT NULL,
	[dtpagto] [date] NULL,
	[situacao] [char](1) NOT NULL,
	[descricao] [varchar](200) NULL
) ON [PRIMARY]
GO


SET IDENTITY_INSERT [dbo].[Reembolsoes2] ON;  

INSERT INTO [dbo].[Reembolsoes2] (reembolsoid,dtcad, tipoid,userid,valor,vencimento,dtpagto,situacao,descricao)
SELECT reembolsoid,dtcad, tipoid,userid,valor,vencimento,dtpagto,situacao,descricao  FROM [dbo].[Reembolsoes]


SET IDENTITY_INSERT [dbo].[Reembolsoes2] OFF;  

CREATE UNIQUE CLUSTERED INDEX [Pk_Reembolsoes] ON [dbo].[Reembolsoes2]
(
	[reembolsoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

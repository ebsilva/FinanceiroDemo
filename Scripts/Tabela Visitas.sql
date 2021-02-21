--drop table [dbo].[Visitas]
CREATE TABLE [dbo].[Visitas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data] Datetime null,
	[descricao] [nvarchar](max) NULL,
	[contato] varchar(100) null,
	[fone] varchar(15) null,
	[propostaid] int null,
	[local]  [nvarchar](max) NULL

 CONSTRAINT [PK_dbo.Visitas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



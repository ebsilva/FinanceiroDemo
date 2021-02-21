--DBCC CHECKIDENT ('NfNumeracaos', RESEED, 2000)
-- drop table NfNumeracaos

alter table NfRecebers alter column NumeroNf int 

CREATE TABLE [dbo].[NfNumeracaos](
	[NfIdGerado] [int] IDENTITY(1,1) NOT NULL,
	[NfIdReceber] [int] Default 0 ,
	[NfImpressoes] [int] Default 0 ,
	[Criacao] [DateTime] default getdate(),
	[Status] [int] default 0,
) ON [PRIMARY]
GO
insert NfNumeracaos ( criacao)   values (getdate())
insert NfNumeracaos ( NfIdReceber)   values ( 7)
select * from  NfNumeracaos



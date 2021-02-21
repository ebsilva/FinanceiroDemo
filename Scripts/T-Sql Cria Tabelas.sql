-- update permissaos set pro_view = 1, pro_edit = 1 where roleid = '68bb3520-cd3e-4a7f-a891-da3a376b8716'
-- update permissoes set pag_view = 1, pag_edit = 1
-- update permissoes set rec_view = 1, rec_edit = 1
-- update permissoes set rep_view = 1, rep_edit = 1
-- update permissoes set ree_view = 1, ree_edit = 1
-- PERMISSOES PAR ADMINISTRADOR
--insert permissaos (roleid,pro_edit,pro_view,rec_edit,rec_view,pag_edit,pag_view,cad_edit,cad_view,rep_edit,rep_view,ree_edit,ree_view)
--values ('2c10023c-568c-48a3-b563-0c2cf69778ac',1,1,1,1,1,1,1,1,1,1,1,1)
select * from ASPNETUSERS u
inner join aspnetuserroles ur on ur.userid = u.id
inner join aspnetroles r on r.id = ur.roleid


alter table propostas add valor Decimal(12,2) not null default 0 
alter table propostas add categoriaid int null
CREATE TABLE [dbo].[Permissoes](
	[RoleId] [nvarchar](128) NOT NULL,
	[Pro_edit] [int] NULL,
	[Pro_view] [int] NULL,
	[Rec_edit] [int] NULL,
	[Rec_view] [int] NULL,
	[Pag_edit] [int] NULL,
	[Pag_view] [int] NULL,
	[Cad_edit] [int] NULL,
	[Cad_view] [int] NULL,
	[Rep_edit] [int] NULL,
	[Rep_view] [int] NULL
 CONSTRAINT [PK_dbo.Permissoes] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Permissoes]  WITH CHECK ADD  CONSTRAINT [FK_1_Permissoes] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Permissoes] CHECK CONSTRAINT [FK_1_Permissoes]
GO


CREATE UNIQUE CLUSTERED INDEX [Pk_Situacoes] ON [dbo].[Situacaos]
(
	[situacaoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


CREATE UNIQUE CLUSTERED INDEX [Pk_Reembolsoes] ON [dbo].[Reembolsoes]
(
	[reembolsoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ReembolsoesUserId_NonClustered] ON [dbo].[Reembolsoes]
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


CREATE UNIQUE CLUSTERED INDEX [Pk_Produtos] ON [dbo].[Produtoes]
(
	[produtoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [Pk_NfDetails] ON [dbo].[NFDetails]
(
	[nfid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
CREATE NONCLUSTERED INDEX [Pk_NfDetailsNonClustered] ON [dbo].[NFDetails]
(
	[nfdetailid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


CREATE UNIQUE CLUSTERED INDEX [Pk_Natureza] ON [dbo].[Natureza]
(
	[naturezaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [ContatoId] ON [dbo].[Contatoes]
(
	[contatoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ContatoClienteID] ON [dbo].[Contatoes]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


CREATE UNIQUE CLUSTERED INDEX [Pk_Categorias] ON [dbo].[Categorias]
(
	[categoriaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NfDetails]  WITH CHECK 
ADD CONSTRAINT [FK_NfDetailslNfs] FOREIGN KEY([nfid])
REFERENCES [dbo].[Nfs] ([nfid])
ON DELETE CASCADE

CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170105-095738] ON [dbo].[NFs]
(
	[nfid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


alter table nf add dtemissao Date null default getdate()
CREATE TABLE [dbo].[Natureza](
	[naturezaoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NOT NULL
) ON [PRIMARY]

alter table nf add contaprid int null

--drop table [dbo].[NF]
alter table categorias alter column nome varchar(100)
Select * from categorias


CREATE TABLE [dbo].[NF](
	[nfid] [int] IDENTITY(1,1) NOT NULL,
	[no] char(5) NULL,
	[cliforid] [int] NOT NULL,
	[dtcad] Date NOT NULL DEFAULT getdate(),
	[vencimento] Date NULL,
	[natureza] int null default 0,
	[impressoes] int null default 0,
	[situacao] char(1) default 'A' 
) ON [PRIMARY]

drop table [dbo].[NFDetails]
CREATE TABLE [dbo].[NFDetails](
	[nfdetailid] [int] IDENTITY(1,1) NOT NULL,
	[nfid] [int] NOT NULL,
	[produtoid] [int] NULL,
	[descricao] [varchar] (100) NULL,
	[quantidade] [int] NOT NULL DEFAULT ((0)),
	[unitario] [decimal](12, 2) NULL DEFAULT ((0))
) 


CREATE TABLE [dbo].[Produtoes](
	[produtoid] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](150) NOT NULL,
	[valor] [Decimal](12,2) NULL
) ON [PRIMARY]

alter table categorias add tipo char(1)
alter table clientes add cidade varchar(100)
alter table clientes add estado char(2)

CREATE TABLE [dbo].[TiposReembolso](
	[tiporeembolsoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL
) ON [PRIMARY]

insert into tiposreembolso(nome) values('Kilometragem')
insert into tiposreembolso(nome) values('Taxi')
insert into tiposreembolso(nome) values('Refeição')
insert into tiposreembolso(nome) values('Hotel')
insert into tiposreembolso(nome) values('Passagem')
insert into tiposreembolso(nome) values('Outros')

--DROP TABLE [dbo].[Reembolso]
CREATE TABLE [dbo].[Reembolsoes](
	[reembolsoid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL DEFAULT (getdate()),
	[tipoid] [int] NULL,
	[userid] [nvarchar](128) NOT NULL,
	[valor] [decimal](12, 2) NOT NULL,
	[vencimento] [date] NOT NULL,
	[dtpagto] [date] NULL,
	[situacao] [char](1) NOT NULL DEFAULT ('A'),
) ON [PRIMARY]

alter table contaprs add descricao varchar(200)
alter table contaprs add noordem varchar(5) default '01/01'
alter table contaprs drop column noordem
alter table Aspnetusers add	[nome] [varchar](60) NULL
alter table Aspnetusers add	[active] [smallint] NOT NULL DEFAULT ((1))
alter table Aspnetusers add	[lastpwdupdate] [datetime] NULL


CREATE TABLE [dbo].[Historicoes](
	[historicoid] [int] IDENTITY(1,1) NOT NULL,
	[fid] [int] NOT NULL,
	[tipo] [char](1) NOT NULL,
	[texto] [varchar](2000) 
) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [Historico_fid] ON [dbo].[Historicoes]
(
	[fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

CREATE TABLE [dbo].[Meses](
	[mes] [int]  NOT NULL,
	[nome] [varchar](20) NOT NULL
) 

GO

Select * from meses
CREATE TABLE [dbo].[dias](
	[dia] [int]  NOT NULL,
) 

GO
delete from dias
insert dias (dia) values(1)
insert dias (dia) values(2)
insert dias (dia) values(3)
insert dias (dia) values(4)
insert dias (dia) values(5)
insert dias (dia) values(6)
insert dias (dia) values(7)
insert dias (dia) values(8)
insert dias (dia) values(9)
insert dias (dia) values(10)
insert dias (dia) values(11)
insert dias (dia) values(12)
insert dias (dia) values(13)
insert dias (dia) values(14)
insert dias (dia) values(15)
insert dias (dia) values(16)
insert dias (dia) values(17)
insert dias (dia) values(18)
insert dias (dia) values(19)
insert dias (dia) values(20)
insert dias (dia) values(21)
insert dias (dia) values(22)
insert dias (dia) values(23)
insert dias (dia) values(24)
insert dias (dia) values(25)
insert dias (dia) values(26)
insert dias (dia) values(27)
insert dias (dia) values(28)
insert dias (dia) values(29)
insert dias (dia) values(30)
insert dias (dia) values(31)


SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))

/*------------------------------------------------------------------------------------------------------

    Select * from Clientes
	SELECT * FROM CIDADES WHERE UF='SP' AND NOME = 'Santa Isabel'
  --drop table [dbo].[Clientes]
    insert clientes (nome,endereco,complemento,bairro,cep,cidadeid,fone,site,tipo)
	       values
		            ('CASAS BAHIA','RUA NEVADA KID,93','LOTE 4','ARALÚ','07500000',5135,'1146561808','www.metasoftsolutions.com.br','C')
------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Clientes](
	[clienteid] [int] IDENTITY(1,1) NOT NULL,
    [dtcad] [Date] default getdate() NOT NUll,
	[nome] [varchar](100) NULL,
	[cnpj] [nvarchar](max) NULL,
	[ie]  [nvarchar](max) NULL,
	[endereco] [nvarchar](max) NULL,
	[complemento] [nvarchar](max) NULL,
	[bairro] [nvarchar](max) NULL,
	[cep] [nvarchar](max) NULL,
	[cidadeid] [int] NOT NULL,
	[fone] [char] (20)  NULL,
	[email]  [varchar](200) NULL,
    [site] [varchar](100) NULL,
	[tipo] [char](1) NULL,

 CONSTRAINT [PK_dbo.Clientes] PRIMARY KEY CLUSTERED 
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

GO

CREATE NONCLUSTERED INDEX [ClietesNome] ON [dbo].[Clientes]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

/*------------------------------------------------------------------------------------------------------
   drop table [dbo].[Propostas]
   delete from propostas
    Select * from Propostas
    Select * from situacaos
	select * from clientes

	insert into Propostas(clienteid,valor,fp, recebido,he,hu)
    values (18,4000,1,1,250,0)

	Select 
	p.propostaid,p.clienteid, c.nome clientenome,
	convert(varchar(25), p.dtcad, 103) dtcad, 
	isnull(convert(varchar(25), p.envio, 103),'N/D') envio, 
	isnull(numpo,'N/D') numpo,isnull(numnf,'N/D') numnf, valor,
	fp.fpid,fp.nome fpnome,recebido,
	[p].[he],[p].[hu],  
	isnull(convert(varchar(25), p.previsao, 103),'') previsao,
	s.situacaoid, s.nome situacao
	from       Propostas p
	inner join Clientes c  on p.clienteid = c.clienteid
	inner join Situacaos s on s.situacaoid = p.situacao
	left  join Fps       fp on fp.fpid = p.fp
	left join (Select propostaid,count(*) np from contaprs where propostaid = 1 group by propostaid ) npt on npt.propostaid = p.propostaid 



------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Propostas](
	[propostaid] [int] IDENTITY(1,1) NOT NULL,
    [dtcad] [Date] default getdate() NOT NUll,
	[clienteid] [int] NOT NULL,
	[envio] [Date]   NUll,
	[numpo] [nvarchar](max) NULL,
	[numnf] [nvarchar](max) NULL,
	[valor]  decimal(12,3) not null ,
	[fp] [int] Default 0 NOT NULL,
	[recebido] [bit] NOT NULL,
	[he] [int] NOT NULL,
	[hu] [int] NULL,
	[previsao] [int]  NULL,
	[situacaoid] [int] DEFAULT 1 NOT NULL),
 CONSTRAINT [PK_dbo.Propostas] PRIMARY KEY CLUSTERED 
(
	[propostaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 

ALTER TABLE ContaPrs 
ADD CONSTRAINT FK_Propostas_CleintId FOREIGN KEY (cliforid) REFERENCES Clientes(clienteid);

GO


/*------------------------------------------------------------------------------------------------------
  --drop table [dbo].[Cidades]
    Select * from [dbo].[Cidades]
------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Cidades](
	[cidadeid] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](150) NOT NULL,
	[UF] [char](2) NOT NULL
)
CREATE UNIQUE CLUSTERED INDEX [CidadesId] ON [dbo].[Cidades]
(
	[cidadeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO
CREATE NONCLUSTERED INDEX [CidadesNome] ON [dbo].[Cidades]
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO


/*------------------------------------------------------------------------------------------------------
 --drop table [dbo].[Fps]
   Select * from [dbo].[Fps]

   insert Fps (nome) values('À vista')
   insert Fps (nome) values('Parcelado')

------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Fps](
	[fpid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL,
)
go

/*------------------------------------------------------------------------------------------------------
  --drop table [dbo].[Situacaos]
    Select * from [dbo].[Situacaos]

	insert Situacaos (nome) values('Em análise')
    insert Situacaos (nome) values('Aceita')
    insert Situacaos (nome) values('Negada')

------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Situacaos](
    [situacaoid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
)
/*------------------------------------------------------------------------------------------------------
   --drop table [dbo].[ContaPrs]
   --delete from [dbo].[ContaPrs]
   -select * from clientes
   --select * from contaprs

select isnull(
		sum(case when (tipo= 'P' and cast(vencimento as date) = CAST(getdate() AS DATE)) then 
		cast(valor as decimal(8,2))  else 0 end),0) as apagarhoje,
       isnull(
		sum(case when tipo= 'R' and cast(vencimento as date) = CAST(getdate() AS DATE) then cast(valor as decimal(8,2))  else 0 end),0) as areceberhoje,
	   isnull(
		sum(case when tipo= 'P' and cast(vencimento as date) < CAST(getdate() AS DATE) then cast(valor as decimal(8,2))  else 0 end),0) as apagaratrasado,
	   isnull(
	    sum(case when tipo= 'R' and cast(vencimento as date) < CAST(getdate() AS DATE) then cast(valor as decimal(8,2))  else 0 end),0) as areceberatrasado
   from contaprs

   delete from contaprs
   Select 
	cpr.contaprid,cpr.cliforid,cpr.propostaid,
	c.nome clifornome,
	isnull(convert(varchar(25), cpr.dtcad, 103),'N/D') dtcad,
	cpr.npar,cpr.valor,
	isnull(convert(varchar(25), cpr.vencimento, 103),'N/D') vencimento,
	cpr.tipo,isnull(convert(varchar(25), cpr.dtpagto, 103),'N/D') dtpagto,
	cpr.situacao
   from contaPRs cpr
   inner join Clientes c on c.clienteid = cpr.cliforid
   left join Propostas p on p.propostaid = cpr.propostaid 
------------------------------------------------------------------------------------------------------*/

CREATE TABLE [dbo].[ContaPrs](
    [contaprid] [int] IDENTITY(1,1) NOT NULL,
    [dtcad] [Date] default getdate() NOT NUll,
	[categoriaid]  int NUll,
	[npar] int not null default 1,
	[cliforid]  int NOT NUll,
	[propostaid]  int NUll default 1,
	[valor]  decimal(12,2) not null ,
	[vencimento] [Date] NOT NUll,
	[tipo] [char] (1) default 'R' NOT NUll,
	[dtpagto] [Date] NUll,
	[situacao] [char] (1) default 'A' NOT NUll,
)
CREATE CLUSTERED INDEX [ContaPrsId] ON [dbo].[ContaPrs]
(
	[contaprid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO
CREATE NONCLUSTERED INDEX [ContasCliForId] ON [dbo].[ContaPrs]
(
	[cliforid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE ContaPrs ADD CONSTRAINT FK_Clientes_UserID FOREIGN KEY (cliforid) REFERENCES Clientes(clienteid);

ALTER TABLE [dbo].[ContaPrs] ADD  recorrente bit default 0;
update [dbo].[ContaPrs] set recorrente = 0;

/*------------------------------------------------------------------------------------------------------*/
   --drop table [dbo].[Contatoes]

------------------------------------------------------------------------------------------------------*/

CREATE TABLE [dbo].[Contatoes](
	[contatoid] [int] IDENTITY(1,1) NOT NULL,
	[clienteid] [int]  NOT NULL,
	[nome] [varchar] (100) not null,
	[fone] [char] (20)  NULL,
	[celular] [char] (20)  NULL,
	[email]  [varchar](200) NULL,
	[sexo]  [char](1) NOT NULL
)

CREATE CLUSTERED INDEX [ContatoId] ON [dbo].[Contatoes]
(
	[contatoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO
CREATE NONCLUSTERED INDEX [ContatoClienteID] ON [dbo].[Contatoes]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

ALTER TABLE Contatoes
ADD CONSTRAINT FK_Contatoes_CleintId FOREIGN KEY (clienteid) REFERENCES Clientes(clienteid);


/*------------------------------------------------------------------------------------------------------*/
   --drop table [dbo].[Categorias] 

   insert categorias (nome) values ('Folha')
------------------------------------------------------------------------------------------------------*/
CREATE TABLE [dbo].[Categorias](
	[categoriaid] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
)
go

*------------------------------------------------------------------------------------------------------*/
   --drop table [dbo].[ContaPrs]

------------------------------------------------------------------------------------------------------*/

CREATE TABLE [dbo].[ContaPrs](
	[contaprid] [int] IDENTITY(1,1) NOT NULL,
	[dtcad] [date] NOT NULL DEFAULT (getdate()),
	[categoriaid] [int] NULL,
	[npar] [int] NOT NULL DEFAULT ((1)),
	[cliforid] [int] NOT NULL,
	[propostaid] [int]  NULL DEFAULT ((0)),
	[valor] [decimal](8, 3) NOT NULL,
	[vencimento] [date] NOT NULL,
	[tipo] [char](1) NOT NULL DEFAULT ('R'),
	[dtpagto] [date] NULL,
	[situacao] [char](1) NOT NULL DEFAULT ('A')
) ON [PRIMARY]

GO

GO





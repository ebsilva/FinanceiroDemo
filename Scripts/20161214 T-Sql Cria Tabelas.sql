--alter table clientes add f0800 varchar(15)

--update clientes set cnpj = '09.235.470/0001-09' where clienteid = 22
--select cnpj,nome,fone,email,site,tipo from clientes where tipo = 'F'
--Select * from meses

--delete from aspnetroles where id = 'c011f278-ec47-4352-90ba-d689c98a3538'
--insert categorias (nome) values('Encargos Funcion�rios - Alimenta��o')
--insert categorias (nome) values('Folha de Pagamento')
--insert categorias (nome) values('Aluguel/IPTU')
--insert categorias (nome) values('Condom�nio')
--insert categorias (nome) values('Energia')
--insert categorias (nome) values('Telefonia Fixa')
--insert categorias (nome) values('Telefonia M�vel')
--insert categorias (nome) values('Internet')
--insert categorias (nome) values('Cafeteria')
--insert categorias (nome) values('Material de Escrit�rio')
--insert categorias (nome) values('Higiene')
--insert categorias (nome) values('Estacionamento')
--insert categorias (nome) values('Comiss�o')
--insert categorias (nome) values('Reembolso')
--insert categorias (nome) values('Impostos')
--insert categorias (nome) values('Presta��o de Servi�os/Terceiros')
--insert categorias (nome) values('Seguro')

insert meses(mes,nome) values(1,'Janeiro')
insert meses(mes,nome) values(2,'Fevereiro')
insert meses(mes,nome) values(3,'Mar�o')
insert meses(mes,nome) values(4,'Abril')
insert meses(mes,nome) values(5,'Maio')
insert meses(mes,nome) values(6,'Junho')
insert meses(mes,nome) values(7,'Julho')
insert meses(mes,nome) values(8,'Agosto')
insert meses(mes,nome) values(9,'Setembro')
insert meses(mes,nome) values(10,'Outubro')
insert meses(mes,nome) values(11,'Novembro')
insert meses(mes,nome) values(12,'Dezembro')


--delete from clientes
--Select * from Clientes
-- update clientes set site= null where site = 'NULL'
--dbcC CHECKIDENT('Clientes',RESEED,0)
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('METASOFT SOLUTIONS','151.511.515/1351-53','NULL','RUA NEVADA KID,93','LOTE 4','ARAL�','7500000',5135,'(11) 4656-1808      ','NULL','www.metasoftsolutions.com.br','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('ELETROPAULO METROPOLITANA ELETRICIDADE S�O PAULO S.A.','616.952.270/001-93','206165226110','Avenida Marcos Penteado de Ulh�a Rodrigues, 939','loja 1 e 2','Tambor�','06460-040',1750,'(08) 0077-9015      ','NULL','www.aeseletropaulo.com.br','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('ALLIANZ SA�DE S.A.','044.396.270/001-02','NULL','Rua Eug�nio de Medeiros, 303','NULL','Pinheiros','05425-000',5214,'(11) 4001-5060      ','rose@roseoliveiracorretora.com.br','www.allianz.com.br/saude','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('HEALTHY LASER & SKIN CARE ASSIST�NCIA M�DICA LTDA.','041.246.870/0012-8','NULL','Avenida Engenheiro Luiz Carlos Berrini, 1500','Conjunto 83 e 84','Cidade Mon��es','04571-000',5214,'(11) 5505-7326      ','adm@clinicahealthy.com.br','NULL','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('MARIA PAULA DE PADUA DEL NERO','517.270.056/00','NULL','Avenida Lavandisca, 538','Apto. 43','Indian�polis','04515-011',5214,'(11) 5505-7326      ','adm@clinicahealthy.com.br','NULL','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('TELEFONICA BRASIL S.A.','025.581.570/0016-2','108383949112','Avenida Engenheiro Luiz Carlos Berrini, 1376',NULL,'Cidade Mon��es','04571-936',5214,'(10) 315            ',NULL,'www.vivo.com.br','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('CONDOM�NIO EDIF�CIO E-OFFICE DESIGN BERRINI','155.983.730/0018-3',NULL,'Avenida Engenheiro Luiz Carlos Berrini, 1774',NULL,'Cidade Mon��es','04571-000',5214,'(11) 4314-7941      ','eoffice.assistente@innova.net.br','www.innova.net.br/','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('IFTNET INFORMATICA LTDA','045.980.510/0011-8','562.242.493-110','Avenida Coronel Jos� Soares Marcondes, 983',NULL,'Bosque','19010-080',5007,'(18) 3226-2222      ',NULL,'www.iftnet.com.br','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('ADONAI VIAGENS E NEG�CIOS CORPORATIVOS - EIRELI - ME','186.971.350/0011-3',NULL,'Rua Afonso Pena, 352','conj.15','Bom Retiro','01124-000',5214,'(08) 0064-9097      ','ricardo@adonaiturismo.com','www.adonaiviagenscorporativas.com.br','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('TICKET SERVI�OS S/A','478.669.340/0017-4',NULL,'Alameda Tocantins, 125','20� ao 23� andar','ALPHAVILLE INDUSTRIAL','06455-020',1750,'(11) 4003-9000      ','falecom-br@edenred.com','http://www.ticket.com.br/portal/','F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('PHONOWAY COM�RCIO E REPRESENTA��O DE SISTEMAS LTDA.','654.144.760/0011-4',NULL,'Alameda Araguaia, 3787','unidade 13 - sala 13','ALPHAVILLE INDUSTRIAL','06455-000',1750,NULL,NULL,NULL,'F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('PORTO SEGURO COMPANHIA DE SEGUROS GERAIS','611.981.640/0016-0',NULL,'Avenida Rio Branco, 1489',NULL,'Campos El�seos','01205-001',5214,NULL,NULL,NULL,'F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('SISTEMA QUATRO T�CNICAS DE CONSERVA��O AMBIENTAL LTDA.','455.263.160/0015-0',NULL,'Avenida Jabaquara, 2940','Conjunto 38','Mirand�polis','04046-500',5214,'(11) 9657-9767      ','renata@sistema4.com.br',NULL,'F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('SALOM�O E ZOPPI M�DICOS E PARTICIPA��ES S/A','477.965.540/0014-5',NULL,'Avenida dos Carin�s, 635',NULL,'Indian�polis','04086-011',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('TICKET SERVI�OS S/A','474.589.340/0017-4',NULL,'Alameda Tocantins, 125',NULL,'Alphaville Industrial','06455-020',1750,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('YAMAHA ADMINISTRADORA DE CONSORCIO LTDA. (OUVIDORIA)','474.581.530/0014-0',NULL,'Rodovia Presidente Dutra, S/N, KM 214',NULL,'Cumbica','07178-580',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('YAMAHA MOTOR DA AMAZONIA LTDA. (FILIAL)','048.170.520/0029-7',NULL,'Rodovia Presidente Dutra, S/N, KM 214',NULL,'Cumbica','07178-580',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('YAMAHA ADMINISTRADORA DE CONSORCIO LTDA. (CONS�RCIO)','474.581.530/0014-0',NULL,'Rodovia Presidente Dutra, S/N, KM 214',NULL,'Cumbica','07178-580',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('BANCO YAMAHA MOTOR DO BRASIL S.A.','103.714.920/0018-5',NULL,'Rodovia Presidente Dutra, S/N, KM 214',NULL,'Cumbica','07178-580',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('YAMAHA MOTOR DA AMAZONIA LTDA. (FILIAL)','048.170.520/0029-7',NULL,'Rodovia Presidente Dutra, S/N, KM 214',NULL,'Cumbica','07178-580',5214,NULL,NULL,NULL,'C')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('FUNDA��O CPQD CENTRO DE PESQUISAS E DESENVOLVIMENTO EM TELECOMUNICA��ES','026.416.630/0011-0',NULL,'Rua Doutor Ricardo Benetton Martins, 1000',NULL,'Polo II de Alta Tecnologia (Campinas)','13086-510',2626,NULL,NULL,NULL,'F')
--Insert clientes (nome,cnpj,ie,endereco,complemento,bairro,cep,cidadeid,fone,email,site,tipo) values('IMPOSTOS','092.354.700/0010-9',NULL,'Avenida Engenheiro Luiz Carlos Berrini',NULL,'Cidade Mon��es','04571-000',5214,NULL,'ledalaurin@hotmail.com',NULL,'F')




--Select * from dias
--insert Fps (nome) values('� vista')
--insert Fps (nome) values('Parcelado')

--insert situacaos(nome) values('Em an�lise')
--insert situacaos(nome) values('Aceita')
--insert situacaos(nome) values('Negada')
--insert situacaos(nome) values('Finalizada')



--CREATE TABLE [dbo].[Situacaos](
--	[situacaoid] [int] IDENTITY(1,1) NOT NULL,
--	[nome] [varchar](50) NOT NULL
--) ON [PRIMARY]

--CREATE TABLE [dbo].[Propostas](
--	[propostaid] [int] IDENTITY(1,1) NOT NULL,
--	[dtcad] [date] NOT NULL DEFAULT (getdate()),
--	[clienteid] [int] NOT NULL,
--	[envio] [date] NULL,
--	[numpo] [nvarchar](max) NULL,
--	[numnf] [nvarchar](max) NULL,
--	[valor] [decimal](12, 3) NOT NULL,
--	[fp] [int] NOT NULL DEFAULT ((0)),
--	[recebido] [bit] NOT NULL,
--	[he] [int] NOT NULL,
--	[hu] [int] NULL,
--	[previsao] [int] NULL,
--	[situacaoid] [int] NOT NULL DEFAULT ((1))
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--CREATE TABLE [dbo].[Meses](
--	[mes] [int] NOT NULL,
--	[nome] [varchar](20) NOT NULL
--) ON [PRIMARY]

--GO


--CREATE TABLE [dbo].[Historicoes](
--	[historicoid] [int] IDENTITY(1,1) NOT NULL,
--	[fid] [int] NOT NULL,
--	[tipo] [char](1) NOT NULL,
--	[data] [datetime] NOT NULL DEFAULT (getdate()),
--	[texto] [varchar](2000) NULL
--) ON [PRIMARY]


--CREATE TABLE [dbo].[Fps](
--	[fpid] [int] IDENTITY(1,1) NOT NULL,
--	[nome] [varchar](150) NOT NULL
--) ON [PRIMARY]

--CREATE TABLE [dbo].[Contatoes](
--	[contatoid] [int] IDENTITY(1,1) NOT NULL,
--	[clienteid] [int] NOT NULL,
--	[nome] [varchar](100) NOT NULL,
--	[fone] [char](20) NULL,
--	[celular] [char](20) NULL,
--	[email] [varchar](200) NULL,
--	[sexo] [char](1) NOT NULL
--) ON [PRIMARY]

--ALTER TABLE [dbo].[Contatoes]  WITH CHECK ADD  CONSTRAINT [FK_Contatos_Clientes_ID] FOREIGN KEY([clienteid])
--REFERENCES [dbo].[Clientes] ([clienteid])
--GO

--ALTER TABLE [dbo].[Contatoes] CHECK CONSTRAINT [FK_Contatos_Clientes_ID]
--GO



--CREATE TABLE [dbo].[ContaPrs](
--	[contaprid] [int] IDENTITY(1,1) NOT NULL,
--	[dtcad] [date] NOT NULL DEFAULT (getdate()),
--	[categoriaid] [int] NULL,
--	[npar] [int] NOT NULL DEFAULT ((1)),
--	[cliforid] [int] NOT NULL,
--	[propostaid] [int] NULL DEFAULT ((1)),
--	[valor] [decimal](12, 2) NOT NULL,
--	[vencimento] [date] NOT NULL,
--	[tipo] [char](1) NOT NULL DEFAULT ('R'),
--	[dtpagto] [date] NULL,
--	[situacao] [char](1) NOT NULL DEFAULT ('A'),
--	[recorrente] [bit] NULL DEFAULT ((0))
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[ContaPrs]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_UserID] FOREIGN KEY([cliforid])
--REFERENCES [dbo].[Clientes] ([clienteid])
--GO

--ALTER TABLE [dbo].[ContaPrs] CHECK CONSTRAINT [FK_Clientes_UserID]
--GO




--CREATE TABLE [dbo].[Clientes](
--	[clienteid] [int] IDENTITY(1,1) NOT NULL,
--	[dtcad] [date] NOT NULL DEFAULT (getdate()),
--	[nome] [varchar](100) NULL,
--	[cnpj] [nvarchar](max) NULL,
--	[ie] [nvarchar](max) NULL,
--	[endereco] [nvarchar](max) NULL,
--	[complemento] [nvarchar](max) NULL,
--	[bairro] [nvarchar](max) NULL,
--	[cep] [nvarchar](max) NULL,
--	[cidadeid] [int] NOT NULL,
--	[fone] [char](20) NULL,
--	[email] [varchar](200) NULL,
--	[site] [varchar](100) NULL,
--	[tipo] [char](1) NULL,
-- CONSTRAINT [PK_dbo.Clientes] PRIMARY KEY CLUSTERED 
--(
--	[clienteid] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--GO

--SET ANSI_PADDING OFF
--GO

--ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Cidades_ID] FOREIGN KEY([cidadeid])
--REFERENCES [dbo].[Cidades] ([cidadeid])
--GO

--ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Cidades_ID]
--GO

--CREATE NONCLUSTERED INDEX [ClientesNome] ON [dbo].[Clientes]
--(
--	[nome] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--GO



--CREATE TABLE [dbo].[Cidades](
--	[cidadeid] [int] IDENTITY(1,1) NOT NULL,
--	[Nome] [varchar](150) NOT NULL,
--	[UF] [char](2) NOT NULL
--) ON [PRIMARY]
--CREATE NONCLUSTERED INDEX [CidadesNome] ON [dbo].[Cidades]
--(
--	[Nome] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--GO


--CREATE TABLE [dbo].[Categorias](
--	[categoriaid] [int] IDENTITY(1,1) NOT NULL,
--	[nome] [varchar](50) NOT NULL
--) ON [PRIMARY]


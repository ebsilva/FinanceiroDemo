DROP INDEX [CidadesNome] ON Cidades
CREATE NONCLUSTERED INDEX [CidadesNome] ON [dbo].[Cidades]
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

DROP INDEX [ClientesNome] ON Clientes
CREATE NONCLUSTERED INDEX [ClientesNome] ON [dbo].[Clientes]
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

DROP INDEX [ContasCliForId] ON [dbo].[ContaPrs]
CREATE NONCLUSTERED INDEX [ContasCliForId] ON [dbo].[ContaPrs]
(
	[cliforid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

DROP INDEX [ContatoClienteID] ON [dbo].[Contatoes]
CREATE NONCLUSTERED INDEX [ContatoClienteID] ON [dbo].[Contatoes]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

DROP INDEX [ContatoClienteID] ON [dbo].[Contatoes]
ALTER TABLE [dbo].[Propostas] ADD  CONSTRAINT [PK_dbo.Propostas] PRIMARY KEY CLUSTERED 
(
	[propostaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

DROP INDEX [PropostaClienteID] ON [dbo].[Propostas]
CREATE NONCLUSTERED INDEX [PropostaClienteID] ON [dbo].[Propostas]
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
GO

select * from Lancamentoes_FullBackup_05062019
where year(dia)= 2018



CREATE TABLE [dbo].[Lancamentoes_2023](
	[id] [int] NOT NULL,
	[userid] [nvarchar](128) NOT NULL,
	[projetoid] [int] NOT NULL,
	[atividadeid] [int] NOT NULL,
	[dia] [date] NOT NULL,
	[horas] [int] NULL,
	[extras] [int] NULL
) ON [PRIMARY]
GO

CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20170403-181353_2023] ON [dbo].[Lancamentoes_2023]
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181419_2023] ON [dbo].[Lancamentoes_2023]
(
	[projetoid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181439_2023] ON [dbo].[Lancamentoes_2023]
(
	[userid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20170403-181850_2023] ON [dbo].[Lancamentoes_2023]
(
	[dia] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO


--INSERT INTO [dbo].[Lancamentoes_2017]
--SELECT * FROM [dbo].[Lancamentoes_FullBackup_05062019]
--WHERE  year(dia)= 2017

--INSERT INTO [dbo].[Lancamentoes_2018]
--SELECT * FROM [dbo].[Lancamentoes_FullBackup_05062019]
--WHERE  year(dia)= 2018
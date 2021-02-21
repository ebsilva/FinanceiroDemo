--drop procedure [dbo].[spContas]
--drop procedure [dbo].[spContas_2017]
--drop procedure [dbo].[spContas_2018]
--drop procedure [dbo].[spContas_2019]
--drop procedure [dbo].[spContas_2020]
--drop procedure [dbo].[spContas_2021]
--drop procedure [dbo].[spContas_2022]
--drop procedure [dbo].[spContas_2023]
--drop procedure [dbo].[spContas_2024]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spContas]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome,cpr.categoriaid,   
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO

CREATE PROCEDURE [dbo].[spContas_2017]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome, cpr.categoriaid,  
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2017 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2017 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO

CREATE PROCEDURE [dbo].[spContas_2018]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome, cpr.categoriaid,  
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2018 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2018 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2019]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome, cpr.categoriaid,  
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2019 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2019 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2020]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome,cpr.categoriaid,   
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2020 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2020 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2021]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome,  cpr.categoriaid, 
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2021 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2021 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2022]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome,  cpr.categoriaid, 
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2022 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2022 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2023]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome, cpr.categoriaid,  
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2023 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2023 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO
CREATE PROCEDURE [dbo].[spContas_2024]  
@tipo AS char(1)
AS
SET NOCOUNT ON
Select  
cpr.contaprid,cpr.cliforid,cpr.propostaid,  
c.nome clifornome,  cpr.categoriaid, 
convert(char(10), cpr.dtcad, 103) dtcad,  
cpr.npar,cpr.valor,  
convert(char(10), cpr.vencimento, 103) vencimento,convert(char(10), cpr.dtpagto, 103) dtpagto,  
case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
isnull(n.nfid,'0') nfid, isnull(cpr.nf,'') nf, cpr.archive  
from contaPRs_2024 cpr  
inner join Clientes c on c.clienteid = cpr.cliforid  
inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
left join Propostas p on p.propostaid = cpr.propostaid  
left join Nfs n on n.contaprid = cpr.contaprid  
left join  
(Select c.contagrupo,max(substring(c.noordem,1,2)) ultima from contaPRs_2024 c group by c.contagrupo)  l on cpr.contagrupo = l.contagrupo  
 where cpr.tipo = @tipo
GO





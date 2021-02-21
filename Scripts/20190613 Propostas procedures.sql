USE [DnkLocalDb]
GO

/****** Object:  StoredProcedure [dbo].[spPropostas]    Script Date: 13/06/2019 13:44:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[spPropostas]  
@isproject AS int
AS
SET NOCOUNT ON
Select  
p.propostaid, 
p.clienteid, c.nome clientenome,p.descricao,  isnull(convert(char(10),p.dtcad, 103),'') dtcad, 
isnull(convert(char(10), p.dtaceite, 103),'') dtaceite, 
isnull(convert(char(10), p.envio, 103),'') envio, isnull(numpo,'') numpo,isnull(numnf,'') numnf,valor, 
recebido, isnull([p].[he],0) horasestimadas,isnull([p].[hu],0) horasutilizadas, isnull(convert(char(10), p.previsao, 103),'') previsao, 
s.situacaoid,s.nome situacao,isnull(npt.np,0) np ,ca.categoriaid,ca.nome categorianome,  
cor, isnull(n.faturado,0) faturado, isnull(p.motivo,'') motivo, isnull(u.Nome,'') responsavel, valor - isnull(n.faturado,0) saldo  
from  Propostas p  
inner join categorias ca on ca.categoriaid = p.categoriaid  
inner join Clientes c  on p.clienteid = c.clienteid  
inner join Situacaos s on s.situacaoid = p.situacaoid  
left join AspnetUsers u on u.id = p.responsavel  
left join (Select propostaid,isnull(count(*),0) np  
from Contaprs  group by propostaid) npt on npt.propostaid = p.propostaid  
left join ( 
   Select propostaid,isnull(cast(sum(valor) as decimal (15,2)),0.00) faturado  
   from contaprs 
   where tipo = 'R' and nf is not null and dtpagto is not null and propostaid <> 0 
   group by propostaid 
 ) n on n.propostaid = p.propostaid   
where p.isproject = @isproject ;
GO



USE [DnkLocalDb]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spClientes]  
@tipo AS char(1)
AS
SET NOCOUNT ON

Select clienteid, nome,tipo,bairro,cidade,estado,cep,isnull(cnpj,'') cnpj,isnull(complemento,'') complemento,endereco,
       dtcad,email,fone, isnull(site,'') site, f0800,atrasados,pagamentos 
from(
Select c.clienteid,c.bairro,c.cidade,c.estado,c.cep,c.cnpj, c.complemento, 
c.endereco,c.nome, c.dtcad,c.email,c.fone,c.site,c.tipo,c.f0800, 
(Select count(*) from contaprs where cliforid = c.clienteid and  tipo =  case when tipo='C' then 'R' else 'P' end  and situacao = 'A'
and cast(vencimento  as date) < cast(getdate() as date) ) atrasados,  
(
	Select count(*) from contaprs where cliforid = c.clienteid and tipo = case when tipo='C' then 'R' else 'P' end  and situacao = 'A'
	and cast(vencimento  as date) >= cast(getdate() as date)) pagamentos 
from clientes c  
where c.tipo =  @tipo
) Clientes 
go
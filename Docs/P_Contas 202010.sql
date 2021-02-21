/*

  Lista as contas por tipo
  Join conm NfNumeracoes para gonseguir o Id da nota fiscal
  para pegar header e detalhes da nota fiscal pesquisa pelo campo NfIdReceber

*/


-- drop procedure P_Contas
-- Exec  P_Contas 'R'
-- s
-- select* from NfNumeracaos
-- select * from Contaprs
/*
  alter table ContaPrs_2017 add nfid int null
  alter table ContaPrs_2018 add nfid int null
  alter table ContaPrs_2019 add nfid int null
  alter table ContaPrs_2020 add nfid int null
  alter table ContaPrs_2021 add nfid int null
  alter table ContaPrs_2022 add nfid int null
  alter table ContaPrs_2023 add nfid int null
  alter table ContaPrs_2024 add nfid int null
  alter table ContaPrs_2025 add nfid int null

*/



Alter PROCEDURE [dbo].[P_Contas]  
@tipo AS char(1)
AS
SET NOCOUNT ON
	Select  
	cpr.contaprid,cpr.cliforid,cpr.propostaid,  
	c.nome clifornome,cpr.categoriaid,   
	convert(char(10), cpr.dtcad, 103) dtcad,  
	cpr.npar,cpr.valor,  
	convert(char(10), cpr.vencimento, 103) vencimento,
	convert(char(10), cpr.dtpagto, 103) dtpagto,  
	case when (CAST(getdate() AS DATE) > CAST(cpr.vencimento as DATE) and cpr.situacao = 'A') then 'S' else 'N' end atrasado,  
	cpr.situacao,cpr.recorrente,ca.nome categorianome,cpr.descricao,isnull(cpr.noordem,'01/01') noordem,  
	--case when isnull(l.ultima,0) = cast(substring(noordem,4,2) as int) then 0 else  
	--case when isnull(l.ultima,0) = cast(substring(noordem,1,2) as int) then 1 else 0 end end allowpar,  
	0 allowpar,
	isnull(nm.NfIdGerado,0) nfid,
	isnull(nm.NfImpressoes,0) numprint,
	isnull(cpr.nf,'') nf, 
	cpr.archive  
	from contaPRs cpr  
	inner join Clientes c on c.clienteid = cpr.cliforid  
	inner join Categorias ca on ca.categoriaid = cpr.categoriaid  
	left join Propostas p on p.propostaid = cpr.propostaid  
	left join NfNumeracaos nm on nm.NfIdGerado = cpr.Nfid 

GO



select count(*) from ContaPRs
select count(*) from ContaPRs_BackUp_Full where Year(vencimento) = 2017 and dtpagto is not null and Year(dtpagto) = 2017 and nf is  null
select count(*) from ContaPRs_BackUp_2017 where Year(vencimento) = 2017 and dtpagto is not null and Year(dtpagto) = 2017 and nf is  null


select count(*) from ContaPRs where dtpagto is not null and Year(dtpagto) = 2017
select count(*) from ContaPRs where Year(vencimento) = 2016

Select *  from  ContaPRs where Year(vencimento) = 2017 and dtpagto is not null and Year(dtpagto) = 2017 and nf is  null
-- delete from  ContaPRs where Year(vencimento) = 2017 and dtpagto is not null and Year(dtpagto) = 2017 and nf is  null

--DBCC DBREINDEX ('Contaprs'); 
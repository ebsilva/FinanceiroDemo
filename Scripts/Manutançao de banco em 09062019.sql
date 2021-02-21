SELECT *
INTO    [dbo].[ContaPrs_Full_Backup_09062019_test]
FROM    [dbo].[Contaprs]


SELECT * FROM ContaPrs_Full_Backup_09062019_test where situacao = 'P' and year(vencimento) = 2017 and dtpagto is not null
-- Test 1597
SELECT * FROM ContaPrs_Full_Backup_09062019_test	
where contaprid  in(
					select contaprid 
					from ContaPrs_2017 
					where situacao = 'P' and year(vencimento) = 2017) 
and situacao = 'P' and year(vencimento) = 2017

--delete FROM ContaPrs_Full_Backup_09062019_test	
--where contaprid  in(
--					select contaprid 
--					from ContaPrs_2017 
--					where situacao = 'P' and year(vencimento) = 2017) 
--and situacao = 'P' and year(vencimento) = 2017

-- Producao 1597
SELECT * FROM ContaPrs	
where contaprid  in(
					select contaprid 
					from ContaPrs_2017 
					where situacao = 'P' and year(vencimento) = 2017) 
and situacao = 'P' and year(vencimento) = 2017
--delete FROM ContaPrs	
--where contaprid  in(
--					select contaprid 
--					from ContaPrs_2017 
--					where situacao = 'P' and year(vencimento) = 2017) 
--and situacao = 'P' and year(vencimento) = 2017
-- 1414 (1597 - 183)


----------------------------------------------------------------------------------------------
--   2018
------------------------------------------------------------------------------------------------


SELECT * FROM ContaPrs where situacao = 'P' and year(vencimento) = 2018
--(11 rows affected)

SELECT * FROM ContaPrs_2018 where situacao = 'P' and year(vencimento) = 2018
--(2553 rows affected)

SELECT * FROM ContaPrs	
where contaprid  in(
					select contaprid 
					from ContaPrs_2018 
					where situacao = 'P' and year(vencimento) = 2018) 
and situacao = 'P' and year(vencimento) = 2018
--(11 rows affected)
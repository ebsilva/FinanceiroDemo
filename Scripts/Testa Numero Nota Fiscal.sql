use DnkLocalDb
-- Proximo numero sera 2022
DBCC CHECKIDENT ('NfNumeracaos', RESEED, 2911)
/*
 delete from nfreceberdetalhes
 delete from nfrecebers
 delete from NfNumeracaos
 update  contaprs set nfid = 0 where categoriaid = 20

 select *  from NfNumeracaos

*/
declare @nfid int
select top 1 @nfid = nfid from nfrecebers order by nfid desc
select * from nfrecebers where nfid = @nfid
select * from contaprs where nfid = @nfid
select * from nfreceberdetalhes where nfid = @nfid





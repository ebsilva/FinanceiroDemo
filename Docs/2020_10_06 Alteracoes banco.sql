exec P_Contas 'R'
select* from contaprs
alter table contaprs alter column nfid int  not null 
ALTER TABLE [dbo].[contaprs] ADD  DEFAULT ((0)) FOR [nfid]
update  contaprs set nfid = 0

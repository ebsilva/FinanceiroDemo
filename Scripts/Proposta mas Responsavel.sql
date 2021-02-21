select * from propostas where propostaid = 525

select * from contaprs where propostaid = 524
select c.nf,convert(char(10), c.dtcad,103) emissao,convert(char(10), c.vencimento,103) vencimento,
isnull(convert(char(10),c.dtpagto,103),'') dtpagto,convert(decimal(15,2),c.valor) valor,
c.descricao,c.noordem, isnull(u.Nome,'') Nome
from contaprs c
left join propostas p on p.propostaid = c.propostaid
left join AspnetUsers u on u.Id = p.Responsavel
where c.propostaid = 525
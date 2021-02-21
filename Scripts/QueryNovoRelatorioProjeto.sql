--select * from UsuarioProjetoes
--select * from lancamentoes
--select * from propostas
/*
 select userid,sum(horas) ttdia from lancamentoes
 where dia = cast('2018-01-01' as DateTime)
 group by  userid

*/
select u.UserName,c.nome, p.propostaid, l.dia,ttdia,extras, p.he,p.hu, dtiniprj, dtfimprvprj
from (select userid,dia,projetoid,sum(horas) ttdia, sum(extras) extras from lancamentoes
	  where month(dia) = 1 and YEAR(dia) = 2018
     group by  userid,dia,projetoid) l
inner join Propostas p on p.propostaid = l.projetoid
inner join Clientes c on c.clienteid = p.clienteid
left join AspNetUsers u  on u.Id = p.Responsavel
order by l.dia
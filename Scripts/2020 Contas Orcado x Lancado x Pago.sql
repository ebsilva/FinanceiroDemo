select a.ano, a.lancado, a.Pago, b.Orcado from 
(
	select year(c.vencimento)  ano,
		   sum(case when situacao= 'A' then c.valor else 0 end) Lancado ,
		   sum(case when situacao ='P' then c.valor else 0 end) Pago
	from contaprs c
	where year(c.vencimento) =2019 
	group by year(c.vencimento)
) a
inner join (
		select ano, sum(oja+ofv+omr+oab+oju+ojl+oag+ost+oot+ono+ode ) Orcado 
		from orcamento  
		where ano = 2019  and cat in (select categoriaid from categorias where tipo = 'R')
		group by ano
) b on b.ano = a.ano



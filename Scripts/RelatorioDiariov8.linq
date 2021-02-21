<Query Kind="Program">
  <Connection>
    <ID>c858dd53-a7b4-4116-9cb0-38eb5cf719dd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>EDSON\SQLEXPRESS</Server>
    <Database>DnkLocalDb</Database>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>E:\Dev20\Templates\ApiWebWpf\MsApi\bin\Debug\netcoreapp3.1\Microsoft.EntityFrameworkCore.dll</Reference>
  <IncludeLinqToSql>true</IncludeLinqToSql>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main(){ 

        /*  Base de Lancamentos No Mes e Ano */
	    List<Anl>  Analistas = AspNetUsers
		            .Where( k => Lancamentoes.Any(l => l.Userid == k.Id) )
					.Select ( a => new Anl{Id= a.Id, Nome = a.Nome})
					.OrderBy( o => o.Nome).ToList();
					
        var lancamentos = Lancamentoes
						   .Join(Propostas, l => l.Projetoid, p  => p.Propostaid, (l,p)   => new {l, p})
						   .Where( lp => lp.l.Dia.Year==2019 && lp.l.Dia.Month == 1)
						   .Where( la => (la.l.Horas > 0 || la.l.Extras > 0)  )
						   .Select((la) => new HorasLancadas{ 
						       UserId = la.l.Userid , 
							   ProjetoId = la.l.Projetoid, 
							   ClienteId = la.p.Clienteid,
							   Dia = la.l.Dia , 
							   Horas = la.l.Horas ?? 0, 
							   Extras = la.l.Extras ?? 0
						}).ToList();


	foreach( var analista in Analistas){
		    analista.Projetos =   GetProjetosDoAnalista(analista.Id);
			foreach(var p in  analista.Projetos){
			    
				p.Lancamentos =  lancamentos
							.Where( k => k.UserId ==  p.UsuarioId &&
				                                 k.ProjetoId == p.ProjetoId &&
												 k.ClienteId == p.ClienteId)
							.Select( l => new HorasLancadas{
							    Dia = l.Dia,
								Horas = l.Horas,
								Extras = l.Extras
							}).ToList();
			
			}
	}
	
	Console.WriteLine(Analistas);
	   

}

public List<Up> GetProjetosDoAnalista(string uid){
	return	UsuarioProjetoes
	        .Join(Propostas, u => u.Projetoid ,  p => p.Propostaid, (u,p)  => new {u,p})
			.Join(Clientes, p => p.p.Clienteid, c => c.Clienteid, (p,c) => new {p,c,})
			.Where( k => k.p.u.Userid == uid && k.p.p.Situacaoprojetoid == 1)
			.Select( up =>  new Up{ 
							UsuarioId = up.p.u.Userid,
							ClienteId = up.p.p.Clienteid, 
							Razao = up.c.Nome,
							ProjetoId= up.p.p.Propostaid,
							Descricao = up.p.p.Descricao,
							Inicio =  up.p.p.Dtiniprj,
							Fim = up.p.p.Dtfimprj,
							PrevisaoFim = up.p.p.Dtfimprvprj
						}).ToList();
}
public class  Up{
	public string UsuarioId {get;set;}
	public int ClienteId {get;set;}
	public string Razao {get;set;}
	public int ProjetoId {get;set;}
	public string Descricao {get;set;}
	public DateTime? Inicio {get;set;}
	public DateTime? Fim {get;set;}
	public DateTime? PrevisaoFim {get;set;}
	public List<HorasLancadas> Lancamentos {get;set;}
}

  
public class RelatorioHorasDiario{
    public  string Id {get;set;}
    public  Anl Analista {get;set;}
}

public class Anl{
    public string Id {get;set;}
	public string Nome {get;set;}
	public List<Up> Projetos {get;set;}
}

public class ProjetoAnalista{
  //  public int  ClienteId {get;set;}
	public string Descricao {get;set;}
//	public List<ProjetoComHoras> Projetos {get;set;}
}

public class ProjetoHoras{
  public  int ProjetoId {get;set;}
  public string Descricao {get;set;}
  public List<HorasLancadas> Lancamentos {get;set;}
}

public class HorasLancadas {
  public string UserId {get;set;}
  public int ProjetoId {get;set;}
  public int ClienteId {get;set;}
  public DateTime Dia {get;set;}
  public int Horas {get;set;} =0;
  public int Extras {get;set;} =0;
}
// 	return   lancamentos
//			.Join(Clientes, prj => prj.ClienteId, c  => c.Clienteid, (p,c)   => new {p,c})
//			.Where( k => lancamentos.Any(l => l.UserId == analista.Id))
//			.Select( pro => new ClientesComProjeto{ ClienteId = pro.c.Clienteid, Nome = pro.c.Nome }).Distinct().ToList();
			 
//             ra.Clientes=  (List<ClientesComProjeto>)clientes;
//
//			 /* Para cada  Cliente adiciona Projetos */
//				foreach( var r in ra.Clientes){
//						/*  Adiciona Projetos do cliente*/
//						var projetos =  lancamentos
//				         .Join(Propostas, l => l.ProjetoId, p  => p.Propostaid, (l,p)   => new {l, p})
//						 .Where(cli => cli.p.Clienteid == r.ClienteId && cli.l.UserId == analista.Id)
//						 .Select( prj => new ProjetoComHoras{
//						     ProjetoId =  prj.p.Propostaid,
//						     Descricao = prj.p.Descricao,
//							 }).Distinct();
//	  					     r.Projetos =  	projetos.ToList();
//							 
//							 foreach(var pro in r.Projetos){
//							    var horas = lancamentos
//							       .Where( l => l.UserId == analista.Id &&  l.ProjetoId == pro.ProjetoId)
//								   .Select( l => new HorasLancadas{
//								     Dia = l.Dia,
//									 Horas = l.Horas,
//									 Extras =l.Extras
//								   });
//								 pro.Lancamentos = horas.ToList();
//							 }
//							 
//							
//				}
//				ral.Add(ra);
//				ra.Clientes=  (List<ClientesComProjeto>)clientes;


	//foreach(var l in ral){
	//   Console.WriteLine(l.Analista.Nome);
	//   foreach(var c in l.Clientes){ 
	//      Console.WriteLine(c.Nome);
	//     foreach(var p in c.Projetos){
	//		 Console.WriteLine(p.Descricao);
	//		  foreach(var h in p.Lancamentos){
	//		  	Console.WriteLine(h.Horas);
	//		  }
	//	 }
	//   }
	//
	//}
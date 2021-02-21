<Query Kind="FSharpProgram">
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

void Main()
{
	var lancamentos = Lancamentoes.Where( l => l.Dia.Year==2019 && l.Dia.Month == 1)
	          .Where( la => la.Horas > 0 )
	          .Select( la => new HorasLancadas{ 
			    UserId = la.Userid , ProjetoId = la.Projetoid, Dia = la.Dia , 
				Horas = la.Horas ?? 0, Extras = la.Extras ?? 0
				});

	
    var analistas = AspNetUsers
					.Where( k => lancamentos.Any(l => l.UserId == k.Id))
					.Select ( a => new Anl{Id= a.Id, Nome = a.Nome});	
    
	var  ral = new  List<RelatorioHorasDiario>();
	foreach( var a in analistas){
	       var  ra  = new  RelatorioHorasDiario();
	       ra.Analista= a;
		   /* Cli nte com Projetos */
		   	var clientes =  lancamentos
	         .Join(Propostas, l => l.ProjetoId, p  => p.Propostaid, (l,p)   => new { pro = p})
			 .Join(Clientes, prj => prj.pro.Clienteid, c  => c.Clienteid, (p,c)   => new { c,p})
			 .Select( pro => new ClientesComProjeto{
			     ClienteId = pro.c.Clienteid,
				 Nome = pro.c.Nome
			 }).Distinct().ToList();
			 ra.Clientes=  (List<ClientesComProjeto>)clientes;
			 ral.Add(ra);
	}
			 
	foreach( var r in ral){
	    Console.WriteLine( r.Analista.Nome);
		foreach(var c in r.Clientes){
		    Console.WriteLine( c.Nome);
			/* Projetos do cliente*/
			var projetos =  lancamentos
	         .Join(Propostas, l => l.ProjetoId, p  => p.Propostaid, (l,p)   => new {l, p})
			 .Where(cli => cli.p.Clienteid == c.ClienteId)
			 .Select( prj => new ProjetoComHoras{
			     ProjetoId =  prj.p.Propostaid,
			     Descricao = prj.p.Descricao,
				 
				 }).Distinct().ToList();
				 Console.WriteLine(projetos);
//				 foreach(var pro in projetos){
//				    var horas = lancamentos
//				       .Where( l => l.UserId == r.Analista.Id && 
//					                l.ProjetoId == pro.ProjetoId)
//					   .Select( l => new HorasLancadas{
//					     Dia = l.Dia,
//						 Horas = l.Horas,
//						 Extras =l.lExtras
//					   })
//				 
//				 }

				 
				 /* Horas lancadas */
   
				
				
		}
	}
				
	
	 
	
}

public class RelatorioHorasDiario{
    public  string Id {get;set;}
    public  Anl Analista {get;set;}
    public List<ClientesComProjeto> Clientes {get;set;}

}

public class Anl{
    public string Id {get;set;}
	public string Nome {get;set;}
}

public class ClientesComProjeto{
    public int    ClienteId {get;set;}
	public string Nome {get;set;}
	public List<ProjetoComHoras> Projetos {get;set;}
}

public class ProjetoComHoras{
  public  int ProjetoId {get;set;}
  public string Descricao {get;set;}
  public List<HorasLancadas> Lancamentos {get;set;}
}


public class HorasLancadas {
  //public string UserId {get;set;}
  //public int ProjetoId {get;set;}
  public DateTime Dia {get;set;}
  public int Horas {get;set;} =0;
  public int Extras {get;set;} =0;
}
// You can define other methods, fields, classes and namespaces here

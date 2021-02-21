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

void Main()
{
	//var c = Clientes.Select( c=> new { Id = c.Clienteid, Nome= c.Nome}).Distinct().ToList();
	//Console.WriteLine(c);
	
	var lan = Lancamentoes.Where( l => l.Dia.Year==2019 && l.Dia.Month == 1)
	          .Where( la => la.Horas > 0 )
	          .Select( la => new Horas{ 
			                          UserId = la.Userid , ProjetoId = la.Projetoid, Dia = la.Dia ,
									  Normais = la.Horas ?? 0, Extras = la.Extras ?? 0
								 });
   var a = lan.Select( a=> a.UserId).Distinct();		  
   foreach( var l in a){
         var analista = l;
		 Console.WriteLine(analista);
         var projetos = lan.Where( u => u.UserId == analista).Select( a=> a.ProjetoId).Distinct();
         foreach(var pro in projetos ){
		    Console.WriteLine(pro);
		    var dias = lan.Where( u => u.UserId == analista && u.ProjetoId == pro).Select( a=> a.Dia).Distinct();
			foreach(var  d in dias){
			   Console.WriteLine(d);
			    var horas = lan.Where( u => u.UserId == analista && 
				                       u.ProjetoId == pro &&
									   u.Dia == d).Select(h=> new { h.Normais, h.Extras});
				foreach(var h in horas){
				  Console.WriteLine( h.Normais.ToString(),h.Extras.ToString());
				}
			}
		 }
		 
   }
	
}




public class Horas {
  public string UserId {get;set;}
  public int ProjetoId {get;set;}
  public DateTime Dia {get;set;}
  public int Normais {get;set;} =0;
  public int Extras {get;set;} =0;
}


// You can define other methods, fields, classes and namespaces here

using Metasoft.ViewModels;

namespace Metasoft.Models.Extensions
{
    public static class ME
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">O instancia do tipo que vai ter  methodo</param>
        /// <param name="m">Mes que vai receber o valor</param>
        /// <param name="valor">O valor atribuido ao mes eque sera retornado</param>
        /// <returns></returns>
        ///       var civm = new CaixaItemVm();
        ///       decimal vu = civm.CaixaItem(m, o);
        public static void AtualizaValorDoMes(this Caixa o, int m, decimal valor)
        {
            switch (m)
            {
                case 1:
                    o.Cja = valor;
                    break;
                case 2:
                    o.Cfv = valor;
                    break;
                case 3:
                    o.Cmr = valor;
                    break;
                case 4:
                    o.Cab = valor;
                    break;
                case 5:
                    o.Cma = valor;
                    break;
                case 6:
                    o.Cju = valor;
                    break;
                case 7:
                    o.Cjl = valor;
                    break;
                case 8:
                    o.Cag = valor;
                    break;
                case 9:
                    o.Cst = valor;
                    break;
                case 10:
                    o.Cou = valor;
                    break;
                case 11:
                    o.Cno = valor;
                    break;
                case 12:
                    o.Cde = valor;
                    break;

            }
            
        }
    }
}
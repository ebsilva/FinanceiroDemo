using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System.Data.SqlClient;
using Metasoft.Models;
using System.Reflection.Emit;

namespace Metasoft.Utilities
{
    public static class Tools
    {
        public static string  ShortText(string text, int maxlen)
        {
            if (text.Length >= maxlen)
            {
                return text.Substring(0, maxlen-3) + "...";
            }
            return text;
        }
        public static string Mdy()

        {
            return DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
        }

        public static  string NotNullMoney(decimal item = 0)
        {
            var result =  item == 0  ? "" : String.Format("{0:N}", item);
            return result;


        }
        public static string NotNullString(string item = null)
        {
            return String.IsNullOrEmpty(item) ? "" :  item;

        }
        public static string Status(decimal o = 0, decimal r = 0 , int mes = 0)
        {
            //if( mes <= DateTime.Now.Month && mes != 13)
            //{
                if (o == 0 && r == 0)  return "owhite";
                
                if (o == r) return "ogreen";

                if (r < o)
                {
                    if (mes > DateTime.Now.Month)
                    {
                        if (mes != 13) { return "owhite"; }
                        else { return "oyellow"; }
                    }
                    else { return "oyellow"; }
                       
                }
                //if (r > o) return "oyellow";
            //}
            return "owhite";


        }
        public static string StatusCaixa(decimal c = 0, decimal r = 0, int mes = 0)
        {
     
            if (c == 0 && r == 0) return "owhite";

            if (c >= r) return "ogreen";

            if (c <= r) return "oorange"; 

            return "owhite";


        }

        public static  string Diferenca(decimal o = 0, decimal r = 0)
        {
            string html = "";
            if (o == 0 && r == 0)
                html= "";

            if (o == r)
                html = "";

            if (r < o)
                html = String.Format("{0:N}", o - r) ;

            if (r > o)
                 html = String.Format("{0:N}", r - o) ;

            return html; 

        }

        public static bool TemDiferenca(decimal o = 0, decimal r = 0)
        {
            bool td = false;
            if (o == 0 && r == 0)
                td = false;

            if (o == r)
                td = false;

            if (r < o)
                td = true;

            if (r > o)
                td = true;

            return td;

        }
        public static string MesExtenso (int mes)
        {
            String[] meses = new String[] { "","Janeiro", "Fevereiro", "Março","Abril","Maio", "Junho","Julho", "Agosto", "Setembro", "Outubro", "Novembro","Dezembro" };
            return meses[mes];
        }


        //public DateTime ToDate(string data)
        //{
        //    DateTime d;
        //    return DateTime.TryParse(data.Substring(6,4)+ '-' + data.Substring(3,2) + '-' + data.Substring(0,2), out d)
        //}

        //public static AdminConfig GetConfigs()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    AdminConfig configs = db.AdminConfigs.Where(cid => cid.AdminConfigId == 1).FirstOrDefault();
        //    db = null;
        //    return configs;
        //}
        public static Boolean IsUserInRole(string id, string role)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                string query = "Select r.name from aspnetusers u " +
                               "inner join AspNetUserRoles ur on ur.UserId = u.id " +
                               "inner join AspNetroles r on r.Id = ur.RoleId " +
                               "where u.id='" + id + "' and r.Name='" + role + "'";

                IEnumerable<RoleViewModel> x = context.Database.SqlQuery<RoleViewModel>(query);
                if (x.ToList().Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException exc)
            {
                var msg = exc.Message;
                return false;
            }
        }
    
    }


    //public class Bootstrap
    //{


    //    public static string Bundle()
    //    {
    //        MetaSoft.Models.ApplicationDbContext db = new MetaSoft.Models.ApplicationDbContext();
    //        var ac = db.AdminConfigs.Find(1);  // Semepre tera um registro
    //        var tema = db.Temas.Find(ac.TemaId);
    //        return "_" + tema.Descricao.ToString().ToLower();
    //    }

    //}

    public class HtmlBuilder
    {
        public static string HTML() { return "<html><body>"; }
        public static string HTML_E() { return "</body></html>"; }
        public static string Table(int width)
        {
            return "<table cellpadding='0' cellspacing='0' bgcolor='white'>";
        }
        public static string Table_E()
        {
            return "</table>";
        }
        public static string TD(string text)
        {
            return "<td font size='3' color='blue'>" + text + "</td>";
        }
        public static string TR(string sColor)
        {
            return "<tr style='color:" + sColor + "'>";
        }
        public static string TR_E()
        {
            return "</tr>";
        }
        public static string TD_E()
        {
            return "</td>";
        }
        public static string P()
        {
            return "<p></p>";
        }
    }

    public class BinaryContentResult : ActionResult
    {
        private string ContentType;
        private byte[] ContentBytes;

        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            this.ContentBytes = contentBytes;
            this.ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = this.ContentType;

            var stream = new MemoryStream(this.ContentBytes);
            stream.WriteTo(response.OutputStream);
            stream.Dispose();
        }
    }

    public class PDFHelper
    {
        /*--------------------------------------------------------
          Exportar um HTML fornecido.
	      - O HTML.
	      - Nome do Arquivo.
          - Link para o CSS.
        ----------------------------------------------------------*/


        public static void Export(string html, string fileName, string linkCss)
        {
            ////reset response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";

            ////define pdf filename
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);




            //Gera o arquivo PDF
            using (var document = new Document(PageSize.A4, 40, 40, 40, 40))
            {
                //html = FormatImageLinks(html);

                //define o  output do  HTML
                var memStream = new MemoryStream();
                TextReader xmlString = new StringReader(html);

                PdfWriter writer = PdfWriter.GetInstance(document, memStream);
                writer.PageEvent = new PDFWriteEvents();

                document.Open();


                //Registra todas as fontes no computador cliente.
                FontFactory.RegisterDirectories();

                // Set factories
                var htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                // Set css
                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.AddCssFile(HttpContext.Current.Server.MapPath(linkCss), true);

                // Exporta
                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                var worker = new XMLWorker(pipeline, true);
                var xmlParse = new XMLParser(true, worker);
                xmlParse.Parse(xmlString);
                xmlParse.Flush();

                document.Close();
                document.Dispose();

                HttpContext.Current.Response.BinaryWrite(memStream.ToArray());
            }

            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }



    }

    public class PDFWriteEvents : PdfPageEventHelper
    {
        public object RunDateFont { get; private set; }
        PdfTemplate template;
        BaseFont bf = null;
        PdfContentByte cb;
        DateTime PrintTime = DateTime.Now;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException exc)
            {
                var msg = exc.Message;
            }
            catch (System.IO.IOException iexc)
            {
                var msg =iexc.Message;
            }
        }


        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            String imageFilePath1 = HttpContext.Current.Server.MapPath("/img/NfHeader.png");
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath1);
            jpg.ScaleToFit(480F, 60F);
            jpg.Alignment = Element.ALIGN_LEFT;

            document.Add(jpg);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {

            base.OnEndPage(writer, document);
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = "Pag. " + pageN + " de ";
            float len = bf.GetWidthPoint(text, 8);
            Rectangle pageSize = document.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Impresso " + PrintTime.ToString() + "                ", pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            cb.EndText();



        }
    }




}


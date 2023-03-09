using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public class ExcelHelper
    {
        public static void PublishHtmlWord(string filename_WithoutExtension, string html)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename_WithoutExtension + ".doc");
            Response.AddHeader("Content-Length", html.Length.ToString());
            Response.Write(html);
            Response.End();
        }

        public static void PublishHtmlExcel(string filename_WithoutExtension, string html)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename_WithoutExtension + ".xls");
            Response.AddHeader("Content-Length", html.Length.ToString());
            Response.Write(html);
            Response.End();
        }

    }
}

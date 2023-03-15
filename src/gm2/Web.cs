using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;

namespace System.gm
{
    public class Web
    {
        public static void AutoRoute(string folder)
        {
            AutoRoute(folder, true);
        }

        public static void AutoRoute(string folder, bool includeSubFolder)
        {
            string rootFolder = HttpContext.Current.Server.MapPath("~/");

            if (folder.StartsWith("~/"))
            { }
            else if (folder.StartsWith("/"))
            {
                folder = "~" + folder;
            }
            else
            {
                folder = "~/" + folder;
            }

            folder = HttpContext.Current.Server.MapPath(folder);

            MapPageRoute(folder, rootFolder, includeSubFolder);
        }

        static void MapPageRoute(string folder, string rootFolder, bool includeSubFolder)
        {
            if (includeSubFolder)
            {
                // obtain sub-folders
                string[] folders = Directory.GetDirectories(folder);

                foreach (var subFolder in folders)
                {
                    MapPageRoute(subFolder, rootFolder, includeSubFolder);
                }
            }

            string[] files = Directory.GetFiles(folder);

            foreach (var file in files)
            {
                // not a page, skip action
                if (!file.EndsWith(".aspx"))
                    continue;

                string webPath = file.Replace(rootFolder, "~/").Replace("\\", "/");

                var filename = Path.GetFileNameWithoutExtension(file);

                if (filename.ToLower() == "default")
                {
                    continue;
                }

                RouteTable.Routes.MapPageRoute(filename, filename, webPath);
            }
        }

        public static string GeneratePaginationHtml(string className, int totalRows, int totalRowsPerPage, int totalSlots, int curPage, string baseUrl)
        {
            if (totalRows == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            int totalPages = totalRows / totalRowsPerPage;

            if (totalRows % totalRowsPerPage > 0)
            {
                totalPages++;
            }

            if (totalPages < 2)
            {
                return "";
            }

            int pagerStartIdx = curPage - (totalSlots / 2);

            if (pagerStartIdx < 1)
                pagerStartIdx = 1;

            sb.Clear();

            sb.AppendLine($"<div class='{className}'>");

            if (pagerStartIdx != 1)
            {
                sb.AppendLine($"<a href='{baseUrl}&page=1'>First</a> ... &nbsp; ");
            }

            int totalSlotAdded = 0;
            int lastpageslot = 0;

            for (int i = pagerStartIdx; i <= totalPages; i++)
            {
                totalSlotAdded++;

                if (totalSlotAdded > totalSlots)
                {
                    break;
                }

                lastpageslot = i;

                if (i == curPage)
                {
                    sb.AppendLine($"<a class='active' href='{baseUrl}&page={i}'>{i}</a>");
                }
                else
                {
                    sb.AppendLine($"<a href='{baseUrl}&page={i}'>{i}</a>");
                }
            }

            if (lastpageslot != totalPages)
            {
                sb.AppendLine($" ... &nbsp; <a href='{baseUrl}&page={totalPages}'>Last</a>");
            }

            sb.Append("</div>");

            return sb.ToString();
        }
    }
}

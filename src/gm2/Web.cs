﻿using System;
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

            MapPageRoute(folder, rootFolder);
        }

        static void MapPageRoute(string folder, string rootFolder)
        {
            // obtain sub-folders
            string[] folders = Directory.GetDirectories(folder);

            foreach (var subFolder in folders)
            {
                MapPageRoute(subFolder, rootFolder);
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
    }
}

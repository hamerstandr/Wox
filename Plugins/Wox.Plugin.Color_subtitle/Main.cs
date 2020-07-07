using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;

namespace Wox.Plugin.Color_subtitle
{
    public sealed class Color_subtitlePlugin : IPlugin, IPluginI18n
    {
        private string DIR_PATH = Path.Combine(Path.GetTempPath(), @"Plugins\Color_subtitle\");
        private PluginInitContext context;
        private const int IMG_SIZE = 32;

        private DirectoryInfo Color_subtitleDirectory { get; set; }

        public Color_subtitlePlugin()
        {
            if (!Directory.Exists(DIR_PATH))
            {
                Color_subtitleDirectory = Directory.CreateDirectory(DIR_PATH);
            }
            else
            {
                Color_subtitleDirectory = new DirectoryInfo(DIR_PATH);
            }
        }

        public List<Result> Query(Query query)
        {
            var raw = query.Search;
            if (!IsAvailable(raw)) return new List<Result>(0);
            try
            {
                var cached = Find(raw);
                if (cached.Length == 0)
                {
                    var path = CreateImage(raw);
                    return new List<Result>
                    {
                        new Result
                        {
                            Title = raw,
                            IcoPath = path,
                            Action = _ =>
                            {
                                Clipboard.SetText(raw);
                                return true;
                            }
                        }
                    };
                }
                return cached.Select(x => new Result
                {
                    Title = raw,
                    IcoPath = x.FullName,
                    Action = _ =>
                    {
                        Clipboard.SetText(raw);
                        return true;
                    }
                }).ToList();
            }
            catch (Exception x)
            {
                // todo: log
                var s = x.Message;
                return new List<Result>(0);
            }
        }

        private bool IsAvailable(string query)
        {
            // todo: rgb, names
            var length = query.Length - 2; // minus `&` sign
            return query.StartsWith("&") && (length == 3 || length == 6 || length == 8);
        }

        public FileInfo[] Find(string name)
        {
            var file = string.Format("{0}.png", name.Substring(1));
            return Color_subtitleDirectory.GetFiles(file, SearchOption.TopDirectoryOnly);
        }
        public string Reverse(string s)
        {
            return ""+s[6]+ s[7]+ s[4]+s[5] + s[0] + s[1];
        }
        private string CreateImage(string name)
        {
            using (var bitmap = new Bitmap(IMG_SIZE, IMG_SIZE))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                name = name.ToUpper().Replace("&H", "");
                var path = CreateFileName(name);
                name = "#" + Reverse(name);
                var color = ColorTranslator.FromHtml(name);
                graphics.Clear(color);

                
                bitmap.Save(path, ImageFormat.Png);
                return path;
            }
        }

        private string CreateFileName(string name)
        {
            return string.Format("{0}{1}.png", Color_subtitleDirectory.FullName, name.Substring(1));
        }

        public void Init(PluginInitContext context)
        {
            this.context = context;
        }


        public string GetTranslatedPluginTitle()
        {
            return context.API.GetTranslation("wox_plugin_color_plugin_name");
        }

        public string GetTranslatedPluginDescription()
        {
            return context.API.GetTranslation("wox_plugin_color_plugin_description");
        }
    }
}
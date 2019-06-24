using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wox.Plugin;

namespace Colorpicker
{
    class Main : IPlugin
    {
        readonly string Location = typeof(Main).Assembly.Location;
        string Directory ="";
        public List<Result> Query(Query query)
        {
            
            var result = new Result
            {
                Title = "Color Picker",
                SubTitle = $"Query: {query.Search}",
                IcoPath = Directory+"\\Images\\app.png",
                Action = _ =>
                {
                    Process.Start(Directory + "\\DesktopColorPicker.exe");
                    return true;
                }
            };
            return new List<Result> { result };
        }

        public void Init(PluginInitContext context)
        {
            Directory = Path.GetDirectoryName(Location);
        }
    }
}

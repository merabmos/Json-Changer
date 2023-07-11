using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp3
{

    public class MacroParams
    {
    }

    public class MacroParamsBlogs : MacroParams
    {
        public string image { get; set; }
        public string cropHeightInPixels { get; set; }
        public string crop { get; set; }
        public string headLine { get; set; }
        public string topHeadline { get; set; }
        public string leftHeadline { get; set; }
        public string headLineColor { get; set; }
        public string caption { get; set; }
        public string ctaLink { get; set; }

    }

    public class Value
    {
        public string macroAlias { get; set; }
        public MacroParams macroParamsDictionary { get; set; }
    }


    public class Editor 
    {
        public string Alias { get; set; }
        public string View { get; set; }
    }

    public class MacroJsonModel
    {
        public Value value { get; set; } 
        public Editor editor { get; set; } 
        public object styles { get; set; }
        public object config { get; set; }

        public static string CreateMacroJson(MacroParamsBlogs macroParams)
        {

            MacroJsonModel rootObject = new MacroJsonModel()
            {
                value = new Value()
                {
                    macroAlias = "GWBlogsWidget",
                    macroParamsDictionary = macroParams ?? null
                },
                editor = new Editor()
                {
                    Alias = "macro",
                    View = "macro"
                },
                styles = null,
                config = null
            };

            return JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        }
    }
}
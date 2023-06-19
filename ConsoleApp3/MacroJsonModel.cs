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
        public List<CtaLink> CtaLink { get; set; }
        public string CssButton { get; set; }
    }

    public class CtaLink
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Published { get; set; }
        public string Target { get; set; }
    }

    public class Value
    {
        public string MacroAlias { get; set; }
        public MacroParams MacroParamsDictionary { get; set; }
    }

    public class Editor
    {
        public string Alias { get; set; }
        public string View { get; set; }
    }

    public class MacroJsonModel
    {
        public Value Value { get; set; }
        public Editor Editor { get; set; }
        public object Styles { get; set; }
        public object Config { get; set; }

        public static string CreateMacroJson(string name, string url, string target)
        {
            MacroJsonModel rootObject = new MacroJsonModel()
            {
                Value = new Value()
                {
                    MacroAlias = "GWCTAButton",
                    MacroParamsDictionary = new MacroParams()
                    {
                        CtaLink = new List<CtaLink>(){
                    new CtaLink()
                    {
                        Name = name,
                        Url = url,
                        Published = true,
                        Icon = "icon_link",
                        Target = target
                    }
                },
                        CssButton = ""
                    }
                },
                Editor = new Editor()
                {
                    Alias = "macro",
                    View = "macro"
                },

                Config = null,
                Styles = null
            };

            return JsonConvert.SerializeObject(rootObject,Formatting.Indented);
        }
    }
}
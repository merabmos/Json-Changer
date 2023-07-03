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
        public string ctaLink { get; set; }
        public string cssButton { get; set; }
    }

    public class CtaLink
    {
        public string name { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public bool published { get; set; }
        public string target { get; set; }
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

        public static string CreateMacroJson(string name, string url, string target, string cssButton)
        {
            var ctaLinkArray = new List<CtaLink>(){
                   new CtaLink()
                    {
                        name = name,
                        url = url,
                        published = true,
                        icon = "icon_link",
                        target = target
                }
            };

            MacroJsonModel rootObject = new MacroJsonModel()
            {
                value = new Value()
                {
                    macroAlias = "GWCTAButton",
                    macroParamsDictionary = new MacroParams()
                    {
                        ctaLink = JsonConvert.SerializeObject(ctaLinkArray),
                        cssButton = cssButton
                    }
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
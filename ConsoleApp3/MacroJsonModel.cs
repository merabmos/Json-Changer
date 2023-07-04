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
        public string displayTo { get; set; }
        public string numberOfItems { get; set; }
        public string headline { get; set; }
        public string theme { get; set; }
        public string tagsBoKTerms { get; set; }
        public string tagsContentLevel { get; set; }
        public string tagsContentType { get; set; }
        public string tagsRegion { get; set; }
        public string tagsSector { get; set; }
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

        public static string CreateMacroJson(MacroParams macroParams)
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
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
        public string buttonText { get; set; }
        public string form { get; set; }
        public string formHeadline { get; set; }
    }

    public class Value
    {
        public string macroAlias { get; set; }
        public MacroParams macroParamsDictionary { get; set; }
    }


    public class Editor 
    {
        public string alias { get; set; }
        public string view { get; set; }
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
                    macroAlias = "GWCollapsedForm",
                    macroParamsDictionary = macroParams ?? null
                },
                editor = new Editor()
                {
                    alias = "macro",
                    view = "macro"
                },
                styles = null,
                config = null
            };

            return JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        }
    }
}
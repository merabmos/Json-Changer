using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp3
{

    public class Editornotes
    {
        public string label { get; set; }
        public string view { get; set; }
        public Config config { get; set; }
        public string alias { get; set; }
        public string value { get; set; }
    }

    public class Config
    {
        public Editor2 editor { get; set; }
    }

    public class Editor2
    {
        public List<string> toolbar { get; set; }
        public List<object> stylesheets { get; set; }
        public List<object> dimensions { get; set; }
        public string mode { get; set; }
    }
    public class Tab
    {
        public string tabHeader { get; set; }
        public Editornotes editornotes { get; set; }
        public string tabContent { get; set; }

    }


    public class Tab1
    {
        public List<Tab> tabs { get; set; }
    }

    public class MacroParams
    {
        public string tabs { get; set; }
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

        public static string CreateMacroJson(WidgetJsonModel root)
        {
            MacroParams macroParams = new MacroParams() { };

            List<string> wordsList = new List<string>
{
    "ace",
    "undo",
    "redo",
    "cut",
    "styleselect",
    "bold",
    "italic",
    "alignleft",
    "aligncenter",
    "alignright",
    "bullist",
    "numlist",
    "link",
    "umbmediapicker",
    "umbmacro",
    "umbembeddialog"
};

            Tab1 tab1 = new Tab1() { tabs = new List<Tab>()};

            foreach (var item in root.value)
            {

                tab1.tabs.Add(new Tab()
                {
                    tabHeader = item.tabHeader.value,
                    tabContent = item.tabContent.value,
                    editornotes = new Editornotes()
                    {
                        view = "/umbraco/views/propertyeditors/rte/rte.html",
                        value = item.tabContent.value,
                        alias = item.tabHeader.editorAlias,
                        label = "",
                        config = new Config()
                        {
                            editor = new Editor2()
                            {
                                toolbar = wordsList,
                                stylesheets = new List<object>(),
                                dimensions = new List<object>(),
                                mode = "classic"
                            }
                        }
                    }
                });
            }

            macroParams.tabs = JsonConvert.SerializeObject(tab1);
            
            MacroJsonModel rootObject = new MacroJsonModel()
            {
                value = new Value()
                {
                    macroAlias = "GWTabs",
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
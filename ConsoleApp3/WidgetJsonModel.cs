using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3 { 
public class TabsHeader
{
    public string value { get; set; }
    public string dataTypeGuid { get; set; }
    public string editorAlias { get; set; }
    public string editorName { get; set; }
}

public class TabContent
{
    public string value { get; set; }
    public string dataTypeGuid { get; set; }
    public string editorAlias { get; set; }
    public string editorName { get; set; }
}
    public class Tabs
    {
        public TabsHeader tabHeader { get; set; }
        public TabContent tabContent { get; set; }
    }

    public class WidgetEditor
    {
        public string alias { get; set; }
    }
    public class WidgetJsonModel
    {
        public List<Tabs> value { get; set; }
        public WidgetEditor editor { get; set; }
        public static WidgetJsonModel CreateWidgetModel(string json)
        {
            WidgetJsonModel rootObject = JsonConvert.DeserializeObject<WidgetJsonModel>(json);
            
            return rootObject;
        }

        public bool active { get; set; }
        public string guid { get; set; }
    }


}

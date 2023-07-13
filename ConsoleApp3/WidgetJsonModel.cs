using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Data
    {
        public string value { get; set; }
        public string dataTypeGuid { get; set; }
        public string editorAlias { get; set; }
        public string editorName { get; set; }
    }
    public class ValueItem
    {
        public Data buttonText { get; set; }
        public Data form { get; set; }
        public Data formHeadline { get; set; }
    }

    public class WidgetEditor
    {
        public string alias { get; set; }
        public object view { get; set; }
    }
    public class WidgetJsonModel
    {
        public List<ValueItem> value { get; set; }
        public WidgetEditor editor { get; set; }
        public object styles { get; set; }
        public object config { get; set; }
        public static WidgetJsonModel CreateWidgetModel(string json)
        {
            WidgetJsonModel rootObject = JsonConvert.DeserializeObject<WidgetJsonModel>(json);
            
            return rootObject;
        }

        public bool active { get; set; }
        public string guid { get; set; }
    }


}

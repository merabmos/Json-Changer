using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class CtaLinkValue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string Hashtarget { get; set; }
    }

    public class WidgetCtaLink
    {
        public CtaLinkValue Value { get; set; }
        public string DataTypeGuid { get; set; }
        public string EditorAlias { get; set; }
        public string EditorName { get; set; }
    }

    public class CssButtonValue
    {
        public object Value { get; set; }
        public string DataTypeGuid { get; set; }
        public string EditorAlias { get; set; }
        public string EditorName { get; set; }
    }

    public class ValueItem
    {
        public WidgetCtaLink CtaLink { get; set; }
        public CssButtonValue CssButton { get; set; }
    }

    public class WidgetJsonModel
    {
        public List<ValueItem> Value { get; set; }
        public static WidgetJsonModel CreateWidgetModel(string json)
        {
            WidgetJsonModel rootObject = JsonConvert.DeserializeObject<WidgetJsonModel>(json);
            
            return rootObject;
        }
    }


}

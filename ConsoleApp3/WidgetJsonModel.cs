using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class ImageData
    {
        public string value { get; set; }
        public string dataTypeGuid { get; set; }
        public string editorAlias { get; set; }
        public string editorName { get; set; }
    }

    public class CTAImageData
    {
        public CtaLinkData value { get; set; }
        public string dataTypeGuid { get; set; }
        public string editorAlias { get; set; }
        public string editorName { get; set; }
    }
    public class ValueItem
    {
        public ImageData image { get; set; }
        public ImageData crop { get; set; }
        public ImageData cropHeightInPixels { get; set; }
        public ImageData headline { get; set; }
        public ImageData topHeadline { get; set; }
        public ImageData leftHeadline { get; set; }
        public ImageData headLineColor { get; set; }
        public ImageData caption { get; set; }
        public CTAImageData ctaLink { get; set; }
    }

    public class CtaLinkData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string target { get; set; }
        public string hashtarget { get; set; }
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

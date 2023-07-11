using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class TagsData
    {
        public string value { get; set; }
        public string dataTypeGuid { get; set; }
        public string editorAlias { get; set; }
        public string editorName { get; set; }
    }

    public class ValueItem
    {
        public TagsData headline { get; set; }
        public TagsData displayTo { get; set; }
        public TagsData numberOfItems { get; set; }
        public TagsData theme { get; set; }
        public TagsData tagsAPMTerms { get; set; }
        public TagsData tagsBoKTerms { get; set; }
        public TagsData tagsBranches { get; set; }
        public TagsData tagsContentLevel { get; set; }
        public TagsData tagsContentType { get; set; }
        public TagsData tagsMajorProjects { get; set; }
        public TagsData tagsNonApmProductsResources { get; set; }
        public TagsData tagsQualifications { get; set; }
        public TagsData tagsRegion { get; set; }
        public TagsData tagsSector { get; set; }
        public TagsData tagsSpecificInterestGroups { get; set; }
    }
    public class BlogsBox
    {
        public string alias { get; set; }
        public object view { get; set; }
    }
    public class WidgetJsonModel
    {
        public List<ValueItem> value { get; set; }
        public BlogsBox editor { get; set; }
        public object styles { get; set; }
        public object config { get; set; }
        public static WidgetJsonModel CreateWidgetModel(string json)
        {
            WidgetJsonModel rootObject = JsonConvert.DeserializeObject<WidgetJsonModel>(json);
            
            return rootObject;
        }
    }


}

using ConsoleApp3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Numerics;

string desktopPath = "C:\\Users\\family\\Desktop\\Work Projects\\English Projects\\Projects Folders\\CIHT 11";
string filePath = desktopPath + "\\changeable.json";
try
{

    string json = File.ReadAllText(filePath);

    JObject obj = JObject.Parse(json);

    var blogsBoxs = obj.Descendants().OfType<JObject>()
            .Where(t => t["editor"] != null && t["editor"]["alias"] != null && t["editor"]["alias"].ToString() == "blogsWidget").ToList();

    foreach (var blogsBox in blogsBoxs)
    {
        if (blogsBox != null)
        {
            JToken parentNode = blogsBox;
            var root = WidgetJsonModel.CreateWidgetModel(JsonConvert.SerializeObject(parentNode, Formatting.Indented));
            MacroParamsBlogs macroParams = new MacroParamsBlogs()
            {
                headline = root.value[0].headline.value,
                tagsSector = root.value[0].tagsSector.value,
                tagsBoKTerms = root.value[0].tagsBoKTerms.value,
                tagsContentLevel = root.value[0].tagsContentLevel.value,
                tagsContentType = root.value[0].tagsContentType.value,
                tagsRegion = root.value[0].tagsRegion.value,
                displayTo = root.value[0].displayTo.value,
                numberOfItems = root.value[0].displayTo.value ,
                theme = root.value[0].theme.value
            };

            var newJson = MacroJsonModel.CreateMacroJson(macroParams);

            JToken newValue = JToken.Parse(newJson);

            parentNode.Replace(newValue);
        }
    }

    File.WriteAllText(filePath, obj.ToString());
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}

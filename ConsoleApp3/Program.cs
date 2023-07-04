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
            .Where(t => t["editor"] != null && t["editor"]["alias"] != null && t["editor"]["alias"].ToString() == "blogsBox").ToList();

    foreach (var blogsBox in blogsBoxs)
    {
        if (blogsBox != null)
        {
            JToken parentNode = blogsBox.Parent.Parent.Parent;
            var root = WidgetJsonModel.CreateWidgetModel(JsonConvert.SerializeObject(parentNode, Formatting.Indented));
            MacroParams macroParams = root.value != null ? new MacroParams()
            {
                headline = root.value[0].headline.value,
                tagsSector = root.value[0].tagsSector.value,
                tagsBoKTerms = root.value[0].tagsBoKTerms.value,
                tagsContentLevel = root.value[0].tagsContentLevel.value,
                tagsContentType = root.value[0].tagsContentType.value,
                tagsRegion = root.value[0].tagsRegion.value,
            } : null;

            var newJson = MacroJsonModel.CreateMacroJson(macroParams);

            JToken newValue = JToken.Parse(newJson);

            parentNode.Replace(newValue);
        }
    }

    File.WriteAllText(filePath, obj.ToString());

    string modifiedJson = obj.ToString();
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}

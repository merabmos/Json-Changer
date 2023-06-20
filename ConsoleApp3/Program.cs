using ConsoleApp3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Numerics;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string filePath = desktopPath + "\\New.json";
try
{

    string json = File.ReadAllText(filePath);

    JObject obj = JObject.Parse(json);

    var ctaLinks = obj.Descendants()
            .Where(t => t.Type == JTokenType.Object && t["ctaLink"] != null).ToList();

    foreach (var ctaLink in ctaLinks)
    {
        if (ctaLink != null)
        {
            JToken parentNode = ctaLink.Parent.Parent.Parent;
            var root = WidgetJsonModel.CreateWidgetModel(JsonConvert.SerializeObject(parentNode, Formatting.Indented));

            var newJson = MacroJsonModel.CreateMacroJson(
       root.Value[0].CtaLink.Value.Name,
       root.Value[0].CtaLink.Value.Url,
       root.Value[0].CtaLink.Value.Target);

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

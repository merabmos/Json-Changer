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
            .Where(t => t["editor"] != null && t["editor"]["alias"] != null && t["editor"]["alias"].ToString() == "tabs").ToList();

    foreach (var blogsBox in blogsBoxs)
    {
        if (blogsBox != null)
        {
            JToken parentNode = blogsBox;
            var root = WidgetJsonModel.CreateWidgetModel(JsonConvert.SerializeObject(parentNode, Formatting.Indented));

          
            var newJson = MacroJsonModel.CreateMacroJson(root);

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

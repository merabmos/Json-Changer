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
            .Where(t => t["editor"] != null && t["editor"]["alias"] != null && t["editor"]["alias"].ToString() == "ctaBox").ToList();

    foreach (var blogsBox in blogsBoxs)
    {
        if (blogsBox != null)
        {
            JToken parentNode = blogsBox;
            var root = WidgetJsonModel.CreateWidgetModel(JsonConvert.SerializeObject(parentNode, Formatting.Indented));
            List<CtaLinkData> ctaLinkDatas = new List<CtaLinkData>()
            {
                new CtaLinkData() {
                    name = root.value[0].ctaLink.value.name,
                    hashtarget = root.value[0].ctaLink.value.hashtarget,
                    id = root.value[0].ctaLink.value.id,
                    target = root.value[0].ctaLink.value.target,
                    url = root.value[0].ctaLink.value.url
                }
            };
            MacroParamsBlogs macroParams = new MacroParamsBlogs()
            {
                caption = root.value[0].caption.value,
                crop = root.value[0].crop?.value ?? "0",
                cropHeightInPixels = root.value[0].cropHeightInPixels.value,
                ctaLink = JsonConvert.SerializeObject(ctaLinkDatas),
                headLine = root.value[0].headline.value,
                headLineColor = root.value[0].headLineColor.value,
                image = root.value[0].image.value,
                leftHeadline = root.value[0].leftHeadline.value,
                topHeadline = root.value[0].topHeadline.value
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

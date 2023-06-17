using ConsoleApp3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string filePath = desktopPath + "\\New.json";

try
{
    string json = File.ReadAllText(filePath);

    List<string> jsons = new List<string>();
    // Deserialize the JSON into a dynamic object
    dynamic jsonObject = JsonConvert.DeserializeObject(json);

    // Access the sections array
    dynamic sections = jsonObject.sections;

    // Iterate over the sections
    foreach (dynamic section in sections)
    {
        // Access the rows array
        dynamic rows = section.rows;

        // Iterate over the rows
        foreach (dynamic row in rows)
        {
            // Access the areas array
            dynamic areas = row.areas;

            // Iterate over the areas
            foreach (dynamic area in areas)
            {
                // Access the controls array
                dynamic controls = area.controls;

                // Iterate over the controls
                foreach (dynamic control in controls)
                {
                    // Check if the control has a "value" property
                    JToken valueToken = JToken.FromObject(control.value);

                    // Recursive method to search for the desired blocks
                    SearchForBlocks(valueToken, jsons);
                }
            }
        }
    }

    foreach (var item in jsons)
    {
        var root = WidgetJsonModel.CreateWidgetModel(item);
/*        
        MacroJsonModel.CreateMacroJson(
            root.Value[0].CtaLink.Value.Name,
            root.Value[0].CtaLink.Value.Url,
            root.Value[0].CtaLink.Value.Target);*/

        Console.WriteLine(item);
    }
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}


static void SearchForBlocks(JToken token, List<string> jsons)
{
    if (token.Type == JTokenType.Object)
    {
        foreach (JProperty property in token.Children<JProperty>())
        {
            // Check if the property name is "editorAlias" and the value is "ctaLink"
            if (property.Name == "editorAlias" && property.Value.ToString() == "ctaLink")
            {
                // Convert the control to JSON string
                string serializedJson = JsonConvert.SerializeObject(token.Parent.Parent.Parent, Formatting.Indented);
                jsons.Add("{\"value\":" + serializedJson + ",\"editor\":{\"alias\":\"ctaButton\",\"view\":null},\"styles\":null,\"config\":null" + "}");
                break;
            }

            // Recursive call to search within child tokens
            SearchForBlocks(property.Value, jsons);
        }
    }
    else if (token.Type == JTokenType.Array)
    {
        foreach (JToken child in token.Children())
        {
            // Recursive call to search within child tokens
            SearchForBlocks(child, jsons);
        }
    }
}

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


    List<string> jsons = new List<string>();
    // Deserialize the JSON into a dynamic object
    dynamic jsonObject = JsonConvert.DeserializeObject(json);

    json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

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

        var newJson = MacroJsonModel.CreateMacroJson(
            root.Value[0].CtaLink.Value.Name,
            root.Value[0].CtaLink.Value.Url,
            root.Value[0].CtaLink.Value.Target);

        var jsone = ModifyJson(root.Value[0].CtaLink.Value.Name,
            root.Value[0].CtaLink.Value.Url,
            root.Value[0].CtaLink.Value.Target);

       var s =  json.Contains(jsone);
        /*        File.WriteAllText(filePath, replacedJson);
        */
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

                var json = "{\"value\":" + serializedJson + ",\"editor\":{\"alias\":\"ctaButton\",\"view\":null},\"styles\":null,\"config\":null" + "}";

                jsons.Add(json);
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



static string ModifyJson(string newName, string newUrl, string newTarget)

{
    string jsonString = "{\r\n                  \"value\": [\r\n                    {\r\n                      \"ctaLink\": {\r\n                        \"value\": {\r\n                          \"id\": 0,\r\n                          \"name\": \">>>Accessnow\",\r\n                          \"url\": \"https://www.cihtlearn.org.uk/\",\r\n                          \"target\": \"_self\",\r\n                          \"hashtarget\": \"\"\r\n                        },\r\n                        \"dataTypeGuid\": \"ed2bed97-05d2-46c3-b265-6d5efb8ef0e5\",\r\n                        \"editorAlias\": \"ctaLink\",\r\n                        \"editorName\": \"CTALink\"\r\n                      },\r\n                      \"cssButton\": {\r\n                        \"value\": null,\r\n                        \"dataTypeGuid\": \"0cc0eba1-9960-42c9-bf9b-60e150b429ae\",\r\n                        \"editorAlias\": \"cssButton\",\r\n                        \"editorName\": \"CssButton\"\r\n                      }\r\n                    }\r\n                  ],\r\n                  \"editor\": {\r\n                    \"alias\": \"ctaButton\",\r\n                    \"view\": null\r\n                  },\r\n                  \"styles\": null,\r\n                  \"config\": null\r\n                }";

    int nameIndex = jsonString.IndexOf("\"name\":");
    int urlIndex = jsonString.IndexOf("\"url\":");
    int targetIndex = jsonString.IndexOf("\"target\":");

    int nameValueStartIndex = jsonString.IndexOf("\"", nameIndex + 8) + 1;
    int nameValueEndIndex = jsonString.IndexOf("\"", nameValueStartIndex);
    int urlValueStartIndex = jsonString.IndexOf("\"", urlIndex + 7) + 1;
    int urlValueEndIndex = jsonString.IndexOf("\"", urlValueStartIndex);
    int targetValueStartIndex = jsonString.IndexOf("\"", targetIndex + 10) + 1;
    int targetValueEndIndex = jsonString.IndexOf("\"", targetValueStartIndex);

    string modifiedJsonString = jsonString.Substring(0, nameValueStartIndex) + newName +
     jsonString.Substring(nameValueEndIndex, urlValueStartIndex - nameValueEndIndex) + newUrl +
     jsonString.Substring(urlValueEndIndex, targetValueStartIndex - urlValueEndIndex) + newTarget +
     jsonString.Substring(targetValueEndIndex);

    return modifiedJsonString;
}
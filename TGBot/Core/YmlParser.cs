using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TGBot.Core;

public class YmlParser
{
    private static IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

    private string path;
    private string content;
    public Dictionary<string, dynamic> fileDict { get; set; }

    public YmlParser(string fileName)
    {
         path = Directory.GetCurrentDirectory() + $@"\Resources\{fileName}";
         
         Parse();
    }

    public void Parse()
    {
        content = File.ReadAllText(path);
        fileDict = deserializer.Deserialize<Dictionary<string, dynamic>>(content);
    }

    public string[] GetKeys()
    {
        return fileDict.Keys.ToArray();
    }
    
    public string Get(string path)
    {
        string[] keys = path.Split(".");

        dynamic current = fileDict;

        foreach (var key in keys)
        {
            if (current is Dictionary<object, object> dict && dict.ContainsKey(key))
            {
                current = dict[key];
            }
            else if (current is Dictionary<string, object> strDict && strDict.ContainsKey(key))
            {
                current = strDict[key];
            }
            else
            {
                return null;
            }
        }
        
        return current?.ToString();
    }
}
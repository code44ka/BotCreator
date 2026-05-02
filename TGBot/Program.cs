using TGBot.Core;
using TGBot.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class Program
{
    private static IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
    
    private static YmlParser MainConfigParser;
    
    private static string token;
    private static BotClient botClient;
    
    
    private static void Main()
    {
        ParseAllConfigs();
        
        botClient = new BotClient(token);
        ActionsManager.AddActions();
        
        Console.ReadLine();
    }

    private static void ParseAllConfigs()
    {
        ParseMainConfig();
    }
    
    private static void ParseMainConfig()
    {
        MainConfigParser = new YmlParser("config.yml");
        
        ParseToken();
    }

    private static void ParseToken() => token = MainConfigParser.Get("token");
    
    //ЕБАТЬ КОПАТЬ ЧЁ Я СДЕЛАЛ
}
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Logging;

public class Config
{
    public static ILogger logger {get;set;}
    public static JsonSerializerOptions JsonOptions {get; set;}
    public static TextEncoderSettings encoderSettings {get; set;} = new TextEncoderSettings();
    public static string UserDirectory {get; set;} = "Users";
    public static string DownloadDirectory {get; set; } = "";


    public static void CreateLogger(LogLevel level)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(level));
        
        logger = factory.CreateLogger("Program");

        logger.LogInformation("Logging is {Description}.", "enabled");
    }
}
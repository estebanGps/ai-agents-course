using System.Text.Json.Serialization;

namespace MyAiAgent.Console.Plugins;

public class Light
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("is_on")]
    public bool IsOn { get; set; }
}
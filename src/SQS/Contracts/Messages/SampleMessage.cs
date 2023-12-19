using System.Text.Json.Serialization;

namespace Contracts.Messages;

public class SampleMessage : IMessage
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    
    [JsonIgnore]
    public string MessageTypeName => nameof(SampleMessage);
}
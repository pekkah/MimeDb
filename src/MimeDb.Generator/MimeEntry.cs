using System.Text.Json.Serialization;

namespace MimeDb.Generator
{
    internal class MimeEntry
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }
        
        [JsonPropertyName("extensions")]
        public string[] Extensions { get; set; }
    }
}
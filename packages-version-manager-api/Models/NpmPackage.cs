using System.Text.Json.Serialization;

namespace packages_version_manager_api.Models
{
    public record NpmPackage
    {
        [JsonPropertyName("dist-tags")]
        public Dictionary<string, string> DistTags { get; set; } = new();

        [JsonPropertyName("time")]
        public Dictionary<string, DateTime> Time { get; set; } = new();
    }
}

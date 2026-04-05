using System.Text.Json.Serialization;

namespace packages_version_manager_api.Models
{
    public record PackageJson
    {
        [JsonPropertyName("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; } = new();
    }
}

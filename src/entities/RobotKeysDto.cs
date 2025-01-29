using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace warren_analysis_desk;

public class RobotKeysDto
{
    [Key]
    [JsonIgnore]
    public int? Id { get; set; }
    public string? KeyName { get; set; }
    public List<string>? RobotKeysList { get; set; }
    [JsonIgnore]
    public ICollection<News>? News { get; set; }
}
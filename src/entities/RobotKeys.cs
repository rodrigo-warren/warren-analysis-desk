using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DocumentFormat.OpenXml.Bibliography;

namespace warren_analysis_desk;

[Table("robot_keys")]
public class RobotKeys
{
    [Key]
    public int? Id { get; set; }
    public string? KeyName { get; set; }
    public List<string>? RobotKeysList { get; set; }
    [JsonIgnore]
    public ICollection<News>? News { get; set; }

    public RobotKeys() {}

    public RobotKeys(RobotKeysDto dto)
    {
        Id = dto.Id;
        KeyName = dto.KeyName;
        RobotKeysList = dto.RobotKeysList;
        News = dto.News;
    }
}
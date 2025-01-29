using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace warren_analysis_desk;

[Index(nameof(Title), Name = "idx_title")]
public class NewsDto
{
    [Key]
    [JsonIgnore]
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? RobotName { get; set; }
    public DateTime? PublishDate { get; set; }
    public DateTime? StartExecution { get; set; }
    public DateTime? EndExecution { get; set; }
    public int RobotKeysId { get; set; }
    [JsonIgnore]
    public RobotKeys? RobotKeys { get; set; }
}
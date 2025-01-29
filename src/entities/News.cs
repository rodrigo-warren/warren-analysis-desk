using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace warren_analysis_desk;

[Index(nameof(Title), Name = "idx_title")]
public class News
{
    [Key]
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

    public News() { }

    public News(NewsDto dto)
    {
        Id = dto.Id;
        Title = dto.Title;
        Url = dto.Url;
        RobotName = dto.RobotName;
        PublishDate = dto.PublishDate;
        StartExecution = dto.StartExecution;
        EndExecution = dto.EndExecution;
        RobotKeysId = dto.RobotKeysId;
    }
}
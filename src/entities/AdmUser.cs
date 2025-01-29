using System.ComponentModel.DataAnnotations;

namespace warren_analysis_desk;

public class AdmUser
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}
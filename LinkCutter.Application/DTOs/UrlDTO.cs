using System.ComponentModel.DataAnnotations;

namespace LinkCutter.Application.DTOs;

public class UrlDTO
{
    [Required]
    public string OriginalUrl { get; set; } = string.Empty;
}

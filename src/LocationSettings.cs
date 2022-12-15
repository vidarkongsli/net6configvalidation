using System.ComponentModel.DataAnnotations;

namespace src;

public class LocationSettings
{
    [Required]
    [StringLength(19, MinimumLength = 19, ErrorMessage = "Coordinates are not valid")]
    public string? Coordinates { get; set; }
}
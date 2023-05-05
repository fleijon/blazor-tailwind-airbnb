using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Images;

public class ImageModel
{
    [Required]
    public string? Base64Content { get;set; }
}
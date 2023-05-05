using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Description;

public class DescriptionModel
{
    [Required]
    public string? Title { get;set; }
    [Required]
    public string? Description { get;set; }
}
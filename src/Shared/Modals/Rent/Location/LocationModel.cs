using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Location;

public class LocationModel
{
    [Required]
    public string? Location { get;set; }
}
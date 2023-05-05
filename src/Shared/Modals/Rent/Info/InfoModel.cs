using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Info;

public class InfoModel
{
    [Required]
    [Range(1, 9999)] // TODO: Make custom 1 or more is required
    public int Guests { get;set; }

    [Required]
    [Range(1, 9999)] // TODO: Make custom 1 or more is required
    public int Rooms { get;set; }

    public int Bathrooms { get;set; }
}
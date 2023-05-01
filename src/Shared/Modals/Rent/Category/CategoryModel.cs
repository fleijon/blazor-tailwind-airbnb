using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Category;

public class CategoryModel
{
    [Required]
    public string? Category { get;set; }
}
using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals;

public class LoginModel
{
    [Required]
    [StringLength(30, ErrorMessage = "Name too long (30 character limit).")]
    public string? Name { get;set; }

    [Required]
    [StringLength(30, ErrorMessage = "Password too long (30 character limit).")]
    public string? Password { get;set; }
}
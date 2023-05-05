namespace blazor_tailwind_airbnb.Models;

public record Reservation(DateOnly From, DateOnly To, decimal TotalPrice);
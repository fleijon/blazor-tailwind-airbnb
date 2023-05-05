namespace blazor_tailwind_airbnb.Models;

public record Country(string Code, string Name, string Region, double Latitude, double Longitude);

// TODO: Use this instead of country. Create a service for locations

//public readonly record struct LocationId();
//public readonly record struct Location(LocationId LocationId, string Code, string Name, string Region, double Latitude, double Longitude);
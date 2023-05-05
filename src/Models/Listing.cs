namespace blazor_tailwind_airbnb.Models;

public readonly record struct ListingId(Guid Value);
public readonly record struct Category(string Value);
public readonly record struct Location(string Value);
public readonly record struct Info(int Guests, int Rooms, int Bathrooms);
public readonly record struct Image(string Base64);
public readonly record struct Description(string Title, string Value);
public readonly record struct Listing(
    UserId Host,
    ListingId ListingId,
    Category Category,
    Location Location,
    Info Info,
    Image Image,
    Description Description,
    Price Price);
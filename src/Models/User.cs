namespace blazor_tailwind_airbnb.Models;

public readonly record struct User(UserId Id, string Name, string Email);
public readonly record struct UserId(Guid Id);

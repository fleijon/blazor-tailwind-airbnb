namespace blazor_tailwind_airbnb.Models;

public readonly record struct DateRangeLocal(DateOnly Start, DateOnly End)
{
    private readonly bool isValid = Start <= End ? true : throw new ArgumentException($"'{nameof(Start)}' should be before '{nameof(End)}'.");
}

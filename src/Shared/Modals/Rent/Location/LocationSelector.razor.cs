using System.Diagnostics.CodeAnalysis;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Location;

public partial class LocationSelector
{
    private ICollection<(string Id, string Name, string Region)> countries => Data.CountriesData.Countries.Select(c => (c.Key, c.Value.Name, c.Value.Region)).ToList();

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = null!;
        return true;
    }
}
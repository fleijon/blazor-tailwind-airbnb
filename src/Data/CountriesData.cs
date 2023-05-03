namespace blazor_tailwind_airbnb.Data;

public static class CountriesData
{
    public record Country(string Code, string Name, string Region, double Latitude, double Longitude);

    public static Dictionary<string, Country> Countries = new()
    {
        {"AT", new Country("AT", "Austria", "Europe", 47.516231, 14.550072)},
        {"FI", new Country("FI", "Finland", "Europe", 61.92411, 25.748151)},
        {"IN", new Country("IN", "India", "Asia", 20.593684, 78.96288)},
        {"SE", new Country("SE", "Sweden", "Europe", 60, 18)}
    };
}
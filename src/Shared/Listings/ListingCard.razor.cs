using blazor_tailwind_airbnb.Models;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Listings;

public partial class ListingCard
{
    [Inject]
    public NavigationManager NavigationManager { get;set; } = null!;
    [EditorRequired]
    [Parameter]
    public Listing Listing { get;set; }
    [Parameter]
    public Reservation? Reservation { get;set; }
    [Parameter]
    public Action<Guid>? OnAction { get;set; }
    [Parameter]
    public bool Disabled { get;set; }
    [Parameter]
    public string? ActionLabel { get;set; }
    [Parameter]
    public string? ActionId { get;set; }
    [Parameter]
    public UserId? CurrentUser { get;set; }

    private Country? location;
    private string imageSource => $"data:image/png;base64,{Listing.Image.Base64}";

    private string? reservationDate => Reservation is null ? null : FormatReservation(Reservation);
    private decimal price => Reservation is null ? Listing.Price.Value : Reservation.TotalPrice;

    private static string FormatReservation(Reservation reservation)
    {
        var start = reservation.From.ToShortDateString();
        var end = reservation.To.ToShortDateString();

        return $"{start} - {end}";
    }

    private void NavigateToListing()
    {
        NavigationManager.NavigateTo($"/listings/{Listing.ListingId.Value}");
    }

    protected override Task OnInitializedAsync()
    {
        if(Listing.Location.Value is null)
            return Task.CompletedTask;

        var countryCode = Listing.Location.Value;
        location = Data.CountriesData.Countries[countryCode];

        return Task.CompletedTask;
    }
}
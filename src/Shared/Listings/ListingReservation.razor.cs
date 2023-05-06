using blazor_tailwind_airbnb.Models;
using blazor_tailwind_airbnb.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Listings;

public partial class ListingReservation
{
    [Inject]
    public IReservations Reservations { get;set; } = null!;
    [EditorRequired]
    [Parameter]
    public Listing Listing { get;set; }
    [Parameter]
    public EventCallback<(ListingId ListingId, DateRangeLocal DateRange)> OnCreateReservation { get;set; }
    [Parameter]
    public Price? TotalPrice { get;set; }
    [Parameter]
    public bool Disabled { get;set; }
    [Parameter]
    public bool DisabledDates { get;set; }

    private DateRangeLocal? dateRange;

    private async void Submit() => await Submit_Task();

    private async Task Submit_Task()
    {
        try
        {
            if(!dateRange.HasValue)
                return;
            await OnCreateReservation.InvokeAsync((Listing.ListingId, dateRange.Value));
        }
        catch (System.Exception)
        {
            // TODO: Handle
        }
    }

    private async Task UpdatePrice()
    {
        if(!dateRange.HasValue)
            return;

        var price = await Reservations.GetTotalPrice(Listing, dateRange.Value.Start, dateRange.Value.End);
        TotalPrice = new Price(price);

        StateHasChanged();
    }
}
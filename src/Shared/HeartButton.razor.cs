using blazor_tailwind_airbnb.Models;
using blazor_tailwind_airbnb.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared;

public partial class HeartButton
{
    [Inject]
    public IWishlist Wishlist { get;set; } = null!;
    [EditorRequired]
    [Parameter]
    public ListingId Listing { get;set; }
    [Parameter]
    public UserId? CurrentUser { get;set; }

    private bool hasFavorited;

    protected override async Task OnParametersSetAsync()
    {
        await SetFavorited();
    }

    private async Task SetFavorited()
    {
        if(!CurrentUser.HasValue)
            return;

        var userFavorites = await Wishlist.GetUserWishlist(CurrentUser.Value);
        hasFavorited = userFavorites.Any(f => f == Listing);
    }

    private async Task ToggleFavorite()
    {
        if(!CurrentUser.HasValue)
            return;

        if(hasFavorited)
        {
            await Wishlist.RemoveFromWishlist(CurrentUser.Value, Listing);
            hasFavorited = false;
        }
        else
        {
            await Wishlist.AddToWishlist(CurrentUser.Value, Listing);
            hasFavorited = true;
        }
    }
}
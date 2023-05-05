using blazor_tailwind_airbnb.Services;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Category;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Description;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Images;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Info;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Location;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Price;
using blazor_tailwind_airbnb.StateContainers;
using blazor_tailwind_airbnb.Models;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent;

public partial class RentModal : IDisposable
{
    [Inject]
    public RentModalState RentModalState { get;set; } = null!;
    [Inject]
    public IListingsService ListingsService { get;set; } = null!;
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;

    protected override void OnInitialized()
    {
        RentModalState.OnChange += OnOpenStateChange;
    }

    protected override void OnParametersSet()
    {
        currentUser = AuthenticationService.CurrentUser;
    }

    private User? currentUser;

    public bool IsOpen { get;set; }

    private void OnOpenStateChange()
    {
        Reset();
        IsOpen = RentModalState.Property.Value;
        InvokeAsync(StateHasChanged);
    }

    private void Reset()
    {
        _category = new();
        _location = new();
        _info = new();
        _image = new();
        _description = new();
        _price = new();
        currentStep = Step.Category;
    }

    private CategoryModel _category = new();
    private LocationModel _location = new();
    private InfoModel _info = new();
    private ImageModel _image = new();
    private DescriptionModel _description = new();
    private PriceModel _price = new();
    //private bool isLoading = false;
    private Step currentStep = Step.Category;

    private bool isLastStep => currentStep == Step.Price;
    private bool isLoading; // TODO: Make sure buttons are input fields are disabled when loading

    private async Task OnValidSubmitCategory(CategoryModel category)
    {
        _category = category;
        await Next();
    }

    private async Task OnValidSubmitLocation(LocationModel location)
    {
        _location = location;
        await Next();
    }

    private async Task OnValidSubmitInfo(InfoModel info)
    {
        _info = info;
        await Next();
    }

    private async Task OnValidSubmitImage(ImageModel image)
    {
        _image = image;
        await Next();
    }

    private async Task OnValidSubmitDescription(DescriptionModel description)
    {
        _description = description;
        await Next();
    }

    private async Task OnValidSubmitPrice(PriceModel price)
    {
        _price = price;
        await Next();
    }

    private async Task Next()
    {
        if(currentStep == Step.Price)
        {
            await AddListing();
        }
        else
        {
            currentStep ++;
            StateHasChanged();
        }
    }

    private async Task AddListing()
    {
        if(!currentUser.HasValue)
            return;

        var category = new Models.Category(_category.Category ?? string.Empty);
        var location = new Models.Location(_location.Location ?? string.Empty);
        var info = new Models.Info(_info.Guests, _info.Rooms, _info.Bathrooms);
        var image = new Models.Image(_image.Base64Content ?? string.Empty);
        var desc = new Models.Description(_description.Title ?? string.Empty, _description.Description ?? string.Empty);
        var price = new Models.Price(_price.PriceDecimal);
        var id = new ListingId(Guid.NewGuid());
        var listing = new Listing(currentUser.Value.Id, id, category, location, info, image, desc, price);

        try
        {
            isLoading = true;
            await ListingsService.AddListing(listing);
        }
        finally
        {
            isLoading = false;
        }
        Reset();
        StateHasChanged();
    }

    private void Back()
    {
        if(currentStep == 0)
            return;

        currentStep--;
        StateHasChanged();
    }

    public void Dispose()
    {
        RentModalState.OnChange -= OnOpenStateChange;
        GC.SuppressFinalize(this);
    }
}
using blazor_tailwind_airbnb.Shared.Modals.Rent.Category;
using blazor_tailwind_airbnb.Shared.Modals.Rent.Location;
using blazor_tailwind_airbnb.StateContainers;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent;

public partial class RentModal : IDisposable
{
    [Inject]
    public RentModalState RentModalState { get;set; } = null!;

    protected override void OnInitialized()
    {
        RentModalState.OnChange += OnOpenStateChange;
    }

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
        currentStep = Step.Category;
    }

    private CategoryModel _category = new();
    private LocationModel _location = new();
    private bool isLoading = false;
    private Step currentStep = Step.Category;
    private void Pay()
    {
    }

    private void OnValidSubmitCategory(CategoryModel category)
    {
        _category = category;

        Next();
    }

    private void OnValidSubmitLocation(LocationModel location)
    {
        _location = location;

        Next();
    }

    private void Next()
    {
        if(currentStep == Step.Price)
        {
            Pay();
        }
        else
        {
            currentStep ++;
            StateHasChanged();
        }
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
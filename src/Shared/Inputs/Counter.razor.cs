using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace blazor_tailwind_airbnb.Shared.Inputs;

public partial class Counter
{
    [Parameter, EditorRequired]
    public Expression<Func<int>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string? Title { get;set; }
    [Parameter]
    public string? SubTitle { get;set; }

    private async Task Add()
    {
        Value++;
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task Reduce()
    {
        if(Value <= 0)
            return;

        Value--;
        await ValueChanged.InvokeAsync(Value);
    }

    private FieldIdentifier _fieldIdentifier;

    protected override void OnParametersSet()
    {
        _fieldIdentifier = FieldIdentifier.Create(ValidationFor);
    }

    private bool HasError() => EditContext.GetValidationMessages(_fieldIdentifier).Any();

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out int result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            result = 0;
            validationErrorMessage = "Value can't be null or empty";
            return false;
        }

        if(!int.TryParse(value, out result))
        {
            result = 0;
            validationErrorMessage = $"Could not parse value {value} to integer";
            return false;
        }

        validationErrorMessage = null!;
        return true;
    }
}
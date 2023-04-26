using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace blazor_tailwind_airbnb.Shared.Inputs;

public partial class InputTextWithLabel
{
    [Parameter, EditorRequired]
    public Expression<Func<string>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string Id { get;set; } = "";
    [Parameter]
    public string Label { get;set; } = "";
    [Parameter]
    public string Type { get;set; } = "text";
    [Parameter]
    public bool Disabled { get;set; } = false;
    [Parameter]
    public bool FormatPrice { get;set; } = false;

    protected override bool TryParseValueFromString(
        string value,
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out string result,
        [System.Diagnostics.CodeAnalysis.NotNullWhen(false)] out string validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null!;
        return true;
    }

    private string textColorCss => HasError() ? "text-rose-500" : "text-zinc-400";
    private string paddingLeftCss => FormatPrice ? "pl-9" : "pl-4";

    private FieldIdentifier _fieldIdentifier;

    protected override void OnParametersSet()
    {
        _fieldIdentifier = FieldIdentifier.Create(ValidationFor);
    }

    private bool HasError() => EditContext.GetValidationMessages(_fieldIdentifier).Any();
}
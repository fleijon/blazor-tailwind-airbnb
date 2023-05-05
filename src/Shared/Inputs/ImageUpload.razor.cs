using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using blazor_tailwind_airbnb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace blazor_tailwind_airbnb.Shared.Inputs;

public partial class ImageUpload
{
    [Parameter, EditorRequired]
    public Expression<Func<string>> ValidationFor { get; set; } = default!;
    private FieldIdentifier _fieldIdentifier;
    private string? ImageSource => Value is null ? null : string.Format("data:image/png;base64,{0}", Value);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender && Value is null)
        {
            Value = Data.Images.DefaultHomeImage.Content;
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task LoadImage(InputFileChangeEventArgs e)
    {
        var ms = new MemoryStream();
        await e.File.OpenReadStream().CopyToAsync(ms);

        var imageBytes = ms.ToArray();
        var base64 = Convert.ToBase64String(imageBytes);

        Value = base64;
        await ValueChanged.InvokeAsync(base64);
    }
    private bool HasError() => EditContext.GetValidationMessages(_fieldIdentifier).Any();
    protected override void OnParametersSet()
    {
        _fieldIdentifier = FieldIdentifier.Create(ValidationFor);
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out string? result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = null!;
        return true;
    }
}
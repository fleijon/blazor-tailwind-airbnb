using System.Diagnostics.CodeAnalysis;
using BlazorDateRangePicker;

namespace blazor_tailwind_airbnb.Shared.Inputs;

public partial class Calendar
{
    public DateTimeOffset? StartDate { get; set; } = DateTime.Today;
    public DateTimeOffset? EndDate { get; set; } = DateTime.Today.AddDays(1).AddTicks(-1);

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out Models.DateRangeLocal? result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = new();
        validationErrorMessage = null!;

        return true;
    }

    protected override void OnParametersSet()
    {
        if(Value.HasValue)
        {
            // TODO: Make sure the conversion is correct between dates
            StartDate = Value.Value.Start.ToDateTime(TimeOnly.MinValue, DateTimeKind.Local)
                                                 .ToUniversalTime();
            EndDate = Value.Value.End.ToDateTime(TimeOnly.MinValue, DateTimeKind.Local)
                                     .ToUniversalTime();
        }
    }

    private async Task OnRangeSelect(DateRange range)
    {
        if(range.Start > range.End)
            return;

        Value = new Models.DateRangeLocal(
            DateOnly.FromDateTime(range.Start.LocalDateTime),
            DateOnly.FromDateTime(range.End.LocalDateTime));

        await ValueChanged.InvokeAsync();
    }
}
using System.ComponentModel.DataAnnotations;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Price;

public class PriceModel
{
    [Required] // TODO: Add attribute that can parse value
    public string? Price { get;set; }

    public decimal PriceDecimal => Price == null ? 0 :
                                                   decimal.TryParse(Price, out var priceDecimal) ? priceDecimal :
                                                                                                   0;
}

using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Services;

public interface IReservations
{
    Task<Reservation?> AddReservation(Listing listing, DateOnly start, DateOnly end);
    Task<decimal> GetTotalPrice(Listing listing, DateOnly start, DateOnly end);
}

public class Reservations : IReservations
{
    public Task<Reservation?> AddReservation(Listing listing, DateOnly start, DateOnly end)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalPrice(Listing listing, DateOnly start, DateOnly end) => Task.FromResult(listing.Price.Value * (end.DayNumber - start.DayNumber));
}
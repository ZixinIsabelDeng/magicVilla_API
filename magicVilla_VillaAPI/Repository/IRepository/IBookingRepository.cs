using magicVilla_VillaAPI.Models;

namespace magicVilla_VillaAPI.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> UpdateAsync(Booking entity);
        Task<IEnumerable<Booking>> GetBookingsByVillaIdAsync(int villaId);
        Task<IEnumerable<Booking>> GetBookingsByUserNameAsync(string userName);
        Task<bool> IsVillaAvailableAsync(int villaId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null);
    }
}


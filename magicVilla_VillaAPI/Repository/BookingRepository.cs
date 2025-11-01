using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace magicVilla_VillaAPI.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Bookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByVillaIdAsync(int villaId)
        {
            return await _db.Bookings
                .Include(b => b.Villa)
                .Where(b => b.VillaId == villaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserNameAsync(string userName)
        {
            return await _db.Bookings
                .Include(b => b.Villa)
                .Where(b => b.UserName == userName)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> IsVillaAvailableAsync(int villaId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
        {
            var overlappingBookings = await _db.Bookings
                .Where(b => b.VillaId == villaId
                    && b.Status != "Cancelled"
                    && b.Id != (excludeBookingId ?? 0)
                    && ((b.CheckInDate <= checkIn && b.CheckOutDate > checkIn)
                        || (b.CheckInDate < checkOut && b.CheckOutDate >= checkOut)
                        || (b.CheckInDate >= checkIn && b.CheckOutDate <= checkOut)))
                .AnyAsync();

            return !overlappingBookings;
        }
    }
}


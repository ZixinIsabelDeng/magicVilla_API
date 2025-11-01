using magicVilla_Web.Models;
using magicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBookingService
    {
        Task<T> GetAllAsync<T>(string token, string userName = null);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(BookingCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(BookingDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CheckAvailabilityAsync<T>(int villaId, DateTime checkIn, DateTime checkOut, string token);
    }
}


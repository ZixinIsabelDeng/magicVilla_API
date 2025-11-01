using MagicVilla_Utility;
using MagicVilla_Web.Models;
using magicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace MagicVilla_Web.Services
{
    public class BookingService : BaseService, IBookingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string bookingUrl;

        public BookingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            bookingUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(BookingCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = bookingUrl + "/api/v1/BookingAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = bookingUrl + "/api/v1/BookingAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token, string userName = null)
        {
            var url = bookingUrl + "/api/v1/BookingAPI";
            if (!string.IsNullOrEmpty(userName))
            {
                url += "?userName=" + Uri.EscapeDataString(userName);
            }
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = url,
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = bookingUrl + "/api/v1/BookingAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(BookingDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = bookingUrl + "/api/v1/BookingAPI/" + dto.Id,
                Token = token
            });
        }

        public Task<T> CheckAvailabilityAsync<T>(int villaId, DateTime checkIn, DateTime checkOut, string token)
        {
            var url = $"{bookingUrl}/api/v1/BookingAPI/CheckAvailability?villaId={villaId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = url,
                Token = token
            });
        }
    }
}


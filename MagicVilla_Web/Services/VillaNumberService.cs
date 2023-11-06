// Import libraries that we'll use later.
// These are like tools in a toolbox for a carpenter.
using MagicVilla_Utility;
using magicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using System.Threading.Tasks; // For asynchronous programming
using System.Net.Http; // For HTTP client
using Microsoft.Extensions.Configuration; // For reading configuration settings

// Create a new "space" or "container" to hold our VillaService class.
namespace MagicVilla_Web.Services
{
    // Define a new class called VillaService. 
    // It inherits features from BaseService and promises to follow rules set by IVillaService.
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        // These are like private lockers to store important stuff.
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        // This is the constructor. Think of it as the setup crew when the class is created.
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            // Store the tools we'll need later (HTTP client and URL).
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
           
            // "https:/localhost:7001";
        }

        // This function creates a new villa and returns the result.
        public Task<T> CreateAsync<T>(VillaNumberCreatedDTO dto, string token)
        {
            // We're sending a POST request to add new data (a new villa).
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumberAPI",
                Token = token
            });
        }

        // This function deletes a villa based on its ID.
        public Task<T> DeleteAsync<T>(int id, string token )
        {
            // We're sending a DELETE request to remove a villa.
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + id,
                Token = token
            });
        }

  

        // This function gets a specific villa based on its ID.
        public Task<T> GetAsync<T>(int id, string token)
        {
            // We're sending a GET request to fetch one specific villa.
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/villaNumberAPI",
                Token = token

            });
        }

        // This function updates a villa's information.
        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
            // We're sending a PUT request to update a villa's information.
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + dto.VillaNo,
                Token = token
            });
        }
    }
}

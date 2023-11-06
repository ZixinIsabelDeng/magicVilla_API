using AutoMapper;
using MagicVilla_Utility;
using magicVilla_Web.Models;
using magicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        // Declare private variables for the villa service and the mapper
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        // Constructor for the VillaController class
        public HomeController(IVillaService villaService, IMapper mapper)
        {
            // Initialize the villa service and the mapper
            _villaService = villaService;
            _mapper = mapper;
        }

        // Asynchronous method to display the list of villas
        public async Task<IActionResult> Index()
        {
            // Create an empty list to store VillaDTO objects
            List<VillaDTO> list = new();

            // Call the GetAllAsync method from the villa service and await its response
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            Console.WriteLine($"API Response: {response}");  // Log the response

            // Check if the response is successful and not null
            if (response != null && response.IsSuccess)
            {
                // Deserialize the JSON result into a list of VillaDTO objects
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            // Return the list to the View
            return View(list);
        }    
    }
}
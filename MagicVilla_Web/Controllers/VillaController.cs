// Import the necessary libraries and namespaces
using AutoMapper;
using MagicVilla_Utility;
using magicVilla_Web.Models;
using magicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


// Define the namespace and the class for the VillaController
namespace MagicVilla_Web.Controllers
{
    // VillaController inherits from the base Controller class
    public class VillaController : Controller
    {
        // Declare private variables for the villa service and the mapper
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        // Constructor for the VillaController class
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            // Initialize the villa service and the mapper
            _villaService = villaService;
            _mapper = mapper;
        }

        // Asynchronous method to display the list of villas
        public async Task<IActionResult> IndexVilla()
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

        // HTTP GET method to display the form for creating a new villa
        [Authorize(Roles="admin")]
        public async Task<IActionResult> CreateVilla()
        {
            // Simply return the View, which presumably contains the form to create a new villa
            return View();
        }

        // HTTP POST method to handle the form submission for creating a new villa

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the CreateAsync method from _villaService to create a new villa
                var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));

                // Check if the API call was successful and the response is not null
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created sucessfully";
                    // Redirect to the IndexVilla action if the creation was successful
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered";
            // If the model state is invalid or the API call was unsuccessful,
            // return the same view with the existing model for the user to correct
            return View(model);
        }


        // Dependency injection for _villaService and _mapper would go here

        // HTTP GET method to fetch and display the villa details for updating
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            // Call the GetAsync method from _villaService to fetch the villa details by ID
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));

            // Check if the API call was successful and the response is not null
            if (response != null && response.IsSuccess)
            {
                // Deserialize the JSON response to a VillaDTO object
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));

                // Map the VillaDTO object to a VillaUpdateDTO object using AutoMapper
                // and pass it to the View
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }

            // If the API call was unsuccessful or the response is null, return a NotFound result
            return NotFound();
        }

        // HTTP POST method to update the villa details
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the UpdateAsync method from _villaService to update the villa details
                var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));

                // Check if the API call was successful and the response is not null
                if (response != null && response.IsSuccess)
                {


                    TempData["success"] = "Villa Updated sucessfully";
                    // Redirect to the IndexVilla action if the update was successful
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            TempData["error"] = "Error encountered";
            // If the model state is invalid or the API call was unsuccessful,
            // return the same view with the existing model for the user to correct
            return View(model);
        }








        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            // Call the GetAsync method from _villaService to fetch the villa details by ID
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));

            // Check if the API call was successful and the response is not null
            if (response != null && response.IsSuccess)
            {
                // Deserialize the JSON response to a VillaDTO object
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));

                // Map the VillaDTO object to a VillaUpdateDTO object using AutoMapper
                // and pass it to the View
                return View(model);
            }

            // If the API call was unsuccessful or the response is null, return a NotFound result
            return NotFound();
        }

        // HTTP POST method to update the villa details
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {
          
                // Call the UpdateAsync method from _villaService to update the villa details
                var response = await _villaService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));

                // Check if the API call was successful and the response is not null
                if (response != null && response.IsSuccess)
                {
                   TempData["success"] = "Villa Deleted sucessfully";
                // Redirect to the IndexVilla action if the update was successful
                return RedirectToAction(nameof(IndexVilla));
                }

            TempData["error"] = "Error encountered.";
            // If the model state is invalid or the API call was unsuccessful,
            // return the same view with the existing model for the user to correct
            return View(model);
        }






    }
}

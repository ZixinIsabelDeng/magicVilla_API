using AutoMapper;
using magicVilla_Web.Models;
using magicVilla_Web.Models.Dto;

using MagicVilla_Web.Models.VM;

using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {

        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;

        // Constructor for the VillaController class
        public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService, IMapper mapper)
        {
            // Initialize the villa service and the mapper
            _villaNumberService = villaNumberService;
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            // Create an empty list to store VillaDTO objects
            List<VillaNumberDTO> list = new();

            // Call the GetAllAsync method from the villa service and await its response
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
          
            // Check if the response is successful and not null
            if (response != null && response.IsSuccess)
            {
                // Deserialize the JSON result into a list of VillaDTO objects
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }



        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();    
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {

                // Deserialize the JSON result into a list of VillaDTO objects
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }

            return View(villaNumberVM);
        }

        // HTTP POST method to handle the form submission for creating a new villa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the CreateAsync method from _villaService to create a new villa
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);

                // Check if the API call was successful and the response is not null
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Successfully";
                    // Redirect to the IndexVilla action if the creation was successful
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }

            // If the model state is invalid or the API call was unsuccessful,
            // return the same view with the existing model for the user to correct
            return View(model);
        }


        // Dependency injection for _villaService and _mapper would go here

        // HTTP GET method to fetch and display the villa details for updating
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)

        {

            VillaNumberUpdateVM villaNumberVM = new();
            // Call the GetAsync method from _villaService to fetch the villa details by ID
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            // Check if the API call was successful and the response is not null
            if (response != null && response.IsSuccess)
            {

                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);

            }


            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {

                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }
        // HTTP POST method to update the villa details
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessage.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            //THIS PART is about drop down
            var resp = await _villaService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                TempData["success"] = "Villa Updated Successfully";
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); 
            }
           
            return View(model);
        }









        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNumberDeleteVM villaNumberVM = new ();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            // Check if the API call was successful and the response is not null
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model= JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = model;
               
            }
            response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList=JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }

        // HTTP POST method to update the villa details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {

            // Call the Delete Async method from _villaService to  the villa details
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo);

            // Check if the API call was successful and the response is not null
            if (response != null && response.IsSuccess)
            {
                // Redirect to the IndexVilla action if the update was successful
                TempData["success"] = "Villa Deleted Successfully";
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            TempData["error"] = "Error encountered.";
            // If the model state is invalid or the API call was unsuccessful,
            // return the same view with the existing model for the user to correct
            return View(model);
        }




    }
}

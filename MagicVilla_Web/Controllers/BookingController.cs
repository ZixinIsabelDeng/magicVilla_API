using MagicVilla_Utility;
using MagicVilla_Web.Models;
using magicVilla_Web.Models;
using magicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVillaService _villaService;

        public BookingController(IBookingService bookingService, IVillaService villaService)
        {
            _bookingService = bookingService;
            _villaService = villaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int villaId)
        {
            BookingCreateDTO booking = new BookingCreateDTO { VillaId = villaId };
            
            // Get villa details
            var villaResponse = await _villaService.GetAsync<APIResponse>(
                villaId, 
                HttpContext.Session.GetString(SD.SessionToken));

            if (villaResponse != null && villaResponse.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(villaResponse.Result));
                ViewBag.Villa = villa;
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(BookingCreateDTO booking)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookingService.CreateAsync<APIResponse>(
                    booking, 
                    HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Booking created successfully!";
                    return RedirectToAction("Confirmation", new { id = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(response.Result)).Id });
                }
                else
                {
                    if (response?.ErrorMessage?.Count > 0)
                    {
                        foreach (var error in response.ErrorMessage)
                        {
                            ModelState.AddModelError("CustomError", error);
                        }
                    }
                }
            }

            // Reload villa details on error
            var villaResponse = await _villaService.GetAsync<APIResponse>(
                booking.VillaId, 
                HttpContext.Session.GetString(SD.SessionToken));

            if (villaResponse != null && villaResponse.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(villaResponse.Result));
                ViewBag.Villa = villa;
            }

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {
            var response = await _bookingService.GetAsync<APIResponse>(
                id, 
                HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                var booking = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(response.Result));
                return View(booking);
            }

            TempData["error"] = "Booking not found";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Auth");
            }

            var response = await _bookingService.GetAllAsync<APIResponse>(
                HttpContext.Session.GetString(SD.SessionToken),
                userName);

            List<BookingDTO> bookings = new();
            if (response != null && response.IsSuccess)
            {
                bookings = JsonConvert.DeserializeObject<List<BookingDTO>>(Convert.ToString(response.Result));
            }

            return View(bookings);
        }
    }
}


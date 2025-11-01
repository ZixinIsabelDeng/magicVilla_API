using AutoMapper;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using magicVilla_VillaAPI.Repository;
using magicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace magicVilla_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/BookingAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BookingAPIController : ControllerBase
    {
        private readonly IBookingRepository _dbBooking;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public BookingAPIController(IBookingRepository dbBooking, IVillaRepository dbVilla, IMapper mapper)
        {
            _dbBooking = dbBooking;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBookings([FromQuery] string? userName = null)
        {
            try
            {
                IEnumerable<Booking> bookingList;
                
                if (!string.IsNullOrEmpty(userName))
                {
                    bookingList = await _dbBooking.GetBookingsByUserNameAsync(userName);
                }
                else
                {
                    bookingList = await _dbBooking.GetAllAsync(includeProperties: "Villa");
                }

                _response.Result = _mapper.Map<List<BookingDTO>>(bookingList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }

        [HttpGet("{id:int}", Name = "GetBooking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var booking = await _dbBooking.GetAsync(u => u.Id == id, includeProperties: "Villa");
                if (booking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                bookingDTO.VillaName = booking.Villa?.Name;
                _response.Result = bookingDTO;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateBooking([FromBody] BookingCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Validate dates
                if (createDTO.CheckInDate >= createDTO.CheckOutDate)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Check-out date must be after check-in date" };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (createDTO.CheckInDate < DateTime.Today)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Check-in date cannot be in the past" };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Check if villa exists
                var villa = await _dbVilla.GetAsync(u => u.Id == createDTO.VillaId);
                if (villa == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Villa not found" };
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Check if villa is available
                var isAvailable = await _dbBooking.IsVillaAvailableAsync(
                    createDTO.VillaId, 
                    createDTO.CheckInDate, 
                    createDTO.CheckOutDate);

                if (!isAvailable)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Villa is not available for the selected dates" };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Calculate total cost
                var nights = (createDTO.CheckOutDate - createDTO.CheckInDate).Days;
                var totalCost = villa.Rate * nights;

                // Create booking
                Booking booking = _mapper.Map<Booking>(createDTO);
                booking.TotalCost = totalCost;
                booking.Status = "Pending";
                booking.CreatedDate = DateTime.Now;
                booking.UpdatedDate = DateTime.Now;

                await _dbBooking.CreateAsync(booking);

                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                bookingDTO.VillaName = villa.Name;
                _response.Result = bookingDTO;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetBooking), new { id = booking.Id, version = "1.0" }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateBooking(int id, [FromBody] BookingUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var booking = await _dbBooking.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Validate dates
                if (updateDTO.CheckInDate >= updateDTO.CheckOutDate)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Check-out date must be after check-in date" };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Check availability if dates or villa changed
                if (booking.VillaId != updateDTO.VillaId || 
                    booking.CheckInDate != updateDTO.CheckInDate || 
                    booking.CheckOutDate != updateDTO.CheckOutDate)
                {
                    var isAvailable = await _dbBooking.IsVillaAvailableAsync(
                        updateDTO.VillaId,
                        updateDTO.CheckInDate,
                        updateDTO.CheckOutDate,
                        id);

                    if (!isAvailable)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessage = new List<string>() { "Villa is not available for the selected dates" };
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    // Recalculate cost if dates or villa changed
                    var villa = await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaId);
                    if (villa != null)
                    {
                        var nights = (updateDTO.CheckOutDate - updateDTO.CheckInDate).Days;
                        booking.TotalCost = villa.Rate * nights;
                    }
                }

                _mapper.Map(updateDTO, booking);
                booking.UpdatedDate = DateTime.Now;
                await _dbBooking.UpdateAsync(booking);

                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                var villaName = await _dbVilla.GetAsync(u => u.Id == booking.VillaId);
                bookingDTO.VillaName = villaName?.Name;
                _response.Result = bookingDTO;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteBooking(int id)
        {
            try
            {
                var booking = await _dbBooking.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbBooking.RemoveAsync(booking);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }

        [HttpGet("CheckAvailability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> CheckAvailability([FromQuery] int villaId, [FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
        {
            try
            {
                var isAvailable = await _dbBooking.IsVillaAvailableAsync(villaId, checkIn, checkOut);
                var villa = await _dbVilla.GetAsync(u => u.Id == villaId);
                
                if (villa == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Villa not found" };
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var nights = (checkOut - checkIn).Days;
                var totalCost = villa.Rate * nights;

                _response.Result = new
                {
                    IsAvailable = isAvailable,
                    VillaId = villaId,
                    VillaName = villa.Name,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    Nights = nights,
                    RatePerNight = villa.Rate,
                    TotalCost = totalCost
                };
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        }
    }
}


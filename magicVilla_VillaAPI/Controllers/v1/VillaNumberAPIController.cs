

using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using magicVilla_VillaAPI.Repository;
using System.Net;



namespace magicVilla_VillaAPI.Controllers.v1
{

    namespace magicVilla_VillaAPI.Controllers
    {
        [Route("api/v{version:apiVersion}/VillaNumberAPI")]
        [ApiController]
        [ApiVersion("1.0", Deprecated = true)]

        public class VillaNumberAPIV1Controller : ControllerBase
        {
            private readonly IVillaNumberRepository _dbVillaNumber;
            private readonly IMapper _mapper;
            private readonly IVillaRepository _dbVilla;
            protected APIResponse _response;

            public VillaNumberAPIV1Controller(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
            {
                _dbVillaNumber = dbVillaNumber;
                _mapper = mapper;
                this._response = new();
                _dbVilla = dbVilla;
            }


            [HttpGet("GetString")]
            public IEnumerable<string> Get()
            {

                return new string[] { "String1", "String2" };
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<APIResponse>> GetVillasNumbers()
            {
                try
                {
                    IEnumerable<VillaNumber> villaList = await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
                    _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaList);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                catch (Exception exception)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage
                        = new List<string>() { exception.ToString() };
                }
                return _response;
            }
            

            [HttpGet("{id:int}", Name = "GetVillaNumber")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
            {
                try
                {
                    if (id == 0)
                    {

                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest(_response);
                    }

                    var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo== id);
                    if (villaNumber == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }
                    _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }

                catch (Exception exception)
                {

                    _response.IsSuccess = false;
                    _response.ErrorMessage
                        = new List<string>() { exception.ToString() };
                }
                return _response;
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreatedDTO createDTO)
            {
                try
                {
                    if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
                    {
                        ModelState.AddModelError("CustomerError", "Villa Number already Exist");
                        return BadRequest(ModelState);
                    }
                    if (await _dbVilla.GetAsync(u => u.Id == createDTO.VillaID) == null)
                    {
                        ModelState.AddModelError("CustomerError", "Villa ID is invalid");
                        return BadRequest(ModelState);
                    }

                    if (createDTO == null)
                    {
                        return BadRequest(createDTO);
                    }
                    VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);
                    await _dbVillaNumber.CreateAsync(villaNumber);

                    _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                    _response.StatusCode = HttpStatusCode.Created;


                    return CreatedAtRoute("GetVilla", new {id =villaNumber.VillaNo }, _response);
                }
                catch (Exception exception)
                {

                    _response.IsSuccess = false;
                    _response.ErrorMessage
                        = new List<string>() { exception.ToString() };
                }
                return _response;
            }

            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]

            public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
            {
                try
                {
                    if (id == 0)
                    {
                        return BadRequest();

                    }
                    var villaNumber = await _dbVillaNumber.GetAsync(u => u. VillaNo== id);
                    if (villaNumber == null)
                    {
                        return NotFound();

                    }
                    _dbVillaNumber.RemoveAsync(villaNumber);


                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                catch (Exception exception)
                {

                    _response.IsSuccess = false;
                    _response.ErrorMessage
                        = new List<string>() { exception.ToString() };
                }
                return _response;

            }

            [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            
            public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
            {
                try
                {
                    if (updateDTO == null || id != updateDTO.VillaNo)
                    {
                        return BadRequest();
                    }
                    if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
                    {
                        ModelState.AddModelError("CustomerError", "Villa ID is invalid");
                        return BadRequest(ModelState);
                    }

                    VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

                    await _dbVillaNumber.UpdateAsync(model);

                    return NoContent();
                }
                catch (Exception exception)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage
                        = new List<string>() { exception.ToString() };
                }
                return _response;
            }
        }
    }
}

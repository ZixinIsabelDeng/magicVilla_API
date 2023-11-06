

using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using magicVilla_VillaAPI.Repository;
using System.Net;



namespace magicVilla_VillaAPI.Controllers.v2
{
    
    namespace magicVilla_VillaAPI.Controllers
    {
        [Route("api/v{version:apiVersion}/VillaNumberAPI")]
        [ApiController]

        [ApiVersion("2.0")]
        public class VillaNumberAPIV2Controller : ControllerBase
        {
            private readonly IVillaNumberRepository _dbVillaNumber;
            private readonly IMapper _mapper;
            private readonly IVillaRepository _dbVilla;
            protected APIResponse _response;

            public VillaNumberAPIV2Controller(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
            {
                _dbVillaNumber = dbVillaNumber;
                _mapper = mapper;
                this._response = new();
                _dbVilla = dbVilla;
            }



            [HttpGet("GetString")]
            public IEnumerable<string> Get()
            {

                return new string[] { "value1", "value2" };
            }


        }
    }
}

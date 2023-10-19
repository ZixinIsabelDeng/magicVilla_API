using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using magicVilla_VillaAPI.Logging;

namespace magicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]

    
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ValuesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            
            return Ok(_db.Villas.ToList());

        }



        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {

            if (id == 0)
            {
              

                return BadRequest();
            }

            var Villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (Villa == null)
            {
                return NotFound();
            }
            return Ok(Villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomerError", "Villa already Exist");
                return BadRequest(ModelState);



            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);

            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = _db.V.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            
            
            
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }



        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();

            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();  
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)    
        {
            if(patchDTO==null||id==0)
            { 
                return BadRequest(); 
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();     
            }

            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);

            }
            return NoContent();
        }
    }
}
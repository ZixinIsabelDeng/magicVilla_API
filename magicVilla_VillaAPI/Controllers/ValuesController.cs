﻿using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace magicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);

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

            var Villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
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
            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
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
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }



        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();

            }
            VillaStore.villaList.Remove(villa);
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
            var villa =VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            villa.Name=villaDTO.Name;
            villa.Sqft=villaDTO.Sqft;
            villa.Occupancy=villaDTO.Occupancy;
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument1)
        {






        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace magicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Occupancy {  get; set; }
        public int Sqft {  get; set; }  

    }
}

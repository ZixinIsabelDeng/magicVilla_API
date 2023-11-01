using System.ComponentModel.DataAnnotations;

namespace magicVilla_Web.Models.Dto
{
    public class VillaNumberCreatedDTO
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaID {  get; set; }
        public string SpecialDetails { get; set; }
        
    }
}

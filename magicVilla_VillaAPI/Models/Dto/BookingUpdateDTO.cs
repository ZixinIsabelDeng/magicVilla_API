using System.ComponentModel.DataAnnotations;

namespace magicVilla_VillaAPI.Models.Dto
{
    public class BookingUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Required]
        public int VillaId { get; set; }
        
        public int? VillaNo { get; set; }
        
        [Required]
        public DateTime CheckInDate { get; set; }
        
        [Required]
        public DateTime CheckOutDate { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int NumberOfGuests { get; set; }
        
        public string SpecialRequests { get; set; }
        
        public string Status { get; set; }
    }
}


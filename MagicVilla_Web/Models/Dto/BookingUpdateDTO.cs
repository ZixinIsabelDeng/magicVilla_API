using System.ComponentModel.DataAnnotations;

namespace magicVilla_Web.Models.Dto
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
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int NumberOfGuests { get; set; }
        
        public string SpecialRequests { get; set; }
        
        public string Status { get; set; }
    }
}


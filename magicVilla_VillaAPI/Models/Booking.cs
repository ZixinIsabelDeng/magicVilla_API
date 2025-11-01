using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magicVilla_VillaAPI.Models
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
        
        public int? VillaNo { get; set; }
        
        [Required]
        public DateTime CheckInDate { get; set; }
        
        [Required]
        public DateTime CheckOutDate { get; set; }
        
        [Required]
        public int NumberOfGuests { get; set; }
        
        [Required]
        public double TotalCost { get; set; }
        
        public string SpecialRequests { get; set; }
        
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}


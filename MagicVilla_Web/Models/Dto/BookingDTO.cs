using System.ComponentModel.DataAnnotations;

namespace magicVilla_Web.Models.Dto
{
    public class BookingDTO
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public int VillaId { get; set; }
        
        public string VillaName { get; set; }
        
        public int? VillaNo { get; set; }
        
        public DateTime CheckInDate { get; set; }
        
        public DateTime CheckOutDate { get; set; }
        
        public int NumberOfGuests { get; set; }
        
        public double TotalCost { get; set; }
        
        public string SpecialRequests { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
    }
}


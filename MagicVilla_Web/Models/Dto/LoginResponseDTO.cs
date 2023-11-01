using MagicVilla_Web.Models.Dto;

namespace magicVilla_Web.Models.Dto
{
    public class LoginResponseDTO
    {
        
        public UserDTO User {  get; set; }
        public string Token {  get; set; }

    }
}

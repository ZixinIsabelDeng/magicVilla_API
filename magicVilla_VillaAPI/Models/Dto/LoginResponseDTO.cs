namespace magicVilla_VillaAPI.Models.Dto
{
    public class LoginResponseDTO
    {
        //token is given as an autheticate user
        //user provides information about the user, it will have all teh details
        public LocalUser User { get; set; }
        public string Token {  get; set; }

    }
}

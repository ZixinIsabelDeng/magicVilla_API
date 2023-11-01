using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;

namespace magicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);

    }
}

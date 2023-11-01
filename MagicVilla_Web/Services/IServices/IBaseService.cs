using magicVilla_Web.Models;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse reponseModel {  get;set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);



    }
}

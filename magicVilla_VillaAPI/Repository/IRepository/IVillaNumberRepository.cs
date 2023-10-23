using magicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace magicVilla_VillaAPI.Repository
{
    public interface IVillaNumberRepository:IRepository<VillaNumber>
    {

      
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
     


    }
}

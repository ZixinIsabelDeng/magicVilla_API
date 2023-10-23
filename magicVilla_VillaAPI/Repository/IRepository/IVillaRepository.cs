using magicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace magicVilla_VillaAPI.Repository
{
    public interface IVillaNumberRepository:IRepository<Villa>
    {

      
        Task<Villa> UpdateAsync(Villa entity);
     


    }
}

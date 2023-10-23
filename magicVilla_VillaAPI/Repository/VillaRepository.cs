using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace magicVilla_VillaAPI.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db; 

        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
      

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

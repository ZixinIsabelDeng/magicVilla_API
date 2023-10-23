using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace magicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository :Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db; 

        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public Task CreateAsync(Villa entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Villa entity)
        {
            throw new NotImplementedException();
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumber.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<Villa> UpdateAsync(Villa entity)
        {
            throw new NotImplementedException();
        }
    }
}

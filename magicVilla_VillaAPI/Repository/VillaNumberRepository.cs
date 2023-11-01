using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;


namespace magicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository :Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db; 

        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumber.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }        
    }
}

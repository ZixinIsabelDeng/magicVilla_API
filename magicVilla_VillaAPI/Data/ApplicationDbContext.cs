using magicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace magicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumber { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Majestic mountaintop estate, lush gardens, opulent rooms, gold-dripped decor, panoramic views, secret passages, grand ballroom, private lake, royal tapestries, enchanted ambiance.",
                    ImageUrl = "https://www.arabianbusiness.com/cloud/2021/09/14/GczvHPLj-arabianranches-2-1200x800.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now

                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Minimalism Villa",
                     Details = "Sprawling hillside palace, verdant terraces, luxurious chambers, marble accents, breathtaking vistas, hidden alcoves, expansive courtyard, serene pond, regal drapery, mystical allure.",
                    ImageUrl = "https://cdn.henleyglobal.com/storage/app/media/REALESTATES/st-kitts-stunning-villa-with-breathtaking-views/1-8-1.jpeg",
                     Occupancy = 9,
                     Rate = 35000,
                     Sqft = 700,
                     Amenity = "",
                     CreatedDate = DateTime.Now

                 },
                  new Villa()
                  {
                      Id = 3,
                      Name = "Forest Villa",
                      Details = " Grand forest manor, vibrant orchards, lavish suites, crystal chandeliers, sweeping landscapes, concealed grottos, magnificent atrium, tranquil waterfall, noble frescoes, magical charm.",
                    ImageUrl = "https://static.ojohosts.ca/p/1001/C7020098_0_mbNfqV_p.jpeg",
                      Occupancy = 2,
                      Rate = 9990,
                      Sqft = 709,
                      Amenity="",

                  });
        }
    }
}

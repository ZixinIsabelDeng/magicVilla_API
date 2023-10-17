using magicVilla_VillaAPI.Models.Dto;

namespace magicVilla_VillaAPI.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> villaList = new()
        {
            new VillaDTO{Id=1,Name="Pool View",Sqft=100, Occupancy=4},
            new VillaDTO{Id=2,Name="Beach View",Sqft=200, Occupancy=2}
        };

    }
}

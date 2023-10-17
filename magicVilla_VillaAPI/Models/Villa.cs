namespace magicVilla_VillaAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedDate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }

    }
}

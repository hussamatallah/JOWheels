namespace JOWheels.Models
{
    public class Governorate
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Region> Regions { get; set; }    // to Many
    }
}

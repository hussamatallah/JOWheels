namespace JOWheels.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GovernorateId { get; set; }  // FK
        public Governorate Governorate { get; set; }    // One to
    }
}

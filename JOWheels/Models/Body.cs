namespace JOWheels.Models
{
    public class Body
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public int BrandId { get; set; }    // FK
        public Brand Brand { get; set; }    // One to
    }
}

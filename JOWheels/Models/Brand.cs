namespace JOWheels.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<Body> Bodies { get; set; }   // to Many
    }
}

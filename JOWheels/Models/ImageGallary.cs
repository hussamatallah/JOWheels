namespace JOWheels.Models
{
    public class ImageGallary
    {

        public int Id { get; set; }
        public string ImageURL { get; set; }

        public int CarId { get; set; }  // FK
        public Car Car { get; set; }

    }
}

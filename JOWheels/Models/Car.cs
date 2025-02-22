namespace JOWheels.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string PlateNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsFavorite { get; set; } = false;


        public int EntityId { get; set; }   //FK
        public User Entity { get; set; }

        public int BrandId { get; set; }    //FK
        public Brand Brand { get; set; }

        public int BodyId { get; set; }    //FK
        public Body Body { get; set; }

        public int TransmissionId { get; set; }   //FK
        public Transmission Transmission { get; set; }

        public int FuleId { get; set; }   //FK
        public Fule Fule { get; set; }

        public int ColorId { get; set; }   //FK
        public Color Color { get; set; }

        public int SeatId { get; set; }   //FK
        public Seat Seat { get; set; }

        public int ConditionId { get; set; }   //FK
        public Condition Condition { get; set; }

        public int MileageId { get; set; }   //FK
        public Mileage Mileage { get; set; }

        public int CustomId { get; set; }   //FK
        public Custom Custom { get; set; }

        public int LicenseId { get; set; }   //FK
        public License License { get; set; }

        public int InsuranceId { get; set; }   //FK
        public Insurance Insurance { get; set; }

        public int PaymentId { get; set; }   //FK
        public Payment Payment { get; set; }

        public int YearId { get; set; }   //FK
        public Year Year { get; set; }

        public IEnumerable<ImageGallary> ImageGallaries { get; set; }   // Many
    }
}

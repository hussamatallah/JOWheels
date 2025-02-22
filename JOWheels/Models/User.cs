using System.Text.Json.Serialization;

namespace JOWheels.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }


        public int GovernorateId { get; set; }  //FK
        public int RegionId { get; set; }   //FK

        [JsonIgnore]
        public Governorate? Governorate { get; set; }
        [JsonIgnore]
        public Region? Region { get; set; }
    }
}

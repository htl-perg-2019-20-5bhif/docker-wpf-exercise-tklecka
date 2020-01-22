using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace docker_wpf_exercise_tklecka.Model
{
    public class Reservation
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [Required]
        [JsonPropertyName("reservationDay")]
        public DateTime ReservationDay { get; set; }
        [Required]
        [JsonPropertyName("carid")]
        public int CarID { get; set; }
        [JsonPropertyName("car")]
        public Car Car { get; set; }
    }
}

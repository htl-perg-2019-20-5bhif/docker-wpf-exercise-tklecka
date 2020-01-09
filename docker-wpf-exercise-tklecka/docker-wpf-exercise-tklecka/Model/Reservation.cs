using System;
using System.ComponentModel.DataAnnotations;

namespace docker_wpf_exercise_tklecka.Model
{
    public class Reservation
    {
        public int ID { get; set; }
        [Required]
        public DateTime ReservationDay { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public int CarID { get; set; }
        public Car Car { get; set; }
    }
}

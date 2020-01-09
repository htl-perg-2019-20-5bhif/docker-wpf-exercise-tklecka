using System.ComponentModel.DataAnnotations;

namespace docker_wpf_exercise_tklecka.Model
{
    public class Car
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Licenseplate { get; set; }
    }
}

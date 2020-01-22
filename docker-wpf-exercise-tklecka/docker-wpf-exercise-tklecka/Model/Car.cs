using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace docker_wpf_exercise_tklecka.Model
{
    public class Car
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        [JsonPropertyName("licenseplate")]
        public string Licenseplate { get; set; }
    }
}

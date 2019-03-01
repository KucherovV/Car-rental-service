using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class City
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "City name lenght must be shorter than 20 characters")]
        public string Name { get; set; }
    }
}

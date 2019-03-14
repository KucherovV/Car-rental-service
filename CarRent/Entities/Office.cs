using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Office
    {
        public int ID { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Description lenght must be between 5 and 40 characters")]
        [Display(Name = "Place Description")]
        public string PlaceDescription { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Address lenght must be between 5 and 40 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number lenght must be between 10 and 15 characters")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public bool IsArchived { get; set; }
    }
}

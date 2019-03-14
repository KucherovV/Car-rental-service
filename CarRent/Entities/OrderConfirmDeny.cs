using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class OrderConfirmDeny
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        [StringLength(maximumLength:100, MinimumLength = 10, ErrorMessage = "Reason lenght must be between 10 and 100 characters")]
        public string Text { get; set; }
    }
}

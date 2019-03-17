using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class OrderProblem
    {
        public int ID { get; set; }

        public int Order_ID { get; set; }

        public virtual Order Order { get; set; }

        public string UserID { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Message lenght must be between 10 and 100 characters")]
        public string Text { get; set; }

        [Required]
        public int Fine { get; set; }
    }
}

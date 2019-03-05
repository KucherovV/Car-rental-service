using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Blocking
    {
        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Message lenght must be betwrrn 5 and 30 characters")]
        public string Reason { get; set; }

        public DateTime BlockStart { get; set; }

        [Required]
        [Display(Name = "Block Finish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BlockFinish { get; set; }
    }
}

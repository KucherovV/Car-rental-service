using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AdditionalOption
    {
        public int ID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Name should be shorter than 25 characters")]
        public string Name { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Additional optiinal cannot be more expensive than 10$")]
        public int Price { get; set; }
    }
}

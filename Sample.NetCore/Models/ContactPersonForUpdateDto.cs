using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Models
{
    public class ContactPersonForUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(10)]
        public string ContactNo { get; set; }
    }
}

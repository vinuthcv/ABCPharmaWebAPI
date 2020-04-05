using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please provide name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Expiry date")]
        public DateTime Expiry { get; set; }
        [Required(ErrorMessage = "Please provide Company name")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please provide Quantity")]
        public int Quantity { get; set; }
    }
}

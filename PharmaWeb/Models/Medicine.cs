using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmaWeb.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime Expiry { get; set; }

        public string Company { get; set; }

        public int Quantity { get; set; }
    }
}
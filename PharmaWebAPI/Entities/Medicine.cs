using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
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

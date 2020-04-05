using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IPharmaRepository
    {
        List<Medicine> FetchMedicines(string searchText = "");

        bool AddMedicine(Medicine medicine);

    }
}

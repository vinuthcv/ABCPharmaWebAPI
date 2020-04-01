using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PharmaRepository : IPharmaRepository
    {
        public bool AddMedicine(Medicine medicine)
        {
            bool success = false;
            try
            {
                var medicines = FetchMedicines();
                if (medicines != null)
                {
                    medicines.Add(medicine);
                }
                else
                {
                    medicines = new List<Medicine>();
                    medicines.Add(medicine);
                }

                using (StreamWriter file = File.CreateText(@"~\Medicines.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, medicines);
                }

                success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }

        public List<Medicine> FetchMedicines(string searchText = "")
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}

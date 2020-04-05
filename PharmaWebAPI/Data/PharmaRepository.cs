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
        public static int count = 1;
        public bool AddMedicine(Medicine medicine)
        {
            bool success = false;
            try
            {
                medicine.Id = count;

                var medicines = FetchMedicines();
                if (medicines != null)
                {
                    medicines.Add(medicine);

                    using (StreamWriter file = File.CreateText(@"C:\Users\Admin\source\repos\vinuthcv\ABCPharmaWebAPI\PharmaWebAPI\Data\Medicines.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, medicines);
                    }
                    success = true;
                    count++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }

        public List<Medicine> FetchMedicines(string searchText = "")
        {
            List<Medicine> medicines = new List<Medicine>();
            try
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Admin\source\repos\vinuthcv\ABCPharmaWebAPI\PharmaWebAPI\Data\Medicines.json"))
                {
                    var stream = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(stream))
                    {
                        var data = JsonConvert.DeserializeObject<List<Medicine>>(stream);
                        if (data != null && data.Any())
                        {
                            medicines.AddRange(data);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return medicines;
        }
    }
}

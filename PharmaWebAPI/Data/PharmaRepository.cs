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

                var Id = medicines.OrderByDescending(x => x.Id).FirstOrDefault();

                if (Id != null)
                {
                    medicine.Id = ++Id.Id;
                }

                if (medicines != null)
                {
                    medicines.Add(medicine);

                    WriteToFile(medicines);

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
                            if (!string.IsNullOrEmpty(searchText))
                            {
                                var filteredData = data.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                                if (filteredData != null && filteredData.Any())
                                {
                                    medicines.AddRange(filteredData);
                                }
                            }
                            else
                            {

                                medicines.AddRange(data);
                            }
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

        public bool DeleteMedicine(int id)
        {
            bool success = false;

            try
            {
                var medicines = FetchMedicines();
                if (medicines != null && medicines.Any())
                {
                    var medicine = medicines.FirstOrDefault(x => x.Id == id);
                    if (medicine != null)
                    {
                        medicines.Remove(medicine);

                        WriteToFile(medicines);

                        success = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return success;
        }

        public bool UpdateMedicine(Medicine medicine)
        {
            bool success = false;

            try
            {
                var medicines = FetchMedicines();
                if (medicines != null && medicines.Any())
                {
                    var currentMedicine = medicines.FirstOrDefault(x => x.Id == medicine.Id);
                    if (currentMedicine != null)
                    {
                        medicines.Remove(currentMedicine);

                        medicines.Add(medicine);

                        WriteToFile(medicines);

                        success = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return success;
        }

        private void WriteToFile(List<Medicine> medicines)
        {
            try
            {
                using (StreamWriter file = File.CreateText(@"C:\Users\Admin\source\repos\vinuthcv\ABCPharmaWebAPI\PharmaWebAPI\Data\Medicines.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, medicines);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PharmaWebAPI.Controllers
{
    public class MedicineController : ApiController
    {
        private readonly IPharmaRepository _pharmaRepository;
        public MedicineController(IPharmaRepository pharmaRepository)
        {
            _pharmaRepository = pharmaRepository;
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            try
            {
                var medicines = _pharmaRepository.FetchMedicines();
                if (medicines != null && medicines.Any())
                {
                    return Ok(medicines);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(Medicine medicine)
        {
            try
            {
                var repository = new PharmaRepository();
                repository.AddMedicine(medicine);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

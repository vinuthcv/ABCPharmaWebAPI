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
        public IHttpActionResult Get(int id)
        {
            try
            {
                var medicine = _pharmaRepository.FetchMedicines().FirstOrDefault(x => x.Id == id);

                if (medicine != null)
                {
                    return Ok(medicine);
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


        public IHttpActionResult Get(string search)
        {
            try
            {
                var medicines = _pharmaRepository.FetchMedicines(search);
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

        // POST api/values
        public IHttpActionResult Post(Medicine medicine)
        {
            try
            {
                var success = _pharmaRepository.AddMedicine(medicine);
                if (!success)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        // PUT api/values/5
        public IHttpActionResult Put(Medicine medicine)
        {
            try
            {
                var success = _pharmaRepository.UpdateMedicine(medicine);
                if (!success)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var success = _pharmaRepository.DeleteMedicine(id);
                if (!success)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

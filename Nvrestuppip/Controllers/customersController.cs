using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nvrestuppip.Models;
using System.Diagnostics.Eventing.Reader;

namespace Nvrestuppip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customersController : ControllerBase
    {
        //otetaan tietokantatoiminto käyttöön luodaan instanssi
        NorthwindOriginalContext db = new();

        //hae kaikki asiakkaat
        //polku on: https://localhost:7013/api/customers
        [HttpGet]

        public ActionResult GetAll()
        {
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }


        //polku on: https://localhost:7013/api/customers/company/Wilman
        //hae nimen osalla 
        [HttpGet("company/{hakusana}")]

        public ActionResult GetByCompanyName(string hakusana)
        {
            var asiakkaat = db.Customers.Where(c => c.CompanyName.Contains(hakusana));
            //var asiakkaat = db.Customers.Where(c => c.CompanyName == (hakusana)); <---- t
            return Ok(asiakkaat);
        }

        [HttpGet("cityname/{hakusana}")]

        public ActionResult GetByCityName(string hakusana)
        {
            var asiakkaat = db.Customers.Where(c => c.City == hakusana);
            //var asiakkaat = db.Customers.Where(c => c.CompanyName == (hakusana)); <---- t
            return Ok(asiakkaat);
        }

        [HttpGet("katuosote/{hakusana}")]

        public ActionResult GetByaddress(string hakusana)
        {
            var asiakkaat = db.Customers.Where(c => c.Address == hakusana);
            //var asiakkaat = db.Customers.Where(c => c.CompanyName == (hakusana)); <---- t
            return Ok(asiakkaat);
        }

        //polku on: https://localhost:7013/api/customers/company/ALFki
        //hae taulun päänvaimella eli id:ll
        [HttpGet("{id}")]
        public ActionResult GetByid(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {
                    return Ok(asiakas);

                }
                else
                {
                    return NotFound($"id:llä {id} ei löytynyt yhtään asiakasta");
                }
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

        }


        
    }
}

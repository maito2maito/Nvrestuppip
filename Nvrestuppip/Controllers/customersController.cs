using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nvrestuppip.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

        //uuden asiakkaan lisääminen

        [HttpPost]
        public ActionResult AddNew([FromBody]Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok("lisättiin asikas:" + customer.CompanyName);
            }

            catch (Exception ex)
            {
                return BadRequest("jotain meni pileen. lue lisää:" + ex.InnerException);
            }
        }
        //asikaanpoistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if(asiakas != null)
                {//jos id:llä löytyy asiakas
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok("asikas" + asiakas.CompanyName + "poistettiin");
                }
                return NotFound("asikas id:llä" + id + "ei löytynyt");
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }


        //asikkaan muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomers(string id, [FromBody] Customer customer)
        {
            var asiakas = db.Customers.Find(id);
            if(asiakas != null)
            {
                asiakas.CompanyName = customer.CompanyName;
                asiakas.ContactName = customer.ContactName;
                asiakas.Address = customer.Address;
                asiakas.City = customer.City;
                asiakas.Region = customer.Region;
                asiakas.PostalCode = customer.PostalCode;
                asiakas.Country = customer.Country;
                asiakas.Phone = customer.Phone;
                asiakas.Fax = customer.Fax;

                db.SaveChanges();
                return Ok("Muokattu asiakasta"+ asiakas.CompanyName);
            }

            return NotFound("Asiakasta ei löytynyt id:llä" + id);
        }

        //hakee nimen osalla
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname));

                //var cust = from x in db.customers where c.companyName.Contains(cname) //select c; <-- sama mutta traditionall
                
                //var cust = db.customers.where(c => c.companyName == cname); <-- perfect match

                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        
    }
}

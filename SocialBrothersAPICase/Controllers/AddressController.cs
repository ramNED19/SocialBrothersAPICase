using Microsoft.AspNetCore.Mvc;
using SocialBrothersAPICase.Data;
using SocialBrothersAPICase.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialBrothersAPICase.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // GET api/<AddressController>/5
        [Microsoft.AspNetCore.Mvc.Route("get")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IEnumerable<Address> GetAll([FromQuery] Filters filters, [FromQuery] string orderBy)
        {
            try
            {
                SQLiteDataReader reader = SQLiteDB.ReadAll(filters, orderBy);
                List<Address> addresses = new List<Address>();
                while (reader.Read())
                {
                    Address address = new Address
                    {
                        Id = reader.GetFieldValue<Int64>(reader.GetOrdinal("ID")),
                        Straat = reader.GetFieldValue<string>(reader.GetOrdinal("Straat")),
                        Huisnummer = reader.GetFieldValue<int>(reader.GetOrdinal("Huisnummer")),
                        Toevoeging = reader.GetFieldValue<string>(reader.GetOrdinal("Toevoeging")),
                        Postcode = reader.GetFieldValue<string>(reader.GetOrdinal("Postcode")),
                        Plaats = reader.GetFieldValue<string>(reader.GetOrdinal("Plaats")),
                        Land = reader.GetFieldValue<string>(reader.GetOrdinal("Land"))
                    };
                    addresses.Add(address);
                }
                return addresses;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.Route("distance")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public double GetDistance([FromQuery] string straat1, [FromQuery] int huisnummer1, [FromQuery] string toevoeging1, [FromQuery] string plaats1, [FromQuery] string land1, [FromQuery] string straat2, [FromQuery] int huisnummer2, [FromQuery] string toevoeging2, [FromQuery] string plaats2, [FromQuery] string land2)
        {
            try
            {
                Address address1 = new Address()
                {
                    Straat = straat1,
                    Huisnummer = huisnummer1,
                    Toevoeging = toevoeging1,
                    Plaats = plaats1,
                    Land = land1,
                    Postcode = "lorem"
                };
                Address address2 = new Address()
                {
                    Straat = straat2,
                    Huisnummer = huisnummer2,
                    Toevoeging = toevoeging2,
                    Plaats = plaats2,
                    Land = land2,
                    Postcode = "lorem"
                };
                if (ModelState.IsValid)
                {
                    return APIClient.GetDistance(address1, address2);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.Route("distanceID")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public double GetDistanceByID([FromQuery] int id1, [FromQuery] int id2)
        {
            try
            {
                Address address1 = SQLiteDB.GetByID(id1);
                Address address2 = SQLiteDB.GetByID(id2);
                if (ModelState.IsValid)
                {
                    return APIClient.GetDistance(address1, address2);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // POST api/<AddressController>
        [Microsoft.AspNetCore.Mvc.Route("create")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([FromQuery] Address address)
        {
            try
            {
                SQLiteDB.CreateAddress(address);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<AddressController>/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public void Put(int id, string newStraat, int? newHuisnummer, string newToevoeging, string newPostcode, string newPlaats, string newLand)
        {
            try
            {
                SQLiteDB.UpdateAddress(id, newStraat, newHuisnummer, newToevoeging, newPostcode, newPlaats, newLand);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<AddressController>/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                SQLiteDB.DeleteAddress(id);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}

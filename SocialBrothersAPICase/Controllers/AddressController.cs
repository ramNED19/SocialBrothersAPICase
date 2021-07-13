using Microsoft.AspNetCore.Mvc;
using SocialBrothersAPICase.Data;
using SocialBrothersAPICase.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialBrothersAPICase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // GET api/<AddressController>/5
        [HttpGet]
        public IEnumerable<Address> GetAll()
        {
            SQLiteDataReader reader = SQLiteDB.ReadAll();
            List<Address> addresses = new List<Address>();
            while (reader.Read())
            {
                Address address = new Address();
                address.Straat = reader.GetFieldValue<string>(reader.GetOrdinal("Straat"));
                address.Huisnummer = reader.GetFieldValue<int>(reader.GetOrdinal("Huisnummer"));
                address.Toevoeging = reader.GetFieldValue<string>(reader.GetOrdinal("Toevoeging"));
                address.Postcode = reader.GetFieldValue<string>(reader.GetOrdinal("Postcode"));
                address.Plaats = reader.GetFieldValue<string>(reader.GetOrdinal("Plaats"));
                address.Land = reader.GetFieldValue<string>(reader.GetOrdinal("Land"));
                addresses.Add(address);
            }
            return addresses;
        }

        // POST api/<AddressController>
        [HttpPost]
        public void Post([FromQuery] Address address)
        {
            SQLiteDB.CreateAddress(address);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

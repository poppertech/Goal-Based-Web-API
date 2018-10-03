using System.Collections.Generic;
using Api.Models.Network;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }

        [HttpPost]
        public Network Post([FromBody]Network network)
        {
            return network;
        }

        [HttpPut("{id}")]
        public Network Put(int id, [FromBody]Network network)
        {
            return network;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            return;
        }
    }
}

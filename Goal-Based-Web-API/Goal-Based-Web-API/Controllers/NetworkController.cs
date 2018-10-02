using System.Collections.Generic;
using Api.Models;
using Api.Models.Network;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        [HttpGet]
        public IEnumerable<NetworkView> Get()
        {
            return new [] { new NetworkView { Id = 1, Name = "Network1" } , new NetworkView { Id = 2, Name = "Network2" } };
        }

        [HttpGet("{id}")]
        public Network Get(int id)
        {
            if(id == 1)
            {
                return new Network { Id = 1, Name = "Network1" };
            }
            else
            {
                return new Network { Id = 2, Name = "Network2" };
            }
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

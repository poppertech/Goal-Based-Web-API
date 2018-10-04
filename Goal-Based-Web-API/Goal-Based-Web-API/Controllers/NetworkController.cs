using Api.Logic;
using Api.Models.Network;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly INetworkService _service;

        public NetworkController(INetworkService service)
        {
            _service = service;
        }

        [HttpGet]
        public INetwork Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public INetwork Get(int id)
        {
            return _service.GetNetworkById(id);
        }

        [HttpPost]
        public INetwork Post([FromBody]INetwork network)
        {
            return network;
        }

        [HttpPut("{id}")]
        public INetwork Put(int id, [FromBody]INetwork network)
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

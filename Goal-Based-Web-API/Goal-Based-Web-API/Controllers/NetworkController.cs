using Api.Logic;
using Api.Models.Network;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly INetworkService _service;
        
        public NetworkController(INetworkService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<NetworkViewModel> Get()
        {
            return _service.GetNetworks();
        }

        [HttpGet("{id}")]
        public INetwork Get(int id)
        {
            return _service.GetNetworkById(id);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public INetwork Post([FromForm]PostNetworkViewModel network)
        {
            return _service.GetNetwork(network);
        }


    }
}

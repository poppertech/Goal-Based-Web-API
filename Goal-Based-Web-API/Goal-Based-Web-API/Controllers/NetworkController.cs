using Api.Logic;
using Api.Models.Network;
using Microsoft.AspNetCore.Mvc;

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
        [Consumes("multipart/form-data")]
        public INetwork Post([FromForm]NetworkViewModel network)
        {
            return _service.GetNetwork(network);
        }


    }
}

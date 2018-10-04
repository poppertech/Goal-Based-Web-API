using Api.Logic;
using Api.Models.Network;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        [Consumes("multipart/form-data")]
        public INetwork Post([FromForm]NetworkViewModel network)
        {
            ICsvFileDeserializer<CashFlow> csvDeserializer = new CsvFileDeserializer<CashFlow>();
            csvDeserializer.ReadFile(network.CashFlows);
            var cashFlows = csvDeserializer.GetRecords();

            IJsonFileDeserializer<Node> jsonDeserializer = new JsonFileDeserializer<Node>();
            jsonDeserializer.ReadFile(network.Tree);
            var tree = jsonDeserializer.GetInstance();

            return null;
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

using Api.Logic;
using Api.Models.Network;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            using (var memoryStream = new MemoryStream())
            {
                network.CashFlows.CopyTo(memoryStream);
                memoryStream.Position = 0;
                TextReader textReader = new StreamReader(memoryStream);
                var csv = new CsvReader(textReader);
                csv.Configuration.HasHeaderRecord = true;
                var records = csv.GetRecords<CashFlow>().ToList();
            }

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

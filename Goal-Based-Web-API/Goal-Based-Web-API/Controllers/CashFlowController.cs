using Api.Logic;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Goal_Based_Web_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CashFlowController : Controller
    {
        private readonly ICashFlowService _service;

        public CashFlowController(ICashFlowService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCashFlows(int networkId)
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            using (var csv = new CsvWriter(streamWriter))
            {
                var cashFlows = _service.GetCashFlowsByNetworkId(networkId);
                csv.WriteRecords(cashFlows);
                streamWriter.Flush();
                var bytes = stream.ToArray();
                var outputStream = new MemoryStream(bytes);
                return File(outputStream, "application/octet-stream");
            }
        }
    }
}
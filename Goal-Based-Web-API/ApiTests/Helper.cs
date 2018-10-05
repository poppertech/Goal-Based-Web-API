using Api.Models.Network;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiTests
{
    public static class Helper
    {
        public static IList<CashFlow> ReadFile(Stream input)
        {
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    using (var csv = new CsvReader(streamReader))
                    {
                        csv.Configuration.HasHeaderRecord = true;
                        return csv.GetRecords<CashFlow>().ToList();
                    }
                }
            }
        }

        public static IFormFile WriteFile(IList<CashFlow> cashFlows)
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.WriteRecords(cashFlows);
                streamWriter.Flush();
                var bytes = stream.ToArray();
                var outputStream = new MemoryStream(bytes);
                return new FormFile(outputStream, 0, outputStream.Length, "CashFlows", "cashflow.csv");
            }
        }

        public static IFormFile WriteFile(Node node)
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, node);
                streamWriter.Flush();
                var bytes = stream.ToArray();
                var outputStream = new MemoryStream(bytes);
                return new FormFile(outputStream, 0, outputStream.Length, "CashFlows", "cashflow.csv");
            }
        }

    }
}

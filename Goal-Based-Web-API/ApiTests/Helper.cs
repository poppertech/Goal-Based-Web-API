using Api.Models.Network;
using CsvHelper;
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

    }
}

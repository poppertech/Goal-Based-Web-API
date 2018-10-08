using Microsoft.AspNetCore.Http;

namespace Api.Models.Network
{
    public class PostNetworkViewModel
    {
       public IFormFile CashFlows { get; set; }
       public IFormFile Tree { get; set; }
    }
}

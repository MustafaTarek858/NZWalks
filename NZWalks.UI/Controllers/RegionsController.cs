using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> Response = new List<RegionDto>();
            try
            {
                //Get all regions from web api 
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7135/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                Response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {
                // log the exception 
            }

            return View(Response);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionRequest)
        {
            var client = httpClientFactory.CreateClient();  

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7135/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionRequest),Encoding.UTF8,"application/json")
            };

            var httpResponseMessage =  await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var respnse =  await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(respnse != null)
            {
                return RedirectToAction("Index", "Regions");
            }   

            return View();
        }
    }
}   
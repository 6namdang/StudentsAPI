using Microsoft.AspNetCore.Mvc;
using Students.UI.Models;
using Students.UI.Models.DTO;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Students.UI.Controllers
{
    public class HousingsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HousingsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<HousingDto> getAllHousingResult = new List<HousingDto>();
            var client = httpClientFactory.CreateClient();
            var httpResponseMessage = await client.GetAsync("https://localhost:7292/api/housings");

            httpResponseMessage.EnsureSuccessStatusCode();
            getAllHousingResult.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<HousingDto>>());
            ViewBag.Respond = getAllHousingResult;

            return View(getAllHousingResult);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddHousingViewModel addHousingViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7292/api/housings"),
                Content = new StringContent(JsonSerializer.Serialize(addHousingViewModel), Encoding.UTF8, "application/json")

            };
            //send the reuqest message
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            //read the body now
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<HousingDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Housings");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponse = await client.GetFromJsonAsync<HousingDto>($"https://localhost:7292/api/housings/{Id.ToString()}");

            if (httpResponse is not null)
            {
                return View(httpResponse);
            }
            return View(null);
        }


        [HttpPut]
        public async Task<IActionResult> Update(HousingDto request)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7292/api/housings/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")

            };

            var httpResponse = await client.SendAsync(httpRequestMessage);

            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<HousingDto>();
            if (response is not null)
            {
                return RedirectToAction("Edit", "Housings");
            }

            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(HousingDto request)
        {

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = await client.DeleteAsync($"https://localhost:7292/api/housings/{request.Id}");
                httpRequestMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Housings");
            }
            catch (Exception)
            {

                //
                //            }
                return View("Edit");
            }

        }
    }
}

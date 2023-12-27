using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MpesaIntegration.Models;

namespace MpesaIntegration.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    public  async Task<string>GetToken(){

        var client = _clientFactory.CreateClient("mpesa");
        var authString = "XkISq6ONJd3p0pfU4S6fCOV1107ZZ7eM:APjIdaBmTJLaZEQr";
        var encodedString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authString));
        var _url = "/oauth/v1/generate?grant_type=client_credentials";

        var request = new HttpRequestMessage(HttpMethod.Get, _url);
        request.Headers.Add("Authorization", $"Basic {encodedString}");

        var response = await client.SendAsync(request);
        var mpesaResponse = await response.Content.ReadAsStringAsync();

        return mpesaResponse;

    }
   

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using POEPartI.Models;
using POEPartI.Models.ViewModels;
using POEPartI.Service;

namespace POEPartI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAzureStorageService _storageService;

        public HomeController(ILogger<HomeController> logger, IAzureStorageService storageService)
        {
            _logger = logger;
            _storageService = storageService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _storageService.GetAllEntitiesAsync<Customer>();
            var products = await _storageService.GetAllEntitiesAsync<Product>();
            var orders = await _storageService.GetAllEntitiesAsync<Order>();

            var featured = products.Take(4).ToList();

            var vm = new HomeViewModel
            {
                CustomerCount = customers?.Count ?? 0,
                ProductCount = products?.Count ?? 0,
                OrderCount = orders?.Count ?? 0,
                FeaturedProducts = featured
            };

            return View(vm); 
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
}

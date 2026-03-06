using App.UI.Models;
using App.UI.RefitItems;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.UI.Controllers
{
    public class HomeController(IDocumentsClient documentsClient) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await documentsClient.getAllDocuments();
            return View();
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

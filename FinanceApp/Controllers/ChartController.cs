using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FinanceApp.Models;
using FinanceApp.Models.IEX;

namespace FinanceApp.Controllers
{
    public class ChartController : Controller
    {
        private static IPriceRepository PriceRepository = new IEXPriceRepository();
        private IChartFactory ChartFactory = new ChartFactory(PriceRepository);

        public IActionResult Index(string stockSymbol)
        {
            if (!PriceRepository.CheckStockSymbolExists(stockSymbol))
            {
                ViewBag.ErrorMessage = "Stock symbol not found!";
            }
            else
            {
                DateTime start = new DateTime();
                DateTime end = new DateTime();                
                List<DataPoint> dataPoints = ChartFactory.GetChart(stockSymbol, start, end, ChartType.Line, PriceType.Close);
                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                double minimum = dataPoints.Min(dp => dp.Y) ?? 0;
                ViewBag.Minimum = minimum - minimum / 10;
            }
            
            ViewBag.StockSymbol = stockSymbol.ToUpper();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
                DateTime start = new DateTime(2019, 1, 8);
                DateTime end = new DateTime(2019, 10, 10);
                Chart chart = ChartFactory.GetChart(stockSymbol, start, end, ChartType.Line, PriceType.Close);

                ViewBag.Labels = JsonConvert.SerializeObject(chart.XLabels);
                ViewBag.Data = JsonConvert.SerializeObject(chart.YValues);
                ViewBag.Title = chart.Title;
                ViewBag.YAxisLabel = "$";
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

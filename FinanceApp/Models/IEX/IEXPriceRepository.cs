using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace FinanceApp.Models.IEX
{
    public class IEXPriceRepository : IPriceRepository
    {
        private const string URL = "https://www.alphavantage.co/query";
        //TODO MOVE KEY elsewhere
        private readonly string APIKEY = "AI3HJ46K28PPPTSM";
        private readonly HttpClient client;

        public IEXPriceRepository()
        {
            this.client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };
        }

        public bool CheckStockSymbolExists(string stockSymbol)
        {
        //TODO implement properly
            return true;
        }

        public List<PricePoint> GetPricesInDateRange(string stockSymbol, DateTime start, DateTime end)
        {
            HttpResponseMessage response = this.client.GetAsync(
                $"?function=TIME_SERIES_DAILY_ADJUSTED&symbol={stockSymbol}&apikey={this.APIKEY}").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBodyString = response.Content.ReadAsStringAsync().Result;
                JObject responseBodyJObject = JObject.Parse(responseBodyString);

                Dictionary<string, Dictionary<string, string>> pricesDictionary =
                    responseBodyJObject["Time Series (Daily)"].ToObject<Dictionary<string, Dictionary<string, string>>>();

                List<PricePoint> pricePoints = pricesDictionary.ToList().Select(
                        pair => new PricePoint(PriceDictToPriceStruct(pair.Value), StringToDateTime(pair.Key))
                    ).Where(pp => pp.dateTime < end & pp.dateTime > start).ToList();

                return pricePoints;

            }
            //throw Exception;
            Price price = new Price();
            DateTime dateTime = new DateTime();

            return new List<PricePoint> { new PricePoint(price, dateTime) };
        }

        private DateTime StringToDateTime(string date)
        {
            List<int> dateParts = date.Split("-").Select(Int32.Parse).ToList();
            return new DateTime(dateParts[0], dateParts[1], dateParts[2]);
        }

        private Price PriceDictToPriceStruct(Dictionary<string, string> priceDict)
        {
            Price price = new Price();
            price.close = Convert.ToDouble(priceDict["4. close"]);
            return price;
        }
    }
}

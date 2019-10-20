using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Models
{
    public class ChartFactory : IChartFactory
    {
        readonly IPriceRepository priceRepository;

        public ChartFactory(IPriceRepository priceRepository)
        {
            this.priceRepository = priceRepository;
        }

        public Chart GetChart(string stockSymbol, DateTime start, DateTime end, ChartType chartType, PriceType priceType)
        {
            List<PricePoint> pricePoints = priceRepository.GetPricesInDateRange(stockSymbol, start, end);

            switch (chartType)
            {
                case ChartType.Line:
                    switch (priceType)
                    {
                        case PriceType.Close:
                            pricePoints = pricePoints.OrderBy(pricePoint => ConvertDateToEpoch(pricePoint.dateTime)).ToList();
                            return new Chart(pricePoints.Select(pp => pp.dateTime.ToString("dd/MM/yyyy")).ToArray(),
                                             pricePoints.Select(pp => pp.price.close ?? 0).ToArray());
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private long ConvertDateToEpoch(DateTime dateTime)
        {
            return dateTime.Ticks;
        }
    }
}

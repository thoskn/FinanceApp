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

        public List<DataPoint> GetChart(string stockSymbol, DateTime start, DateTime end, ChartType chartType, PriceType priceType)
        {
            List<PricePoint> pricePoints = priceRepository.GetPricesInDateRange(stockSymbol, start, end);

            switch (chartType)
            {
                case ChartType.Line:
                    switch (priceType)
                    {
                        case PriceType.Close:
                            return pricePoints.OrderBy(pricePoint => ConvertDateToEpoch(pricePoint.dateTime))
                                              .Select((pricePoint, index) => new DataPoint(index, pricePoint.price.close ?? 0))
                                              .ToList();
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

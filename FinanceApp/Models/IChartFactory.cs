using System;
using System.Collections.Generic;

namespace FinanceApp.Models
{
    public interface IChartFactory
    {
        Chart GetChart(string stockSymbol, DateTime start, DateTime end, ChartType chartType, PriceType priceType);
    }
}
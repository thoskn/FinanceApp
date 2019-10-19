using System;
using System.Collections.Generic;

namespace FinanceApp.Models
{
    public interface IPriceRepository
    {
        List<PricePoint> GetPricesInDateRange(string stockSymbol, DateTime start, DateTime end);

        bool CheckStockSymbolExists(string stockSymbol);
    }
}

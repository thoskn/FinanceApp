using System;
namespace FinanceApp.Models
{
    public struct PricePoint
    {
        public Price price;
        public DateTime dateTime;

        public PricePoint(Price price, DateTime dateTime)
        {
            this.price = price;
            this.dateTime = dateTime;
        }
    }
}

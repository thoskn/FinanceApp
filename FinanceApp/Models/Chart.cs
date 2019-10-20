using System;

namespace FinanceApp.Models
{
    public struct Chart
    {
        //Using arrays rather than list because they are both fixed in an order
        //that shouldn't be changed.
        public string[] XLabels;
        public double[] YValues;
        public string Title;

        public Chart(string[] x, double[] y)
        {
            XLabels = x;
            YValues = y;
            Title = "";
        }

        public Chart(string[] x, double[] y, string title)
        {
            XLabels = x;
            YValues = y;
            Title = title;
        }
    }
}
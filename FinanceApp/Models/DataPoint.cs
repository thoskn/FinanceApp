using System;
using System.Runtime.Serialization;
namespace FinanceApp.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        [DataMember(Name = "x")]
        public double? X;

        [DataMember(Name = "y")]
        public double? Y;
    }
}

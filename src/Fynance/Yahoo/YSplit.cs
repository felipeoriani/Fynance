namespace Fynance.Yahoo
{
    internal class YSplitResponse
    {
        public double date { get; set; }

        public double numerator { get; set; }

        public double denominator { get; set; }

        public string splitRatio { get; set; }
    }
}
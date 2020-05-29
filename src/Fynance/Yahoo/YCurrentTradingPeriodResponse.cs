namespace Fynance.Yahoo
{
    internal class YCurrentTradingPeriodResponse
    {
        public YPreResponse pre { get; set; }

        public YRegularResponse regular { get; set; }

        public YPostResponse post { get; set; }
    }
}
using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YEventResponse
    {
        public Dictionary<double, YDividendResponse> dividends { get; set; }

        public Dictionary<double, YSplitResponse> splits { get; set; }
    }
}
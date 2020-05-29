using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YIndicatorResponse
    {
        public IList<YQuoteResponse> quote { get; set; }

        public IList<YIndicatorAdjCloseResponse> adjclose { get; set; }
    }
}
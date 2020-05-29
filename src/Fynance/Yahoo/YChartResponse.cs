using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YChartResponse
    {
        public IList<YResultResponse> result { get; set; }

        public YErrorResponse error { get; set; }
    }
}
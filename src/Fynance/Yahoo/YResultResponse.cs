using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YResultResponse
    {
        public YMetaResponse meta { get; set; }
        
        public IList<double?> timestamp { get; set; }
        
        public YIndicatorResponse indicators { get; set; }

        public YEventResponse events { get; set; }
    }
}
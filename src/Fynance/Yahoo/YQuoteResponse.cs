using System.Collections.Generic;

namespace Fynance.Yahoo
{
    internal class YQuoteResponse
    {
        public IList<double?> low { get; set; }
        
        public IList<double?> volume { get; set; }
        
        public IList<double?> close { get; set; }
        
        public IList<double?> open { get; set; }
        
        public IList<double?> high { get; set; }
    }
}
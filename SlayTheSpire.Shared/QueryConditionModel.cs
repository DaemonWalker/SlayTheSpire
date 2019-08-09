using System;
using System.Collections.Generic;
using System.Text;

namespace SlayTheSpire.Shared
{
    public class QueryConditionModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Color { get; set; } = 0;
        public int Rarity { get; set; } = 0;
        public int CardType { get; set; } = 0;
        public string Flavor { get; set; } = string.Empty;
    }
}

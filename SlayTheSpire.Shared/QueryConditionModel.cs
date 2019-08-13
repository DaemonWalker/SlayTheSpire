using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SlayTheSpire.Shared
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QueryConditionModel
    {
        [JsonProperty]
        public string Name { get; set; } = string.Empty;

        [JsonProperty]
        public string Description { get; set; } = string.Empty;

        [JsonProperty]
        public int Color { get; set; } = 0;

        [JsonProperty]
        public int Rarity { get; set; } = 0;

        [JsonProperty]
        public int CardType { get; set; } = 0;

        [JsonProperty]
        public string Flavor { get; set; } = string.Empty;
    }
}

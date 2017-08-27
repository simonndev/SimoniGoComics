using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NewestFeaturesContract
    {
        [JsonProperty("feed_name")]
        public string FeedName { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("features")]
        public List<FeatureContract> Features { get; set; }
    }
}

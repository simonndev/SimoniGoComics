using Newtonsoft.Json;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SortableFeatureContract
    {
        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("feature")]
        public FeatureBase Feature { get; set; }
    }
}

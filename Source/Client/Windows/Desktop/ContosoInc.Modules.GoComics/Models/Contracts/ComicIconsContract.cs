using Newtonsoft.Json;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ComicIconsContract
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("mid")]
        public string Medium { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("tiny")]
        public string Tiny { get; set; }
    }
}

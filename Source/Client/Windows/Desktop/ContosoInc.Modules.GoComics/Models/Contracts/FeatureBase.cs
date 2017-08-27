using Newtonsoft.Json;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FeatureBase
    {
        /// <summary>
        /// Eg. 32
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Eg. Calvin and Hobbes
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Eg. Bill Watterson
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Eg. http://avatar.amuniversal.com/feature_avatars/avatars_large/features/ch/tiny_avatar.png
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("icons")]
        public ComicIconsContract Icons { get; set; }

        [JsonProperty("political_slant")]
        public bool? IsPoliticalSlant { get; set; }
    }
}

using Newtonsoft.Json;
using System;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FeatureItemContract
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("rights")]
        public string Copyrights { get; set; }

        [JsonProperty("gocomics_link")]
        public string GoComicsLink { get; set; }

        [JsonProperty("feature_link")]
        public string FeatureLink { get; set; }

        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("feature_code")]
        public string FeatureCode { get; set; }

        [JsonProperty("sort_name")]
        public string ShortName { get; set; }

        [JsonProperty("current_link")]
        public string CurrentLink { get; set; }

        [JsonProperty("previous_link")]
        public string PreviousLink { get; set; }

        [JsonProperty("next_link")]
        public string NextLink { get; set; }

        [JsonProperty("image_link")]
        public string ImageLink { get; set; }

        [JsonProperty("webready_link")]
        public string WebreadyLink { get; set; }

        [JsonProperty("image_width")]
        public int ImageWidth { get; set; }

        [JsonProperty("image_height")]
        public int ImageHeight { get; set; }

        [JsonProperty("display_date")]
        public string DisplayDateString { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDateString { get; set; }

        [JsonProperty("updated")]
        public DateTime UpdatedDate { get; set; }
    }
}

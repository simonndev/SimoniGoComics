using Newtonsoft.Json;
using System;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FeatureContract : FeatureBase
    {
        [JsonProperty("sort_title")]
        public string SortTitle { get; set; }

        [JsonProperty("permalink_title")]
        public string PermalinkTitle { get; set; }

        [JsonProperty("feature_code")]
        public string FeatureCode { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("gocomics_link")]
        public string GoComicsLink { get; set; }

        /// <summary>
        /// Eg. 2015-08-16
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime StartedDate { get; set; }

        /// <summary>
        /// Eg. 2015-08-14T10:44:28-05:00
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

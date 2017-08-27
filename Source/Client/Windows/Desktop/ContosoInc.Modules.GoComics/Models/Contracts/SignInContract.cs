using Newtonsoft.Json;
using System;

namespace ContosoInc.Modules.GoComics.Models.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SignInContract
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 2017-08-27T10:30:06.528Z
        /// </summary>
        [JsonProperty("token_expires")]
        public DateTime? TokenExpiresOn { get; set; }

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        [JsonProperty("avatar_large")]
        public string LargeAvatarUrl { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Short date string that tells when this user first registered on.
        /// Eg. 2013-05-03
        /// </summary>
        [JsonProperty("member_since")]
        public string MemberSince { get; set; }

        [JsonProperty("activation_date")]
        public DateTime ActivationDate { get; set; }

        [JsonProperty("gocomics_url")]
        public string GoComicsProfileUrl { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("pro")]
        public bool IsPro { get; set; }

        [JsonProperty("pro_expires")]
        public DateTime? ProExpiresOn { get; set; }

        [JsonProperty("auto_renew")]
        public bool AutoRenew { get; set; }

        [JsonProperty("token")]
        public int AccessToken { get; set; }
    }
}

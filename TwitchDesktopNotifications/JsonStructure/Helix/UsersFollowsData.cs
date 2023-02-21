using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    public class UsersFollowsData
    {
        public UsersFollowsData() { }

        [JsonPropertyName("from_id")]
        public string FromID { get; set; }

        [JsonPropertyName("from_login")]
        public string FromUserName { get; set; }

        [JsonPropertyName("from_name")]
        public string FromDisplayName { get; set; }

        [JsonPropertyName("to_id")]
        public string ToID { get; set; }

        [JsonPropertyName("to_login")]
        public string ToUserName { get; set; }

        [JsonPropertyName("to_name")]
        public string ToDisplayName { get; set; }

        [JsonPropertyName("followed_at")]
        public string FollowedISODateTime { get; set; }

    }
}

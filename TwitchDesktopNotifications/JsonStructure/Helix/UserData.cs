using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    public class UserData
    {
        public UserData() { }

        [JsonPropertyName("id")]
        public string UserId { get; set; }

        [JsonPropertyName("login")]
        public string UserName { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("broadcaster_type")]
        public string BroadcasterType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("profile_image_url")]
        public string ProfileImage { get; set; }

        [JsonPropertyName("offline_image_url")]
        public string OfflineImage { get; set; }

        [JsonPropertyName("view_count")]
        public int TotalViewers { get; set; }

        [JsonPropertyName("email")]
        public string EMail { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedISODate { get; set; }
    }
}

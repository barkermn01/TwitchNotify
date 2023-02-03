using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchDesktopNotifications.JsonStructure.Helix;

namespace TwitchDesktopNotifications.JsonStructure
{
    internal class Store
    {
        public Store() { }

        [JsonPropertyName("authentication")]
        public Authentication Authentication { get; set; }

        [JsonPropertyName("user_data")]
        public UserData UserData { get; set; }

        [JsonPropertyName("notifications_for")]
        public SteamersToNotify SteamersToNotify { get; set; };
    }
}

using System.Text.Json.Serialization;
using TwitchDesktopNotifications.Core;

namespace TwitchDesktopNotifications.JsonStructure
{
    public class SteamersToIgnore
    {
        [JsonPropertyName("IgnoredStreamers")]
        public List<UIStreamer> Streamers { get; set; } = new List<UIStreamer>();
    }
}

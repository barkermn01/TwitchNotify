using System.Text.Json.Serialization;

namespace TwitchDesktopNotifications.JsonStructure
{
    internal class SteamersToNotify
    {
        [JsonPropertyName("notifyAll")]
        public Boolean notifyAll = true;

        [JsonPropertyName("notify")]
        public List<String> Streamers { get; set; } = new List<String>();
    }
}

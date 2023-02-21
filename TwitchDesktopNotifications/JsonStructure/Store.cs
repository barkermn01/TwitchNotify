using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchDesktopNotifications.JsonStructure.Helix;

namespace TwitchDesktopNotifications.JsonStructure
{
    public class Store
    {
        public Store() { }

        [JsonPropertyName("ignore")]
        public SteamersToIgnore ignore { get; set; }

        [JsonPropertyName("authentication")]
        public Authentication Authentication { get; set; }

        [JsonPropertyName("user_data")]
        public UserData UserData { get; set; }

        [JsonIgnore]
        public SteamersToIgnore SteamersToIgnore { 
            get { 
                if(ignore == null) { ignore = new SteamersToIgnore(); }
                return ignore;
            } 
            set { 
                ignore = value;
            } 
        } 
    }
}

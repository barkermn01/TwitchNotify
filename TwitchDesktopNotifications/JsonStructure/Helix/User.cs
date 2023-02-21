using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    public class User
    {
        public User() { }

        [JsonPropertyName("data")]
        public List<UserData> Data { get; set; }
    }
}

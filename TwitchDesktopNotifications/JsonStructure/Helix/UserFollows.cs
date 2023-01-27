using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    internal class UserFollows
    {
        public UserFollows() { }

        [JsonPropertyName("data")]
        public List<UsersFollowsData> results { get; set; }

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }
}

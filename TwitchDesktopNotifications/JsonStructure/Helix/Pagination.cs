using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    internal class Pagination
    {
        public Pagination() { }

        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure.Helix
{
    public class Streams
    {
        public Streams() { }

        [JsonPropertyName("data")]
        public List<StreamsData> Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.JsonStructure
{
    internal class Authentication
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresSeconds { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public List<string> Scopes { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expiresAt")]
        public long ExpiresAt { get; set; }

        [JsonIgnore]
        public DateTime ExpiresAsDate { 
            get
            {
                return (new DateTime(1970, 1, 1)).AddMilliseconds(ExpiresAt);
            } 
            set
            {
                DateTime unixStart = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
                ExpiresAt = (long)Math.Floor((value.ToUniversalTime() - unixStart).TotalMilliseconds);
            }
        }
    }
}

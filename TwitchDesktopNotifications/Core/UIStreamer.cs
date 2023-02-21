using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TwitchDesktopNotifications.Core
{
    public class UIStreamer
    {
        public UIStreamer() {
        }

        public static UIStreamer GetCreateStreamer(string name)
        {

            if (DataStore.GetInstance().Store.SteamersToIgnore == null)
            {
                DataStore.GetInstance().Store.SteamersToIgnore = new JsonStructure.SteamersToIgnore();
            }
            if (DataStore.GetInstance().Store.SteamersToIgnore.Streamers == null)
            {
                DataStore.GetInstance().Store.SteamersToIgnore.Streamers = new List<UIStreamer>();
            }

            UIStreamer strmr = null;
            try
            {
                strmr = DataStore.GetInstance().Store.SteamersToIgnore.Streamers.Where((UIStreamer strmr) => strmr.Name == name).First();
            }
            catch { }
            finally
            {

                if (strmr == null)
                {
                    strmr = new UIStreamer() { IsIgnored = false, Name = name };
                    DataStore.GetInstance().Store.SteamersToIgnore.Streamers.Add(strmr);
                    DataStore.GetInstance().Save();
                }
            }
            return strmr;
        }

        [JsonIgnore]
        public List<UIStreamer> StreamersToIgnore
        {
            get
            {
                return DataStore.GetInstance().Store.SteamersToIgnore.Streamers;
            }
        }


        [JsonPropertyName("IsIgnored")]
        public bool IsIgnored { get; set; } = false;
        [JsonPropertyName("StreamerName")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Link { 
            get {
                return String.Format("https://www.twitch.tv/{0}", Name);
            } 
        }
    }
}

using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TwitchDesktopNotifications.JsonStructure.Helix;

namespace TwitchDesktopNotifications.Core
{
    internal class NotifyManager
    {

        public static Boolean ShouldNotify(String streamerName)
        {
            if(DataStore.GetInstance().Store.SteamersToIgnore == null || DataStore.GetInstance().Store.SteamersToIgnore.Streamers == null) { 
                return true;
            }
            return !UIStreamer.GetCreateStreamer(streamerName).IsIgnored;
        }

        public static void AddStreamerToIgnoreList(string streamerName)
        {
            UIStreamer.GetCreateStreamer(streamerName).IsIgnored = true;
            DataStore.GetInstance().Save();
        }
    }
}

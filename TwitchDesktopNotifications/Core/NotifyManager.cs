using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDesktopNotifications.JsonStructure.Helix;

namespace TwitchDesktopNotifications.Core
{
    internal class NotifyManager
    {

        public Boolean shouldNotify(String streamerName)
        {
            return notifyAll || !DataStore.GetInstance().Store.SteamersToNotify.Streamers.Contains(streamerName);
        }

        public void AddStreamerToNotifyList(String streamerName)
        {
            DataStore.GetInstance().Store.SteamersToNotify.Streamers.Add(streamerName);
            DataStore.GetInstance().Save();
        }

        public void RemoveStreamerToNotifyList(String streamerName)
        {
            DataStore.GetInstance().Store.SteamersToNotify.Streamers.Remove(streamerName);
            DataStore.GetInstance().Save();
        }

        public void ClearListOfEnabled()
        {
            DataStore.GetInstance().Store.SteamersToNotify.Streamers = new List<string>();
            DataStore.GetInstance().Save();
        }

        public bool notifyAll
        {
            get
            {
                return DataStore.GetInstance().Store.SteamersToNotify.notifyAll;
            }
            set { 
                DataStore.GetInstance().Store.SteamersToNotify.notifyAll = value;
                DataStore.GetInstance().Save();
            }
        }
    }
}

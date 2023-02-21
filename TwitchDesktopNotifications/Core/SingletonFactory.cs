using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.Core
{
    public interface Singleton
    {

    }
    public class SingletonFactory<T> where T : Singleton, new()
    {
        public static Dictionary<String, Singleton> store = new Dictionary<String, Singleton>();

        public static T GetInstance(){
            string name = typeof(T).FullName;
            if (!store.ContainsKey(name))
            {
                store.Add(name, new T());
            }
            return (T)store[name];
        }
    }
}

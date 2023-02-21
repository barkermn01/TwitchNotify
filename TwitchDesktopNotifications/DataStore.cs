using System.Text.Json;
using TwitchDesktopNotifications.JsonStructure;
using System.IO;
using TwitchDesktopNotifications.Core;

namespace TwitchDesktopNotifications
{
    public class DataStore : SingletonFactory<DataStore>, Singleton
    {
        private Store _store;
        public JsonStructure.Store Store {
            get {
                if (_store == null)
                {
                    Load();
                }
                return _store;
            }
            private set {
                _store = value;
            }
        }

        public bool isLoaded { get; private set; }

        public void Save()
        {
            String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");
            String FileName = "store.json";

            string fileContent = JsonSerializer.Serialize<JsonStructure.Store>(Store);

            Directory.CreateDirectory(FilePath);
            File.WriteAllText(FilePath + "/" + FileName, fileContent);
        }

        public void Load() {
            if (!isLoaded)
            {
                String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");
                String FileName = "store.json";

                Directory.CreateDirectory(FilePath);

                if (File.Exists(FilePath + "/" + FileName))
                {

                    string fileContent = File.ReadAllText(FilePath + "/" + FileName);
                    Store = JsonSerializer.Deserialize<JsonStructure.Store>(fileContent);
                }
                else
                {
                    Store = new JsonStructure.Store();
                }
                isLoaded = true;
            }
        }
    }
}

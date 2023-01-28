using System.Text.Json;
using TwitchDesktopNotifications.JsonStructure;
using System.IO;

namespace TwitchDesktopNotifications
{
    internal class DataStore
    {
        private DataStore() { }

        public static DataStore Instance { get; private set; }
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

        public static DataStore GetInstance()
        {
            if(Instance == null)
            {
                Instance = new DataStore();
            }
            return Instance;
        }

        public void Save()
        {
            String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");
            String FileName = "store.json";

            string fileContent = JsonSerializer.Serialize<JsonStructure.Store>(Store);

            Console.WriteLine("I'm trying to save:");
            Console.WriteLine(fileContent);
            Console.WriteLine("to {0}", FilePath + "/" + FileName);
            Directory.CreateDirectory(FilePath);
            File.WriteAllText(FilePath + "/" + FileName, fileContent);
        }

        public void Load() {
            String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");
            String FileName = "store.json";

            Directory.CreateDirectory(FilePath);

            if(File.Exists(FilePath+"/"+ FileName)) {

                string fileContent = File.ReadAllText(FilePath+"/"+ FileName);
                Store = JsonSerializer.Deserialize<JsonStructure.Store>(fileContent);
            }
            else
            {
                Store = new JsonStructure.Store();
            }
            isLoaded= true;
        }
    }
}

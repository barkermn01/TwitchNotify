using System.Windows;
using System.Windows.Forms;
using TwitchDesktopNotifications.Core;

namespace TwitchDesktopNotifications
{
    /// <summary>
    /// Interaction logic for ManageIgnores.xaml
    /// </summary>
    public partial class ManageIgnores : Window
    {
        private bool updated = false;
        public ManageIgnores()
        {
            InitializeComponent();
        }

        List<UIStreamer> StreamersToIgnore = DataStore.GetInstance().Store.SteamersToIgnore.Streamers;

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            updated= true;
        }
    }
}

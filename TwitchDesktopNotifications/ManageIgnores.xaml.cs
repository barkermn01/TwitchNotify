using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Threading;
using TwitchDesktopNotifications.Core;

namespace TwitchDesktopNotifications
{
    /// <summary>
    /// Interaction logic for ManageIgnores.xaml
    /// </summary>
    public partial class ManageIgnores : Window
    {
        public ManageIgnores()
        {
            InitializeComponent();
        }

        List<UIStreamer> StreamersToIgnore = DataStore.GetInstance().Store.SteamersToIgnore.Streamers;

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgrdIgnore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => dgrdIgnore.UnselectAll()));
        }

        private void HyperLink_Click(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)e.OriginalSource).NavigateUri.OriginalString;

            var psi = new ProcessStartInfo
            {
                FileName = link,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}

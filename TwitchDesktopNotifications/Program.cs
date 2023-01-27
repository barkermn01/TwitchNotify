// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using TwitchDesktopNotifications;
using TwitchDesktopNotifications.Core;
using TwitchDesktopNotifications.JsonStructure;

internal class Program
{

    static bool isConnecting = false;
    static WebServer ws = WebServer.GetInstance();

    private static NotifyIcon notifyIcon;
    private static ContextMenuStrip cms;


    public static void Ws_CodeRecived(object? sender, EventArgs e)
    {
        ws.CodeRecived -= Ws_CodeRecived;

        string response = TwitchFetcher.GetInstance().endConnection(((WebServer)sender).TwitchCode);

        if (!DataStore.GetInstance().isLoaded)
        {
            DataStore.GetInstance().Load();
        }

        DataStore.GetInstance().Store.Authentication = JsonSerializer.Deserialize<Authentication>(response);

        DateTime unixStart = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
        DataStore.GetInstance().Store.Authentication.ExpiresAt = (long)Math.Floor((DateTime.Now.AddSeconds(DataStore.GetInstance().Store.Authentication.ExpiresSeconds) - unixStart).TotalMilliseconds);
        DataStore.GetInstance().Save();

        isConnecting = false;
        ws.Stop();
    }

    protected static void Reconnect_Click(object? sender, System.EventArgs e)
    {
        TriggerAuthentication();
    }

    protected static void Quit_Click(object? sender, System.EventArgs e)
    {
        notifyIcon.Visible = false;
        notifyIcon.Dispose();
        Environment.Exit(0);
    }

    private async static void TriggerAuthentication()
    {
        ws.CodeRecived += Ws_CodeRecived;
        ws.Start();
        isConnecting = true;
        TwitchFetcher.GetInstance().BeginConnection();
        if (DataStore.GetInstance().Store.Authentication == null)
        {
            var timerForCrash = new PeriodicTimer(TimeSpan.FromSeconds(10));
            await timerForCrash.WaitForNextTickAsync();
            if (isConnecting)
            {
                MessageBox.Show("Twitch Connection not authenticated Exiting for saftey.", "Twitch Notify");
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                Environment.Exit(1);
            }
        }
    }

    private static async Task Main(string[] args)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        notifyIcon = new NotifyIcon();
        notifyIcon.Icon = new Icon("Assets/icon.ico");
        notifyIcon.Text = "Notify";

        cms = new ContextMenuStrip();

        cms.Items.Add(new ToolStripMenuItem("Reconnect", null, new EventHandler(Reconnect_Click)));
        cms.Items.Add(new ToolStripSeparator());
        cms.Items.Add(new ToolStripMenuItem("Quit", null, new EventHandler(Quit_Click), "Quit"));

        notifyIcon.ContextMenuStrip = cms;
        notifyIcon.Visible = true;

        if (DataStore.GetInstance().Store.Authentication == null)
        {
            TriggerAuthentication();
        }

        new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(10000);
                TwitchFetcher.GetInstance().GetLiveFollowingUsers();
            }
        }).Start();

        Application.Run();

    }
}
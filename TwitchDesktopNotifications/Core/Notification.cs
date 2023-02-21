using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Foundation.Collections;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.Notifications;
using System.Net.Http;

namespace TwitchDesktopNotifications.Core
{
    public class Notification : SingletonFactory<Notification>, Singleton
    {
        private WebClient webClient = new WebClient();
        public Notification() {
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                try
                {
                    if ( 
                        // action is defined and set to watch
                        ( args.Contains("action") && args["action"] == "watch" )
                    ||
                        // action is not defined so the user just generally clicked on the notification
                        !args.Contains("action")
                    ){
                        Process myProcess = new Process();
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = args["streamerUrl"];
                        myProcess.Start();
                    }else if( args.Contains("action") && args["action"] == "ignore")
                    {
                        NotifyManager.AddStreamerToIgnoreList(args["streamerName"]);
                    }
                }catch(Exception ex) {
                    Logger.GetInstance().Writer.WriteLine(ex.ToString());
                }
            };
        }

        public void sendNotification(String streamerName, String streamerUrl, String profilePic, String streamThumbnail, String title)
        {
            String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");

            streamThumbnail = streamThumbnail.Replace("{width}", 260.ToString()).Replace("{height}", 147.ToString());

            // download there profile picture
            string fileNameProfilePic = profilePic.Split("/").Last();
            if (!File.Exists(FilePath + "/" + fileNameProfilePic))
            {
                webClient.DownloadFile(new Uri(profilePic), FilePath + "/" + fileNameProfilePic);
            }

            // download there profile picture
            string fileNameThumbnailPic = streamThumbnail.Split("/").Last();
            webClient.DownloadFile(new Uri(streamThumbnail), 
                FilePath + "/" + fileNameThumbnailPic
            );

            if (NotifyManager.ShouldNotify(streamerName))
            {
                var builder = new ToastContentBuilder()
                    .AddArgument("streamerUrl", streamerUrl)
                    .AddArgument("streamerName", streamerName)
                    .AddArgument("thumbnail_path", FilePath + "/" + fileNameThumbnailPic)
                    .AddText(streamerName + " is now live on Twitch")
                    .AddHeroImage(new Uri("file://" + (FilePath + "/" + fileNameThumbnailPic).Replace("\\", "/")))
                    .AddAppLogoOverride(new Uri("file://" + (FilePath + "/" + fileNameProfilePic).Replace("\\", "/")), ToastGenericAppLogoCrop.Circle)
                    .AddButton(new ToastButton()
                        .SetContent("Watch ")
                        .AddArgument("action", "watch")
                        .SetBackgroundActivation())
                    .AddButton(new ToastButton()
                        .SetContent("Dismiss")
                        .AddArgument("action", "nothing")
                        .SetBackgroundActivation())
                    .AddButton(new ToastButton()
                        .SetContent("Ignore")
                        .AddArgument("action", "ignore")
                        .SetBackgroundActivation());

                if (title != "")
                {
                    builder.AddText(title);
                }
                builder.Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddSeconds(15);
                    toast.Dismissed += (ToastNotification sender, ToastDismissedEventArgs args) =>
                    {
                        try
                        {
                            File.Delete(FilePath + "/" + fileNameThumbnailPic);
                        }
                        catch (Exception) { }
                        builder = null;
                    };
                    toast.Activated += (ToastNotification sender, object args) =>
                    {
                        try
                        {
                            File.Delete(FilePath + "/" + fileNameThumbnailPic);
                        }
                        catch (Exception) { }
                        builder = null;
                    };
                });
            }
        }
    }
}

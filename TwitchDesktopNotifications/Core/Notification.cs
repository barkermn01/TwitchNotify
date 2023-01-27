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

namespace TwitchDesktopNotifications.Core
{
    internal class Notification
    {
        private Notification() {
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                try
                {
                    Process myProcess = new Process();
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = args["streamerUrl"];
                    myProcess.Start();
                }catch(Exception ex) { }
            };
        }

        private static Notification Instance;
        
        public static Notification GetInstance()
        {
            if(Instance == null)
            {
                Instance = new Notification();
            }
            return Instance;
        }

        public void sendNotification(String streamerName, String streamerUrl, String profilePic, String streamThumbnail, String title)
        {
            String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");

            streamThumbnail = streamThumbnail.Replace("{width}", 260.ToString()).Replace("{height}", 147.ToString());

            // download there profile picture
            string fileNameProfilePic = profilePic.Split("/").Last();
            (new WebClient()).DownloadFile(new Uri(profilePic), FilePath+"/"+ fileNameProfilePic);

            // download there profile picture
            string fileNameThumbnailPic = streamThumbnail.Split("/").Last();
            (new WebClient()).DownloadFile(new Uri(streamThumbnail), 
                FilePath + "/" + fileNameThumbnailPic
            );

            var builder = new ToastContentBuilder()
                .AddArgument("streamerUrl", streamerUrl)
                .AddText(streamerName + " is now live on Twitch")
                .AddHeroImage(new Uri("file://" + (FilePath + "/" + fileNameThumbnailPic).Replace("\\", "/")))
                .AddAppLogoOverride(new Uri("file://" + (FilePath + "/" + fileNameProfilePic).Replace("\\", "/")), ToastGenericAppLogoCrop.Circle)
                .AddButton(new ToastButton()
                    .SetContent("Watch " + streamerName)
                    .AddArgument("action", "watch")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Dismiss")
                    .AddArgument("action", "nothing")
                    .SetBackgroundActivation());

            if(title != "") {
                builder.AddText(title);
            }
            builder.Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now.AddSeconds(15);
            });
        }


    }
}

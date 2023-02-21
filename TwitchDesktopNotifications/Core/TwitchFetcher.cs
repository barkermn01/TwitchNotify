using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using TwitchDesktopNotifications.JsonStructure;
using TwitchDesktopNotifications.JsonStructure.Helix;

namespace TwitchDesktopNotifications.Core
{
    internal class TwitcherRefreshException : Exception
    {
        public TwitcherRefreshException(string? message, Exception? innerException) : base(message, innerException) { 
        }
    }
    internal class TwitchFetcher
    {
        private TwitchFetcher() {
        }

        ReconnectionNeeded rnFrm;

        public void OpenFailedNotification()
        {
            if (rnFrm == null)
            {
                rnFrm = new ReconnectionNeeded();
            }
            if (rnFrm.IsActive)
            {
                rnFrm.Show();
            }
        }

        public static TwitchFetcher instance { get; private set; }

        List <StreamsData> currentlyLive = null;

        public string guid { get; private set; }

        public static TwitchFetcher GetInstance()
        {
            if(instance == null)
            {
                instance = new TwitchFetcher();
            }
            return instance;
        }

        private byte[] buildPostData(Dictionary<string, string> postData)
        {
            string content = "";
            foreach (var pair in postData)
            {
                content += HttpUtility.UrlEncode(pair.Key) + "=" + HttpUtility.UrlEncode(pair.Value) + "&";
            }
            content = content.TrimEnd('&');
            return Encoding.UTF8.GetBytes(content);
        }

        private T MakeRequest<T>(string endpoint)
        {
            if (DataStore.GetInstance().Store == null)
            {
                throw new Exception("Not Authenticated");
            }
            if (DataStore.GetInstance().Store.Authentication.ExpiresAsDate <= DateTime.UtcNow)
            {
                Refresh();
            }

            try
            {
                WebRequest request = WebRequest.Create("https://api.twitch.tv/" + endpoint);
                request.Method = "GET";
                request.Headers[HttpRequestHeader.Authorization] = String.Format("Bearer {0}", DataStore.GetInstance().Store.Authentication.AccessToken);
                request.Headers["Client-ID"] = TwitchDetails.TwitchClientID;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return JsonSerializer.Deserialize<T>(responseFromServer);
            }
            catch (TwitcherRefreshException ex)
            {
                OpenFailedNotification();
            }
            catch(Exception ex)
            {
                Logger.GetInstance().Writer.WriteLineAsync(ex.ToString());
            }
            return default(T);
        }

        public void FetchCurrentUser()
        {
            try
            {
                var UserList = MakeRequest<User>("helix/users");
                if (UserList.Data.Count > 0) {
                    DataStore.GetInstance().Store.UserData = UserList.Data[0];
                    DataStore.GetInstance().Save();
                }
            }
            catch (TwitcherRefreshException ex)
            {
                OpenFailedNotification();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Writer.WriteLineAsync(ex.ToString());
            }
        }

        public UserData FetchUserData(string user_id)
        {
            try
            {
                var Response = MakeRequest<User>("helix/users?id=" + user_id);
                if (Response.Data.Count > 0)
                {
                    return Response.Data[0];
                }
            }
            catch (TwitcherRefreshException ex)
            {
                OpenFailedNotification();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Writer.WriteLineAsync(ex.ToString());
            }
            return null;
        }

        public void GetLiveFollowingUsers()
        {
            try
            {
                bool isFinished = false;
                if (DataStore.GetInstance().Store.UserData == null)
                {
                    FetchCurrentUser();
                }

                string QueryUrl = "helix/streams/followed?first=100&user_id=" + DataStore.GetInstance().Store.UserData.UserId;
                Streams following = MakeRequest<Streams>(QueryUrl);

                if (following != null && currentlyLive != null)
                {
                    following.Data.ForEach(x =>
                    {
                        bool found = false;

                        foreach (StreamsData sd in currentlyLive)
                        {
                            if (sd.UserId == x.UserId) found = true;
                        }

                        if (!found)
                        {
                            UserData streamer = FetchUserData(x.UserId);
                            UIStreamer.GetCreateStreamer(x.DisplayName);
                            Notification.GetInstance().sendNotification(streamer.DisplayName, "https://twitch.tv/" + streamer.UserName, streamer.ProfileImage, x.ThumbnailImg, x.Title);
                        }
                    });
                }

                currentlyLive = following.Data;
            }
            catch (TwitcherRefreshException ex)
            {
                OpenFailedNotification();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Writer.WriteLineAsync(ex.ToString());
            }
        }

        public void Refresh()
        {
            try
            {
                Dictionary<string, string> postData = new Dictionary<string, string>();

                postData["client_id"] = TwitchDetails.TwitchClientID;
                postData["client_secret"] = TwitchDetails.TwitchClientSecret;
                postData["grant_type"] = "refresh_token";
                postData["refresh_token"] = DataStore.GetInstance().Store.Authentication.RefreshToken;

                byte[] byteArray = buildPostData(postData);

                WebRequest request = WebRequest.Create("https://id.twitch.tv/oauth2/token");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                DataStore.GetInstance().Store.Authentication = JsonSerializer.Deserialize<Authentication>(responseFromServer);
                DateTime unixStart = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
                DataStore.GetInstance().Store.Authentication.ExpiresAt = (long)Math.Floor((DateTime.Now.AddSeconds(DataStore.GetInstance().Store.Authentication.ExpiresSeconds) - unixStart).TotalMilliseconds);
                DataStore.GetInstance().Save();
            }catch(Exception e)
            {
                throw new TwitcherRefreshException("Unable to refresh", e);
            }
        }

        async public void BeginConnection()
        {
            guid = Guid.NewGuid().ToString();
            WebServer.GetInstance().TwitchState = guid;
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = String.Format("https://id.twitch.tv/oauth2/authorize?&redirect_uri=http://localhost:32584/twitchRedirect&scope=user:read:subscriptions%20user:read:follows%20user:read:email%20openid&response_type=code&state={0}&nonce={1}&client_id={2}", guid, guid, TwitchDetails.TwitchClientID);
            myProcess.Start();
        }

        public string endConnection(string code)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();

            postData["client_id"] = TwitchDetails.TwitchClientID;
            postData["client_secret"] = TwitchDetails.TwitchClientSecret;
            postData["grant_type"] = "authorization_code";
            postData["redirect_uri"] = "http://localhost:32584/twitchRedirect";
            postData["code"] = code;

            byte[] byteArray = buildPostData(postData);

            WebRequest request = WebRequest.Create("https://id.twitch.tv/oauth2/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}

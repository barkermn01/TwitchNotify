using System.Net;
using System.Text;
using System.Web;
using System.IO;

namespace TwitchDesktopNotifications.Core
{
    internal class WebServer
    {
        public int Port = 32584;

        private HttpListener listener;

        private static WebServer Instance;

        public static WebServer GetInstance()
        {
            if(Instance == null)
            {
                Instance= new WebServer();
            }
            return Instance;
        }

        public String TwitchCode { get; private set; }
        public String TwitchState { get; set; }

        public void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:" + Port.ToString() + "/");
            listener.Prefixes.Add("http://localhost:" + Port.ToString() + "/");
            listener.Start();
            new Thread(new ThreadStart(ThreadManagedServer)).Start();
        }

        public event EventHandler CodeRecived;

        public void Stop()
        {
            listener.Stop();
        }

        private void RespondConnection(HttpListenerRequest request, HttpListenerResponse response)
        {
            var query = HttpUtility.ParseQueryString(request.Url.Query);
            if (request.HttpMethod == "GET" && query["state"] == this.TwitchState)
            {
                this.TwitchCode = query["code"]; 
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentType = "text/html";
                response.OutputStream.Write(Encoding.ASCII.GetBytes("<!DOCTYPE html><html><head><title>Twitch Connected!</title><style>p.title{font-size:20px;font-weight:bold;margin-top:0px;}.container{width:240px;border:2px solid #bf94ff;padding:20px;margin:auto;border-radius:10px;}button{margin-left:195px;}</style></head><body><div class=\"container\"><p class=\"title\">Twitch Desktop Notification</p><p class=\"msg\">Twitch has been success fully connected. Please close this tab.</p><button onclick=\"javascript:window.close();\">Close</button></div></body></html>"));
                response.OutputStream.Close();
                CodeRecived?.Invoke(this, new EventArgs());
            }
            else
            {;
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.ContentType = "text/html";
                response.OutputStream.Write(Encoding.ASCII.GetBytes("<!DOCTYPE html><html><head><title>State Missmatch</title></head><body><h1>State Missmatch</h1><p>State does not match up preventing XSS.</p></body></html>"));
                response.OutputStream.Close();
            }
        }

        private void RespondFavicon(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "image/x-icon";
            response.OutputStream.Write(File.ReadAllBytes("Assets/icon.ico"));
            response.OutputStream.Close();
        }

        private void processRequestThread(object? obj)
        {
            HttpListenerContext context = (HttpListenerContext)obj;
            HttpListenerRequest request = context.Request;

            if (request.Url.AbsolutePath == "/favicon.ico")
            {
                RespondFavicon(context.Response);
            }
            else if (request.Url.AbsolutePath == "/twitchRedirect")
            {
                RespondConnection(request, context.Response);
            }
            else
            {
                HttpListenerResponse response = context.Response;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.ContentType = "text/html";
                response.OutputStream.Write(Encoding.ASCII.GetBytes("<!DOCTYPE html><html><head><title>Not Found</title></head><body><h1>Not Found</h1><p>File not found</p></body></html>"));
                response.OutputStream.Close();
            }
        }

        private void ThreadManagedServer()
        {
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    ParameterizedThreadStart pts = new ParameterizedThreadStart(processRequestThread);
                    pts.Invoke(context);
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}

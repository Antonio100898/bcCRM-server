using Newtonsoft.Json;
using System;
using System.Net;

public sealed class SlackClient
{
    public static readonly Uri DefaultWebHookUri = new Uri("https://hooks.slack.com/services/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");

    private readonly Uri _webHookUri;

    public SlackClient(Uri webHookUri)
    {
        this._webHookUri = webHookUri;
    }

    public void SendSlackMessage(SlackMessage message)
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] request = System.Text.Encoding.UTF8.GetBytes("payload=" + JsonConvert.SerializeObject(message));
                byte[] response = webClient.UploadData(this._webHookUri, "POST", request);

                // ...handle response...
            }
        }
        catch (Exception e)
        {
            Logger.ErrorLog("SendSlackMessage", e, "");
        }
    }

    public sealed class SlackMessage
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon_emoji")]
        public string Icon
        {
            get { return ":computer:"; }
        }
    }
}

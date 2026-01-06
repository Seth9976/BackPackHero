using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class TwitchTokenHTTPServer
{
	// Token: 0x06000F78 RID: 3960 RVA: 0x00097150 File Offset: 0x00095350
	public async Task HandleIncomingConnections()
	{
		bool runServer = true;
		while (runServer)
		{
			HttpListenerContext httpListenerContext = await this.listener.GetContextAsync();
			HttpListenerRequest request = httpListenerContext.Request;
			HttpListenerResponse resp = httpListenerContext.Response;
			if (request.Url.AbsolutePath == "/favicon.ico")
			{
				resp.ContentType = "text/html";
				resp.ContentEncoding = Encoding.UTF8;
				resp.ContentLength64 = 0L;
				await resp.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(""), 0, 0);
				resp.Close();
			}
			else if (request.Url.AbsolutePath == "/auth")
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string.Format(this.stageOne, this.url));
				resp.ContentType = "text/html";
				resp.ContentEncoding = Encoding.UTF8;
				resp.ContentLength64 = (long)bytes.Length;
				await resp.OutputStream.WriteAsync(bytes, 0, bytes.Length);
				resp.Close();
			}
			else
			{
				if (request.Url.AbsolutePath == "/auth2")
				{
					Debug.Log(request.Url.ToString());
					Debug.Log(request.UserHostName);
					string value = request.QueryString["access_token"] ?? "";
					string text;
					TwitchTokenHTTPServer.ResultType result;
					if (value == "")
					{
						text = "Something went wrong, please try again.";
						result = TwitchTokenHTTPServer.ResultType.TwitchError;
					}
					else
					{
						text = "You have successfully linked your Twitch Account to Backpack Hero.<br>You can now close this browser window and continue in-game.";
						result = TwitchTokenHTTPServer.ResultType.Token;
					}
					byte[] bytes2 = Encoding.UTF8.GetBytes(string.Format(this.pageData, text));
					resp.ContentType = "text/html";
					resp.ContentEncoding = Encoding.UTF8;
					resp.ContentLength64 = (long)bytes2.Length;
					await resp.OutputStream.WriteAsync(bytes2, 0, bytes2.Length);
					resp.Close();
					this.requestCallback(result, value);
					value = null;
				}
				resp = null;
			}
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00097194 File Offset: 0x00095394
	public async Task StartServer(TwitchManager.GotRequestCallback callback)
	{
		if (this.listener != null)
		{
			await this.StopServer();
		}
		this.requestCallback = callback;
		this.listener = new HttpListener();
		this.listener.Prefixes.Add(this.url);
		this.listener.Start();
		Debug.Log("Starting temporary HTTP Server at " + this.url);
		this.HandleIncomingConnections();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000971E0 File Offset: 0x000953E0
	public async Task StopServer()
	{
		Debug.Log("Shutting down temporary HTTP Server");
		this.runServer = false;
		await Task.Delay(500);
		this.listener.Close();
	}

	// Token: 0x04000C95 RID: 3221
	public HttpListener listener;

	// Token: 0x04000C96 RID: 3222
	public string url = "http://localhost:50123/";

	// Token: 0x04000C97 RID: 3223
	public bool runServer;

	// Token: 0x04000C98 RID: 3224
	public string stageOne = "<script>location.replace('{0}auth2?'+document.location.hash.slice(1));</script>If your browser does not automatically redirect, please enable JavaScript for this page.";

	// Token: 0x04000C99 RID: 3225
	public string pageData = "<!DOCTYPE><html style=\"background: #090a14 url('data:image/png;base64,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') bottom left repeat-x;background-size: 80em; image-rendering: pixelated; background-attachment: fixed;\">  <head>    <title>Backpack Hero</title>  </head>  <body>    <br><br><br><br><center><h1 style='font-size: 3em;color: white; font-family: Verdana, Arial, Tahoma, Sans-Serif;'>{0}</h1></center>  </body></html>";

	// Token: 0x04000C9A RID: 3226
	private TwitchManager.GotRequestCallback requestCallback;

	// Token: 0x0200045A RID: 1114
	public enum ResultType
	{
		// Token: 0x040019D6 RID: 6614
		InvalidRequest,
		// Token: 0x040019D7 RID: 6615
		TwitchError,
		// Token: 0x040019D8 RID: 6616
		Token
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Auth;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class TwitchManager : MonoBehaviour
{
	// Token: 0x06000F2D RID: 3885 RVA: 0x00094D34 File Offset: 0x00092F34
	private static string RandomString(int length)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			char c = "abcdefghijklmnopqrstuvwxyz0123456789"[TwitchManager.random.Next(0, "abcdefghijklmnopqrstuvwxyz0123456789".Length)];
			stringBuilder.Append(c);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00094D84 File Offset: 0x00092F84
	public async void AuthTwitch()
	{
		this.status = TwitchManager.Status.Authenticating;
		this.errorCode = 0;
		await this.server.StartServer(new TwitchManager.GotRequestCallback(this.AuthProcessRequest));
		string text = Uri.EscapeDataString(string.Join(" ", this.scopes.ToArray()));
		string text2 = TwitchManager.RandomString(20);
		Application.OpenURL(string.Concat(new string[] { "https://id.twitch.tv/oauth2/authorize?response_type=token&client_id=", this.clientId, "&redirect_uri=", this.returnUrl, "&scope=", text, "&state=", text2 }));
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00094DBC File Offset: 0x00092FBC
	public bool hasValidToken()
	{
		return !(this.token == "") && DateTime.UtcNow.ToUnixTimeSeconds() <= this.tokenExpiresAt - 86400L;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00094E04 File Offset: 0x00093004
	public void AuthProcessRequest(TwitchTokenHTTPServer.ResultType type, string result)
	{
		this.server.StopServer();
		if (type == TwitchTokenHTTPServer.ResultType.Token)
		{
			Debug.Log("Access Token aquired! ");
			this.token = result;
			this.SetupAndValidateAPIandUser();
			return;
		}
		Debug.Log("Error while authenticating: " + result);
		this.status = TwitchManager.Status.AuthError;
		this.errorCode = 1;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00094E58 File Offset: 0x00093058
	public async Task SetupAndValidateAPIandUser()
	{
		if (!this.validOAuth)
		{
			this.api = new TwitchAPI(null, null, null, null);
			this.api.Settings.ClientId = this.clientId;
			this.api.Settings.AccessToken = this.token;
			ValidateAccessTokenResponse validateAccessTokenResponse = await this.api.Auth.ValidateAccessTokenAsync(this.token);
			if (validateAccessTokenResponse == null)
			{
				this.Clear();
				this.status = TwitchManager.Status.AuthError;
				this.errorCode = 2;
				return;
			}
			this.loginName = validateAccessTokenResponse.Login;
			this.userId = validateAccessTokenResponse.UserId;
			long num = DateTime.UtcNow.ToUnixTimeSeconds();
			this.tokenExpiresAt = num + (long)validateAccessTokenResponse.ExpiresIn;
			this.validOAuth = true;
			this.errorCode = 0;
			GetUsersResponse getUsersResponse = await this.api.Helix.Users.GetUsersAsync(new List<string> { this.userId }, null, this.token);
			if (getUsersResponse.Users.Count<User>() != 1)
			{
				this.Disconnect();
				this.status = TwitchManager.Status.ConnectionError;
				this.errorCode = 3;
				return;
			}
			this.displayName = getUsersResponse.Users[0].DisplayName;
			string text = getUsersResponse.Users[0].BroadcasterType;
			if (text == null || text.Length != 0)
			{
				if (!(text == "affiliate"))
				{
					if (!(text == "partner"))
					{
						this.broadcasterType = TwitchManager.BroadcasterType.Normal;
					}
					else
					{
						this.broadcasterType = TwitchManager.BroadcasterType.Partner;
					}
				}
				else
				{
					this.broadcasterType = TwitchManager.BroadcasterType.Affiliate;
				}
			}
			else
			{
				this.broadcasterType = TwitchManager.BroadcasterType.Normal;
			}
			this.SaveTwitchData();
			TwitchMenu twitchMenu = Object.FindObjectOfType<TwitchMenu>();
			if (twitchMenu)
			{
				twitchMenu.CheckForData();
			}
		}
		this.status = TwitchManager.Status.LoggedIn;
		if (Singleton.Instance.twitchIntegrationEnabled)
		{
			new Thread(delegate
			{
				Thread.CurrentThread.IsBackground = true;
				this.StartChatClient();
			}).Start();
		}
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00094E9C File Offset: 0x0009309C
	public void SaveTwitchData()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		es3Settings.path = "TwitchData.sav";
		ES3.Save<string>("twitchToken", this.token, es3Settings);
		ES3.Save<long>("twitchDate", this.tokenExpiresAt, es3Settings);
		ES3.Save<int>("twitchDeathCounterGlobal", this.deathCounter.deathsGlobal, es3Settings);
		ES3.Save<TwitchPollManager.PollSetting>("twitchPollSettings", this.pollManager.setting, es3Settings);
		ES3.Save<bool>("twitchEnabled", Singleton.Instance.twitchIntegrationEnabled, es3Settings);
		ES3.Save<bool>("twitchPollsEnabled", Singleton.Instance.twitchEnablePolls, es3Settings);
		ES3.Save<bool>("twitchDeathEnabled", Singleton.Instance.twitchEnableDeathCounter, es3Settings);
		ES3.Save<bool>("twitchEmoteEnabled", Singleton.Instance.twitchEnableEmoteEffect, es3Settings);
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00094F60 File Offset: 0x00093160
	public void LoadTwitchData()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		es3Settings.path = "TwitchData.sav";
		if (!ES3.FileExists(es3Settings.path))
		{
			return;
		}
		this.token = ES3.Load<string>("twitchToken", es3Settings);
		this.tokenExpiresAt = ES3.Load<long>("twitchDate", es3Settings);
		this.deathCounter.deathsGlobal = ES3.Load<int>("twitchDeathCounterGlobal", 0, es3Settings);
		this.pollManager.setting = ES3.Load<TwitchPollManager.PollSetting>("twitchPollSettings", TwitchPollManager.PollSetting.DeepCopy<TwitchPollManager.PollSetting>(this.pollManager.settingsPresets[0]), es3Settings);
		this.pollManager.settingsPresets[this.pollManager.settingsPresets.FindIndex((TwitchPollManager.PollSetting x) => x.name == this.pollManager.setting.name)] = this.pollManager.setting;
		Singleton.Instance.twitchIntegrationEnabled = ES3.Load<bool>("twitchEnabled", false, es3Settings);
		Singleton.Instance.twitchEnablePolls = ES3.Load<bool>("twitchPollsEnabled", true, es3Settings);
		Singleton.Instance.twitchEnableDeathCounter = ES3.Load<bool>("twitchDeathEnabled", false, es3Settings);
		Singleton.Instance.twitchEnableEmoteEffect = ES3.Load<bool>("twitchEmoteEnabled", false, es3Settings);
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x00095082 File Offset: 0x00093282
	public bool TwitchDataExists()
	{
		return ES3.FileExists(new ES3Settings(null, null)
		{
			path = Application.persistentDataPath + "/TwitchData.sav"
		}.path);
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000950AA File Offset: 0x000932AA
	public void EraseTwitchData()
	{
		this.Clear();
		this.SaveTwitchData();
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000950B8 File Offset: 0x000932B8
	public async Task MakeAnnouncement(string message)
	{
		await this.api.Helix.Chat.SendChatAnnouncementAsync(this.userId, this.userId, message, null, this.token);
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00095103 File Offset: 0x00093303
	public void Disconnect()
	{
		Debug.Log("Disconnecting");
		this.twitchChat.disconnect();
		this.errorCode = 4;
		this.status = TwitchManager.Status.LoggedIn;
		Singleton.Instance.twitchIntegrationEnabled = false;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00095134 File Offset: 0x00093334
	public void Clear()
	{
		this.Disconnect();
		this.api.Settings.ClientId = "";
		this.api.Settings.AccessToken = "";
		this.token = "";
		this.validOAuth = false;
		this.userId = "";
		this.loginName = "";
		this.displayName = "";
		this.status = TwitchManager.Status.NoAuth;
		this.errorCode = 0;
		Singleton.Instance.twitchIntegrationEnabled = false;
		this.chatterList.chatters = new List<Chatter>();
		this.SaveTwitchData();
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000951D4 File Offset: 0x000933D4
	public void StartChatClient()
	{
		Debug.Log("Connect called from " + new StackTrace().GetFrame(1).GetMethod().Name);
		this.status = TwitchManager.Status.Connecting;
		this.twitchChat.connect(this.loginName, this.token);
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00095224 File Offset: 0x00093424
	private void Awake()
	{
		if (TwitchManager.Instance)
		{
			if (TwitchManager.Instance.pollManager.isPollRunning())
			{
				TwitchManager.Instance.pollManager.currentPoll.EndPoll();
			}
			return;
		}
		TwitchManager.Instance = this;
		this.chatterList = new TwitchChatterList(this, this.twitchChat);
		this.deathCounter = new TwitchDeathCounter(this, this.twitchChat);
		GameObject gameObject = Object.Instantiate<GameObject>(this.emoteManagerPrefab, base.transform);
		this.emoteManager = gameObject.GetComponent<TwitchEmoteManager>();
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.pollManagerPrefab, base.transform);
		this.pollManager = gameObject2.GetComponent<TwitchPollManager>();
		this.pollManager.twitchManager = this;
		this.pollManager.twitchChat = this.twitchChat;
		this.emoteManager.Init(this, this.twitchChat);
		this.LoadTwitchData();
		Debug.Log("Valid token: " + this.hasValidToken().ToString());
		if (this.hasValidToken())
		{
			this.SetupAndValidateAPIandUser();
		}
		Debug.Log("Twitch Manager Active");
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00095334 File Offset: 0x00093534
	private void Start()
	{
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00095336 File Offset: 0x00093536
	private void OnDestroy()
	{
		if (TwitchManager.Instance == this)
		{
			TwitchManager.Instance = null;
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0009534B File Offset: 0x0009354B
	private void Update()
	{
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x0009534D File Offset: 0x0009354D
	public void onDeath()
	{
		this.deathCounter.OnDeath();
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0009535A File Offset: 0x0009355A
	public static bool isRunningPolls()
	{
		return !(TwitchManager.Instance == null) && !(TwitchManager.Instance.pollManager == null);
	}

	// Token: 0x04000C47 RID: 3143
	public static TwitchManager Instance;

	// Token: 0x04000C48 RID: 3144
	public string clientId = "";

	// Token: 0x04000C49 RID: 3145
	public string returnUrl = "";

	// Token: 0x04000C4A RID: 3146
	public List<string> scopes;

	// Token: 0x04000C4B RID: 3147
	public int chatMaxTries = 1;

	// Token: 0x04000C4C RID: 3148
	public float chatTimeout = 15f;

	// Token: 0x04000C4D RID: 3149
	public GameObject emoteManagerPrefab;

	// Token: 0x04000C4E RID: 3150
	public GameObject pollManagerPrefab;

	// Token: 0x04000C4F RID: 3151
	public bool validOAuth;

	// Token: 0x04000C50 RID: 3152
	public TwitchManager.Status status;

	// Token: 0x04000C51 RID: 3153
	public int errorCode;

	// Token: 0x04000C52 RID: 3154
	public string token = "";

	// Token: 0x04000C53 RID: 3155
	public string userId = "";

	// Token: 0x04000C54 RID: 3156
	public string loginName = "";

	// Token: 0x04000C55 RID: 3157
	public string displayName = "";

	// Token: 0x04000C56 RID: 3158
	public long tokenExpiresAt;

	// Token: 0x04000C57 RID: 3159
	public TwitchManager.BroadcasterType broadcasterType = TwitchManager.BroadcasterType.Normal;

	// Token: 0x04000C58 RID: 3160
	private static Random random = new Random();

	// Token: 0x04000C59 RID: 3161
	private TwitchTokenHTTPServer server = new TwitchTokenHTTPServer();

	// Token: 0x04000C5A RID: 3162
	private TwitchAPI api;

	// Token: 0x04000C5B RID: 3163
	public TwitchChat twitchChat = new TwitchChat();

	// Token: 0x04000C5C RID: 3164
	public TwitchChatterList chatterList;

	// Token: 0x04000C5D RID: 3165
	public TwitchEmoteManager emoteManager;

	// Token: 0x04000C5E RID: 3166
	public TwitchPollManager pollManager;

	// Token: 0x04000C5F RID: 3167
	public TwitchDeathCounter deathCounter;

	// Token: 0x02000449 RID: 1097
	// (Invoke) Token: 0x06001A40 RID: 6720
	public delegate void GotRequestCallback(TwitchTokenHTTPServer.ResultType type, string result);

	// Token: 0x0200044A RID: 1098
	public enum BroadcasterType
	{
		// Token: 0x0400198F RID: 6543
		Partner,
		// Token: 0x04001990 RID: 6544
		Affiliate,
		// Token: 0x04001991 RID: 6545
		Normal
	}

	// Token: 0x0200044B RID: 1099
	public enum Status
	{
		// Token: 0x04001993 RID: 6547
		NoAuth,
		// Token: 0x04001994 RID: 6548
		AuthError,
		// Token: 0x04001995 RID: 6549
		NoConnection,
		// Token: 0x04001996 RID: 6550
		ConnectionError,
		// Token: 0x04001997 RID: 6551
		Authenticating,
		// Token: 0x04001998 RID: 6552
		LoggedIn,
		// Token: 0x04001999 RID: 6553
		Connecting,
		// Token: 0x0400199A RID: 6554
		Connected
	}
}

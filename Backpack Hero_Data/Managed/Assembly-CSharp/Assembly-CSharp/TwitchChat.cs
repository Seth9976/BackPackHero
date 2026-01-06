using System;
using System.Diagnostics;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class TwitchChat
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000F04 RID: 3844 RVA: 0x00094524 File Offset: 0x00092724
	// (remove) Token: 0x06000F05 RID: 3845 RVA: 0x0009455C File Offset: 0x0009275C
	public event OnMessageReceivedDelegate OnMessageReceivedEvent;

	// Token: 0x06000F07 RID: 3847 RVA: 0x0009459C File Offset: 0x0009279C
	public void connect(string userName, string token)
	{
		Debug.Log("Connect called from " + new StackTrace().GetFrame(1).GetMethod().Name);
		if (this.client != null)
		{
			while (this.disconnecting)
			{
				Thread.Sleep(50);
			}
		}
		this.disconnect();
		this.client = new TwitchClient(this.customClient, ClientProtocol.WebSocket, null);
		this.client.OnLog += this.Client_OnLog;
		this.client.OnJoinedChannel += this.Client_OnJoinedChannel;
		this.client.OnMessageReceived += this.Client_OnMessageReceived;
		this.client.OnConnected += this.Client_OnConnected;
		ClientOptions clientOptions = new ClientOptions
		{
			MessagesAllowedInPeriod = 2000,
			ThrottlingPeriod = TimeSpan.FromSeconds(30.0)
		};
		this.customClient = new WebSocketClient(clientOptions);
		this.userName = userName;
		this.token = token;
		this.credentials = new ConnectionCredentials(userName, token, "wss://irc-ws.chat.twitch.tv:443", false, null);
		this.client.Initialize(this.credentials, userName, '!', '!', true);
		try
		{
			this.client.Connect();
		}
		catch (ObjectDisposedException)
		{
			Thread.Sleep(500);
			this.client.Connect();
		}
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x000946FC File Offset: 0x000928FC
	public void disconnect()
	{
		this.disconnecting = true;
		if (this.client != null)
		{
			this.client.OnLog -= this.Client_OnLog;
			this.client.OnJoinedChannel -= this.Client_OnJoinedChannel;
			this.client.OnMessageReceived -= this.Client_OnMessageReceived;
			this.client.OnConnected -= this.Client_OnConnected;
			try
			{
				this.client.Disconnect();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}
		this.client = null;
		this.disconnecting = false;
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000947A8 File Offset: 0x000929A8
	public void reconnect()
	{
		this.client.Reconnect();
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x000947B5 File Offset: 0x000929B5
	public void SubscribeMessage(OnMessageReceivedDelegate callback)
	{
		this.OnMessageReceivedEvent += callback;
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000947BE File Offset: 0x000929BE
	public void UnsubscribeMessage(OnMessageReceivedDelegate callback)
	{
		this.OnMessageReceivedEvent -= callback;
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000947C7 File Offset: 0x000929C7
	public void SendMessage(string message)
	{
		if (this.client.IsConnected)
		{
			this.client.SendMessage(this.userName, message, false);
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x000947E9 File Offset: 0x000929E9
	private void Client_OnLog(object sender, OnLogArgs e)
	{
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x000947EB File Offset: 0x000929EB
	private void Client_OnConnected(object sender, OnConnectedArgs e)
	{
		Debug.Log("Connected to " + e.BotUsername);
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x00094802 File Offset: 0x00092A02
	private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
	{
		TwitchManager.Instance.status = TwitchManager.Status.Connected;
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0009480F File Offset: 0x00092A0F
	private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
	{
		if (this.OnMessageReceivedEvent != null)
		{
			this.OnMessageReceivedEvent(sender, e);
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x00094826 File Offset: 0x00092A26
	private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
	{
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x00094828 File Offset: 0x00092A28
	private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
	{
	}

	// Token: 0x04000C2A RID: 3114
	public TwitchClient client;

	// Token: 0x04000C2B RID: 3115
	private string userName;

	// Token: 0x04000C2C RID: 3116
	private string token;

	// Token: 0x04000C2D RID: 3117
	private WebSocketClient customClient;

	// Token: 0x04000C2E RID: 3118
	private ConnectionCredentials credentials;

	// Token: 0x04000C2F RID: 3119
	private bool disconnecting;
}

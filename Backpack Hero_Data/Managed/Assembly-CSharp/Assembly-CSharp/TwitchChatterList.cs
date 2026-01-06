using System;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class TwitchChatterList
{
	// Token: 0x06000F21 RID: 3873 RVA: 0x000948A4 File Offset: 0x00092AA4
	public TwitchChatterList()
	{
		this.twitchManager = GameObject.Find("TwitchManager").GetComponent<TwitchManager>();
		this.twitchChat = this.twitchManager.twitchChat;
		this.twitchChat.SubscribeMessage(new OnMessageReceivedDelegate(this.OnMessageReceived));
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000948FF File Offset: 0x00092AFF
	public TwitchChatterList(TwitchManager manager, TwitchChat chat)
	{
		this.twitchManager = manager;
		this.twitchChat = chat;
		this.twitchChat.SubscribeMessage(new OnMessageReceivedDelegate(this.OnMessageReceived));
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x00094938 File Offset: 0x00092B38
	public void OnMessageReceived(object sender, OnMessageReceivedArgs e)
	{
		ChatMessage message = e.ChatMessage;
		Chatter chatterFromMessage = TwitchChatterList.getChatterFromMessage(message);
		int num = this.chatters.FindIndex((Chatter x) => x.userName == message.Username);
		if (num < 0)
		{
			this.chatters.Add(chatterFromMessage);
			return;
		}
		this.chatters[num] = chatterFromMessage;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0009499C File Offset: 0x00092B9C
	public static Chatter getChatterFromMessage(ChatMessage message)
	{
		return new Chatter
		{
			userName = message.Username,
			displayName = message.DisplayName,
			color = message.ColorHex,
			isSubscriber = message.IsSubscriber,
			isModerator = message.IsModerator,
			isVip = message.IsVip,
			lastMessageTimestamp = message.TmiSentTs
		};
	}

	// Token: 0x04000C38 RID: 3128
	private TwitchManager twitchManager;

	// Token: 0x04000C39 RID: 3129
	private TwitchChat twitchChat;

	// Token: 0x04000C3A RID: 3130
	public List<Chatter> chatters = new List<Chatter>();

	// Token: 0x04000C3B RID: 3131
	public List<string> botList;
}

using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000178 RID: 376
public class TwitchEmoteManager : MonoBehaviour
{
	// Token: 0x06000F27 RID: 3879 RVA: 0x00094B63 File Offset: 0x00092D63
	public void Init(TwitchManager twitchManager, TwitchChat twitchChat)
	{
		this.twitchManager = twitchManager;
		this.twitchChat = twitchChat;
		twitchChat.SubscribeMessage(new OnMessageReceivedDelegate(this.OnMessageReceived));
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00094B88 File Offset: 0x00092D88
	public void OnMessageReceived(object sender, OnMessageReceivedArgs e)
	{
		foreach (Emote emote in e.ChatMessage.EmoteSet.Emotes)
		{
			if (!this.emoteCache.ContainsKey(emote.Id) && !this.emoteDownloadQueue.Contains(emote.Id) && !this.emotesCurrentlyDownloading.Contains(emote.Id))
			{
				Debug.Log("Downloading emote " + emote.Name + " " + emote.Id);
				this.emoteDownloadQueue.Enqueue(emote.Id);
			}
			this.emoteSpawnQueue.Enqueue(emote.Id);
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00094C60 File Offset: 0x00092E60
	private void Update()
	{
		while (this.emoteDownloadQueue.Count > 0)
		{
			base.StartCoroutine(this.GetEmote(this.emoteDownloadQueue.Dequeue()));
		}
		Queue<string> queue = new Queue<string>();
		while (this.emoteSpawnQueue.Count > 0)
		{
			string text = this.emoteSpawnQueue.Dequeue();
			if (this.emoteCache.ContainsKey(text))
			{
				this.spawnEmote(text);
			}
			else
			{
				queue.Enqueue(text);
			}
		}
		this.emoteSpawnQueue = queue;
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00094CDC File Offset: 0x00092EDC
	private IEnumerator GetEmote(string Id)
	{
		this.emotesCurrentlyDownloading.Add(Id);
		string text = "https://static-cdn.jtvnw.net/emoticons/v2/" + Id + "/static/dark/3.0";
		Debug.Log("Downloading texture for " + Id + " at " + text);
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(text))
		{
			yield return uwr.SendWebRequest();
			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				Texture2D content = DownloadHandlerTexture.GetContent(uwr);
				this.emoteCache.Add(Id, Sprite.Create(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0.5f, 0.5f)));
			}
		}
		UnityWebRequest uwr = null;
		this.emotesCurrentlyDownloading.Remove(Id);
		yield break;
		yield break;
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00094CF2 File Offset: 0x00092EF2
	public void spawnEmote(string id)
	{
		bool twitchEnableEmoteEffect = Singleton.Instance.twitchEnableEmoteEffect;
	}

	// Token: 0x04000C41 RID: 3137
	public TwitchManager twitchManager;

	// Token: 0x04000C42 RID: 3138
	public TwitchChat twitchChat;

	// Token: 0x04000C43 RID: 3139
	public Dictionary<string, Sprite> emoteCache = new Dictionary<string, Sprite>();

	// Token: 0x04000C44 RID: 3140
	private Queue<string> emoteDownloadQueue = new Queue<string>();

	// Token: 0x04000C45 RID: 3141
	private List<string> emotesCurrentlyDownloading = new List<string>();

	// Token: 0x04000C46 RID: 3142
	private Queue<string> emoteSpawnQueue = new Queue<string>();
}

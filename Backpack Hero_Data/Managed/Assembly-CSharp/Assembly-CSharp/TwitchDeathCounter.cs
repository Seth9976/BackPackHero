using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class TwitchDeathCounter
{
	// Token: 0x06000F25 RID: 3877 RVA: 0x00094A10 File Offset: 0x00092C10
	public TwitchDeathCounter(TwitchManager twitchManager, TwitchChat twitchChat)
	{
		this.twitchManager = twitchManager;
		this.twitchChat = twitchChat;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00094A94 File Offset: 0x00092C94
	public void OnDeath()
	{
		this.deathsSession++;
		this.deathsGlobal++;
		this.twitchManager.SaveTwitchData();
		if (Singleton.Instance.twitchEnableDeathCounter)
		{
			string textByKey = LangaugeManager.main.GetTextByKey(this.exclamations[Random.Range(0, this.exclamations.Count)]);
			this.twitchManager.MakeAnnouncement(LangaugeManager.main.GetTextByKey("twitchDeathChatMsg").Replace("/x", textByKey).Replace("/y", this.twitchManager.displayName)
				.Replace("/z", this.deathsGlobal.ToString())
				.Replace("/w", this.deathsSession.ToString()));
		}
	}

	// Token: 0x04000C3C RID: 3132
	public TwitchManager twitchManager;

	// Token: 0x04000C3D RID: 3133
	public TwitchChat twitchChat;

	// Token: 0x04000C3E RID: 3134
	public int deathsSession;

	// Token: 0x04000C3F RID: 3135
	public int deathsGlobal;

	// Token: 0x04000C40 RID: 3136
	public List<string> exclamations = new List<string> { "twitchDeathEx1", "twitchDeathEx2", "twitchDeathEx3", "twitchDeathEx4", "twitchDeathEx5", "twitchDeathEx6", "twitchDeathEx7", "twitchDeathEx8" };
}

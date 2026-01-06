using System;

// Token: 0x0200017B RID: 379
public class PollOption
{
	// Token: 0x06000F44 RID: 3908 RVA: 0x00095440 File Offset: 0x00093640
	public PollOption(string text, TwitchPollManager.ActionType action, object param, TwitchPollManager.ActionAffiliation affiliation)
	{
		this.text = text;
		this.action = action;
		this.param = param;
		this.affiliation = affiliation;
	}

	// Token: 0x04000C67 RID: 3175
	public string text;

	// Token: 0x04000C68 RID: 3176
	public TwitchPollManager.ActionType action;

	// Token: 0x04000C69 RID: 3177
	public object param;

	// Token: 0x04000C6A RID: 3178
	public TwitchPollManager.ActionAffiliation affiliation;

	// Token: 0x04000C6B RID: 3179
	public int count;
}

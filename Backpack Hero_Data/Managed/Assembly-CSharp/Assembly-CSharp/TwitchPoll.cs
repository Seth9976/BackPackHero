using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class TwitchPoll : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000F4D RID: 3917 RVA: 0x00095468 File Offset: 0x00093668
	// (remove) Token: 0x06000F4E RID: 3918 RVA: 0x000954A0 File Offset: 0x000936A0
	public event OnPollEndDelegate OnPollEndEvent;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000F4F RID: 3919 RVA: 0x000954D8 File Offset: 0x000936D8
	// (remove) Token: 0x06000F50 RID: 3920 RVA: 0x00095510 File Offset: 0x00093710
	public event OnVotedDelegate OnVotedEvent;

	// Token: 0x06000F51 RID: 3921 RVA: 0x00095548 File Offset: 0x00093748
	public void Init(TwitchManager manager, TwitchChat chat, string title, List<PollOption> pollOptions, float timeout, bool checkDuplicate)
	{
		this.twitchManager = manager;
		this.twitchChat = chat;
		this.pollTitle = title;
		this.timeRemaining = timeout;
		this.timeTotal = timeout;
		this.checkDuplicate = checkDuplicate;
		foreach (PollOption pollOption in pollOptions)
		{
			this.options.Add(pollOption);
		}
		this.pollStatus = PollStatus.NotStarted;
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x000955D4 File Offset: 0x000937D4
	public void OnMessageReceived(object sender, OnMessageReceivedArgs e)
	{
		try
		{
			ChatMessage message = e.ChatMessage;
			Chatter chatterFromMessage = TwitchChatterList.getChatterFromMessage(message);
			int num = int.Parse(Regex.Match(message.Message, "\\d+").Value);
			if (num >= 1 && num <= this.options.Count)
			{
				num--;
				bool flag = false;
				if (this.checkDuplicate)
				{
					int num2 = this.voters.FindIndex(([TupleElementNames(new string[] { "voter", "choice" })] ValueTuple<Chatter, int> x) => x.Item1.userName == message.Username);
					if (num2 >= 0)
					{
						if (num2 == num)
						{
							return;
						}
						this.options[this.voters[num2].Item2].count--;
						this.voters[num2] = new ValueTuple<Chatter, int>(chatterFromMessage, num);
						this.options[num].count++;
						flag = true;
					}
				}
				this.voters.Add(new ValueTuple<Chatter, int>(chatterFromMessage, num));
				if (!flag)
				{
					this.options[num].count++;
					this.votes++;
				}
				if (this.OnVotedEvent != null)
				{
					this.OnVotedEvent(chatterFromMessage, num);
				}
			}
		}
		catch (FormatException)
		{
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0009575C File Offset: 0x0009395C
	public void StartPoll()
	{
		this.pollStatus = PollStatus.Running;
		this.twitchChat.SubscribeMessage(new OnMessageReceivedDelegate(this.OnMessageReceived));
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0009577C File Offset: 0x0009397C
	public void EndPoll()
	{
		this.twitchChat.UnsubscribeMessage(new OnMessageReceivedDelegate(this.OnMessageReceived));
		this.pollStatus = PollStatus.Ended;
		if (this.timeRemaining != 0f)
		{
			this.aborted = true;
		}
		this.ranking = this.options.OrderByDescending((PollOption t) => t.count).ToList<PollOption>();
		if (this.ranking[0].count == 0)
		{
			this.noVotes = true;
		}
		if (this.ranking[0].count == this.ranking[1].count)
		{
			this.draw = true;
		}
		if (this.OnPollEndEvent != null)
		{
			this.OnPollEndEvent(this.ranking, this.noVotes, this.draw);
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0009585A File Offset: 0x00093A5A
	public void SubscribeEndPoll(OnPollEndDelegate callback)
	{
		this.OnPollEndEvent += callback;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00095863 File Offset: 0x00093A63
	public void UnsubscribeEndPoll(OnPollEndDelegate callback)
	{
		this.OnPollEndEvent -= callback;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0009586C File Offset: 0x00093A6C
	public void SubscribeVoted(OnVotedDelegate callback)
	{
		this.OnVotedEvent += callback;
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00095875 File Offset: 0x00093A75
	public void UnsubscribeVoted(OnVotedDelegate callback)
	{
		this.OnVotedEvent -= callback;
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0009587E File Offset: 0x00093A7E
	private void Start()
	{
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00095880 File Offset: 0x00093A80
	private void Update()
	{
		if (this.pollStatus == PollStatus.Running)
		{
			this.timeRemaining -= Time.deltaTime;
			if (this.timeRemaining <= 0f)
			{
				this.timeRemaining = 0f;
				this.EndPoll();
			}
		}
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000958BC File Offset: 0x00093ABC
	public List<Chatter> getLatestVoters(int latestNumber)
	{
		if (this.voters.Count == 0)
		{
			return new List<Chatter>();
		}
		int num = this.voters.Count - latestNumber;
		if (num < 0)
		{
			num = 0;
		}
		List<ValueTuple<Chatter, int>> range = this.voters.GetRange(num, Mathf.Min(latestNumber, this.voters.Count - num));
		List<Chatter> list = new List<Chatter>();
		foreach (ValueTuple<Chatter, int> valueTuple in range)
		{
			list.Add(valueTuple.Item1);
		}
		return list;
	}

	// Token: 0x04000C6C RID: 3180
	private TwitchManager twitchManager;

	// Token: 0x04000C6D RID: 3181
	private TwitchChat twitchChat;

	// Token: 0x04000C70 RID: 3184
	public string pollTitle = "";

	// Token: 0x04000C71 RID: 3185
	public bool checkDuplicate;

	// Token: 0x04000C72 RID: 3186
	public float timeTotal;

	// Token: 0x04000C73 RID: 3187
	public float timeRemaining;

	// Token: 0x04000C74 RID: 3188
	public List<PollOption> options = new List<PollOption>();

	// Token: 0x04000C75 RID: 3189
	[TupleElementNames(new string[] { "voter", "choice" })]
	public List<ValueTuple<Chatter, int>> voters = new List<ValueTuple<Chatter, int>>();

	// Token: 0x04000C76 RID: 3190
	public PollStatus pollStatus;

	// Token: 0x04000C77 RID: 3191
	public List<PollOption> ranking;

	// Token: 0x04000C78 RID: 3192
	public PollOption winner;

	// Token: 0x04000C79 RID: 3193
	public bool draw;

	// Token: 0x04000C7A RID: 3194
	public bool noVotes;

	// Token: 0x04000C7B RID: 3195
	public bool aborted;

	// Token: 0x04000C7C RID: 3196
	public int votes;
}

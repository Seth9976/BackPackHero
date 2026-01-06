using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public class TwitchCanvasManager : MonoBehaviour
{
	// Token: 0x060010BC RID: 4284 RVA: 0x0009EBBC File Offset: 0x0009CDBC
	private void Start()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0009EBCC File Offset: 0x0009CDCC
	private void Update()
	{
		if (!TwitchManager.Instance || TwitchManager.Instance.pollManager.currentPoll == null)
		{
			this.state = TwitchCanvasManager.State.None;
			this.canvasGroup.alpha = 0f;
			this.canvasGroup.interactable = false;
			return;
		}
		if (!TwitchManager.Instance.pollManager.isPollRunning() && this.state == TwitchCanvasManager.State.Poll)
		{
			foreach (TwitchOptionDisplay twitchOptionDisplay in this.twitchOptionDisplays)
			{
				Object.Destroy(twitchOptionDisplay);
			}
			this.twitchOptionDisplays = new List<TwitchOptionDisplay>();
			if (TwitchManager.Instance.pollManager.currentPoll.noVotes || TwitchManager.Instance.pollManager.currentPoll.aborted)
			{
				base.GetComponent<Animator>().Play("TwitchPollOut", 0, 0f);
				this.canvasGroup.interactable = false;
				this.state = TwitchCanvasManager.State.None;
			}
			else
			{
				base.GetComponent<Animator>().Play("TwitchPollToResult", 0, 0f);
				this.resultTimeout = 15f;
				this.state = TwitchCanvasManager.State.Result;
				PollOption winner = TwitchManager.Instance.pollManager.currentPoll.winner;
				TwitchOptionDisplay component = this.resultCanvas.gameObject.GetComponent<TwitchOptionDisplay>();
				component.option = winner;
				component.Setup();
				component.isResult = true;
				this.resultCanvas.gameObject.GetComponent<BoxCollider2D>().enabled = true;
				this.resultCanvas.interactable = true;
				this.pollCanvas.interactable = false;
				this.resultText.text = winner.text;
				this.resultVotesText.text = LangaugeManager.main.GetTextByKey("TwitchPollResult").Replace("/x", winner.count.ToString());
				switch (TwitchManager.Instance.pollManager.currentPoll.winner.action)
				{
				case TwitchPollManager.ActionType.DecideRelics:
				case TwitchPollManager.ActionType.GetItem:
					this.resultImage.sprite = ((Item2)winner.param).gameObject.GetComponent<SpriteRenderer>().sprite;
					goto IL_033F;
				case TwitchPollManager.ActionType.PlayerStatusEffect:
				case TwitchPollManager.ActionType.EnemyStatusEffect:
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.statusEffectPrefab);
					this.resultImage.sprite = gameObject.GetComponent<StatusEffect>().GetSpriteFromType(((ValueTuple<StatusEffect.Type, int>)winner.param).Item1);
					Object.Destroy(gameObject);
					goto IL_033F;
				}
				case TwitchPollManager.ActionType.Blessing:
				{
					GameObject gameObject2 = GameManager.main.SpawnCurseOrBlessing(1, Curse.Type.Blessing);
					this.resultImage.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
					Object.Destroy(gameObject2);
					goto IL_033F;
				}
				case TwitchPollManager.ActionType.Curse:
				{
					GameObject gameObject3 = Object.Instantiate<GameObject>(this.statusEffectPrefab);
					this.resultImage.sprite = gameObject3.GetComponent<StatusEffect>().GetSpriteFromType(StatusEffect.Type.curse);
					Object.Destroy(gameObject3);
					goto IL_033F;
				}
				case TwitchPollManager.ActionType.DecideBoss:
					this.resultImage.sprite = ((DungeonLevel.EnemyEncounter2)winner.param).enemiesInGroup[0].GetComponentInChildren<SpriteRenderer>().sprite;
					goto IL_033F;
				case TwitchPollManager.ActionType.Hazard:
					this.resultImage.sprite = ((GameObject)winner.param).GetComponent<SpriteRenderer>().sprite;
					goto IL_033F;
				}
				this.resultImage.sprite = this.questionMark;
				IL_033F:
				this.resultImage.SetNativeSize();
				this.resultImage.rectTransform.sizeDelta = this.resultImage.rectTransform.sizeDelta.normalized * 250f;
			}
		}
		if (this.state == TwitchCanvasManager.State.None && this.canvasGroup.alpha == 0f)
		{
			foreach (GameObject gameObject4 in this.twitchOptionObjects)
			{
				Object.Destroy(gameObject4);
			}
			this.twitchOptionDisplays = new List<TwitchOptionDisplay>();
			this.twitchOptionObjects = new List<GameObject>();
		}
		if (TwitchManager.Instance.pollManager.isNewPoll())
		{
			List<PollOption> list = new List<PollOption>();
			foreach (PollOption pollOption in TwitchManager.Instance.pollManager.currentPoll.options)
			{
				list.Add(pollOption);
			}
			this.GetNewOptions(list);
			LangaugeManager.main.SetFont(base.transform);
			if (this.state == TwitchCanvasManager.State.None)
			{
				base.GetComponent<Animator>().Play("TwitchPollIn", 0, 0f);
			}
			if (this.state == TwitchCanvasManager.State.Result)
			{
				base.GetComponent<Animator>().Play("TwitchResultToPoll", 0, 0f);
			}
			TwitchOptionDisplay component2 = this.resultCanvas.gameObject.GetComponent<TwitchOptionDisplay>();
			component2.option = null;
			component2.Setup();
			this.resultCanvas.interactable = false;
			this.resultCanvas.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			this.state = TwitchCanvasManager.State.Poll;
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
		}
		if (this.state == TwitchCanvasManager.State.Result)
		{
			TwitchOptionDisplay component3 = this.resultCanvas.gameObject.GetComponent<TwitchOptionDisplay>();
			if (!component3.isHovering || TwitchManager.Instance.pollManager.currentPoll.pollStatus == PollStatus.Fulfilled)
			{
				this.resultTimeout -= Time.deltaTime;
			}
			if (TwitchManager.Instance.pollManager.currentPoll.pollStatus == PollStatus.Fulfilled)
			{
				this.resultTimeout = Mathf.Min(this.resultTimeout, 2f);
			}
			if (this.resultTimeout <= 0f)
			{
				component3.option = null;
				component3.Setup();
				this.state = TwitchCanvasManager.State.None;
				this.canvasGroup.interactable = false;
				this.resultCanvas.interactable = false;
				this.resultCanvas.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				base.GetComponent<Animator>().Play("TwitchResultOut", 0, 0f);
			}
		}
		if (this.state == TwitchCanvasManager.State.Poll)
		{
			this.remainingTime = TwitchManager.Instance.pollManager.currentPoll.timeRemaining;
			this.votes = TwitchManager.Instance.pollManager.currentPoll.votes;
			for (int i = 0; i < TwitchManager.Instance.pollManager.currentPoll.options.Count; i++)
			{
				int count = TwitchManager.Instance.pollManager.currentPoll.options[i].count;
				this.twitchOptionDisplays[i].SetVote(count);
				this.twitchOptionDisplays[i].UpdateBar(this.votes);
			}
			List<Chatter> latestVoters = TwitchManager.Instance.pollManager.currentPoll.getLatestVoters(7);
			this.GetVoters(latestVoters);
			this.remainingTime = Mathf.Clamp(this.remainingTime, 0f, 9999f);
			string text = Mathf.Floor(this.remainingTime / 60f).ToString();
			string text2 = Mathf.RoundToInt(this.remainingTime % 60f).ToString();
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			this.stats.text = LangaugeManager.main.GetTextByKey("twitchtimevotes").Replace("/x", text + ":" + text2).Replace("/y", this.votes.ToString());
			this.title.text = TwitchManager.Instance.pollManager.currentPoll.pollTitle;
		}
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0009F3A0 File Offset: 0x0009D5A0
	private void GetNewOptions(List<PollOption> options)
	{
		this.votes = 0;
		foreach (GameObject gameObject in this.twitchOptionObjects)
		{
			Object.Destroy(gameObject);
		}
		this.twitchOptionDisplays = new List<TwitchOptionDisplay>();
		this.twitchOptionObjects = new List<GameObject>();
		int num = 0;
		foreach (PollOption pollOption in options)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.optionPrefab, this.optionsParent.transform);
			this.twitchOptionObjects.Add(gameObject2);
			gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = " " + (num + 1).ToString() + ". " + pollOption.text;
			TwitchOptionDisplay component = gameObject2.GetComponent<TwitchOptionDisplay>();
			this.twitchOptionDisplays.Add(component);
			component.option = pollOption;
			num++;
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0009F4BC File Offset: 0x0009D6BC
	private void GetVoters(List<Chatter> latestChatters)
	{
		string text = "";
		for (int i = 0; i < latestChatters.Count; i++)
		{
			string textByKey = LangaugeManager.main.GetTextByKey("twitchvoted");
			string text2 = latestChatters[i].color;
			if (text2 == null || text2 == "")
			{
				text2 = "#ffffff";
			}
			text = string.Concat(new string[]
			{
				text,
				"<color=",
				text2,
				">",
				latestChatters[i].displayName,
				"</color> ",
				textByKey,
				"<br>"
			});
		}
		this.voteCounter.text = text;
	}

	// Token: 0x04000D98 RID: 3480
	private TwitchCanvasManager.State state = TwitchCanvasManager.State.None;

	// Token: 0x04000D99 RID: 3481
	[SerializeField]
	private GameObject optionsParent;

	// Token: 0x04000D9A RID: 3482
	[SerializeField]
	private GameObject optionPrefab;

	// Token: 0x04000D9B RID: 3483
	[SerializeField]
	private TextMeshProUGUI title;

	// Token: 0x04000D9C RID: 3484
	[SerializeField]
	private TextMeshProUGUI stats;

	// Token: 0x04000D9D RID: 3485
	[SerializeField]
	private TextMeshProUGUI voteCounter;

	// Token: 0x04000D9E RID: 3486
	[SerializeField]
	private CanvasGroup pollCanvas;

	// Token: 0x04000D9F RID: 3487
	[SerializeField]
	private CanvasGroup resultCanvas;

	// Token: 0x04000DA0 RID: 3488
	[SerializeField]
	private Image resultImage;

	// Token: 0x04000DA1 RID: 3489
	[SerializeField]
	private TextMeshProUGUI resultText;

	// Token: 0x04000DA2 RID: 3490
	[SerializeField]
	private TextMeshProUGUI resultVotesText;

	// Token: 0x04000DA3 RID: 3491
	[SerializeField]
	private GameObject statusEffectPrefab;

	// Token: 0x04000DA4 RID: 3492
	[SerializeField]
	private Sprite questionMark;

	// Token: 0x04000DA5 RID: 3493
	private List<TwitchOptionDisplay> twitchOptionDisplays = new List<TwitchOptionDisplay>();

	// Token: 0x04000DA6 RID: 3494
	private List<GameObject> twitchOptionObjects = new List<GameObject>();

	// Token: 0x04000DA7 RID: 3495
	private float remainingTime = 30f;

	// Token: 0x04000DA8 RID: 3496
	private float resultTimeout = 15f;

	// Token: 0x04000DA9 RID: 3497
	private int votes;

	// Token: 0x04000DAA RID: 3498
	private CanvasGroup canvasGroup;

	// Token: 0x04000DAB RID: 3499
	private string winningItem = "";

	// Token: 0x0200047B RID: 1147
	private enum State
	{
		// Token: 0x04001A60 RID: 6752
		Poll,
		// Token: 0x04001A61 RID: 6753
		Result,
		// Token: 0x04001A62 RID: 6754
		None
	}
}

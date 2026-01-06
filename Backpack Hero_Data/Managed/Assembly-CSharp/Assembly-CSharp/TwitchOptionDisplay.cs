using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A9 RID: 425
public class TwitchOptionDisplay : CustomInputHandler
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000A028B File Offset: 0x0009E48B
	// (set) Token: 0x060010D6 RID: 4310 RVA: 0x000A0293 File Offset: 0x0009E493
	public GameObject previewCard { get; private set; }

	// Token: 0x060010D7 RID: 4311 RVA: 0x000A029C File Offset: 0x0009E49C
	private void Start()
	{
		if (!this.isResult)
		{
			this.UpdateBar(0);
			if (this.AffiliationColors.Count != 0 && this.option != null)
			{
				TwitchOptionDisplay.AffiliationColorPair affiliationColorPair = this.AffiliationColors.Find((TwitchOptionDisplay.AffiliationColorPair x) => x.affiliation == this.option.affiliation);
				if (affiliationColorPair != null)
				{
					base.GetComponent<Image>().color = affiliationColorPair.color;
				}
			}
		}
		this.Setup();
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x000A02FF File Offset: 0x0009E4FF
	private void Update()
	{
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x000A0304 File Offset: 0x0009E504
	public void Setup()
	{
		this.OnCursorEnd();
		this.item = null;
		this.status = null;
		this.enemy = null;
		this.typeOrRarity = null;
		this.simpleText = null;
		if (this.option == null)
		{
			return;
		}
		switch (this.option.action)
		{
		case TwitchPollManager.ActionType.DecideRelics:
		case TwitchPollManager.ActionType.GetItem:
			this.item = (Item2)this.option.param;
			return;
		case TwitchPollManager.ActionType.GetItemType:
			this.typeOrRarity = LangaugeManager.main.GetTextByKey(((Item2.ItemType)this.option.param).ToString());
			return;
		case TwitchPollManager.ActionType.GetItemRarity:
			this.typeOrRarity = LangaugeManager.main.GetTextByKey(((Item2.Rarity)this.option.param).ToString());
			return;
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		case TwitchPollManager.ActionType.EnemyStatusEffect:
			this.status = new StatusEffect();
			this.status.type = ((ValueTuple<StatusEffect.Type, int>)this.option.param).Item1;
			this.status.value = ((ValueTuple<StatusEffect.Type, int>)this.option.param).Item2;
			return;
		case TwitchPollManager.ActionType.Blessing:
			this.simpleText = LangaugeManager.main.GetTextByKey("twitchBlessingCard");
			return;
		case TwitchPollManager.ActionType.Curse:
			this.simpleText = LangaugeManager.main.GetTextByKey("twitchCurseCard");
			return;
		case TwitchPollManager.ActionType.DecideBoss:
			this.enemy = ((DungeonLevel.EnemyEncounter2)this.option.param).enemiesInGroup[0];
			return;
		case TwitchPollManager.ActionType.Hazard:
			this.item = ((GameObject)this.option.param).GetComponent<Item2>();
			return;
		default:
			return;
		}
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x000A04A8 File Offset: 0x0009E6A8
	public void SetVote(int vote)
	{
		this.votes = vote;
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x000A04B4 File Offset: 0x0009E6B4
	public void UpdateBar(int totalVotes)
	{
		if (!this.voteBar)
		{
			return;
		}
		float num;
		if (this.votes == 0)
		{
			num = 0f;
		}
		else
		{
			num = (float)this.votes / (float)totalVotes;
		}
		this.voteBar.fillAmount = num;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000A04F6 File Offset: 0x0009E6F6
	public override void OnCursorHold()
	{
		this.isHovering = true;
		this.OnCursorHoldRarityOrType();
		this.OnCursorHoldSimple();
		this.OnCursorHoldStatus();
		this.OnCursorHoldEnemy();
		this.OnCursorHoldItem();
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x000A0520 File Offset: 0x0009E720
	public void OnCursorHoldEnemy()
	{
		if (this.enemy == null)
		{
			return;
		}
		if (!this.previewCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.enemyCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			this.enemy.GetComponent<Enemy>().ApplyToCard(this.previewCard);
			this.previewCard.GetComponent<Card>().deleteOnDeactivate = false;
		}
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000A059C File Offset: 0x0009E79C
	public void OnCursorHoldStatus()
	{
		if (this.status == null)
		{
			return;
		}
		if (!this.viewingCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.simpleCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			Card component = this.previewCard.GetComponent<Card>();
			if (this.status.type == StatusEffect.Type.toughHide)
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.status.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.status.type)).Replace("/x", 50.ToString() ?? "") }, base.gameObject);
			}
			else
			{
				component.GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.status.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.status.type)).Replace("/x", this.status.value.ToString() ?? "") }, base.gameObject);
			}
			this.viewingCard = true;
		}
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x000A06FC File Offset: 0x0009E8FC
	public void OnCursorHoldRarityOrType()
	{
		if (this.typeOrRarity == null)
		{
			return;
		}
		if (!this.viewingCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.simpleCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			this.previewCard.GetComponent<Card>().GetDescriptionsSimple(new List<string> { LangaugeManager.main.GetTextByKey("twitchItemCard").Replace("/x", this.typeOrRarity) }, base.gameObject);
			this.viewingCard = true;
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000A0790 File Offset: 0x0009E990
	public void OnCursorHoldSimple()
	{
		if (this.simpleText == null)
		{
			return;
		}
		if (!this.viewingCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.simpleCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			this.previewCard.GetComponent<Card>().GetDescriptionsSimple(new List<string> { this.simpleText }, base.gameObject);
			this.viewingCard = true;
		}
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000A080C File Offset: 0x0009EA0C
	public void OnCursorHoldItem()
	{
		if (this.item == null)
		{
			return;
		}
		if (this.viewingCard)
		{
			return;
		}
		if (this.item.itemType.Contains(Item2.ItemType.Carving))
		{
			this.previewCard = Object.Instantiate<GameObject>(this.carvingCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
		}
		else
		{
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
		}
		this.previewCard.transform.position = Vector3.zero;
		this.item.GetComponent<ItemMovement>().ApplyCardToItem(this.previewCard, null, null, false);
		this.previewCard.GetComponent<Card>().deleteOnDeactivate = false;
		this.viewingCard = true;
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x000A08EC File Offset: 0x0009EAEC
	public override void OnCursorEnd()
	{
		this.isHovering = false;
		if (this.enemy != null)
		{
			this.RemoveCardEnemy();
		}
		if (this.status != null)
		{
			this.RemoveCardStatus();
		}
		if (this.typeOrRarity != null)
		{
			this.RemoveCardTypeOrRarity();
		}
		if (this.simpleText != null)
		{
			this.RemoveCardSimple();
		}
		if (this.item != null)
		{
			this.RemoveCard();
		}
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x000A094C File Offset: 0x0009EB4C
	public void RemoveCard()
	{
		if (this.previewCard)
		{
			this.previewCard.GetComponentInChildren<Card>().EndHover();
			this.viewingCard = false;
		}
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x000A0972 File Offset: 0x0009EB72
	private void RemoveCardStatus()
	{
		this.RemoveCardSimple();
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000A097A File Offset: 0x0009EB7A
	private void RemoveCardTypeOrRarity()
	{
		this.RemoveCardSimple();
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x000A0982 File Offset: 0x0009EB82
	private void RemoveCardSimple()
	{
		if (this.previewCard)
		{
			this.previewCard.GetComponentInChildren<Card>().EndHover();
			Object.Destroy(this.previewCard);
			this.viewingCard = false;
		}
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x000A09B3 File Offset: 0x0009EBB3
	private void RemoveCardEnemy()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000A09CD File Offset: 0x0009EBCD
	public void OnDestroy()
	{
		this.OnCursorEnd();
	}

	// Token: 0x04000DBF RID: 3519
	[SerializeField]
	private Image voteBar;

	// Token: 0x04000DC0 RID: 3520
	[SerializeField]
	private List<TwitchOptionDisplay.AffiliationColorPair> AffiliationColors;

	// Token: 0x04000DC1 RID: 3521
	public PollOption option;

	// Token: 0x04000DC2 RID: 3522
	private Item2 item;

	// Token: 0x04000DC3 RID: 3523
	private StatusEffect status;

	// Token: 0x04000DC4 RID: 3524
	private string typeOrRarity;

	// Token: 0x04000DC5 RID: 3525
	private string simpleText;

	// Token: 0x04000DC6 RID: 3526
	private GameObject enemy;

	// Token: 0x04000DC7 RID: 3527
	private bool viewingCard;

	// Token: 0x04000DC8 RID: 3528
	public bool isResult;

	// Token: 0x04000DC9 RID: 3529
	public bool isHovering;

	// Token: 0x04000DCB RID: 3531
	public GameObject simpleCardPrefab;

	// Token: 0x04000DCC RID: 3532
	public GameObject cardPrefab;

	// Token: 0x04000DCD RID: 3533
	public GameObject enemyCardPrefab;

	// Token: 0x04000DCE RID: 3534
	public GameObject carvingCardPrefab;

	// Token: 0x04000DCF RID: 3535
	private int votes;

	// Token: 0x02000480 RID: 1152
	[Serializable]
	public class AffiliationColorPair
	{
		// Token: 0x04001A6D RID: 6765
		public TwitchPollManager.ActionAffiliation affiliation;

		// Token: 0x04001A6E RID: 6766
		public Color color;
	}
}

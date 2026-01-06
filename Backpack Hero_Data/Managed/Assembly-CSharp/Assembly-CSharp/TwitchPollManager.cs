using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class TwitchPollManager : MonoBehaviour
{
	// Token: 0x06000F5D RID: 3933 RVA: 0x00095985 File Offset: 0x00093B85
	private void Start()
	{
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x00095987 File Offset: 0x00093B87
	private void Update()
	{
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0009598C File Offset: 0x00093B8C
	public void Init()
	{
		if (this.currentPoll != null)
		{
			Object.Destroy(this.currentPoll);
			this.currentPoll = null;
		}
		this.newPoll = false;
		this.winningItem = null;
		this.bypassPollDraw = false;
		this.pollReorganizing = false;
		this.combatEndPhase = false;
		this.battlePollCount = 0;
		this.pollRelic = null;
		this.bossPollSpawned = false;
		this.pollBossChosen = false;
		this.playerTurnAction = null;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00095A00 File Offset: 0x00093C00
	public void onFloorStart()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		if (this.setting.specialActions.Contains(TwitchPollManager.ActionType.DecideBoss) && !this.pollBossChosen)
		{
			if (GameManager.main.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
			{
				if (this.currentPoll.options.Exists((PollOption x) => x.action == TwitchPollManager.ActionType.DecideBoss))
				{
					this.currentPoll.EndPoll();
					return;
				}
			}
			else
			{
				this.StartBossPoll();
			}
		}
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00095A96 File Offset: 0x00093C96
	public void onPlayerTurnStart()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		this.ProcessPollPlayerTurn();
		if (!this.pollReorganizing)
		{
			this.onPlayerTurnStartLate();
		}
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00095AC5 File Offset: 0x00093CC5
	public void onPlayerTurnStartLate()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		this.pollReorganizing = false;
		this.StartBattlePoll();
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00095AED File Offset: 0x00093CED
	public void onEnemyTurnStart()
	{
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00095AF0 File Offset: 0x00093CF0
	public void onCombatStart()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		this.battlePollCount = 0;
		this.allValidItems = GameManager.main.itemsToSpawn.ConvertAll<GameObject>((Item2 item) => item.gameObject);
		if (GameManager.main.player.characterName == Character.CharacterName.Tote)
		{
			this.allValidItems.RemoveAll((GameObject item) => !item.GetComponent<Item2>().itemType.Contains(Item2.ItemType.Carving));
		}
		this.validItemTypes = new List<Item2.ItemType>();
		foreach (GameObject gameObject in this.allValidItems)
		{
			foreach (Item2.ItemType itemType in gameObject.GetComponent<Item2>().itemType)
			{
				this.validItemTypes.Add(itemType);
			}
		}
		this.validItemTypes = this.validItemTypes.Distinct<Item2.ItemType>().ToList<Item2.ItemType>();
		if (GameManager.main.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			this.pollBossChosen = false;
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.noRelics) == null && GameManager.main.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && this.setting.specialActions.Contains(TwitchPollManager.ActionType.DecideRelics))
		{
			this.StartRelicPoll();
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00095C88 File Offset: 0x00093E88
	public void onAbort()
	{
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00095C8C File Offset: 0x00093E8C
	public Item2 onCombatEnd()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return null;
		}
		this.battlePollCount = 0;
		if (TwitchManager.Instance.pollManager.isPollRunning())
		{
			this.bypassPollDraw = true;
			this.currentPoll.EndPoll();
		}
		this.combatEndPhase = true;
		this.ProcessPollPlayerTurn();
		this.combatEndPhase = false;
		return this.winningItem;
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00095CF8 File Offset: 0x00093EF8
	public void StartBossPoll()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		if (this.currentPoll != null)
		{
			if (this.currentPoll.options.Exists((PollOption x) => x.action == TwitchPollManager.ActionType.DecideBoss))
			{
				return;
			}
		}
		List<DungeonLevel.EnemyEncounter2> possibleBossEncounters = Object.FindObjectOfType<DungeonSpawner>().GetPossibleBossEncounters();
		List<DungeonLevel.EnemyEncounter2> list = new List<DungeonLevel.EnemyEncounter2>();
		for (int i = 0; i < this.setting.optionCount; i++)
		{
			if (possibleBossEncounters.Count > 0)
			{
				int num = Random.Range(0, possibleBossEncounters.Count);
				list.Add(possibleBossEncounters[num]);
				possibleBossEncounters.RemoveAt(num);
			}
		}
		Debug.Log("Possible bosses to vote on" + list.Count.ToString());
		if (list.Count > 1)
		{
			if (TwitchManager.Instance.pollManager.isPollRunning())
			{
				this.bypassPollDraw = true;
				this.currentPoll.EndPoll();
			}
			List<PollOption> list2 = new List<PollOption>();
			foreach (DungeonLevel.EnemyEncounter2 enemyEncounter in list)
			{
				string text = Enemy.GetRealName(enemyEncounter.enemiesInGroup[0].gameObject.name);
				text = LangaugeManager.main.GetTextByKey(text);
				list2.Add(new PollOption(text, TwitchPollManager.ActionType.DecideBoss, enemyEncounter, TwitchPollManager.ActionAffiliation.Neutral));
			}
			this.battlePollCount++;
			TwitchManager.Instance.pollManager.createAndStartPoll(LangaugeManager.main.GetTextByKey("twitchPollTitleBoss"), list2, this.setting.pollTimeout, Singleton.Instance.twitchPollCheckDuplicate, new OnPollEndDelegate(this.OnPollEnd), null);
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00095ED0 File Offset: 0x000940D0
	public void StartRelicPoll()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		List<Item2> possibleRelics = GameManager.main.getPossibleRelics();
		List<Item2> list = new List<Item2>();
		for (int i = 0; i < this.setting.optionCount; i++)
		{
			if (possibleRelics.Count > 0)
			{
				int num = Random.Range(0, possibleRelics.Count);
				list.Add(possibleRelics[num]);
				possibleRelics.RemoveAt(num);
			}
		}
		if (list.Count > 1)
		{
			if (TwitchManager.Instance.pollManager.isPollRunning())
			{
				this.bypassPollDraw = true;
				this.currentPoll.EndPoll();
			}
			List<PollOption> list2 = new List<PollOption>();
			foreach (Item2 item in list)
			{
				string text = Item2.GetDisplayName(item.name);
				text = LangaugeManager.main.GetTextByKey(text);
				list2.Add(new PollOption(text, TwitchPollManager.ActionType.DecideRelics, item, TwitchPollManager.ActionAffiliation.Special));
			}
			this.battlePollCount++;
			TwitchManager.Instance.pollManager.createAndStartPoll(LangaugeManager.main.GetTextByKey("twitchPollTitleRelic"), list2, this.setting.pollTimeout, Singleton.Instance.twitchPollCheckDuplicate, new OnPollEndDelegate(this.OnPollEnd), null);
		}
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x00096038 File Offset: 0x00094238
	public void StartBattlePoll()
	{
		if (!Singleton.Instance.twitchIntegrationEnabled || !Singleton.Instance.twitchEnablePolls)
		{
			return;
		}
		if (TwitchManager.Instance.pollManager.isPollRunning())
		{
			return;
		}
		if (this.battlePollCount >= this.setting.pollsPerCombat)
		{
			return;
		}
		List<TwitchPollManager.ActionType> list = new List<TwitchPollManager.ActionType>(this.setting.battleActionsNegative);
		if (list.Contains(TwitchPollManager.ActionType.Curse))
		{
			if (GameManager.main.player.stats.GetStatusEffects().Find((StatusEffect x) => x.type == StatusEffect.Type.curse))
			{
				list.Remove(TwitchPollManager.ActionType.Curse);
			}
		}
		if (this.setting.battleActionsPositive.Count == 0 && list.Count == 0)
		{
			return;
		}
		List<PollOption> list2 = new List<PollOption>();
		int num = 0;
		int num2 = 0;
		if (list.Count == 0)
		{
			num = this.setting.optionCount;
		}
		else if (this.setting.battleActionsPositive.Count == 0)
		{
			num2 = this.setting.optionCount;
		}
		else
		{
			num = Random.Range(1, this.setting.optionCount);
			num2 = this.setting.optionCount - num;
		}
		int num3 = 50;
		for (int i = 0; i < num; i++)
		{
			bool flag = false;
			for (int j = 0; j < num3; j++)
			{
				PollOption optionToAdd2 = this.BattlePollPositive();
				if (!list2.Exists((PollOption x) => TwitchPollManager.PollOptionIsSimilar(x, optionToAdd2)))
				{
					flag = true;
					list2.Add(optionToAdd2);
					break;
				}
			}
			if (!flag)
			{
				list2.Add(this.BattlePollPositive());
			}
		}
		for (int k = 0; k < num2; k++)
		{
			bool flag2 = false;
			for (int l = 0; l < num3; l++)
			{
				PollOption optionToAdd = this.BattlePollNegative(list);
				if (!list2.Exists((PollOption x) => TwitchPollManager.PollOptionIsSimilar(x, optionToAdd)))
				{
					flag2 = true;
					list2.Add(optionToAdd);
					break;
				}
			}
			if (!flag2)
			{
				list2.Add(this.BattlePollNegative(list));
			}
		}
		if (list2.Count > 1)
		{
			list2 = list2.OrderBy((PollOption a) => Random.value).ToList<PollOption>();
			this.battlePollCount++;
			TwitchManager.Instance.pollManager.createAndStartPoll(LangaugeManager.main.GetTextByKey("twitchPollTitleNext"), list2, this.setting.pollTimeout, Singleton.Instance.twitchPollCheckDuplicate, new OnPollEndDelegate(this.OnPollEnd), null);
		}
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x000962C8 File Offset: 0x000944C8
	private PollOption BattlePollPositive()
	{
		TwitchPollManager.ActionType actionType = this.setting.battleActionsPositive[Random.Range(0, this.setting.battleActionsPositive.Count)];
		switch (actionType)
		{
		case TwitchPollManager.ActionType.GetItem:
		{
			bool flag;
			GameObject gameObject = Item2.ChooseRandomFromList(Item2.GetItemOfRarity(GameManager.main.ChooseRarity(out flag, 0f, true), this.allValidItems), true);
			if (this.itemOverride != null)
			{
				gameObject = this.itemOverride;
			}
			Item2 component = gameObject.GetComponent<Item2>();
			if (component == null)
			{
				return this.BattlePollPositive();
			}
			string text = Item2.GetDisplayName(gameObject.name);
			text = LangaugeManager.main.GetTextByKey(text);
			return new PollOption(text, TwitchPollManager.ActionType.GetItem, component, TwitchPollManager.ActionAffiliation.Positive);
		}
		case TwitchPollManager.ActionType.GetItemType:
		{
			Item2.ItemType itemType = this.validItemTypes[Random.Range(0, this.validItemTypes.Count)];
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollItemType").Replace("/x", LangaugeManager.main.GetTextByKey(itemType.ToString())), TwitchPollManager.ActionType.GetItemType, itemType, TwitchPollManager.ActionAffiliation.Positive);
		}
		case TwitchPollManager.ActionType.GetItemRarity:
		{
			Item2.Rarity rarity = (Item2.Rarity)Random.Range(0, Enum.GetValues(typeof(Item2.Rarity)).Length);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollItemRarity").Replace("/x", LangaugeManager.main.GetTextByKey(rarity.ToString())), TwitchPollManager.ActionType.GetItemRarity, rarity, TwitchPollManager.ActionAffiliation.Positive);
		}
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		{
			StatusEffect.Type type = this.statusEffectsPlayerPositive[Random.Range(0, this.statusEffectsPlayerPositive.Count)];
			int num = Random.Range(1, 7);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollStatusPlayer").Replace("/x", num.ToString()).Replace("/y", LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(type))), TwitchPollManager.ActionType.PlayerStatusEffect, new ValueTuple<StatusEffect.Type, int>(type, num), TwitchPollManager.ActionAffiliation.Positive);
		}
		case TwitchPollManager.ActionType.EnemyStatusEffect:
		{
			StatusEffect.Type type2 = this.statusEffectsEnemyNegative[Random.Range(0, this.statusEffectsEnemyNegative.Count)];
			int num2 = Random.Range(1, 7);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollStatusEnemy").Replace("/x", num2.ToString()).Replace("/y", LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(type2))), TwitchPollManager.ActionType.EnemyStatusEffect, new ValueTuple<StatusEffect.Type, int>(type2, num2), TwitchPollManager.ActionAffiliation.Positive);
		}
		case TwitchPollManager.ActionType.Blessing:
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollBlessing"), TwitchPollManager.ActionType.Blessing, null, TwitchPollManager.ActionAffiliation.Positive);
		default:
			Debug.LogError("Poll Creation Unhandled Poll Action Type " + actionType.ToString());
			return null;
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00096574 File Offset: 0x00094774
	private PollOption BattlePollNegative(List<TwitchPollManager.ActionType> updatedNegativeActions)
	{
		TwitchPollManager.ActionType actionType = updatedNegativeActions[Random.Range(0, updatedNegativeActions.Count)];
		switch (actionType)
		{
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		{
			StatusEffect.Type type = this.statusEffectsPlayerNegative[Random.Range(0, this.statusEffectsPlayerNegative.Count)];
			int num = Random.Range(1, 7);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollStatusPlayer").Replace("/x", num.ToString()).Replace("/y", LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(type))), TwitchPollManager.ActionType.PlayerStatusEffect, new ValueTuple<StatusEffect.Type, int>(type, num), TwitchPollManager.ActionAffiliation.Negative);
		}
		case TwitchPollManager.ActionType.EnemyStatusEffect:
		{
			StatusEffect.Type type2 = this.statusEffectsEnemyPositive[Random.Range(0, this.statusEffectsEnemyPositive.Count)];
			int num2 = Random.Range(1, 7);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollStatusEnemy").Replace("/x", num2.ToString()).Replace("/y", LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(type2))), TwitchPollManager.ActionType.EnemyStatusEffect, new ValueTuple<StatusEffect.Type, int>(type2, num2), TwitchPollManager.ActionAffiliation.Negative);
		}
		case TwitchPollManager.ActionType.Curse:
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollCurse"), TwitchPollManager.ActionType.Curse, null, TwitchPollManager.ActionAffiliation.Negative);
		case TwitchPollManager.ActionType.Hazard:
		{
			GameObject gameObject = this.hazards[Random.Range(0, this.hazards.Count)];
			string text = Item2.GetDisplayName(gameObject.name);
			text = LangaugeManager.main.GetTextByKey(text);
			return new PollOption(LangaugeManager.main.GetTextByKey("twitchPollHazard").Replace("/x", text), TwitchPollManager.ActionType.Hazard, gameObject, TwitchPollManager.ActionAffiliation.Negative);
		}
		}
		Debug.LogError("Poll Creation Unhandled Poll Action Type " + actionType.ToString());
		return null;
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00096738 File Offset: 0x00094938
	public static bool PollOptionIsSimilar(PollOption a, PollOption b)
	{
		if (a.action != b.action)
		{
			return false;
		}
		if (a.param == null)
		{
			return true;
		}
		if (a.param.GetType() != b.param.GetType())
		{
			return false;
		}
		switch (a.action)
		{
		case TwitchPollManager.ActionType.GetItem:
			return ((Item2)a.param).name == ((Item2)b.param).name;
		case TwitchPollManager.ActionType.GetItemType:
			return (Item2.ItemType)a.param == (Item2.ItemType)b.param;
		case TwitchPollManager.ActionType.GetItemRarity:
			return (Item2.Rarity)a.param == (Item2.Rarity)b.param;
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		{
			ref ValueTuple<StatusEffect.Type, int> ptr = (ValueTuple<StatusEffect.Type, int>)a.param;
			ValueTuple<StatusEffect.Type, int> valueTuple = (ValueTuple<StatusEffect.Type, int>)b.param;
			return ptr.Item1 == valueTuple.Item1;
		}
		case TwitchPollManager.ActionType.EnemyStatusEffect:
		{
			ref ValueTuple<StatusEffect.Type, int> ptr2 = (ValueTuple<StatusEffect.Type, int>)a.param;
			ValueTuple<StatusEffect.Type, int> valueTuple2 = (ValueTuple<StatusEffect.Type, int>)b.param;
			return ptr2.Item1 == valueTuple2.Item1;
		}
		case TwitchPollManager.ActionType.Blessing:
		case TwitchPollManager.ActionType.Curse:
			return true;
		case TwitchPollManager.ActionType.Hazard:
		{
			string displayName = ((GameObject)a.param).GetComponent<Item2>().displayName;
			string displayName2 = ((GameObject)b.param).GetComponent<Item2>().displayName;
			return displayName == displayName2;
		}
		}
		Debug.Log("PollOption is Similar Unhandled Type " + a.action.ToString());
		return false;
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000968B0 File Offset: 0x00094AB0
	public void OnPollEnd(List<PollOption> ranking, bool noVotes, bool draw)
	{
		if (this.currentPoll.pollStatus == PollStatus.ResultProcessed)
		{
			return;
		}
		this.currentPoll.pollStatus = PollStatus.ResultProcessed;
		if (noVotes || ranking.Count == 0)
		{
			Debug.Log("Nobody voted or invalid poll");
			return;
		}
		if (draw)
		{
			if (!this.bypassPollDraw)
			{
				int count = ranking[0].count;
				List<PollOption> list = new List<PollOption>();
				foreach (PollOption pollOption in ranking)
				{
					if (pollOption.count == count)
					{
						pollOption.count = 0;
						list.Add(pollOption);
					}
				}
				TwitchManager.Instance.pollManager.createAndStartPoll(this.currentPoll.pollTitle, list, this.setting.pollTimeout, Singleton.Instance.twitchPollCheckDuplicate, new OnPollEndDelegate(this.OnPollEnd), null);
				return;
			}
			List<PollOption> list2 = ranking.FindAll((PollOption x) => x.count == ranking[0].count);
			ranking = new List<PollOption> { list2[Random.Range(0, list2.Count)] };
			this.bypassPollDraw = false;
		}
		this.bypassPollDraw = false;
		PollOption pollOption2 = ranking[0];
		TwitchManager.Instance.MakeAnnouncement(LangaugeManager.main.GetTextByKey("twitchPollChatMsg").Replace("/x", pollOption2.text));
		switch (pollOption2.action)
		{
		case TwitchPollManager.ActionType.DecideRelics:
			this.pollRelic = (Item2)pollOption2.param;
			break;
		case TwitchPollManager.ActionType.GetItem:
		case TwitchPollManager.ActionType.GetItemType:
		case TwitchPollManager.ActionType.GetItemRarity:
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		case TwitchPollManager.ActionType.EnemyStatusEffect:
		case TwitchPollManager.ActionType.Blessing:
		case TwitchPollManager.ActionType.Curse:
		case TwitchPollManager.ActionType.Hazard:
			this.playerTurnAction = pollOption2;
			break;
		case TwitchPollManager.ActionType.DecideBoss:
			Object.FindObjectOfType<DungeonSpawner>().SetBoss((DungeonLevel.EnemyEncounter2)pollOption2.param);
			this.pollBossChosen = true;
			break;
		default:
			Debug.LogError("Poll End Unhandled Poll Action Type " + pollOption2.action.ToString());
			break;
		}
		this.currentPoll.winner = pollOption2;
		Debug.Log("Poll Winner: " + pollOption2.text);
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x00096AFC File Offset: 0x00094CFC
	public void ProcessPollPlayerTurn()
	{
		if (this.playerTurnAction == null)
		{
			return;
		}
		switch (this.playerTurnAction.action)
		{
		case TwitchPollManager.ActionType.GetItem:
			this.SpawnItem((Item2)this.playerTurnAction.param);
			goto IL_03A6;
		case TwitchPollManager.ActionType.GetItemType:
		{
			GameObject gameObject = GameManager.main.SpawnItemOfType((Item2.ItemType)this.playerTurnAction.param);
			if (!gameObject)
			{
				Debug.LogError("Failed to spawn item of type " + this.playerTurnAction.param.ToString());
				goto IL_03A6;
			}
			this.ReorganizeItem(gameObject);
			goto IL_03A6;
		}
		case TwitchPollManager.ActionType.GetItemRarity:
		{
			List<GameObject> itemOfRarity = Item2.GetItemOfRarity((Item2.Rarity)this.playerTurnAction.param, this.allValidItems);
			if (itemOfRarity.Count == 0)
			{
				Debug.LogError("Failed to spawn item of rarity " + this.playerTurnAction.param.ToString());
				goto IL_03A6;
			}
			this.SpawnItem(Item2.ChooseRandomFromList(itemOfRarity, true).GetComponent<Item2>());
			goto IL_03A6;
		}
		case TwitchPollManager.ActionType.PlayerStatusEffect:
		{
			if (this.combatEndPhase)
			{
				return;
			}
			ValueTuple<StatusEffect.Type, int> valueTuple = (ValueTuple<StatusEffect.Type, int>)this.playerTurnAction.param;
			GameManager.main.player.stats.AddStatusEffect(valueTuple.Item1, (float)valueTuple.Item2, Item2.Effect.MathematicalType.summative);
			goto IL_03A6;
		}
		case TwitchPollManager.ActionType.EnemyStatusEffect:
		{
			if (this.combatEndPhase)
			{
				return;
			}
			ValueTuple<StatusEffect.Type, int> valueTuple2 = (ValueTuple<StatusEffect.Type, int>)this.playerTurnAction.param;
			Enemy[] array = Object.FindObjectsOfType<Enemy>();
			if (array.Length != 0)
			{
				array[Random.Range(0, array.Length)].stats.AddStatusEffect(valueTuple2.Item1, (float)valueTuple2.Item2, Item2.Effect.MathematicalType.summative);
				goto IL_03A6;
			}
			return;
		}
		case TwitchPollManager.ActionType.Blessing:
		{
			GameObject gameObject2 = GameManager.main.SpawnCurseOrBlessing(Random.Range(1, 2), Curse.Type.Blessing);
			if (this.combatEndPhase)
			{
				GameManager.main.MoveAllItems();
				goto IL_03A6;
			}
			this.pollReorganizing = true;
			base.StartCoroutine(GameManager.main.CreateCurseReorg(new List<GameObject> { gameObject2 }, false));
			goto IL_03A6;
		}
		case TwitchPollManager.ActionType.Curse:
		{
			if (this.combatEndPhase)
			{
				return;
			}
			int num = Mathf.RoundToInt(Random.Range(7f + (float)GameManager.main.floor * 1.5f, 12f + (float)GameManager.main.floor * 1.5f));
			GameManager.main.player.stats.AddStatusEffect(StatusEffect.Type.curse, (float)num, Item2.Effect.MathematicalType.summative);
			goto IL_03A6;
		}
		case TwitchPollManager.ActionType.Hazard:
		{
			if (this.combatEndPhase)
			{
				return;
			}
			GameObject gameObject3 = (GameObject)this.playerTurnAction.param;
			int num2 = Mathf.RoundToInt(Random.Range(7f + (float)GameManager.main.floor * 1.5f, 12f + (float)GameManager.main.floor * 1.5f) / 9f);
			num2 = ((num2 < 1) ? 1 : num2);
			List<GameObject> list = new List<GameObject>();
			Vector2 vector = base.transform.position;
			vector += new Vector2(-1.5f, -1.5f) * (float)num2 / 2f;
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject4 = GameManager.main.CreateCursePublic(new List<GameObject> { gameObject3 });
				gameObject4.transform.position = vector + new Vector2((float)i * 1.5f, (float)i * 1.5f);
				list.Add(gameObject4);
			}
			base.StartCoroutine(GameManager.main.CreateCurseReorg(list, false));
			goto IL_03A6;
		}
		}
		Debug.LogError("Process Player Turn Unhandled Poll action! " + this.playerTurnAction.action.ToString());
		IL_03A6:
		if (this.currentPoll != null)
		{
			this.currentPoll.pollStatus = PollStatus.Fulfilled;
		}
		this.playerTurnAction = null;
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00096ED0 File Offset: 0x000950D0
	private void SpawnItem(Item2 item)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(item.gameObject, Vector3.zero, Quaternion.identity);
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("ItemParent");
		if (gameObject2 != null && !this.combatEndPhase)
		{
			gameObject.transform.SetParent(gameObject2.transform);
		}
		this.ReorganizeItem(gameObject);
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00096F28 File Offset: 0x00095128
	private void ReorganizeItem(GameObject item)
	{
		if (item.GetComponent<Item2>().itemType.Contains(Item2.ItemType.Carving))
		{
			base.StartCoroutine("SpawnCarvingRoutine", item);
			return;
		}
		if (this.combatEndPhase)
		{
			GameManager.main.MoveAllItems();
			return;
		}
		List<Vector2> list = new List<Vector2>();
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			list.Add(new Vector2(gridSquare.transform.localPosition.x, gridSquare.transform.localPosition.y));
		}
		this.pollReorganizing = true;
		base.StartCoroutine("SpawnItemRoutine", new ValueTuple<GameObject, List<Vector2>>(item, list));
		SpriteRenderer component = item.GetComponent<SpriteRenderer>();
		if (component)
		{
			component.enabled = true;
			component.sortingOrder = 2;
		}
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00097018 File Offset: 0x00095218
	private IEnumerator SpawnItemRoutine([TupleElementNames(new string[] { "item", "vecs" })] ValueTuple<GameObject, List<Vector2>> param)
	{
		yield return new WaitForEndOfFrame();
		GameManager.main.StartCoroutine(GameManager.main.StartOverwriteOnlyReorg(new List<GameObject> { param.Item1 }, true));
		yield break;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00097027 File Offset: 0x00095227
	private IEnumerator SpawnCarvingRoutine(GameObject item)
	{
		yield return new WaitForEndOfFrame();
		Object.FindObjectOfType<Tote>().AddNewCarvingToUndrawn(item);
		yield break;
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00097038 File Offset: 0x00095238
	public TwitchPoll createPoll(string title, List<PollOption> pollOptions, float timeout, bool checkDuplicate, OnPollEndDelegate pollEndCallback, OnVotedDelegate pollVoteCallback)
	{
		if (this.currentPoll != null)
		{
			Object.Destroy(this.currentPoll.gameObject);
		}
		TwitchPoll component = Object.Instantiate<GameObject>(this.pollPrefab).GetComponent<TwitchPoll>();
		component.Init(this.twitchManager, this.twitchChat, title, pollOptions, timeout, checkDuplicate);
		component.SubscribeEndPoll(pollEndCallback);
		component.SubscribeVoted(pollVoteCallback);
		this.currentPoll = component;
		this.newPoll = true;
		return component;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000970AA File Offset: 0x000952AA
	public TwitchPoll createAndStartPoll(string title, List<PollOption> pollOptions, float timeout, bool checkDuplicate, OnPollEndDelegate pollEndCallback, OnVotedDelegate pollVoteCallback)
	{
		TwitchPoll twitchPoll = this.createPoll(title, pollOptions, timeout, checkDuplicate, pollEndCallback, pollVoteCallback);
		twitchPoll.StartPoll();
		return twitchPoll;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000970C1 File Offset: 0x000952C1
	public bool isPollRunning()
	{
		return !(this.currentPoll == null) && this.currentPoll.pollStatus == PollStatus.Running;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000970E4 File Offset: 0x000952E4
	public bool isNewPoll()
	{
		if (this.newPoll)
		{
			this.newPoll = false;
			return true;
		}
		return false;
	}

	// Token: 0x04000C7D RID: 3197
	[SerializeField]
	private GameObject itemOverride;

	// Token: 0x04000C7E RID: 3198
	public GameObject pollPrefab;

	// Token: 0x04000C7F RID: 3199
	public TwitchPoll currentPoll;

	// Token: 0x04000C80 RID: 3200
	private bool newPoll;

	// Token: 0x04000C81 RID: 3201
	public TwitchManager twitchManager;

	// Token: 0x04000C82 RID: 3202
	public TwitchChat twitchChat;

	// Token: 0x04000C83 RID: 3203
	public Item2 winningItem;

	// Token: 0x04000C84 RID: 3204
	public bool bypassPollDraw;

	// Token: 0x04000C85 RID: 3205
	private List<GameObject> allValidItems = new List<GameObject>();

	// Token: 0x04000C86 RID: 3206
	private List<Item2.ItemType> validItemTypes = new List<Item2.ItemType>();

	// Token: 0x04000C87 RID: 3207
	public bool pollReorganizing;

	// Token: 0x04000C88 RID: 3208
	public bool combatEndPhase;

	// Token: 0x04000C89 RID: 3209
	public int battlePollCount;

	// Token: 0x04000C8A RID: 3210
	public Item2 pollRelic;

	// Token: 0x04000C8B RID: 3211
	public bool bossPollSpawned;

	// Token: 0x04000C8C RID: 3212
	public bool pollBossChosen;

	// Token: 0x04000C8D RID: 3213
	public PollOption playerTurnAction;

	// Token: 0x04000C8E RID: 3214
	[SerializeField]
	private List<StatusEffect.Type> statusEffectsPlayerPositive = new List<StatusEffect.Type>();

	// Token: 0x04000C8F RID: 3215
	[SerializeField]
	private List<StatusEffect.Type> statusEffectsPlayerNegative = new List<StatusEffect.Type>();

	// Token: 0x04000C90 RID: 3216
	[SerializeField]
	private List<StatusEffect.Type> statusEffectsEnemyPositive = new List<StatusEffect.Type>();

	// Token: 0x04000C91 RID: 3217
	[SerializeField]
	private List<StatusEffect.Type> statusEffectsEnemyNegative = new List<StatusEffect.Type>();

	// Token: 0x04000C92 RID: 3218
	[SerializeField]
	public List<GameObject> hazards;

	// Token: 0x04000C93 RID: 3219
	[SerializeField]
	public TwitchPollManager.PollSetting setting;

	// Token: 0x04000C94 RID: 3220
	[SerializeField]
	public List<TwitchPollManager.PollSetting> settingsPresets;

	// Token: 0x02000451 RID: 1105
	[Serializable]
	public class PollSetting
	{
		// Token: 0x06001A4E RID: 6734 RVA: 0x000D3DA8 File Offset: 0x000D1FA8
		public static T DeepCopy<T>(T other)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, other);
				memoryStream.Position = 0L;
				t = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return t;
		}

		// Token: 0x040019AC RID: 6572
		public string name = "twitchPresetCustom";

		// Token: 0x040019AD RID: 6573
		public int optionCount = 4;

		// Token: 0x040019AE RID: 6574
		public int pollsPerCombat = 3;

		// Token: 0x040019AF RID: 6575
		public float pollTimeout = 60f;

		// Token: 0x040019B0 RID: 6576
		[SerializeField]
		public bool disableLoot;

		// Token: 0x040019B1 RID: 6577
		[SerializeField]
		public List<TwitchPollManager.ActionType> battleActionsPositive;

		// Token: 0x040019B2 RID: 6578
		[SerializeField]
		public List<TwitchPollManager.ActionType> battleActionsNegative;

		// Token: 0x040019B3 RID: 6579
		[SerializeField]
		public List<TwitchPollManager.ActionType> roamingActions;

		// Token: 0x040019B4 RID: 6580
		[SerializeField]
		public List<TwitchPollManager.ActionType> specialActions;
	}

	// Token: 0x02000452 RID: 1106
	public enum ActionType
	{
		// Token: 0x040019B6 RID: 6582
		DecideRelics,
		// Token: 0x040019B7 RID: 6583
		GetItem,
		// Token: 0x040019B8 RID: 6584
		GetItemType,
		// Token: 0x040019B9 RID: 6585
		GetItemRarity,
		// Token: 0x040019BA RID: 6586
		PlayerStatusEffect,
		// Token: 0x040019BB RID: 6587
		EnemyStatusEffect,
		// Token: 0x040019BC RID: 6588
		Blessing,
		// Token: 0x040019BD RID: 6589
		Curse,
		// Token: 0x040019BE RID: 6590
		DecideBoss,
		// Token: 0x040019BF RID: 6591
		Hazard
	}

	// Token: 0x02000453 RID: 1107
	public enum ActionAffiliation
	{
		// Token: 0x040019C1 RID: 6593
		Positive,
		// Token: 0x040019C2 RID: 6594
		Negative,
		// Token: 0x040019C3 RID: 6595
		Neutral,
		// Token: 0x040019C4 RID: 6596
		Special
	}
}

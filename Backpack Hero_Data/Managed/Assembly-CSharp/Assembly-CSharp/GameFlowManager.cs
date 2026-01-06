using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class GameFlowManager : MonoBehaviour
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x0006560D File Offset: 0x0006380D
	private void Awake()
	{
		GameFlowManager.main = this;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00065615 File Offset: 0x00063815
	private void OnDestroy()
	{
		if (GameFlowManager.main == this)
		{
			GameFlowManager.main = null;
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0006562C File Offset: 0x0006382C
	private void ResetCombatStatsForTurn()
	{
		for (int i = 0; i < this.combatStats.Count; i++)
		{
			bool flag = false;
			if (this.combatStats[i].length == GameFlowManager.CombatStat.Length.turn)
			{
				for (int j = 0; j < this.combatStats.Count; j++)
				{
					if (this.combatStats[j].length == GameFlowManager.CombatStat.Length.combat && this.combatStats[j].type == this.combatStats[i].type)
					{
						this.combatStats[j].value += this.combatStats[i].value;
						this.combatStats.RemoveAt(i);
						i--;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.combatStats[i].length = GameFlowManager.CombatStat.Length.combat;
				}
			}
		}
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00065710 File Offset: 0x00063910
	public int GetCombatStat(GameFlowManager.CombatStat.Type type, GameFlowManager.CombatStat.Length length = GameFlowManager.CombatStat.Length.combat)
	{
		int num = 0;
		foreach (GameFlowManager.CombatStat combatStat in this.combatStats)
		{
			if (combatStat.type == type && combatStat.length == GameFlowManager.CombatStat.Length.turn && length == GameFlowManager.CombatStat.Length.turn)
			{
				return combatStat.value;
			}
			if (combatStat.type == type && length == GameFlowManager.CombatStat.Length.combat)
			{
				num += combatStat.value;
			}
		}
		if (num != 0)
		{
			return num;
		}
		return -1;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0006579C File Offset: 0x0006399C
	public int GetCombatStatSpecificItem(Item2 item, GameFlowManager.CombatStat.Length length)
	{
		foreach (GameFlowManager.CombatStat combatStat in this.combatStats)
		{
			if (combatStat.type == GameFlowManager.CombatStat.Type.specificItemUsed && combatStat.item == item && combatStat.length == length)
			{
				return combatStat.value;
			}
		}
		return -1;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00065814 File Offset: 0x00063A14
	public void AddCombatStat(GameFlowManager.CombatStat.Type type, int value, Item2 item = null)
	{
		foreach (GameFlowManager.CombatStat combatStat in this.combatStats)
		{
			if (combatStat.type == type && combatStat.length == GameFlowManager.CombatStat.Length.turn && combatStat.item == item)
			{
				combatStat.value += value;
				return;
			}
		}
		GameFlowManager.CombatStat combatStat2 = new GameFlowManager.CombatStat();
		combatStat2.type = type;
		combatStat2.value = value;
		combatStat2.item = item;
		combatStat2.length = GameFlowManager.CombatStat.Length.turn;
		this.combatStats.Add(combatStat2);
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000658C0 File Offset: 0x00063AC0
	private void ConsideFirstUse(Item2 item)
	{
		if (this.GetCombatStatSpecificItem(item, GameFlowManager.CombatStat.Length.turn) == -1)
		{
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onFirstUse, item, null, true, false);
		}
		this.AddCombatStat(GameFlowManager.CombatStat.Type.specificItemUsed, 1, item);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x000658E4 File Offset: 0x00063AE4
	private void ClearRecordedItemTypesOfLength(GameFlowManager.RecordedTypesUsed.Length length)
	{
		for (int i = 0; i < this.recordedTypesUsed.Count; i++)
		{
			if (this.recordedTypesUsed[i].length == length)
			{
				this.recordedTypesUsed.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0006592C File Offset: 0x00063B2C
	private void RecordItemUse(List<Item2.ItemType> types)
	{
		foreach (Item2.ItemType itemType in types)
		{
			this.AddItemTypesUsed(itemType, GameFlowManager.RecordedTypesUsed.Length.thisTurn);
			this.AddItemTypesUsed(itemType, GameFlowManager.RecordedTypesUsed.Length.thisCombat);
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00065984 File Offset: 0x00063B84
	private void AddItemTypesUsed(Item2.ItemType type, List<GameFlowManager.RecordedTypesUsed.Length> lengths)
	{
		foreach (GameFlowManager.RecordedTypesUsed.Length length in lengths)
		{
			this.AddItemTypesUsed(type, length);
		}
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000659D4 File Offset: 0x00063BD4
	private void AddItemTypesUsed(Item2.ItemType type, GameFlowManager.RecordedTypesUsed.Length length)
	{
		foreach (GameFlowManager.RecordedTypesUsed recordedTypesUsed in this.recordedTypesUsed)
		{
			if (recordedTypesUsed.type == type && recordedTypesUsed.length == length)
			{
				return;
			}
		}
		this.recordedTypesUsed.Add(new GameFlowManager.RecordedTypesUsed(type, length));
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00065A48 File Offset: 0x00063C48
	private void SwapRecordedItemTurns()
	{
		this.ClearRecordedItemTypesOfLength(GameFlowManager.RecordedTypesUsed.Length.lastTurn);
		foreach (GameFlowManager.RecordedTypesUsed recordedTypesUsed in this.recordedTypesUsed)
		{
			if (recordedTypesUsed.length == GameFlowManager.RecordedTypesUsed.Length.thisTurn)
			{
				recordedTypesUsed.length = GameFlowManager.RecordedTypesUsed.Length.lastTurn;
			}
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00065AAC File Offset: 0x00063CAC
	private void SwapRecordedItemCombats()
	{
		this.ClearRecordedItemTypesOfLength(GameFlowManager.RecordedTypesUsed.Length.lastCombat);
		foreach (GameFlowManager.RecordedTypesUsed recordedTypesUsed in this.recordedTypesUsed)
		{
			if (recordedTypesUsed.length == GameFlowManager.RecordedTypesUsed.Length.thisCombat)
			{
				recordedTypesUsed.length = GameFlowManager.RecordedTypesUsed.Length.lastCombat;
			}
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00065B10 File Offset: 0x00063D10
	public bool HasUsedItemType(List<Item2.ItemType> types, GameFlowManager.RecordedTypesUsed.Length length)
	{
		foreach (Item2.ItemType itemType in types)
		{
			if (this.HasUsedItemType(itemType, length))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00065B68 File Offset: 0x00063D68
	public bool HasUsedItemType(Item2.ItemType type, GameFlowManager.RecordedTypesUsed.Length length)
	{
		foreach (GameFlowManager.RecordedTypesUsed recordedTypesUsed in this.recordedTypesUsed)
		{
			if (recordedTypesUsed.type == type && recordedTypesUsed.length == length)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00065BD0 File Offset: 0x00063DD0
	public void AddItemStatusEffectToApplyAtEndOfQueuedActions(Item2 item, Item2.ItemStatusEffect.Type type, Item2.ItemStatusEffect.Length length, int value)
	{
		if (this.isCheckingEffects)
		{
			Item2.Effect effect = new Item2.Effect();
			effect.type = Item2.Effect.Type.ItemStatusEffect;
			effect.itemStatusEffect = new List<Item2.ItemStatusEffect>();
			Item2.ItemStatusEffect itemStatusEffect = new Item2.ItemStatusEffect();
			itemStatusEffect.type = type;
			itemStatusEffect.length = length;
			itemStatusEffect.num = value;
			effect.itemStatusEffect.Add(itemStatusEffect);
			this.itemStatusEffectsToApplyAtEndOfQueuedActions.Add(new GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions(base.name, item, effect, true));
			return;
		}
		item.ChangeStatusEffectValue(type, length, value);
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00065C4C File Offset: 0x00063E4C
	private void Start()
	{
		this.queuedConsiderations = new List<GameFlowManager.Consideration>();
		this.gameManager = GameManager.main;
		this.battlePhase = GameFlowManager.BattlePhase.outOfBattle;
		this.player = Player.main;
		this.playerAnimator = this.player.GetComponentInChildren<Animator>();
		this.combatStats = new List<GameFlowManager.CombatStat>();
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00065CA0 File Offset: 0x00063EA0
	private bool AnyEnemiesAlive()
	{
		bool flag = false;
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (!enemy.dead && !enemy.isPet)
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00065D04 File Offset: 0x00063F04
	private void Update()
	{
		if (this.selectedItem && this.selectedItem.destroyed)
		{
			GameManager.main.EndChooseItem();
		}
		if (this.battlePhase == GameFlowManager.BattlePhase.playerTurn)
		{
			if (!CR8Manager.instance || !CR8Manager.instance.isTesting)
			{
				if (!this.AnyEnemiesAlive())
				{
					if (this.timeToEnd < 0f)
					{
						base.StartCoroutine(this.EndCombat());
					}
					this.timeToEnd -= Time.deltaTime;
				}
				else
				{
					this.timeToEnd = 0.1f;
				}
			}
			if (this.player.characterName == Character.CharacterName.CR8 && !this.isCheckingEffects && CR8Manager.instance && CR8Manager.instance.setToRunForever && !CR8Manager.instance.turnSpent && !this.isRunningAutoEnd && this.battlePhase == GameFlowManager.BattlePhase.playerTurn && !this.endingTurn)
			{
				foreach (Item2 item in Item2.GetItemOfType(Item2.ItemType.Core, Item2.GetAllItemsInGrid()))
				{
					base.StartCoroutine(CR8Manager.instance.StartTurn(false, item.gameObject));
				}
				base.StartCoroutine(this.AutoEndTurn());
			}
		}
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00065E60 File Offset: 0x00064060
	public IEnumerator AutoEndTurn()
	{
		if (this.isRunningAutoEnd)
		{
			yield break;
		}
		this.interupt = false;
		this.isRunningAutoEnd = true;
		yield return null;
		yield return null;
		yield return new WaitForSeconds(0.1f);
		yield return null;
		while (((CR8Manager.instance && CR8Manager.instance.isRunning) || this.isCheckingEffects) && !this.endingTurn && this.battlePhase != GameFlowManager.BattlePhase.outOfBattle && this.battlePhase != GameFlowManager.BattlePhase.enemyTurn)
		{
			Debug.Log("Waiting for auto end turn");
			yield return null;
		}
		this.isRunningAutoEnd = false;
		if (this.interupt)
		{
			this.interupt = false;
			yield break;
		}
		this.interupt = false;
		this.EndTurn();
		yield break;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00065E70 File Offset: 0x00064070
	public void CheckConstants()
	{
		Item2.GetAllItemsInGrid();
		new List<int>();
		for (int i = 0; i < this.queuedConsiderations.Count; i++)
		{
			GameFlowManager.Consideration consideration = this.queuedConsiderations[i];
			if (consideration.trigger == Item2.Trigger.ActionTrigger.constantClearWhile || consideration.trigger == Item2.Trigger.ActionTrigger.constantExtraEarly || consideration.trigger == Item2.Trigger.ActionTrigger.constantEarly || consideration.trigger == Item2.Trigger.ActionTrigger.constant)
			{
				this.queuedConsiderations.RemoveAt(i);
				i--;
			}
		}
		if (this.queuedConsiderations.Count == 0)
		{
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.constantClearWhile, null, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.constantExtraEarly, null, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.constantEarly, null, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.constant, null, null, true, false);
			return;
		}
		this.queuedConsiderations.Insert(0, new GameFlowManager.Consideration(Item2.Trigger.ActionTrigger.constantClearWhile, null, null, null, true));
		this.queuedConsiderations.Insert(1, new GameFlowManager.Consideration(Item2.Trigger.ActionTrigger.constantExtraEarly, null, null, null, true));
		this.queuedConsiderations.Insert(2, new GameFlowManager.Consideration(Item2.Trigger.ActionTrigger.constantEarly, null, null, null, true));
		this.queuedConsiderations.Insert(3, new GameFlowManager.Consideration(Item2.Trigger.ActionTrigger.constant, null, null, null, true));
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00065F7A File Offset: 0x0006417A
	public void PerformActionEffect()
	{
		this.actionPerformed = true;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00065F83 File Offset: 0x00064183
	public void CompleteAction()
	{
		this.actionFinished = true;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00065F8C File Offset: 0x0006418C
	public void AddMarker(MetaProgressSaveManager.MetaProgressMarker marker)
	{
		this.markers.Add(marker);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00065F9A File Offset: 0x0006419A
	public void StartCombat()
	{
		base.StopAllCoroutines();
		this.isCheckingEffects = false;
		this.queuedConsiderations = new List<GameFlowManager.Consideration>();
		base.StartCoroutine(this.StartCombatRoutine());
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00065FC1 File Offset: 0x000641C1
	private IEnumerator StartCombatRoutine()
	{
		this.combatEndedAbruptly = false;
		if (this.gameManager)
		{
			this.gameManager.AnalyzeEnemies();
		}
		if (TwitchManager.isRunningPolls())
		{
			TwitchManager.Instance.pollManager.onCombatStart();
		}
		this.markers = new List<MetaProgressSaveManager.MetaProgressMarker>();
		this.gameManager.StartCombatUI();
		this.SwapRecordedItemCombats();
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager.playType == TutorialManager.PlayType.tutorial && tutorialManager != null)
		{
			tutorialManager.ConsiderTutorial("combatTutorial1");
		}
		if (this.player.characterName == Character.CharacterName.Pochette && tutorialManager != null)
		{
			tutorialManager.ConsiderTutorial("PetsInCombatTutorial");
		}
		this.player.AP = 0;
		this.player.ShowAP();
		this.combatStats = new List<GameFlowManager.CombatStat>();
		this.turnNumber = 0;
		this.gameManager.battleSpawnItems = 0;
		this.gameManager.rewardItems = new List<GameObject>();
		this.actionFinished = true;
		this.actionPerformed = true;
		this.gameManager.inventoryPhase = GameManager.InventoryPhase.locked;
		this.battlePhase = GameFlowManager.BattlePhase.enemyTurn;
		if (tutorialManager && tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.moveToCombat)
		{
			tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.firstCombat;
			this.gameManager.StartTutorial2();
		}
		if (this.player.characterName == Character.CharacterName.Tote)
		{
			Object.FindObjectOfType<Tote>().StartCombat();
		}
		else if (this.player.characterName == Character.CharacterName.CR8)
		{
			Object.FindObjectOfType<CR8Manager>().StartCombat();
		}
		PetMaster.StartCombat();
		ItemPouch.CloseAllPouches();
		this.gameManager.RemoveItemsOutsideGrid();
		this.gameManager.ShowInventory();
		this.playerAnimator.Play("Player_Idle");
		string text = "combat_start";
		if (EventManager.instance != null)
		{
			switch (EventManager.instance.eventType)
			{
			case EventManager.EventType.Winter:
				text = "bph_combat_start_winter";
				goto IL_01F9;
			case EventManager.EventType.Halloween:
				text = "combat_start_halloween";
				goto IL_01F9;
			case EventManager.EventType.Summer:
				text = "combat_start_summer";
				goto IL_01F9;
			}
			text = "combat_start";
		}
		IL_01F9:
		SoundManager.main.PlaySFX(text);
		SoundManager.main.MuteAllSongs();
		if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.bossesEncountered, 1);
			SoundManager.main.PlaySongSudden("battle2", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "The Crypt")
		{
			SoundManager.main.PlaySongSudden("battle", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "The Bramble")
		{
			SoundManager.main.PlaySongSudden("bph_game_combat_bramble", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "Enchanted Swamp")
		{
			SoundManager.main.PlaySongSudden("bph_game_combat_swamp", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "Deep Cave")
		{
			SoundManager.main.PlaySongSudden("bph_game_combat_cave", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "Magma Core")
		{
			SoundManager.main.PlaySongSudden("bph_game_combat_lava", 0.8f, 0f, true);
		}
		else if (this.gameManager.dungeonLevel.areaName == "Frozen Heart")
		{
			SoundManager.main.PlaySongSudden("bph_game_combat_ice", 0.8f, 0f, true);
		}
		else
		{
			int num = Random.Range(0, 3);
			if (num == 0)
			{
				SoundManager.main.PlaySongSudden("battle", 0.8f, 0f, true);
			}
			else if (num == 1)
			{
				SoundManager.main.PlaySongSudden("bph_game_combat_swamp", 0.8f, 0f, true);
			}
			else if (num == 2)
			{
				SoundManager.main.PlaySongSudden("bph_game_combat_bramble", 0.8f, 0f, true);
			}
		}
		this.gameManager.Announcement(LangaugeManager.main.GetTextByKey("gmb"), "gmb");
		yield return new WaitForSeconds(1f);
		this.CheckConstants();
		Enemy enemy = Object.FindObjectOfType<Enemy>();
		if (enemy)
		{
			this.gameManager.SelectEnemy(enemy);
		}
		Object.FindObjectOfType<TutorialManager>().CombatStart();
		this.player.CombatStart();
		yield return this.PlayerTurnStart();
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00065FD0 File Offset: 0x000641D0
	public void AddEndToConsiderations()
	{
		for (int i = 0; i < this.queuedConsiderations.Count; i++)
		{
			if (this.queuedConsiderations[i].trigger == Item2.Trigger.ActionTrigger.endOfActions)
			{
				if (i != this.queuedConsiderations.Count - 1)
				{
					GameFlowManager.Consideration consideration = this.queuedConsiderations[i];
					this.queuedConsiderations.RemoveAt(i);
					this.queuedConsiderations.Add(consideration);
				}
				return;
			}
		}
		this.queuedConsiderations.Add(new GameFlowManager.Consideration(Item2.Trigger.ActionTrigger.endOfActions, null, null, null, true));
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00066054 File Offset: 0x00064254
	public bool CheckForEffectQueue(Item2.Trigger.ActionTrigger trigger, List<Item2> activeItems)
	{
		foreach (GameFlowManager.Consideration consideration in this.queuedConsiderations)
		{
			if (consideration.trigger == trigger)
			{
				int num = 0;
				foreach (Item2 item in activeItems)
				{
					if (consideration.items.Contains(item))
					{
						num++;
					}
				}
				if (num >= activeItems.Count)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00066108 File Offset: 0x00064308
	public void ConsiderAllEffectsPublicList(Item2.Trigger.ActionTrigger trigger, List<Item2> activeItems, List<Status> stat, List<PetMaster> canApplyTriggerToAllItemsAndEnemies = null, bool tryToCheckEarly = false, bool canLaunchEnemyAction = true)
	{
		if (!this.isCheckingEffects)
		{
			this.isnNewCheck = true;
			this.isCheckingEffects = true;
			this.itemsDestroyedInThisEffectSequence = new List<Item2>();
			base.StartCoroutine(this.ConsiderAllEffects(trigger, activeItems, new List<GameFlowManager.Consideration.ConsiderationItems>(), stat, canApplyTriggerToAllItemsAndEnemies, canLaunchEnemyAction));
		}
		else if (!tryToCheckEarly)
		{
			this.queuedConsiderations.Add(new GameFlowManager.Consideration(trigger, activeItems, stat, canApplyTriggerToAllItemsAndEnemies, canLaunchEnemyAction));
		}
		else
		{
			this.queuedConsiderations.Insert(0, new GameFlowManager.Consideration(trigger, activeItems, stat, canApplyTriggerToAllItemsAndEnemies, canLaunchEnemyAction));
		}
		this.AddEndToConsiderations();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0006618C File Offset: 0x0006438C
	public void ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger trigger, Item2 activeItem, List<Status> stat = null, bool canLaunchEnemyAction = true, bool tryToCheckEarly = false)
	{
		if (activeItem)
		{
			this.ConsiderAllEffectsPublicList(trigger, new List<Item2> { activeItem }, stat, null, tryToCheckEarly, canLaunchEnemyAction);
			return;
		}
		this.ConsiderAllEffectsPublicList(trigger, new List<Item2>(), stat, null, tryToCheckEarly, canLaunchEnemyAction);
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000661C2 File Offset: 0x000643C2
	private IEnumerator ConsiderAllEffects(Item2.Trigger.ActionTrigger trigger, List<Item2> activeItems, List<GameFlowManager.Consideration.ConsiderationItems> alreadyActivatedItems, List<Status> statsEffected, List<PetMaster> canApplyTriggerToAllItemsAndEnemies = null, bool canLaunchEnemyAction = true)
	{
		Item2.GetAllEffectTotals();
		List<Item2> items = Item2.GetAllItemsInGrid();
		if (trigger == Item2.Trigger.ActionTrigger.constantClearWhile)
		{
			HazardManager.FindAllHazards();
			foreach (Status status in Status.allStatuses)
			{
				status.ResetForNewConsiderationSequence();
			}
			for (int j = 0; j < this.itemStatusEffectsToApplyAtEndOfQueuedActions.Count; j++)
			{
				GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions itemStatusEffectToApplyAtEndOfQueuedActions = this.itemStatusEffectsToApplyAtEndOfQueuedActions[j];
				if (!itemStatusEffectToApplyAtEndOfQueuedActions.canOnlyBeAppliedAtEndOfTotalSequence)
				{
					if (!itemStatusEffectToApplyAtEndOfQueuedActions.item || itemStatusEffectToApplyAtEndOfQueuedActions.effect.itemStatusEffect == null || itemStatusEffectToApplyAtEndOfQueuedActions.effect.itemStatusEffect.Count == 0)
					{
						this.itemStatusEffectsToApplyAtEndOfQueuedActions.RemoveAt(j);
						j--;
					}
					else if (itemStatusEffectToApplyAtEndOfQueuedActions.effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.BanishCarving)
					{
						for (int k = 0; k < this.itemStatusEffectsToApplyAtEndOfQueuedActions.Count; k++)
						{
							if (j != k)
							{
								GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions itemStatusEffectToApplyAtEndOfQueuedActions2 = this.itemStatusEffectsToApplyAtEndOfQueuedActions[k];
								if (!itemStatusEffectToApplyAtEndOfQueuedActions2.item || itemStatusEffectToApplyAtEndOfQueuedActions2.effect.itemStatusEffect == null || itemStatusEffectToApplyAtEndOfQueuedActions2.effect.itemStatusEffect.Count == 0)
								{
									this.itemStatusEffectsToApplyAtEndOfQueuedActions.RemoveAt(k);
									k--;
								}
								else if (itemStatusEffectToApplyAtEndOfQueuedActions2.item == itemStatusEffectToApplyAtEndOfQueuedActions.item && itemStatusEffectToApplyAtEndOfQueuedActions2.effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.DiscardCarving)
								{
									this.itemStatusEffectsToApplyAtEndOfQueuedActions.RemoveAt(k);
									k--;
								}
							}
						}
					}
				}
			}
			List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions> list = new List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions>();
			for (int l = 0; l < this.itemStatusEffectsToApplyAtEndOfQueuedActions.Count; l++)
			{
				GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions itemStatusEffectToApplyAtEndOfQueuedActions3 = this.itemStatusEffectsToApplyAtEndOfQueuedActions[l];
				if (itemStatusEffectToApplyAtEndOfQueuedActions3 != null && itemStatusEffectToApplyAtEndOfQueuedActions3.canOnlyBeAppliedAtEndOfTotalSequence)
				{
					list.Add(itemStatusEffectToApplyAtEndOfQueuedActions3);
				}
				else if (!itemStatusEffectToApplyAtEndOfQueuedActions3.item || itemStatusEffectToApplyAtEndOfQueuedActions3.effect.itemStatusEffect == null || itemStatusEffectToApplyAtEndOfQueuedActions3.effect.itemStatusEffect.Count == 0)
				{
					this.itemStatusEffectsToApplyAtEndOfQueuedActions.RemoveAt(l);
					l--;
				}
				else
				{
					Item2.ApplyItemStatusEffect(itemStatusEffectToApplyAtEndOfQueuedActions3.item, itemStatusEffectToApplyAtEndOfQueuedActions3.effect, itemStatusEffectToApplyAtEndOfQueuedActions3.name);
				}
			}
			this.itemStatusEffectsToApplyAtEndOfQueuedActions = new List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions>(list);
			foreach (Item2 item in items)
			{
				if (this.isnNewCheck)
				{
					item.lastNumberOfModifiers = item.appliedModifiers.Count;
				}
				item.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.whileActive }, -1);
			}
			this.isnNewCheck = false;
		}
		else if (trigger == Item2.Trigger.ActionTrigger.clearActivatedItems)
		{
			alreadyActivatedItems = new List<GameFlowManager.Consideration.ConsiderationItems>();
		}
		else
		{
			foreach (Item2 item2 in activeItems)
			{
				if (!items.Contains(item2))
				{
					items.Add(item2);
				}
			}
			if ((canApplyTriggerToAllItemsAndEnemies == null || canApplyTriggerToAllItemsAndEnemies.Count == 0) && activeItems.Count == 0)
			{
				activeItems = items;
			}
			List<Item2.ActiveCreateEffect> activeCreateEffects = new List<Item2.ActiveCreateEffect>();
			List<Item2.ActiveEffect> list2 = new List<Item2.ActiveEffect>();
			List<Item2.ActiveModifiers> activeModifiers = new List<Item2.ActiveModifiers>();
			List<Item2.ActiveSpecialEffects> activeSpecialEffects = new List<Item2.ActiveSpecialEffects>();
			List<Item2.ActiveEnergyEmitterEffects> activeEnergyEmitterEffects = new List<Item2.ActiveEnergyEmitterEffects>();
			List<Item2.ActiveAddModifiers> activeAddModifiers = new List<Item2.ActiveAddModifiers>();
			List<Item2.ActiveValueChangers> list3 = new List<Item2.ActiveValueChangers>();
			List<Item2.ActiveMovementEffect> activeMovementEffects = new List<Item2.ActiveMovementEffect>();
			foreach (Item2 item3 in items)
			{
				if (((trigger != Item2.Trigger.ActionTrigger.onCombatStart && trigger != Item2.Trigger.ActionTrigger.onTurnStart && trigger != Item2.Trigger.ActionTrigger.onTurnEnd) || item3.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated) || item3.CanBeUsed(false, new List<Item2.Cost>(), true, new List<SpecificConditionToUse.ConditionTime> { SpecificConditionToUse.ConditionTime.forTurnAndCombatStart })) && item3 && item3.gameObject && item3.itemMovement && (!item3.destroyed || activeItems.Contains(item3)) && !item3.itemType.Contains(Item2.ItemType.Treat) && !item3.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
				{
					item3.ConsiderConstantSelfModifiers(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeModifiers);
					if ((!item3.petItem || item3.petItem.combatPet || Item2.Trigger.IsConstant(trigger)) && (item3.itemMovement.inGrid || item3.destroyed || trigger == Item2.Trigger.ActionTrigger.onRemove || trigger == Item2.Trigger.ActionTrigger.onDiscard || trigger == Item2.Trigger.ActionTrigger.onDestroy || trigger == Item2.Trigger.ActionTrigger.whenLeftBehind || trigger == Item2.Trigger.ActionTrigger.whenNotPlayed))
					{
						PetMaster petMasterFromInventory = PetMaster.GetPetMasterFromInventory(item3.parentInventoryGrid);
						if ((!petMasterFromInventory || !(petMasterFromInventory.combatPet == null) || this.battlePhase == GameFlowManager.BattlePhase.outOfBattle) && ((trigger != Item2.Trigger.ActionTrigger.whenAttacked && trigger != Item2.Trigger.ActionTrigger.onTakeDamage && trigger != Item2.Trigger.ActionTrigger.onSummonPet && trigger != Item2.Trigger.ActionTrigger.whenZombied) || (statsEffected != null && statsEffected.Count != 0 && !(statsEffected[0] != PetItem2.GetStatus(item3.gameObject)))))
						{
							item3.ConsiderCreateEffects(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeCreateEffects);
							item3.ConsiderEffects(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, list2);
							item3.ConsiderModifiers(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeModifiers);
							item3.ConsiderSpecialEffects(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeSpecialEffects);
							item3.ConsiderEnergyEffects(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeEnergyEmitterEffects);
							item3.ConsiderAddModifiers(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeAddModifiers);
							item3.ConsiderValueChangers(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, list3);
							item3.ConsiderMovementEffects(new List<Item2.Trigger.ActionTrigger> { trigger }, activeItems, activeMovementEffects);
						}
					}
				}
			}
			List<Enemy> enemiesEffectedProxy = new List<Enemy>();
			foreach (Item2.ActiveEffect activeEffect in list2)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Active effect: ",
					activeEffect.item.name,
					"  ",
					trigger.ToString(),
					" ",
					activeEffect.effect.effect.type.ToString()
				}));
				if (activeEffect.item)
				{
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(activeEffect.item.ApplyEffect(activeEffect.effect, enemiesEffectedProxy, statsEffected));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveEffect>.Enumerator enumerator3 = default(List<Item2.ActiveEffect>.Enumerator);
			foreach (Item2.ActiveModifiers activeModifiers2 in activeModifiers)
			{
				if (activeModifiers2.item)
				{
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(activeModifiers2.item.ApplyModifier(activeModifiers2.effect, alreadyActivatedItems, enemiesEffectedProxy));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveModifiers>.Enumerator enumerator4 = default(List<Item2.ActiveModifiers>.Enumerator);
			foreach (Item2.ActiveCreateEffect activeCreateEffect in activeCreateEffects)
			{
				if (activeCreateEffect.item)
				{
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(activeCreateEffect.item.ApplyCreateEffect(activeCreateEffect.createEffect, activeItems));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveCreateEffect>.Enumerator enumerator5 = default(List<Item2.ActiveCreateEffect>.Enumerator);
			foreach (Item2.ActiveSpecialEffects activeSpecialEffects2 in activeSpecialEffects)
			{
				if (activeSpecialEffects2.item)
				{
					SpecialItem effect = activeSpecialEffects2.effect;
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(effect.ApplySpecialEffect(enemiesEffectedProxy));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveSpecialEffects>.Enumerator enumerator6 = default(List<Item2.ActiveSpecialEffects>.Enumerator);
			foreach (Item2.ActiveEnergyEmitterEffects activeEnergyEmitterEffects2 in activeEnergyEmitterEffects)
			{
				if (activeEnergyEmitterEffects2.item)
				{
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(activeEnergyEmitterEffects2.item.ApplyEnergyEffect(activeEnergyEmitterEffects2.effect));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveEnergyEmitterEffects>.Enumerator enumerator7 = default(List<Item2.ActiveEnergyEmitterEffects>.Enumerator);
			foreach (Item2.ActiveAddModifiers activeAddModifiers2 in activeAddModifiers)
			{
				if (activeAddModifiers2.item)
				{
					activeAddModifiers2.item.ApplyAddModifier(activeAddModifiers2.effect, alreadyActivatedItems, enemiesEffectedProxy);
				}
			}
			foreach (Item2.ActiveMovementEffect activeMovementEffect in activeMovementEffects)
			{
				if (activeMovementEffect.item)
				{
					this.isWaitingForItemRoutine = true;
					base.StartCoroutine(activeMovementEffect.item.ApplyMovementEffect(activeMovementEffect.movementEffect));
					while (this.isWaitingForItemRoutine)
					{
						yield return null;
					}
				}
			}
			List<Item2.ActiveMovementEffect>.Enumerator enumerator9 = default(List<Item2.ActiveMovementEffect>.Enumerator);
			List<Item2.ItemType> itemTypes = new List<Item2.ItemType>();
			foreach (Item2 item4 in activeItems)
			{
				foreach (Item2.ItemType itemType in item4.itemType)
				{
					itemTypes.Add(itemType);
				}
			}
			if (canLaunchEnemyAction && (canApplyTriggerToAllItemsAndEnemies == null || canApplyTriggerToAllItemsAndEnemies.Count == 0))
			{
				int num;
				for (int i = 0; i < Enemy.allEnemies.Count; i = num + 1)
				{
					Enemy enemy = Enemy.allEnemies[i];
					if (enemy && enemy.stats && enemy.stats.health > 0 && !enemy.dead)
					{
						base.StartCoroutine(enemy.ConsiderActionResponse(trigger, statsEffected, itemTypes));
						while (this.isWaitingForItemRoutine)
						{
							yield return null;
						}
					}
					num = i;
				}
			}
			foreach (PetMaster petMaster in PetMaster.petMasters)
			{
				if (petMaster && petMaster.combatPetCom && petMaster.combatPetCom.stats && petMaster.combatPetCom.stats.health > 0 && (canApplyTriggerToAllItemsAndEnemies == null || canApplyTriggerToAllItemsAndEnemies.Count == 0 || canApplyTriggerToAllItemsAndEnemies.Contains(petMaster)))
				{
					foreach (Item2.CombattEffect combattEffect in petMaster.petEffects)
					{
						if (combattEffect.trigger.trigger == trigger && (Item2.ShareItemTypes(combattEffect.trigger.types, itemTypes) || combattEffect.trigger.types.Count == 0 || combattEffect.trigger.types.Contains(Item2.ItemType.Any)))
						{
							yield return petMaster.combatPetCom.ApplyEffect(combattEffect);
						}
					}
					List<Item2.CombattEffect>.Enumerator enumerator12 = default(List<Item2.CombattEffect>.Enumerator);
					petMaster = null;
				}
			}
			List<PetMaster>.Enumerator enumerator11 = default(List<PetMaster>.Enumerator);
			foreach (Item2 item5 in items)
			{
				if (item5 && item5.itemMovement && !item5.destroyed && item5.appliedModifiers.Count > item5.lastNumberOfModifiers)
				{
					item5.lastNumberOfModifiers = item5.appliedModifiers.Count;
					yield return item5.GetComponent<ItemMovement>().ModifiedAnimation();
				}
			}
			List<Item2>.Enumerator enumerator13 = default(List<Item2>.Enumerator);
			activeCreateEffects = null;
			activeModifiers = null;
			activeSpecialEffects = null;
			activeEnergyEmitterEffects = null;
			activeAddModifiers = null;
			activeMovementEffects = null;
			enemiesEffectedProxy = null;
			itemTypes = null;
		}
		yield return new WaitForEndOfFrame();
		if (this.queuedConsiderations.Count == 0)
		{
			Item2.GetAllEffectTotals();
			Enemy.AllEnemiesConsiderActionOverride();
		}
		while (this.queuedConsiderations.Count > 0)
		{
			GameFlowManager.Consideration consideration = this.queuedConsiderations[0];
			this.queuedConsiderations.RemoveAt(0);
			if (consideration.items != null && consideration.items.Count > 0)
			{
				yield return this.ConsiderAllEffects(consideration.trigger, consideration.items, alreadyActivatedItems, consideration.stats, consideration.canApplyTriggerToAllItemsAndEnemies, consideration.canLaunchEnemyAction);
			}
			else
			{
				yield return this.ConsiderAllEffects(consideration.trigger, new List<Item2>(), alreadyActivatedItems, consideration.stats, consideration.canApplyTriggerToAllItemsAndEnemies, consideration.canLaunchEnemyAction);
			}
		}
		if (this.selectedItem && (Item2.GetCurrentCost(Item2.Cost.Type.energy, this.selectedItem.costs) > this.player.AP || ((Item2.GetCurrentCost(Item2.Cost.Type.mana, this.selectedItem.costs) > this.gameManager.GetCurrentMana()) | (Item2.GetCurrentCost(Item2.Cost.Type.gold, this.selectedItem.costs) > this.gameManager.GetCurrentGold()))))
		{
			this.DeselectItem();
		}
		ScriptedTrigger.ResetTimesPerformed();
		if (this.isCheckingEffects)
		{
			while (this.itemStatusEffectsToApplyAtEndOfQueuedActions.Count > 0)
			{
				List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions> list4 = new List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions>(this.itemStatusEffectsToApplyAtEndOfQueuedActions);
				this.itemStatusEffectsToApplyAtEndOfQueuedActions = new List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions>();
				for (int m = 0; m < list4.Count; m++)
				{
					GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions itemStatusEffectToApplyAtEndOfQueuedActions4 = list4[m];
					if (!itemStatusEffectToApplyAtEndOfQueuedActions4.item || itemStatusEffectToApplyAtEndOfQueuedActions4.effect.itemStatusEffect == null || itemStatusEffectToApplyAtEndOfQueuedActions4.effect.itemStatusEffect.Count == 0)
					{
						this.itemStatusEffectsToApplyAtEndOfQueuedActions.RemoveAt(m);
						m--;
					}
					else
					{
						Item2.ApplyItemStatusEffect(itemStatusEffectToApplyAtEndOfQueuedActions4.item, itemStatusEffectToApplyAtEndOfQueuedActions4.effect, itemStatusEffectToApplyAtEndOfQueuedActions4.name);
					}
				}
			}
			this.isCheckingEffects = false;
		}
		yield break;
		yield break;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000661FE File Offset: 0x000643FE
	private IEnumerator PlayerTurnStart()
	{
		this.DoAllSavedDestroys();
		this.battlePhase = GameFlowManager.BattlePhase.enemyTurn;
		this.actionFinished = false;
		this.actionPerformed = false;
		if (this.gameManager.dead)
		{
			yield break;
		}
		this.SwapRecordedItemTurns();
		this.turnNumber++;
		yield return this.gameManager.SetEnemyIntentions();
		yield return new WaitForSeconds(0.4f);
		yield return this.player.stats.ConsiderBurn();
		yield return this.player.stats.NextTurn();
		foreach (CombatPet combatPet in CombatPet.combatPets)
		{
			if (combatPet && combatPet.stats && !combatPet.dead)
			{
				yield return combatPet.stats.ConsiderBurn();
			}
		}
		List<CombatPet>.Enumerator enumerator = default(List<CombatPet>.Enumerator);
		yield return CombatPet.AllPetsNextTurn();
		this.player.NextTurn();
		Item2.GetAllEffectTotals();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(0.1f);
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		if (Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.blockIsNotRemoved, null, false))
		{
			this.player.stats.ChangeArmor((float)(-(float)this.player.stats.armor / 2), Item2.Effect.MathematicalType.summative);
		}
		else
		{
			this.player.stats.EndArmor();
		}
		this.ResetCombatStatsForTurn();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		List<Item2> list = Object.FindObjectsOfType<Item2>().ToList<Item2>();
		List<GameObject> allCards = Object.FindObjectOfType<Tote>().GetAllCards();
		List<GameObject> list2 = new List<GameObject>();
		foreach (GameObject gameObject in allCards)
		{
			if (gameObject)
			{
				if (!gameObject.activeInHierarchy)
				{
					list2.Add(gameObject);
				}
				gameObject.SetActive(true);
				Item2 component = gameObject.GetComponent<Item2>();
				if (component)
				{
					list.Add(component);
				}
			}
		}
		foreach (GameObject gameObject2 in list2)
		{
			gameObject2.SetActive(false);
		}
		foreach (Item2 item in Item2.allItems)
		{
			item.SetColor();
		}
		Enemy.AllEnemiesNextTurnStarts();
		this.CheckConstants();
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		if (this.turnNumber == 1)
		{
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onCombatStart, null, null, true, false);
		}
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onTurnStart, null, null, true, false);
		Item2.GetAllEffectTotals();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(0.3f);
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		tutorialManager.TutorialTurnStart();
		Object.FindObjectOfType<ActionButtonManager>().EnableBattleButtons();
		if (this.turnNumber == 2 && tutorialManager.playType == TutorialManager.PlayType.tutorial)
		{
			TutorialManager tutorialManager2 = Object.FindObjectOfType<TutorialManager>();
			if (tutorialManager2 != null)
			{
				tutorialManager2.ConsiderTutorial("combatTutorial2");
			}
		}
		if (this.player.characterName == Character.CharacterName.Tote)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			tutorialManager.ConsiderTutorial("toteA");
			if (this.turnNumber == 2)
			{
				if ((from x in Item2.GetAllItemsInGrid()
					where x.itemType.Contains(Item2.ItemType.Carving)
					select x).Count<Item2>() > 0)
				{
					tutorialManager.ConsiderTutorial("toteBb");
				}
			}
			Object.FindObjectOfType<Tote>().StartTurn();
		}
		else if (this.player.characterName == Character.CharacterName.Pochette)
		{
			if (this.turnNumber == 2)
			{
				tutorialManager.ConsiderTutorial("movingPets");
			}
		}
		else if (this.player.characterName == Character.CharacterName.CR8)
		{
			Object.FindObjectOfType<CR8Manager>().SetupTurn();
		}
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		Enemy.SetAllEnemyIntentionsBackup();
		this.gameManager.ShowBasicButtons(GameManager.ButtonsToEnable.endTurnButton);
		this.endingTurn = false;
		this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
		this.actionFinished = true;
		this.actionPerformed = true;
		if (TwitchManager.isRunningPolls())
		{
			TwitchManager.Instance.pollManager.onPlayerTurnStart();
		}
		yield break;
		yield break;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00066210 File Offset: 0x00064410
	public void DoAllSavedDestroys()
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in GameObject.FindGameObjectWithTag("ItemParent").transform)
		{
			Transform transform = (Transform)obj;
			if (!transform.gameObject.activeInHierarchy)
			{
				transform.gameObject.SetActive(true);
				list.Add(transform);
			}
		}
		for (int i = 0; i < Item2.allItems.Count; i++)
		{
			Item2 item = Item2.allItems[i];
			if (item && item.destroyed)
			{
				Item2.allItems.RemoveAt(i);
				Object.Destroy(item.gameObject);
				i--;
			}
		}
		foreach (Transform transform2 in list)
		{
			if (transform2 && transform2.gameObject)
			{
				transform2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00066344 File Offset: 0x00064544
	public void ConsiderSummon(PetItem2 petItem2)
	{
		if (this.battlePhase != GameFlowManager.BattlePhase.playerTurn || this.isCheckingEffects || !this.actionFinished || !this.actionPerformed)
		{
			return;
		}
		if (!petItem2 || !petItem2.myItem)
		{
			return;
		}
		if (petItem2.combatPet)
		{
			return;
		}
		if (!petItem2.myItem.CanBeUsedActive(true, petItem2.summoningCosts, true, false, null, false))
		{
			return;
		}
		base.StartCoroutine(this.Summon(petItem2));
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x000663C0 File Offset: 0x000645C0
	public void ConsiderUse(Item2 activeItem, bool dontShowNegative = false)
	{
		Debug.Log("Consider Use of " + activeItem.name);
		Item2.GetAllEffectTotals();
		if ((this.battlePhase == GameFlowManager.BattlePhase.playerTurn && this.actionFinished && this.actionPerformed && this.gameManager.targetedEnemy && !this.gameManager.targetedEnemy.dead) || (!this.isCheckingEffects && this.battlePhase == GameFlowManager.BattlePhase.outOfBattle && this.selectedItem))
		{
			if (this.selectedItem)
			{
				if (this.selectedItem == activeItem)
				{
					this.DeselectItem();
					return;
				}
				if (!activeItem.canBeComboed)
				{
					this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm24"));
					activeItem.itemMovement.StartCoroutine(activeItem.itemMovement.Shake());
					return;
				}
				SpecificConditionToUse[] components = this.selectedItem.GetComponents<SpecificConditionToUse>();
				int i = 0;
				while (i < components.Length)
				{
					SpecificConditionToUse specificConditionToUse = components[i];
					if (!specificConditionToUse.CanBeUsed(new List<SpecificConditionToUse.ConditionTime> { SpecificConditionToUse.ConditionTime.onAlternateUseSelect }, activeItem))
					{
						if (specificConditionToUse.explanationKey.Length > 1)
						{
							string text = LangaugeManager.main.GetTextByKey(specificConditionToUse.explanationKey);
							text = text.Replace("/x", specificConditionToUse.value.ToString());
							this.gameManager.CreatePopUp(text);
							return;
						}
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
						return;
					}
					else
					{
						i++;
					}
				}
				foreach (Item2 item in Item2.allItems)
				{
					item.isChosenAsComboRecepient = false;
				}
				activeItem.isChosenAsComboRecepient = true;
				base.StartCoroutine(this.UseItem(this.selectedItem, true, true, Item2.PlayerAnimation.UseDefault, false, false));
				if (this.selectedCosts != null)
				{
					Item2.DetractCosts(this.selectedItem, this.selectedCosts, null);
					return;
				}
			}
			else if (activeItem.CanBeUsedActive(!dontShowNegative, activeItem.costs, true, false, null, false))
			{
				if (activeItem.playType == Item2.PlayType.Active)
				{
					base.StartCoroutine(this.UseItem(activeItem, false, false, Item2.PlayerAnimation.UseDefault, false, false));
					return;
				}
				if (activeItem.playType == Item2.PlayType.Combo)
				{
					this.SelectItem(activeItem, null);
					return;
				}
			}
			else if (!dontShowNegative)
			{
				activeItem.itemMovement.StartCoroutine(activeItem.itemMovement.Shake());
			}
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00066620 File Offset: 0x00064820
	public void SelectItem(Item2 activeItem, List<Item2.Cost> costs = null)
	{
		this.selectedCosts = costs;
		this.selectedItem = activeItem;
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onComboSelect, activeItem, null, true, false);
		this.DetermineItemsThatCanBeComboed();
		Item2.GetAllEffectTotals();
		this.gameManager.selectedItem = activeItem;
		this.gameManager.inventoryPhase = GameManager.InventoryPhase.choose;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00066660 File Offset: 0x00064860
	public void DeselectItem()
	{
		this.selectedCosts = null;
		this.selectedItem = null;
		foreach (Item2 item in Item2.allItems)
		{
			item.canBeComboed = true;
			item.SetColor();
			item.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.whileComboing }, -1);
		}
		Item2.GetAllEffectTotals();
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000666DC File Offset: 0x000648DC
	private void DetermineItemsThatCanBeComboed()
	{
		if (!this.selectedItem)
		{
			this.DeselectItem();
			return;
		}
		foreach (Item2.Modifier modifier in this.selectedItem.modifiers)
		{
			if (modifier.trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.selectedItem.FindItemsAndGridsinArea(list, list2, modifier.areasToModify, Item2.AreaDistance.all, null, null, null, true, false, true);
				foreach (Item2 item in Item2.allItems)
				{
					if (list.Contains(item) && (Item2.ShareItemTypes(item.itemType, modifier.typesToModify) || modifier.typesToModify.Contains(Item2.ItemType.Any)))
					{
						item.canBeComboed = true;
					}
					else
					{
						item.canBeComboed = false;
					}
					item.SetColor();
				}
				foreach (Item2 item2 in Tote.main.GetAllCarvings())
				{
					if (Item2.ShareItemTypes(item2.itemType, modifier.typesToModify) || modifier.typesToModify.Contains(Item2.ItemType.Any))
					{
						item2.canBeComboed = true;
					}
					else
					{
						item2.canBeComboed = false;
					}
					item2.SetColor();
				}
			}
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000668A0 File Offset: 0x00064AA0
	public static Transform AcceptableSpaceForManaFlow(PathFinding.Location location)
	{
		foreach (GameObject gameObject in GridObject.GetItemsAtPosition(location.position).ConvertAll<GameObject>((GridObject x) => x.gameObject))
		{
			Item2 componentInParent = gameObject.GetComponentInParent<Item2>();
			ItemMovement componentInParent2 = gameObject.GetComponentInParent<ItemMovement>();
			if (componentInParent && componentInParent2 && componentInParent2.inGrid && (componentInParent.CheckForStatusEffect(Item2.ItemStatusEffect.Type.conductive) || componentInParent.itemType.Contains(Item2.ItemType.ManaStone)))
			{
				return gameObject.transform;
			}
		}
		return null;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00066964 File Offset: 0x00064B64
	public Transform AcceptableManaStone(PathFinding.Location location)
	{
		foreach (GameObject gameObject in GridObject.GetItemsAtPosition(location.position).ConvertAll<GameObject>((GridObject x) => x.gameObject))
		{
			Item2 component = gameObject.GetComponent<Item2>();
			ItemMovement componentInParent = gameObject.GetComponentInParent<ItemMovement>();
			if (component && componentInParent && componentInParent.inGrid && component.itemType.Contains(Item2.ItemType.ManaStone) && ItemPouch.FindPouchForItem(component.gameObject) == null)
			{
				return component.transform;
			}
		}
		return null;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00066A2C File Offset: 0x00064C2C
	public static Transform AcceptableItem(PathFinding.Location location)
	{
		foreach (GameObject gameObject in GridObject.GetItemsAtPosition(location.position).ConvertAll<GameObject>((GridObject x) => x.gameObject))
		{
			Item2 componentInParent = gameObject.GetComponentInParent<Item2>();
			ItemMovement componentInParent2 = gameObject.GetComponentInParent<ItemMovement>();
			if (componentInParent && componentInParent2 && componentInParent2.inGrid && !componentInParent2.inPouch)
			{
				return componentInParent.transform;
			}
		}
		return null;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00066ADC File Offset: 0x00064CDC
	public List<PathFinding.TransformAndPath> FindAllItemsOfTypeConnected(Item2 activeItem)
	{
		List<PathFinding.TransformAndPath> list = new List<PathFinding.TransformAndPath>();
		List<Vector2> list2 = activeItem.GetComponent<ItemMovement>().FindAllColliders();
		if (list2.Count > 0)
		{
			foreach (PathFinding.TransformAndPath transformAndPath in PathFinding.SearchForItems(activeItem, list2[0], 30, new Func<PathFinding.Location, Transform>(GameFlowManager.AcceptableSpaceForManaFlow), new Func<PathFinding.Location, Transform>(GameFlowManager.AcceptableItem), new string[] { "n", "s", "e", "w" }))
			{
				bool flag = false;
				using (List<PathFinding.TransformAndPath>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.t == transformAndPath.t)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(transformAndPath);
				}
			}
		}
		return list;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00066BEC File Offset: 0x00064DEC
	public List<PathFinding.TransformAndPath> FindAllManaStonesFromAllColliders(Item2 activeItem)
	{
		List<PathFinding.TransformAndPath> list = new List<PathFinding.TransformAndPath>();
		List<Vector2> list2 = activeItem.GetComponent<ItemMovement>().FindAllColliders();
		if (list2.Count > 0)
		{
			foreach (PathFinding.TransformAndPath transformAndPath in PathFinding.SearchForItems(activeItem, list2[0], 30, new Func<PathFinding.Location, Transform>(GameFlowManager.AcceptableSpaceForManaFlow), new Func<PathFinding.Location, Transform>(this.AcceptableManaStone), new string[] { "n", "s", "e", "w" }))
			{
				bool flag = false;
				using (List<PathFinding.TransformAndPath>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.t == transformAndPath.t)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(transformAndPath);
				}
			}
		}
		return list;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00066CFC File Offset: 0x00064EFC
	public bool TestForMana(Item2 activeItem, int cost)
	{
		return ConnectionManager.main.SumAvailableMana(activeItem) >= cost;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00066D10 File Offset: 0x00064F10
	public void FlowPositiveMana(Item2 activeItem, int amountToAdd)
	{
		if (!activeItem || !activeItem.gameObject.activeInHierarchy || !activeItem.itemMovement || !activeItem.itemMovement.inGrid)
		{
			return;
		}
		List<PathFinding.TransformAndPath> list = this.FindAllManaStonesFromAllColliders(activeItem);
		while (amountToAdd > 0)
		{
			int num = -999;
			PathFinding.TransformAndPath transformAndPath = null;
			foreach (PathFinding.TransformAndPath transformAndPath2 in list)
			{
				List<Vector2> pathList = transformAndPath2.pathList;
				ManaStone component = transformAndPath2.t.GetComponent<ManaStone>();
				if (component.currentPower < component.maxPower && component.currentPower > num)
				{
					transformAndPath = transformAndPath2;
					num = component.currentPower;
				}
			}
			if (transformAndPath == null)
			{
				break;
			}
			ManaStone component2 = transformAndPath.t.GetComponent<ManaStone>();
			int num2 = component2.maxPower - component2.currentPower;
			component2.currentPower = Mathf.Min(component2.currentPower + amountToAdd, component2.maxPower);
			amountToAdd -= num2;
			transformAndPath.pathList.Add(transformAndPath.t.position);
			transformAndPath.pathList.Reverse();
			GameObject gameObject = Object.Instantiate<GameObject>(this.manaFlowPrefab, activeItem.transform.position + Vector3.back, Quaternion.identity, component2.transform.parent);
			gameObject.transform.position = transformAndPath.pathList[0];
			ManaFlow component3 = gameObject.GetComponent<ManaFlow>();
			component3.StartCoroutine(component3.FlowManaAnimation(transformAndPath.pathList));
		}
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00066EB0 File Offset: 0x000650B0
	public bool AcceptableSpaceForManaFlow2(PathFinding.Location location, bool b)
	{
		foreach (GameObject gameObject in GridObject.GetItemsAtPosition(Vector2Int.RoundToInt(location.position)).ConvertAll<GameObject>((GridObject x) => x.gameObject))
		{
			Item2 componentInParent = gameObject.GetComponentInParent<Item2>();
			ItemMovement componentInParent2 = gameObject.GetComponentInParent<ItemMovement>();
			if (componentInParent && componentInParent2 && componentInParent2.inGrid && (componentInParent.CheckForStatusEffect(Item2.ItemStatusEffect.Type.conductive) || componentInParent.itemType.Contains(Item2.ItemType.ManaStone)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00066F70 File Offset: 0x00065170
	private void OnDrawGizmosSelected()
	{
		if (PathFinding.storedLocationsTried == null)
		{
			return;
		}
		int num = 0;
		foreach (PathFinding.Location location in PathFinding.storedLocationsTried)
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.5f + (float)num / (float)PathFinding.storedLocationsTried.Count / 2f);
			Gizmos.DrawSphere(GameManager.main.mainGridParent.transform.position + location.position, 0.3f);
			num++;
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0006702C File Offset: 0x0006522C
	public List<Vector2> FindManaPath(Item2 activeItem, Transform manaStone)
	{
		GridObject component = manaStone.GetComponent<GridObject>();
		GridObject component2 = activeItem.GetComponent<GridObject>();
		if (!component || !component2)
		{
			return null;
		}
		List<Vector2> list;
		if (PathFinding.FindPath(component2.gridPositions, component.gridPositions, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpaceForManaFlow2), out list, null))
		{
			list.Add(PathFinding.endingPosition);
			return GridObject.CellToWorld(list);
		}
		return null;
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00067090 File Offset: 0x00065290
	public void FlowMana(Item2 activeItem, Transform manaStone, bool reverse)
	{
		List<Vector2> list = this.FindManaPath(activeItem, manaStone);
		if (list != null)
		{
			if (reverse)
			{
				list.Reverse();
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.manaFlowPrefab, list[list.Count - 1], Quaternion.identity, activeItem.transform.parent);
			gameObject.transform.position += Vector3.back;
			ManaFlow component = gameObject.GetComponent<ManaFlow>();
			component.StartCoroutine(component.FlowManaAnimation(list));
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00067110 File Offset: 0x00065310
	public void FlowMana(Item2 activeItem, int manaCost)
	{
		if (!activeItem || !activeItem.gameObject.activeInHierarchy || !activeItem.itemMovement || !activeItem.itemMovement.inGrid)
		{
			return;
		}
		List<PathFinding.TransformAndPath> list = this.FindAllManaStonesFromAllColliders(activeItem);
		while (manaCost > 0)
		{
			int num = 999;
			PathFinding.TransformAndPath transformAndPath = null;
			foreach (PathFinding.TransformAndPath transformAndPath2 in list)
			{
				List<Vector2> pathList = transformAndPath2.pathList;
				ManaStone component = transformAndPath2.t.GetComponent<ManaStone>();
				if (component.currentPower > 0 && component.currentPower < num)
				{
					transformAndPath = transformAndPath2;
					num = component.currentPower;
				}
			}
			if (transformAndPath == null)
			{
				break;
			}
			ManaStone component2 = transformAndPath.t.GetComponent<ManaStone>();
			int currentPower = component2.currentPower;
			component2.currentPower = Mathf.Max(component2.currentPower - manaCost, 0);
			manaCost -= currentPower;
			if (Singleton.Instance.playerAnimations)
			{
				ManaFlow component3 = Object.Instantiate<GameObject>(this.manaFlowPrefab, component2.transform.position + Vector3.back, Quaternion.identity, component2.transform.parent).GetComponent<ManaFlow>();
				component3.StartCoroutine(component3.FlowManaAnimation(transformAndPath.pathList));
			}
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00067268 File Offset: 0x00065468
	public void ConsiderItemUseIndirect(Item2 activeItem)
	{
		if (!activeItem.CanBeUsed(false, activeItem.costs, true, null))
		{
			return;
		}
		Item2.DetractCosts(activeItem, activeItem.costs, new List<Item2.Cost.Type>
		{
			Item2.Cost.Type.mana,
			Item2.Cost.Type.gold
		});
		this.ConsideFirstUse(activeItem);
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.useEarly, activeItem, null, true, false);
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onComboUse, activeItem, null, true, false);
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUse, activeItem, null, true, false);
		EnergyEmitter component = activeItem.GetComponent<EnergyEmitter>();
		if (component && !component.WillBeOverheated())
		{
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUseUntilOverheat, activeItem, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUseUntilOverheatLate, activeItem, null, true, false);
		}
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.useLate, activeItem, null, true, false);
		foreach (Item2.LimitedUses limitedUses in activeItem.usesLimits)
		{
			Debug.Log("Limited uses for item " + activeItem.name + " is " + limitedUses.currentValue.ToString());
			limitedUses.currentValue -= 1f;
			if (limitedUses.currentValue <= 0f && (limitedUses.type == Item2.LimitedUses.Type.total || activeItem.usesLimits.Count == 1))
			{
				this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onOutOfUses, activeItem, null, true, false);
			}
		}
		activeItem.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.untilUse }, -1);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x000673C4 File Offset: 0x000655C4
	public IEnumerator Summon(PetItem2 petItem)
	{
		while (!this.IsComplete())
		{
			yield return null;
		}
		this.battlePhase = GameFlowManager.BattlePhase.playerAction;
		this.actionPerformed = false;
		this.actionFinished = false;
		ItemPouch.CloseAllPouches();
		Item2.GetAllEffectTotals();
		Item2.DetractCosts(petItem.myItem, petItem.summoningCosts, null);
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		if (Singleton.Instance.playerAnimations)
		{
			this.player.itemToInteractWith.sprite = petItem.GetComponent<SpriteRenderer>().sprite;
			this.playerAnimator.Play("Player_UseItem", 0, 0f);
			float t = 0f;
			while (!this.actionPerformed)
			{
				t += Time.deltaTime;
				if (t > 3f)
				{
					Debug.Log("Frozen in animation");
					this.actionPerformed = true;
					this.actionFinished = true;
					break;
				}
				yield return null;
			}
		}
		else
		{
			this.actionPerformed = true;
			this.actionFinished = true;
		}
		petItem.SummonPet();
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
		yield break;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x000673DC File Offset: 0x000655DC
	public void ConsiderPetAnimation(Item2 activeItem)
	{
		PetItem2 component = activeItem.GetComponent<PetItem2>();
		if (component && component.combatPet)
		{
			CombatPet component2 = component.combatPet.GetComponent<CombatPet>();
			if (component2)
			{
				component2.positionAnimator.Play("enemyPos_buff", 0, 0f);
			}
		}
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0006742F File Offset: 0x0006562F
	public IEnumerator UseItem(Item2 activeItem, bool forceUse = false, bool alternateUse = false, Item2.PlayerAnimation playerAnimation = Item2.PlayerAnimation.UseDefault, bool skipAnimation = false, bool isCR8Test = false)
	{
		if (!activeItem.itemMovement)
		{
			yield break;
		}
		bool inGridOld = activeItem.itemMovement.inGrid;
		bool decreaseNumberAllowed = false;
		if (activeItem.itemMovement.returnsToOutOfInventoryPosition && !activeItem.isOwned && !inGridOld)
		{
			this.gameManager.ChangeItemsAllowedToTakePerm(-1);
			decreaseNumberAllowed = true;
		}
		if (forceUse)
		{
			activeItem.itemMovement.inGrid = true;
		}
		while (!this.IsComplete())
		{
			yield return null;
		}
		if (!activeItem || !activeItem.gameObject.activeInHierarchy || !activeItem.itemMovement || !activeItem.itemMovement.inGrid)
		{
			yield break;
		}
		if (!forceUse)
		{
			if (this.battlePhase != GameFlowManager.BattlePhase.playerTurn)
			{
				yield break;
			}
			if (ItemPouch.FindPouchForItem(activeItem.gameObject))
			{
				yield break;
			}
			if (!activeItem.CanBeUsedActive(true, activeItem.costs, true, false, null, false))
			{
				yield break;
			}
		}
		this.isUsingAnItem = true;
		if (this.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			this.battlePhase = GameFlowManager.BattlePhase.playerAction;
		}
		this.actionPerformed = false;
		this.actionFinished = false;
		EnergyEmitter energyEmitter = activeItem.GetComponent<EnergyEmitter>();
		ItemPouch.CloseAllPouches();
		Item2.GetAllEffectTotals();
		PetItem2 component = activeItem.GetComponent<PetItem2>();
		if (component && component.currentAP > 0)
		{
			int num = Item2.GetCurrentCost(Item2.Cost.Type.energy, activeItem.costs);
			int currentAP = component.currentAP;
			component.ChangeAP(-num);
			num = Mathf.Max(0, num - currentAP);
			this.player.ChangeAP(-num);
			Item2.DetractCosts(activeItem, activeItem.costs, new List<Item2.Cost.Type>
			{
				Item2.Cost.Type.mana,
				Item2.Cost.Type.gold
			});
		}
		else if (!alternateUse && (!energyEmitter || !energyEmitter.WillBeOverheated() || !energyEmitter.ignoreCostsOnOverheat))
		{
			Item2.DetractCosts(activeItem, activeItem.costs, null);
		}
		this.RecordItemUse(activeItem.itemType);
		float t = 0f;
		if (activeItem.soundEffect == Item2.SoundEffect.cymbal)
		{
			SoundManager.main.PlaySFX("instr_cymbal_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.flute)
		{
			SoundManager.main.PlaySFX("instr_flute_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.guitar)
		{
			SoundManager.main.PlaySFX("instr_guitar_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.piano)
		{
			SoundManager.main.PlaySFX("instr_tpiano_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.trumpet)
		{
			SoundManager.main.PlaySFX("instr_trumpet_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.violin)
		{
			SoundManager.main.PlaySFX("instr_violin_" + Random.Range(1, 5).ToString());
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.genericThud)
		{
			SoundManager.main.PlaySFX("generic");
		}
		else if (activeItem.soundEffect == Item2.SoundEffect.electricItem)
		{
			SoundManager.main.PlaySFX("elecActivateItem");
		}
		if (Singleton.Instance.playerAnimations && !skipAnimation)
		{
			this.player.itemToInteractWith.sprite = activeItem.GetComponent<SpriteRenderer>().sprite;
			string text = "";
			if (playerAnimation == Item2.PlayerAnimation.UseDefault)
			{
				playerAnimation = activeItem.playerAnimation;
			}
			if (playerAnimation == Item2.PlayerAnimation.Attack)
			{
				text = "Player_Attack";
			}
			else if (playerAnimation == Item2.PlayerAnimation.OverheadAttack)
			{
				text = "Player_Attack_Overhead";
			}
			else if (playerAnimation == Item2.PlayerAnimation.UseItem)
			{
				text = "Player_UseItem";
				if (Random.Range(0, 2) == 0)
				{
					SoundManager.main.PlaySFX("use1");
				}
				else
				{
					SoundManager.main.PlaySFX("use2");
				}
			}
			else if (playerAnimation == Item2.PlayerAnimation.Block)
			{
				text = "Player_Block";
			}
			else if (playerAnimation == Item2.PlayerAnimation.Hurt)
			{
				text = "Player_Hurt";
			}
			else if (playerAnimation == Item2.PlayerAnimation.FireArrow)
			{
				text = "Player_FireArrow";
			}
			else if (playerAnimation == Item2.PlayerAnimation.Command)
			{
				text = "Player_Command";
			}
			VisualEffectOnItemUse component2 = activeItem.GetComponent<VisualEffectOnItemUse>();
			if (component2)
			{
				component2.PlayEffect();
			}
			this.playerAnimator.Play(text, 0, 0f);
			t = 0f;
			bool considerPet = false;
			while (!this.actionPerformed)
			{
				t += Time.deltaTime;
				if (t > 3f)
				{
					Debug.Log("Frozen in animation");
					this.actionPerformed = true;
					this.actionFinished = true;
					break;
				}
				if (t > 0.4f && !considerPet)
				{
					this.ConsiderPetAnimation(activeItem);
					considerPet = true;
				}
				yield return null;
			}
			if (!considerPet)
			{
				this.ConsiderPetAnimation(activeItem);
				considerPet = true;
			}
		}
		else
		{
			this.actionPerformed = true;
			this.actionFinished = true;
		}
		if (!alternateUse)
		{
			this.ConsideFirstUse(activeItem);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.useEarly, activeItem, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUse, activeItem, null, true, false);
			if (energyEmitter && !energyEmitter.WillBeOverheated())
			{
				this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUseUntilOverheat, activeItem, null, true, false);
				this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUseUntilOverheatLate, activeItem, null, true, false);
			}
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveCombat, activeItem, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.useLate, activeItem, null, true, false);
		}
		else
		{
			activeItem.ConsiderTakingAsLimitedItemGet();
			activeItem.isOwned = true;
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onComboUse, activeItem, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onAlternateUse, activeItem, null, true, false);
		}
		foreach (Item2.LimitedUses limitedUses in activeItem.usesLimits)
		{
			limitedUses.currentValue -= 1f;
			if (limitedUses.currentValue <= 0f && (limitedUses.type == Item2.LimitedUses.Type.total || activeItem.usesLimits.Count == 1))
			{
				this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onOutOfUses, activeItem, null, true, false);
			}
		}
		t = 0f;
		while (!this.actionFinished)
		{
			t += Time.deltaTime;
			if (t > 3f)
			{
				Debug.Log("Frozen in animation");
				IL_07C2:
				while (this.isCheckingEffects)
				{
					yield return null;
				}
				activeItem.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.untilUse }, -1);
				this.DetermineItemsThatCanBeComboed();
				if (inGridOld && !activeItem.destroyed)
				{
					activeItem.itemMovement.inGrid = inGridOld;
					if (activeItem.itemMovement.returnsToOutOfInventoryPosition && decreaseNumberAllowed)
					{
						this.gameManager.ChangeItemsAllowedToTakePerm(1);
						decreaseNumberAllowed = false;
					}
				}
				if (!inGridOld && !activeItem.destroyed)
				{
					activeItem.itemMovement.inGrid = inGridOld;
				}
				this.CheckConstants();
				if (activeItem.destroyed || (this.selectedCosts != null && !activeItem.CanAffordCosts(this.selectedCosts)))
				{
					GameManager.main.EndChooseItem();
				}
				if (CR8Manager.instance && CR8Manager.instance.isTesting)
				{
					this.battlePhase = GameFlowManager.BattlePhase.outOfBattle;
				}
				else if (this.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
				{
					this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
				}
				this.isUsingAnItem = false;
				yield break;
			}
			yield return null;
		}
		goto IL_07C2;
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00067463 File Offset: 0x00065663
	public IEnumerator EndPlayerAction()
	{
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
		yield break;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00067474 File Offset: 0x00065674
	public void EndTurn()
	{
		if (this.isCheckingEffects)
		{
			return;
		}
		if (this.endingTurn)
		{
			return;
		}
		if (DigitalCursor.main && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			ItemMovement.RemoveAllCards();
		}
		this.endingTurn = true;
		Object.FindObjectOfType<TutorialManager>().TutorialTurnEnd();
		EnergyBall.ResetAllHeat();
		base.StopAllCoroutines();
		this.isCheckingEffects = false;
		base.StartCoroutine(this.EnemyTurn());
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x000674E1 File Offset: 0x000656E1
	private IEnumerator EnemyTurn()
	{
		if (this.gameManager.endTurnButton.activeInHierarchy)
		{
			Animator component = this.gameManager.endTurnButton.GetComponent<Animator>();
			if (component)
			{
				component.Play("Out");
			}
		}
		this.DeselectItem();
		yield return this.player.stats.TurnEnd();
		foreach (CombatPet combatPet in CombatPet.combatPets)
		{
			yield return combatPet.stats.TurnEnd();
		}
		List<CombatPet>.Enumerator enumerator = default(List<CombatPet>.Enumerator);
		this.player.HideAP();
		this.player.APEndedTurnWith = this.player.AP;
		this.player.AP = 0;
		if (this.player.characterName == Character.CharacterName.Tote)
		{
			Object.FindObjectOfType<Tote>().EndTurn();
		}
		else if (this.player.characterName == Character.CharacterName.CR8)
		{
			Object.FindObjectOfType<CR8Manager>().EndTurn();
		}
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onTurnEnd, null, null, true, false);
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		Item2.RemoveAllModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.forTurn }, -1);
		this.endingTurn = false;
		this.battlePhase = GameFlowManager.BattlePhase.enemyTurn;
		this.gameManager.Announcement(LangaugeManager.main.GetTextByKey("gmet"), "gmet");
		yield return new WaitForSeconds(1.1f);
		this.gameManager.ConsiderReorganizeItem();
		Object.FindObjectOfType<CR8Manager>();
		if (this.gameManager.endTurnButton.activeInHierarchy)
		{
			this.gameManager.endTurnButton.SetActive(false);
		}
		if (TwitchManager.isRunningPolls())
		{
			TwitchManager.Instance.pollManager.onEnemyTurnStart();
		}
		bool continueEnemies = true;
		List<Enemy> enemiesList = new List<Enemy>(Enemy.allEnemies);
		enemiesList = Enemy.SortEnemiesLeftToRight(enemiesList);
		foreach (Enemy enemy in enemiesList)
		{
			if (enemy && !enemy.dead)
			{
				yield return enemy.stats.ConsiderBurn();
			}
		}
		List<Enemy>.Enumerator enumerator2 = default(List<Enemy>.Enumerator);
		enemiesList = Enemy.SortEnemiesLeftToRight(enemiesList);
		using (List<Enemy>.Enumerator enumerator3 = enemiesList.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				Enemy enemy2 = enumerator3.Current;
				enemy2.stats.EndArmor();
			}
			goto IL_0429;
		}
		IL_035B:
		continueEnemies = false;
		List<Enemy> list = new List<Enemy>(Enemy.allEnemies);
		list = Enemy.SortEnemiesLeftToRight(list);
		foreach (Enemy enemy3 in list)
		{
			if (!enemy3.isPet)
			{
				if (!enemy3 || enemy3.myNextAttack == null)
				{
					break;
				}
				yield return enemy3.Turn();
				continueEnemies = true;
				while (this.isCheckingEffects)
				{
					yield return null;
				}
			}
		}
		enumerator2 = default(List<Enemy>.Enumerator);
		IL_0429:
		if (!continueEnemies)
		{
			foreach (Enemy enemy4 in new List<Enemy>(Enemy.allEnemies))
			{
				if (enemy4 && !enemy4.dead)
				{
					enemy4.stats.ConsiderEndCharm();
				}
			}
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			this.gameManager.Announcement(LangaugeManager.main.GetTextByKey("gmpt"), "gmpt");
			if (this.AnyEnemiesAlive())
			{
				yield return this.PlayerTurnStart();
			}
			else
			{
				yield return this.EndCombat();
			}
			yield break;
		}
		goto IL_035B;
		yield break;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x000674F0 File Offset: 0x000656F0
	public bool IsLivingNonCoward()
	{
		bool flag = false;
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy && !enemy.dead && enemy.stats && !enemy.stats.IsCharmed() && !enemy.isPet && !enemy.CheckForProperty(Enemy.EnemyProperty.Type.cowardly))
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00067584 File Offset: 0x00065784
	public void EndCombatAbruptly()
	{
		this.combatEndedAbruptly = true;
		this.player.HideAP();
		PlayerStatsDisplay playerStatsDisplay = Object.FindObjectOfType<PlayerStatsDisplay>();
		if (playerStatsDisplay)
		{
			playerStatsDisplay.EndEvent();
		}
		GameManager.main.EndCombatUI();
		SoundManager.main.PlayOrContinueSong(this.gameManager.dungeonLevel.levelSong);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x000675DB File Offset: 0x000657DB
	public IEnumerator EndCombat()
	{
		if (this.endingCombat)
		{
			yield break;
		}
		this.endingTurn = false;
		if (!this.gameManager || this.gameManager.dead)
		{
			yield break;
		}
		if (this.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			yield break;
		}
		this.endingCombat = true;
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		ItemPouch.CloseAllPouches();
		EnergyBall.ResetAllHeat();
		EnergyBall.EndAllEnergyBalls();
		if (GameManager.main.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
		{
			switch (GameManager.main.dungeonLevel.zone)
			{
			case DungeonLevel.Zone.dungeon:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatCrypt, 1);
				break;
			case DungeonLevel.Zone.deepCave:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatDeepCave, 1);
				break;
			case DungeonLevel.Zone.magmaCore:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatMagmaCore, 1);
				break;
			case DungeonLevel.Zone.EnchantedSwamp:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatEnchantedSwamp, 1);
				break;
			case DungeonLevel.Zone.theBramble:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatBramble, 1);
				break;
			case DungeonLevel.Zone.ice:
				MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.beatFrozenHeart, 1);
				break;
			}
		}
		AnalyticsManager analyticsManager = Object.FindObjectOfType<AnalyticsManager>();
		if (analyticsManager)
		{
			analyticsManager.AddItem("enemiesEncountered", this.gameManager.currentEnemyEncounter + " " + (this.gameManager.currentHealthAmount - this.player.stats.health).ToString());
		}
		Object.FindObjectOfType<TutorialManager>().TutorialTurnEnd();
		this.combatStats = new List<GameFlowManager.CombatStat>();
		this.battlePhase = GameFlowManager.BattlePhase.outOfBattle;
		this.player.HideAP();
		PetItem2.ReviveAllDefeatedPets();
		this.DeselectItem();
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in this.markers)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(metaProgressMarker);
		}
		this.markers = new List<MetaProgressSaveManager.MetaProgressMarker>();
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager && tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.done)
		{
			tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
		}
		tutorialManager.CombatEnd();
		this.player.CombatEnd();
		if (this.player.characterName == Character.CharacterName.Satchel)
		{
			CramButton.ResetAllCramButtonCosts();
		}
		SlidingRune[] array = Object.FindObjectsOfType<SlidingRune>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].used = false;
		}
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onTurnEnd, null, null, true, false);
		this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onCombatEnd, null, null, true, false);
		yield return new WaitForEndOfFrame();
		yield return null;
		yield return new WaitForFixedUpdate();
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		if (this.gameManager.dead)
		{
			yield break;
		}
		List<Item2> list = new List<Item2>(Item2.allItems);
		foreach (Item2 item in list)
		{
			if (item && item && item.itemMovement && !item.destroyed)
			{
				Debug.Log("Checking item " + item.name + " for hazards");
				if (item.itemType.Contains(Item2.ItemType.Hazard) || item.itemType.Contains(Item2.ItemType.Blessing) || item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.temporary))
				{
					item.itemMovement.DelayDestroy();
					yield return null;
					while (this.isCheckingEffects)
					{
						yield return null;
					}
				}
			}
		}
		List<Item2>.Enumerator enumerator2 = default(List<Item2>.Enumerator);
		Debug.Log("No more hazards");
		while (this.isCheckingEffects)
		{
			yield return null;
		}
		yield return null;
		Debug.Log("No more hazards 2");
		PetItem2.RemoveAllPets();
		foreach (ManaStone manaStone in Object.FindObjectsOfType<ManaStone>())
		{
			manaStone.currentPower = manaStone.maxPower;
		}
		this.player.stats.EndArmor();
		this.gameManager.EndCombatUI();
		Status[] array3 = Object.FindObjectsOfType<Status>();
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].EndCombat();
		}
		this.gameManager.ShowInventory();
		this.gameManager.EndBattle();
		if (!this.combatEndedAbruptly)
		{
			this.playerAnimator.Play("Player_Win");
		}
		if (this.player.characterName == Character.CharacterName.Tote)
		{
			Object.FindObjectOfType<Tote>().EndCombat();
		}
		Item2.RemoveAllModifiers(new List<Item2.Modifier.Length>
		{
			Item2.Modifier.Length.forTurn,
			Item2.Modifier.Length.forCombat,
			Item2.Modifier.Length.untilUse,
			Item2.Modifier.Length.untilDiscard
		}, -1);
		this.DoAllSavedDestroys();
		Item2.GetAllEffectTotals();
		base.StopAllCoroutines();
		this.isCheckingEffects = false;
		this.endingCombat = false;
		yield break;
		yield break;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000675EA File Offset: 0x000657EA
	public void EndCombatMusicSwitch(bool levelUp)
	{
		if (levelUp)
		{
			return;
		}
		SoundManager.main.PlayOrContinueSong(this.gameManager.dungeonLevel.levelSong);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0006760A File Offset: 0x0006580A
	public bool IsComplete()
	{
		return !this.isCheckingEffects && this.actionFinished && this.actionPerformed && !this.isUsingAnItem;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0006762F File Offset: 0x0006582F
	public IEnumerator Scratch(int power = 3, int APCostOverride = 1, bool projectile = false)
	{
		int timesToDo = 1;
		Item2.UseAllItemsIndirectWithStatusEffect(Item2.ItemStatusEffect.Type.doubleScratch, null);
		while (!this.IsComplete())
		{
			yield return null;
		}
		List<Item2> itemsWithStatusEffect = Item2.GetItemsWithStatusEffect(Item2.ItemStatusEffect.Type.doubleScratch, null, false);
		timesToDo += itemsWithStatusEffect.Count;
		if (this.player.AP < APCostOverride)
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
			yield break;
		}
		this.player.ChangeAP(APCostOverride * -1);
		int num;
		for (int i = 0; i < timesToDo; i = num + 1)
		{
			this.battlePhase = GameFlowManager.BattlePhase.playerAction;
			this.actionPerformed = false;
			this.actionFinished = false;
			if (Singleton.Instance.playerAnimations)
			{
				if (this.player.characterName == Character.CharacterName.Tote)
				{
					this.playerAnimator.Play("Player_FireArrow", 0, 0f);
				}
				else
				{
					this.playerAnimator.Play("Player_Attack", 0, 0f);
				}
				while (!this.actionPerformed)
				{
					yield return null;
				}
			}
			else
			{
				this.actionPerformed = true;
				this.actionFinished = true;
			}
			int scratchPower = GameFlowManager.GetScratchPower(power);
			this.gameManager.targetedEnemy.stats.Attack(this.player.stats, scratchPower * -1, null, projectile, true, true);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onScratch, null, null, true, false);
			this.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.clearActivatedItems, null, null, true, false);
			while (!this.actionFinished)
			{
				yield return null;
			}
			if (this.gameManager.targetedEnemy && this.gameManager.targetedEnemy.dead)
			{
				yield return null;
			}
			this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
			num = i;
		}
		yield break;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00067654 File Offset: 0x00065854
	public static int GetScratchPower(int power)
	{
		int num = power + Item2.GetEffectValues(Item2.Effect.Type.AddDamageToScratch);
		StatusEffect statusEffectOfType = Player.main.stats.GetStatusEffectOfType(StatusEffect.Type.rage);
		if (statusEffectOfType)
		{
			num += statusEffectOfType.value;
		}
		StatusEffect statusEffectOfType2 = Player.main.stats.GetStatusEffectOfType(StatusEffect.Type.weak);
		if (statusEffectOfType2)
		{
			num -= statusEffectOfType2.value;
		}
		return Mathf.Max(num, 1);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x000676B8 File Offset: 0x000658B8
	public IEnumerator Whistle()
	{
		this.battlePhase = GameFlowManager.BattlePhase.playerAction;
		this.actionPerformed = false;
		this.actionFinished = false;
		SoundManager.main.PlaySFX("instr_flute_" + Random.Range(1, 5).ToString());
		if (Singleton.Instance.playerAnimations)
		{
			this.player.itemToInteractWith.sprite = null;
			this.playerAnimator.Play("Player_UseItem", 0, 0f);
			while (!this.actionPerformed)
			{
				yield return null;
			}
		}
		else
		{
			this.actionPerformed = true;
			this.actionFinished = true;
		}
		int num = 1;
		int num2 = 3;
		this.player.ChangeAP(num * -1);
		this.gameManager.targetedEnemy.stats.AddStatusEffect(StatusEffect.Type.charm, (float)num2, Item2.Effect.MathematicalType.summative);
		while (!this.actionFinished)
		{
			yield return null;
		}
		if (this.gameManager.targetedEnemy && this.gameManager.targetedEnemy.dead)
		{
			yield return null;
		}
		this.battlePhase = GameFlowManager.BattlePhase.playerTurn;
		yield break;
	}

	// Token: 0x0400081D RID: 2077
	public bool isWaitingForItemRoutine;

	// Token: 0x0400081E RID: 2078
	public static GameFlowManager main;

	// Token: 0x0400081F RID: 2079
	[SerializeField]
	private List<MetaProgressSaveManager.MetaProgressMarker> markers = new List<MetaProgressSaveManager.MetaProgressMarker>();

	// Token: 0x04000820 RID: 2080
	[SerializeField]
	private GameObject manaFlowPrefab;

	// Token: 0x04000821 RID: 2081
	public GameFlowManager.BattlePhase battlePhase;

	// Token: 0x04000822 RID: 2082
	public bool actionPerformed = true;

	// Token: 0x04000823 RID: 2083
	public bool actionFinished = true;

	// Token: 0x04000824 RID: 2084
	[HideInInspector]
	public Item2 selectedItem;

	// Token: 0x04000825 RID: 2085
	public List<Item2.Cost> selectedCosts = new List<Item2.Cost>();

	// Token: 0x04000826 RID: 2086
	public List<GameFlowManager.CombatStat> combatStats;

	// Token: 0x04000827 RID: 2087
	private List<GameFlowManager.RecordedTypesUsed> recordedTypesUsed = new List<GameFlowManager.RecordedTypesUsed>();

	// Token: 0x04000828 RID: 2088
	private GameManager gameManager;

	// Token: 0x04000829 RID: 2089
	private Player player;

	// Token: 0x0400082A RID: 2090
	private Animator playerAnimator;

	// Token: 0x0400082B RID: 2091
	public int turnNumber;

	// Token: 0x0400082C RID: 2092
	public bool isUsingAnItem;

	// Token: 0x0400082D RID: 2093
	public bool isCheckingEffects;

	// Token: 0x0400082E RID: 2094
	public List<GameFlowManager.Consideration> queuedConsiderations = new List<GameFlowManager.Consideration>();

	// Token: 0x0400082F RID: 2095
	public List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions> itemStatusEffectsToApplyAtEndOfQueuedActions = new List<GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions>();

	// Token: 0x04000830 RID: 2096
	public List<Item2> itemsDestroyedInThisEffectSequence = new List<Item2>();

	// Token: 0x04000831 RID: 2097
	private float timeToEnd = 0.1f;

	// Token: 0x04000832 RID: 2098
	public bool isRunningAutoEnd;

	// Token: 0x04000833 RID: 2099
	public bool interupt;

	// Token: 0x04000834 RID: 2100
	private bool isnNewCheck;

	// Token: 0x04000835 RID: 2101
	private bool endingTurn;

	// Token: 0x04000836 RID: 2102
	public bool combatEndedAbruptly;

	// Token: 0x04000837 RID: 2103
	private bool endingCombat;

	// Token: 0x02000399 RID: 921
	public enum BattlePhase
	{
		// Token: 0x040015AB RID: 5547
		outOfBattle,
		// Token: 0x040015AC RID: 5548
		playerTurn,
		// Token: 0x040015AD RID: 5549
		playerAction,
		// Token: 0x040015AE RID: 5550
		enemyTurn,
		// Token: 0x040015AF RID: 5551
		petTurn
	}

	// Token: 0x0200039A RID: 922
	[Serializable]
	public class CombatStat
	{
		// Token: 0x040015B0 RID: 5552
		public GameFlowManager.CombatStat.Type type;

		// Token: 0x040015B1 RID: 5553
		public int value;

		// Token: 0x040015B2 RID: 5554
		public Item2 item;

		// Token: 0x040015B3 RID: 5555
		public GameFlowManager.CombatStat.Length length;

		// Token: 0x020004B6 RID: 1206
		public enum Type
		{
			// Token: 0x04001C42 RID: 7234
			itemsUsed,
			// Token: 0x04001C43 RID: 7235
			damageTaken,
			// Token: 0x04001C44 RID: 7236
			carvingsUsed,
			// Token: 0x04001C45 RID: 7237
			boardsCleared,
			// Token: 0x04001C46 RID: 7238
			specificItemUsed,
			// Token: 0x04001C47 RID: 7239
			cursesReceived,
			// Token: 0x04001C48 RID: 7240
			damageDealt,
			// Token: 0x04001C49 RID: 7241
			invincibleHitsTaken
		}

		// Token: 0x020004B7 RID: 1207
		public enum Length
		{
			// Token: 0x04001C4B RID: 7243
			turn,
			// Token: 0x04001C4C RID: 7244
			combat
		}
	}

	// Token: 0x0200039B RID: 923
	public class RecordedTypesUsed
	{
		// Token: 0x0600176A RID: 5994 RVA: 0x000C54E3 File Offset: 0x000C36E3
		public RecordedTypesUsed(Item2.ItemType _type, GameFlowManager.RecordedTypesUsed.Length _length)
		{
			this.type = _type;
			this.length = _length;
		}

		// Token: 0x040015B4 RID: 5556
		public Item2.ItemType type;

		// Token: 0x040015B5 RID: 5557
		public GameFlowManager.RecordedTypesUsed.Length length;

		// Token: 0x020004B8 RID: 1208
		public enum Length
		{
			// Token: 0x04001C4E RID: 7246
			thisCombat,
			// Token: 0x04001C4F RID: 7247
			lastCombat,
			// Token: 0x04001C50 RID: 7248
			thisTurn,
			// Token: 0x04001C51 RID: 7249
			lastTurn
		}
	}

	// Token: 0x0200039C RID: 924
	[Serializable]
	public class Consideration
	{
		// Token: 0x0600176B RID: 5995 RVA: 0x000C54FC File Offset: 0x000C36FC
		public Consideration(Item2.Trigger.ActionTrigger _trigger, List<Item2> _items, List<Status> _stats, List<PetMaster> _canApplyTriggerToAllItemsAndEnemies, bool canLaunchEnemyAction = true)
		{
			this.trigger = _trigger;
			this.items = _items;
			this.stats = _stats;
			this.canLaunchEnemyAction = canLaunchEnemyAction;
			if (_canApplyTriggerToAllItemsAndEnemies == null)
			{
				this.canApplyTriggerToAllItemsAndEnemies = new List<PetMaster>();
				return;
			}
			this.canApplyTriggerToAllItemsAndEnemies = _canApplyTriggerToAllItemsAndEnemies;
		}

		// Token: 0x040015B6 RID: 5558
		public Item2.Trigger.ActionTrigger trigger;

		// Token: 0x040015B7 RID: 5559
		public List<Item2> items;

		// Token: 0x040015B8 RID: 5560
		public List<Status> stats;

		// Token: 0x040015B9 RID: 5561
		public List<GameFlowManager.Consideration.ConsiderationItems> alreadyEffectedItems;

		// Token: 0x040015BA RID: 5562
		public List<PetMaster> canApplyTriggerToAllItemsAndEnemies;

		// Token: 0x040015BB RID: 5563
		public bool canLaunchEnemyAction = true;

		// Token: 0x020004B9 RID: 1209
		public class ConsiderationItems
		{
			// Token: 0x06001AF7 RID: 6903 RVA: 0x000D6DD3 File Offset: 0x000D4FD3
			public ConsiderationItems(Item2.Modifier _modifier, Item2 _item)
			{
				this.modifier = _modifier;
				this.item = _item;
			}

			// Token: 0x04001C52 RID: 7250
			public Item2.Modifier modifier;

			// Token: 0x04001C53 RID: 7251
			public Item2 item;
		}
	}

	// Token: 0x0200039D RID: 925
	public class ItemStatusEffectToApplyAtEndOfQueuedActions
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x000C554B File Offset: 0x000C374B
		public ItemStatusEffectToApplyAtEndOfQueuedActions(string _name, Item2 _item, Item2.Effect _effect, bool canOnlyBeAppliedAtEndOfTotalSequence = false)
		{
			this.name = _name;
			this.item = _item;
			this.effect = _effect;
			this.canOnlyBeAppliedAtEndOfTotalSequence = canOnlyBeAppliedAtEndOfTotalSequence;
		}

		// Token: 0x040015BC RID: 5564
		public string name = "";

		// Token: 0x040015BD RID: 5565
		public Item2 item;

		// Token: 0x040015BE RID: 5566
		public Item2.Effect effect;

		// Token: 0x040015BF RID: 5567
		public bool canOnlyBeAppliedAtEndOfTotalSequence;
	}
}

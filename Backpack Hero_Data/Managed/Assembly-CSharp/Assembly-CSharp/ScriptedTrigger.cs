using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class ScriptedTrigger : MonoBehaviour
{
	// Token: 0x06000380 RID: 896 RVA: 0x000145FC File Offset: 0x000127FC
	private void Start()
	{
		this.item = base.GetComponent<Item2>();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001460C File Offset: 0x0001280C
	public void ShowHighlights()
	{
		if (this.scriptedTriggerType == ScriptedTrigger.ScriptedTriggerType.whenConnectedToXItemsOfType)
		{
			foreach (PathFinding.TransformAndPath transformAndPath in ConnectionManager.FindAllItemsOfTypeConnected(this.item, this.itemTypes))
			{
				ItemMovement component = transformAndPath.t.GetComponent<ItemMovement>();
				if (component != null)
				{
					component.CreateHighlight(Color.yellow, null);
				}
			}
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00014688 File Offset: 0x00012888
	public int GetValue()
	{
		switch (this.scriptedTriggerType)
		{
		case ScriptedTrigger.ScriptedTriggerType.whenConnectedToXItemsOfType:
			return ConnectionManager.FindAllItemsOfTypeConnected(this.item, this.itemTypes).Count;
		case ScriptedTrigger.ScriptedTriggerType.whenOutOfMana:
			return 0;
		case ScriptedTrigger.ScriptedTriggerType.whenXInvisibleCharges:
			if (!this.item)
			{
				return -1;
			}
			return this.item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.invisbleCharge);
		case ScriptedTrigger.ScriptedTriggerType.whenXMana:
			return ConnectionManager.main.SumAvailableMana(this.item);
		case ScriptedTrigger.ScriptedTriggerType.whenMoreThanXBlock:
		case ScriptedTrigger.ScriptedTriggerType.whenEqualToOrLessThanXBlock:
			return Player.main.stats.armor;
		case ScriptedTrigger.ScriptedTriggerType.whenDealtNoDamage:
			return GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.damageDealt, GameFlowManager.CombatStat.Length.turn);
		case ScriptedTrigger.ScriptedTriggerType.whenYouHaveEnergy:
			if (Player.main.characterName == Character.CharacterName.CR8)
			{
				EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
				if (currentEnergyBall)
				{
					return currentEnergyBall.energyValue + 1;
				}
			}
			return Player.main.AP;
		case ScriptedTrigger.ScriptedTriggerType.repeatMultipleTimes:
			return this.currentTimesPerformed;
		default:
			return 0;
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00014766 File Offset: 0x00012966
	private void Awake()
	{
		if (!ScriptedTrigger.allTriggers.Contains(this))
		{
			ScriptedTrigger.allTriggers.Add(this);
		}
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00014780 File Offset: 0x00012980
	private void OnDestory()
	{
		ScriptedTrigger.allTriggers.Remove(this);
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00014790 File Offset: 0x00012990
	public static void ResetTimesPerformed()
	{
		foreach (ScriptedTrigger scriptedTrigger in ScriptedTrigger.allTriggers)
		{
			scriptedTrigger.ResetTimesPerformedIndividual();
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000147E0 File Offset: 0x000129E0
	public void ResetTimesPerformedIndividual()
	{
		this.currentTimesPerformed = 0;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x000147EC File Offset: 0x000129EC
	public void ConsiderRecursion(List<Item2.Trigger.ActionTrigger> _triggers)
	{
		if (!this.recursive)
		{
			return;
		}
		if (this.onlyInCombat && GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			return;
		}
		if (!_triggers.Contains(this.triggerType) && !_triggers.Contains(Item2.Trigger.ActionTrigger.recursive))
		{
			return;
		}
		if (GameFlowManager.main.CheckForEffectQueue(Item2.Trigger.ActionTrigger.recursive, new List<Item2> { this.item }))
		{
			return;
		}
		if (!this.recursiveUsesCosts)
		{
			GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.recursive, this.item, null, false, false);
			this.currentTimesPerformed++;
			return;
		}
		if (this.item.CanBeUsedActive(false, this.item.costs, true, false, null, false))
		{
			foreach (Item2.Cost cost in this.item.costs)
			{
				cost.GetCurrentValue();
				if (cost.type == Item2.Cost.Type.energy && cost.currentValue <= 0 && cost.baseValue != 0)
				{
					EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
					if (currentEnergyBall)
					{
						currentEnergyBall.ChangeEnergy(-1);
					}
					Player.main.ChangeAP(-1);
				}
			}
			Item2.DetractCosts(this.item, this.item.costs, null);
			GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.recursive, this.item, null, true, false);
			this.currentTimesPerformed++;
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001495C File Offset: 0x00012B5C
	public bool CheckTrigger2(List<Item2.Trigger.ActionTrigger> _triggers)
	{
		if (!this.item || !this.item.itemMovement || (this.item.destroyed && !_triggers.Contains(Item2.Trigger.ActionTrigger.onDestroy)))
		{
			return false;
		}
		if (!_triggers.Contains(this.triggerType) && !_triggers.Contains(Item2.Trigger.ActionTrigger.recursive))
		{
			return false;
		}
		if (this.onlyInCombat && GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			return false;
		}
		int value = this.GetValue();
		int num = this.triggerValue;
		if (this.triggerValueType == ScriptedTrigger.TriggerValueType.numberOfInvincbleHitsTaken)
		{
			num = GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.invincibleHitsTaken, GameFlowManager.CombatStat.Length.combat);
			num = Mathf.Clamp(num, 1, 999);
		}
		switch (this.scriptedTriggerType)
		{
		case ScriptedTrigger.ScriptedTriggerType.whenConnectedToXItemsOfType:
			if (value >= num)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenOutOfMana:
			if (!GameFlowManager.main.TestForMana(this.item, 1))
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenXInvisibleCharges:
			if (value >= num)
			{
				this.item.RemoveStatusEffect(Item2.ItemStatusEffect.Type.invisbleCharge, Item2.ItemStatusEffect.Length.turns);
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenXMana:
			if (value >= num)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenMoreThanXBlock:
			if (value >= num)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenEqualToOrLessThanXBlock:
			if (value <= num)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenDealtNoDamage:
			if (value <= 0)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.whenYouHaveEnergy:
			if (value >= num)
			{
				return true;
			}
			break;
		case ScriptedTrigger.ScriptedTriggerType.repeatMultipleTimes:
			if (value < num)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x0400027F RID: 639
	public string triggerKey;

	// Token: 0x04000280 RID: 640
	public string secondTriggerKey = "";

	// Token: 0x04000281 RID: 641
	public List<Item2.ItemType> itemTypes;

	// Token: 0x04000282 RID: 642
	public Item2.Trigger.ActionTrigger triggerType;

	// Token: 0x04000283 RID: 643
	public ScriptedTrigger.ScriptedTriggerType scriptedTriggerType;

	// Token: 0x04000284 RID: 644
	public bool showProgress;

	// Token: 0x04000285 RID: 645
	public bool onlyInCombat;

	// Token: 0x04000286 RID: 646
	public bool recursive;

	// Token: 0x04000287 RID: 647
	public bool recursiveUsesCosts;

	// Token: 0x04000288 RID: 648
	public ScriptedTrigger.TriggerValueType triggerValueType;

	// Token: 0x04000289 RID: 649
	public int triggerValue;

	// Token: 0x0400028A RID: 650
	private int currentTimesPerformed;

	// Token: 0x0400028B RID: 651
	private Item2 item;

	// Token: 0x0400028C RID: 652
	private static List<ScriptedTrigger> allTriggers = new List<ScriptedTrigger>();

	// Token: 0x020002A9 RID: 681
	public enum ScriptedTriggerType
	{
		// Token: 0x0400101A RID: 4122
		whenConnectedToXItemsOfType,
		// Token: 0x0400101B RID: 4123
		whenOutOfMana,
		// Token: 0x0400101C RID: 4124
		whenXInvisibleCharges,
		// Token: 0x0400101D RID: 4125
		whenXMana,
		// Token: 0x0400101E RID: 4126
		whenMoreThanXBlock,
		// Token: 0x0400101F RID: 4127
		whenEqualToOrLessThanXBlock,
		// Token: 0x04001020 RID: 4128
		whenDealtNoDamage,
		// Token: 0x04001021 RID: 4129
		whenYouHaveEnergy,
		// Token: 0x04001022 RID: 4130
		repeatMultipleTimes
	}

	// Token: 0x020002AA RID: 682
	public enum TriggerValueType
	{
		// Token: 0x04001024 RID: 4132
		integer,
		// Token: 0x04001025 RID: 4133
		numberOfInvincbleHitsTaken
	}
}

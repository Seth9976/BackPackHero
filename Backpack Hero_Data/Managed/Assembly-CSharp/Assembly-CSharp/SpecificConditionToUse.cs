using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class SpecificConditionToUse : MonoBehaviour
{
	// Token: 0x06000868 RID: 2152 RVA: 0x00057EC0 File Offset: 0x000560C0
	public static bool CanBePlayedOnSpaceWithTheseItems(SpecificConditionToUse[] specificConditionToUses, List<GameObject> items)
	{
		return SpecificConditionToUse.CanBePlayedOnSpaceWithTheseItems(specificConditionToUses, Item2.GetItemsFromGameObjects(items));
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00057ED0 File Offset: 0x000560D0
	public static bool CanBePlayedOnSpaceWithTheseItems(SpecificConditionToUse[] specificConditionToUses, List<Item2> items)
	{
		if (specificConditionToUses.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < specificConditionToUses.Length; i++)
		{
			if (!specificConditionToUses[i].CanBePlayedOnSpaceWithTheseItems(items))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00057F01 File Offset: 0x00056101
	public bool CanBePlayedOnSpaceWithTheseItems(List<GameObject> items)
	{
		return this.CanBePlayedOnSpaceWithTheseItems(Item2.GetItemsFromGameObjects(items));
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00057F10 File Offset: 0x00056110
	public bool CanBePlayedOnSpaceWithTheseItems(List<Item2> items)
	{
		if (this.conditionTime == SpecificConditionToUse.ConditionTime.onDragEnd && this.conditionType == SpecificConditionToUse.ConditionType.mustBeUsedOnTypesListed)
		{
			foreach (Item2 item in items)
			{
				if (item && Item2.ShareItemTypes(this.itemTypes, item.itemType))
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00057F8C File Offset: 0x0005618C
	public bool CanBeUsed(List<SpecificConditionToUse.ConditionTime> conditionTimes, Item2 combineWithItem = null)
	{
		this.MakeReferences();
		Status status = null;
		if (GameManager.main.targetedEnemy)
		{
			status = GameManager.main.targetedEnemy.stats;
		}
		Status status2 = PetMaster.GetStatusFromInventory(this.item.lastParentInventoryGrid, this.player);
		if (status2 == null)
		{
			status2 = this.player.stats;
		}
		if (this.conditionTime != SpecificConditionToUse.ConditionTime.always && (conditionTimes == null || !conditionTimes.Contains(this.conditionTime)))
		{
			return true;
		}
		if (!this.item || !this.itemMovement || !this.player)
		{
			this.MakeReferences();
		}
		if (this.conditionType == SpecificConditionToUse.ConditionType.mustBeUsedOnDefeatedPet)
		{
			List<Item2> list = new List<Item2>();
			this.item.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
			using (List<Item2>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Item2 item = enumerator.Current;
					if (!(item == this.item))
					{
						PetItem2 component = item.GetComponent<PetItem2>();
						if (component && component.health <= 0)
						{
							return true;
						}
					}
				}
				return false;
			}
		}
		if (this.conditionType == SpecificConditionToUse.ConditionType.mustBeUsedOnUncleansedCurse)
		{
			return combineWithItem && combineWithItem.itemType.Contains(Item2.ItemType.Curse) && !combineWithItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.Cleansed);
		}
		if (this.conditionType == SpecificConditionToUse.ConditionType.mustBeUsedOnTypesListed)
		{
			List<Item2> list2 = new List<Item2>();
			bool inGrid = this.item.itemMovement.inGrid;
			if (!inGrid)
			{
				this.gridObject.SnapToGrid();
			}
			this.item.FindItemsAndGridsinArea(list2, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
			if (!inGrid)
			{
				this.gridObject.ClearGridPositions();
			}
			if (list2.Count == 0 && this.canAlsoBeOnEmptySpace)
			{
				return true;
			}
			using (List<Item2>.Enumerator enumerator = list2.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					return false;
				}
				if (Item2.ShareItemTypes(enumerator.Current.itemType, this.itemTypes))
				{
					return true;
				}
				return false;
			}
		}
		if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveAPetInFront)
		{
			if (PetMaster.GetRightMostCombatPetStats() == this.player.stats)
			{
				this.explanationKey = "gm56";
				return false;
			}
			return true;
		}
		else
		{
			if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveAPetOnBothSides)
			{
				Status rightMostCombatPetStats = PetMaster.GetRightMostCombatPetStats();
				return this.player.mySpacerLocation.GetSiblingIndex() > 0 && !(rightMostCombatPetStats == this.player.stats);
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveLessThanXPercentHealth)
			{
				return (float)status2.health / (float)status2.maxHealth <= this.value / 100f;
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.targetMustHaveLessThanXHealth)
			{
				return (float)status.health < this.value;
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveMoreThanXPercentHealth)
			{
				return (float)status2.health / (float)status2.maxHealth >= this.value / 100f;
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.ifYouUsedTheItemTypeLastTurnCantUse)
			{
				return !GameFlowManager.main.HasUsedItemType(this.itemTypes, GameFlowManager.RecordedTypesUsed.Length.lastTurn) && GameFlowManager.main.turnNumber > 1;
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.youMustUseItemTypeFirst)
			{
				return GameFlowManager.main.HasUsedItemType(this.itemTypes, GameFlowManager.RecordedTypesUsed.Length.thisTurn);
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveNotClearedCarvings)
			{
				return GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.boardsCleared, GameFlowManager.CombatStat.Length.combat) <= 0;
			}
			if (this.conditionType == SpecificConditionToUse.ConditionType.mustNotBeValidPetSummon)
			{
				PetItem2 component2 = this.item.GetComponent<PetItem2>();
				if (!component2)
				{
					return false;
				}
				if (component2.health <= 0)
				{
					this.explanationKey = "gm53";
					return false;
				}
				if (component2.combatPet)
				{
					this.explanationKey = "gm51";
					return false;
				}
				return true;
			}
			else
			{
				if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveItemInArea)
				{
					List<Item2> list3 = new List<Item2>();
					List<GridSquare> list4 = new List<GridSquare>();
					this.item.FindItemsAndGridsinArea(list3, list4, this.itemAreas, this.areaDistance, null, null, null, true, false, true);
					if (this.itemTypes.Contains(Item2.ItemType.Grid) || this.itemTypes.Contains(Item2.ItemType.GridEmpty))
					{
						list4 = Item2.GetGridSquares(this.itemTypes, list4);
						if (list4.Count > 0)
						{
							return true;
						}
					}
					else if (list3.Count > 0)
					{
						return true;
					}
					return false;
				}
				if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveAP)
				{
					return Player.main && Player.main.AP > 0;
				}
				if (this.conditionType == SpecificConditionToUse.ConditionType.mustHaveStatusEffect)
				{
					return Player.main.stats.GetStatusEffectOfType(this.statusEffectType);
				}
			}
		}
		return false;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00058454 File Offset: 0x00056654
	private void MakeReferences()
	{
		if (!this.item)
		{
			this.item = base.GetComponent<Item2>();
		}
		if (!this.itemMovement)
		{
			this.itemMovement = base.GetComponent<ItemMovement>();
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.gridObject)
		{
			this.gridObject = base.GetComponent<GridObject>();
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000584C4 File Offset: 0x000566C4
	private void Start()
	{
		this.MakeReferences();
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000584CC File Offset: 0x000566CC
	private void Update()
	{
	}

	// Token: 0x04000683 RID: 1667
	private Item2 item;

	// Token: 0x04000684 RID: 1668
	private ItemMovement itemMovement;

	// Token: 0x04000685 RID: 1669
	private GridObject gridObject;

	// Token: 0x04000686 RID: 1670
	private Player player;

	// Token: 0x04000687 RID: 1671
	public bool canAlsoBeOnEmptySpace;

	// Token: 0x04000688 RID: 1672
	[Header("------------------------Condition Type------------------------")]
	[SerializeField]
	public SpecificConditionToUse.ConditionType conditionType;

	// Token: 0x04000689 RID: 1673
	[SerializeField]
	private SpecificConditionToUse.ConditionTime conditionTime;

	// Token: 0x0400068A RID: 1674
	[SerializeField]
	private StatusEffect.Type statusEffectType;

	// Token: 0x0400068B RID: 1675
	[Header("------------------------Conditional Properties------------------------")]
	[SerializeField]
	private Item2.AreaDistance areaDistance;

	// Token: 0x0400068C RID: 1676
	[SerializeField]
	private List<Item2.Area> itemAreas = new List<Item2.Area>();

	// Token: 0x0400068D RID: 1677
	[SerializeField]
	private List<Item2.ItemType> itemTypes = new List<Item2.ItemType>();

	// Token: 0x0400068E RID: 1678
	[SerializeField]
	public float value;

	// Token: 0x0400068F RID: 1679
	[Header("------------------------Text------------------------")]
	[SerializeField]
	public string explanationKey;

	// Token: 0x04000690 RID: 1680
	[SerializeField]
	public string cardKey;

	// Token: 0x0200036B RID: 875
	public enum ConditionType
	{
		// Token: 0x04001478 RID: 5240
		mustBeUsedOnDefeatedPet,
		// Token: 0x04001479 RID: 5241
		mustHaveAPetInFront,
		// Token: 0x0400147A RID: 5242
		mustBeUsedOnTypesListed,
		// Token: 0x0400147B RID: 5243
		mustHaveLessThanXPercentHealth,
		// Token: 0x0400147C RID: 5244
		targetMustHaveLessThanXHealth,
		// Token: 0x0400147D RID: 5245
		ifYouUsedTheItemTypeLastTurnCantUse,
		// Token: 0x0400147E RID: 5246
		mustHaveMoreThanXPercentHealth,
		// Token: 0x0400147F RID: 5247
		youMustUseItemTypeFirst,
		// Token: 0x04001480 RID: 5248
		mustHaveAPetOnBothSides,
		// Token: 0x04001481 RID: 5249
		mustHaveNotClearedCarvings,
		// Token: 0x04001482 RID: 5250
		mustNotBeValidPetSummon,
		// Token: 0x04001483 RID: 5251
		mustBeUsedOnUncleansedCurse,
		// Token: 0x04001484 RID: 5252
		mustHaveItemInArea,
		// Token: 0x04001485 RID: 5253
		mustHaveAP,
		// Token: 0x04001486 RID: 5254
		mustHaveStatusEffect
	}

	// Token: 0x0200036C RID: 876
	public enum ConditionTime
	{
		// Token: 0x04001488 RID: 5256
		always,
		// Token: 0x04001489 RID: 5257
		onDragEnd,
		// Token: 0x0400148A RID: 5258
		forTurnAndCombatStart,
		// Token: 0x0400148B RID: 5259
		onAlternateUseSelect
	}
}

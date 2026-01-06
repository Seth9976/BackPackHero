using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class ValueChanger : MonoBehaviour
{
	// Token: 0x06000878 RID: 2168 RVA: 0x000585FD File Offset: 0x000567FD
	private void Start()
	{
		this.FindReferences();
		this.storedEffect = null;
		this.item = base.GetComponent<Item2>();
		if (!this.item)
		{
			this.item = base.GetComponentInParent<Item2>();
		}
		this.FindEffect();
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00058637 File Offset: 0x00056837
	private void FindReferences()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.saveManager = Object.FindObjectOfType<SaveManager>();
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0005865A File Offset: 0x0005685A
	public void FindEffectInAFrameRoutine()
	{
		base.StartCoroutine(this.FindEffectInAFrame());
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00058669 File Offset: 0x00056869
	private IEnumerator FindEffectInAFrame()
	{
		yield return new WaitForEndOfFrame();
		this.FindEffectTemp();
		yield break;
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00058678 File Offset: 0x00056878
	public void FindEffectTemp()
	{
		this.item = base.GetComponent<Item2>();
		if (!this.item)
		{
			this.item = base.GetComponentInParent<Item2>();
		}
		this.FindEffect();
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000586A8 File Offset: 0x000568A8
	private void FindEffect()
	{
		foreach (Item2.Modifier modifier in this.item.modifiers)
		{
			foreach (Item2.Effect effect in modifier.effects)
			{
				if (effect.value == this.valueToReplace)
				{
					this.storedEffect = effect;
					modifier.originalValue = effect.value;
					if (this.descriptionOverrideKey.Length > 1)
					{
						modifier.descriptionKey = this.descriptionOverrideKey;
						if (this.descriptionDisplayValue != -999f)
						{
							modifier.descriptionDisplayValue = this.descriptionDisplayValue;
						}
						else
						{
							modifier.descriptionDisplayValue = this.multiplier;
						}
					}
					if (this.triggerOverrideKey.Length > 1)
					{
						modifier.triggerKey = this.triggerOverrideKey;
						if (this.triggerDisplayValue != -999f)
						{
							modifier.triggerDisplayValue = this.triggerDisplayValue;
						}
						else
						{
							modifier.triggerDisplayValue = this.multiplier;
						}
					}
					return;
				}
			}
		}
		foreach (Item2.Modifier modifier2 in this.item.modifiers)
		{
			if (modifier2.originalValue == this.valueToReplace)
			{
				using (List<Item2.Effect>.Enumerator enumerator2 = modifier2.effects.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						Item2.Effect effect2 = enumerator2.Current;
						this.storedEffect = effect2;
						if (this.descriptionOverrideKey.Length > 1)
						{
							modifier2.descriptionKey = this.descriptionOverrideKey;
							if (this.descriptionDisplayValue != -999f)
							{
								modifier2.descriptionDisplayValue = this.descriptionDisplayValue;
							}
							else
							{
								modifier2.descriptionDisplayValue = this.multiplier;
							}
						}
						if (this.triggerOverrideKey.Length > 1)
						{
							modifier2.triggerKey = this.triggerOverrideKey;
							if (this.triggerDisplayValue != -999f)
							{
								modifier2.triggerDisplayValue = this.triggerDisplayValue;
							}
							else
							{
								modifier2.triggerDisplayValue = this.multiplier;
							}
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0005891C File Offset: 0x00056B1C
	private void Update()
	{
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00058920 File Offset: 0x00056B20
	public static void ResetAllValueChangesForSaving()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("ItemParent");
		if (!gameObject)
		{
			Debug.Log("The itemParent transform couldn't be found at all");
			return;
		}
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			ValueChanger.SetValueChanger(transform);
			foreach (object obj2 in transform)
			{
				ValueChanger.SetValueChanger((Transform)obj2);
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Dungeon Event");
		for (int i = 0; i < array.Length; i++)
		{
			foreach (object obj3 in array[i].transform)
			{
				ValueChanger.SetValueChanger((Transform)obj3);
			}
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00058A3C File Offset: 0x00056C3C
	public static void SetValueChanger(Transform t)
	{
		if (!t || !t.gameObject)
		{
			return;
		}
		if (t.gameObject.activeInHierarchy)
		{
			ValueChanger component = t.GetComponent<ValueChanger>();
			if (component)
			{
				component.ResetValueForSaving();
				return;
			}
		}
		else
		{
			t.gameObject.SetActive(true);
			ValueChanger component2 = t.GetComponent<ValueChanger>();
			if (component2)
			{
				component2.ResetValueForSaving();
			}
			t.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00058AAF File Offset: 0x00056CAF
	public void ResetValueForSaving()
	{
		if (this.storedEffect != null)
		{
			this.storedEffect.value = this.valueToReplace;
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00058ACC File Offset: 0x00056CCC
	public void ChangeValue()
	{
		if (!this.gameManager || !this.saveManager)
		{
			this.FindReferences();
		}
		if (!this.gameManager || !this.saveManager)
		{
			return;
		}
		if (this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (!this.item)
		{
			return;
		}
		if (this.storedEffect == null)
		{
			this.FindEffect();
		}
		if (this.storedEffect == null)
		{
			return;
		}
		float value = this.storedEffect.value;
		float num = 0f;
		Status statusFromInventory = PetMaster.GetStatusFromInventory(this.item.lastParentInventoryGrid, this.player);
		Status selectedEnemyStatus = Enemy.GetSelectedEnemyStatus();
		Enemy.GetAllEnemyStats();
		if (this.replaceWithValue == ValueChanger.ReplaceWithValue.block && statusFromInventory)
		{
			num = (float)statusFromInventory.armor;
		}
		else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfItem)
		{
			List<Item2> list = new List<Item2>();
			List<GridSquare> list2 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list, list2, this.areas, this.areaDistance, null, null, null, true, false, true);
			num = (float)Item2.GetAllItemsCopies(this.itemPrefabs, list).Count;
		}
		else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfItemType)
		{
			List<Item2> list3 = new List<Item2>();
			List<GridSquare> list4 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list3, list4, this.areas, this.areaDistance, null, null, null, true, false, true);
			if (this.types.Count == 1 && this.types[0] == Item2.ItemType.Grid)
			{
				num = (float)list4.Count;
			}
			else if (this.types.Count == 1 && this.types[0] == Item2.ItemType.GridEmpty)
			{
				num = (float)list4.Where((GridSquare x) => x.CountAsEmpty()).Count<GridSquare>();
			}
			else
			{
				num = (float)Item2.FilterByTypes(this.types, list3).Count;
			}
		}
		else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.carvingsPlayed)
		{
			num = (float)Mathf.Max(0, GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.carvingsUsed, GameFlowManager.CombatStat.Length.combat));
		}
		else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.carvingsInDiscard)
		{
			num = (float)Mathf.Max(0, Object.FindObjectOfType<Tote>().GetCarvingsInDiscard());
		}
		else
		{
			if (this.replaceWithValue == ValueChanger.ReplaceWithValue.getSizeOfItem)
			{
				List<Item2> list5 = new List<Item2>();
				List<GridSquare> list6 = new List<GridSquare>();
				this.item.FindItemsAndGridsinArea(list5, list6, this.areas, this.areaDistance, null, null, null, true, false, true);
				num = 0f;
				if (list5.Count <= 0)
				{
					goto IL_0650;
				}
				using (List<Item2>.Enumerator enumerator = list5.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Item2 item = enumerator.Current;
						if (!(item == this.item) && item && this.item.itemMovement)
						{
							num += (float)Mathf.Max(0, item.itemMovement.GetSpacesNeeded());
						}
					}
					goto IL_0650;
				}
			}
			if (this.replaceWithValue == ValueChanger.ReplaceWithValue.amountOfPoison && statusFromInventory)
			{
				num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.poison));
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.amountOfCharmOnTarget && selectedEnemyStatus)
			{
				num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.charm));
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfPockets)
			{
				if (!this.pm)
				{
					this.pm = PocketManager.main;
				}
				if (this.pm)
				{
					num = (float)Mathf.Max(0, this.pm.GetNumOfPockets());
				}
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfSpacesInThisPocket)
			{
				List<PocketManager.Pocket> pocketsFromItem = PocketManager.GetPocketsFromItem(this.item);
				int num2 = 0;
				foreach (PocketManager.Pocket pocket in pocketsFromItem)
				{
					num2 += PocketManager.GetSpacesInPocket(pocket);
				}
				num = (float)num2;
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfSpacesInAllOtherPockets)
			{
				List<PocketManager.Pocket> pocketsFromItem2 = PocketManager.GetPocketsFromItem(this.item);
				int num3 = Object.FindObjectsOfType<GridSquare>().Length;
				int num4 = 0;
				foreach (PocketManager.Pocket pocket2 in pocketsFromItem2)
				{
					num4 += PocketManager.GetSpacesInPocket(pocket2);
				}
				num = (float)(num3 - num4);
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfPocketsInArea)
			{
				List<PocketManager.Pocket> list7 = new List<PocketManager.Pocket>();
				List<Item2> list8 = new List<Item2>();
				List<GridSquare> list9 = new List<GridSquare>();
				this.item.FindItemsAndGridsinArea(list8, list9, this.areas, this.areaDistance, null, null, null, true, false, true);
				foreach (GridSquare gridSquare in list9)
				{
					PocketManager.Pocket pocket3 = PocketManager.GetPocket(gridSquare);
					if (pocket3 != null && !list7.Contains(pocket3))
					{
						list7.Add(pocket3);
					}
				}
				num = (float)list7.Count;
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfItemsInThisPouch)
			{
				ItemPouch component = base.GetComponent<ItemPouch>();
				if (component)
				{
					num = (float)component.itemsInside.Count;
				}
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfCursesOnThisItem)
			{
				int num5 = 0;
				using (List<Item2.Modifier>.Enumerator enumerator4 = this.item.appliedModifiers.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						if (enumerator4.Current.name.ToLower() == "curse")
						{
							num5++;
						}
					}
				}
				foreach (Item2.ItemStatusEffect itemStatusEffect in this.item.activeItemStatusEffects)
				{
					if (itemStatusEffect.source != null && itemStatusEffect.source == "curse")
					{
						num5++;
					}
				}
				num = (float)num5;
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.petsSummoned)
			{
				num = (float)CombatPet.GetLivePets().Count;
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.random)
			{
				num = Random.Range(-this.multiplier, this.multiplier);
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.deadPets)
			{
				num = (float)CombatPet.GetDeadPets();
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.gold)
			{
				num = (float)GameManager.main.GetCurrentGold();
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numberOfTimesHurt)
			{
				num = (float)GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.invincibleHitsTaken, GameFlowManager.CombatStat.Length.combat);
			}
			else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.currentCharges)
			{
				num = (float)this.item.currentCharges;
			}
		}
		IL_0650:
		this.storedEffect.value = (float)Mathf.FloorToInt(num * this.multiplier);
		if (this.storedEffect.value != value && this.item && this.item.itemMovement)
		{
			base.StartCoroutine(this.item.itemMovement.ModifiedAnimation());
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x000591D4 File Offset: 0x000573D4
	public void AddHighlights()
	{
		List<Item2> list = new List<Item2>();
		if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfItem)
		{
			List<Item2> list2 = new List<Item2>();
			List<GridSquare> list3 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list2, list3, this.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
			list = Item2.GetAllItemsCopies(this.itemPrefabs, list2);
		}
		else if (this.replaceWithValue == ValueChanger.ReplaceWithValue.numOfItemType)
		{
			List<Item2> list4 = new List<Item2>();
			List<GridSquare> list5 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list4, list5, this.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
			list = Item2.FilterByTypes(this.types, list4);
		}
		foreach (Item2 item in list)
		{
			item.GetComponent<ItemMovement>().CreateHighlight(Color.yellow, null);
		}
	}

	// Token: 0x0400069A RID: 1690
	[SerializeField]
	public string triggerOverrideKey;

	// Token: 0x0400069B RID: 1691
	[SerializeField]
	public float triggerDisplayValue = -999f;

	// Token: 0x0400069C RID: 1692
	[SerializeField]
	public string descriptionOverrideKey;

	// Token: 0x0400069D RID: 1693
	[SerializeField]
	public bool showPlusSign;

	// Token: 0x0400069E RID: 1694
	[SerializeField]
	public float descriptionDisplayValue = -999f;

	// Token: 0x0400069F RID: 1695
	public ValueChanger.ReplaceWithValue replaceWithValue;

	// Token: 0x040006A0 RID: 1696
	[SerializeField]
	public float valueToReplace = -999f;

	// Token: 0x040006A1 RID: 1697
	[SerializeField]
	public float multiplier;

	// Token: 0x040006A2 RID: 1698
	[SerializeField]
	public List<GameObject> itemPrefabs;

	// Token: 0x040006A3 RID: 1699
	[SerializeField]
	public List<Item2.Area> areas;

	// Token: 0x040006A4 RID: 1700
	[SerializeField]
	public List<Item2.ItemType> types;

	// Token: 0x040006A5 RID: 1701
	[SerializeField]
	public Item2.AreaDistance areaDistance;

	// Token: 0x040006A6 RID: 1702
	private GameManager gameManager;

	// Token: 0x040006A7 RID: 1703
	private Player player;

	// Token: 0x040006A8 RID: 1704
	private SaveManager saveManager;

	// Token: 0x040006A9 RID: 1705
	public Item2.Effect storedEffect;

	// Token: 0x040006AA RID: 1706
	private PocketManager pm;

	// Token: 0x040006AB RID: 1707
	private Item2 item;

	// Token: 0x0200036F RID: 879
	public enum ReplaceWithValue
	{
		// Token: 0x04001493 RID: 5267
		block,
		// Token: 0x04001494 RID: 5268
		numOfItem,
		// Token: 0x04001495 RID: 5269
		numOfItemType,
		// Token: 0x04001496 RID: 5270
		carvingsPlayed,
		// Token: 0x04001497 RID: 5271
		carvingsInDiscard,
		// Token: 0x04001498 RID: 5272
		getSizeOfItem,
		// Token: 0x04001499 RID: 5273
		distanceChargeTraveled,
		// Token: 0x0400149A RID: 5274
		amountOfPoison,
		// Token: 0x0400149B RID: 5275
		numOfPockets,
		// Token: 0x0400149C RID: 5276
		numOfSpacesInThisPocket,
		// Token: 0x0400149D RID: 5277
		numOfSpacesInAllOtherPockets,
		// Token: 0x0400149E RID: 5278
		numOfPocketsInArea,
		// Token: 0x0400149F RID: 5279
		numOfItemsInThisPouch,
		// Token: 0x040014A0 RID: 5280
		numOfCursesOnThisItem,
		// Token: 0x040014A1 RID: 5281
		petsSummoned,
		// Token: 0x040014A2 RID: 5282
		random,
		// Token: 0x040014A3 RID: 5283
		amountOfCharmOnTarget,
		// Token: 0x040014A4 RID: 5284
		deadPets,
		// Token: 0x040014A5 RID: 5285
		gold,
		// Token: 0x040014A6 RID: 5286
		numberOfTimesHurt,
		// Token: 0x040014A7 RID: 5287
		currentCharges
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class Item2 : MonoBehaviour
{
	// Token: 0x0600072A RID: 1834 RVA: 0x000452B0 File Offset: 0x000434B0
	public static List<Item2.ItemType> GetAllItemTypesExcluding(List<Item2.ItemType> excluding = null)
	{
		List<Item2.ItemType> list = new List<Item2.ItemType>();
		foreach (object obj in Enum.GetValues(typeof(Item2.ItemType)))
		{
			Item2.ItemType itemType = (Item2.ItemType)obj;
			if (itemType != Item2.ItemType.Any && (excluding == null || !excluding.Contains(itemType)))
			{
				list.Add(itemType);
			}
		}
		return list;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00045328 File Offset: 0x00043528
	public static List<Item2.Cost> GetCosts(List<Item2.Cost> costs)
	{
		for (int i = 0; i < costs.Count; i++)
		{
			Item2.Cost cost = costs[i];
			cost.GetCurrentValue();
			if (cost.currentValue == 0 && cost.costModifiers.Count == 0 && !cost.originalCost)
			{
				costs.RemoveAt(i);
				i--;
			}
		}
		return costs;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00045380 File Offset: 0x00043580
	public static string GetCurrentCostText(Item2.Cost.Type costType, List<Item2.Cost> costs)
	{
		costs = Item2.GetCosts(costs);
		int currentCost = Item2.GetCurrentCost(costType, costs);
		foreach (Item2.Cost cost in costs)
		{
			if (cost.type == costType)
			{
				if (cost.displayAsX)
				{
					return "X";
				}
				return Mathf.Max(currentCost, 0).ToString();
			}
		}
		return "";
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0004540C File Offset: 0x0004360C
	public static int GetCurrentCost(Item2.Cost.Type costType, List<Item2.Cost> costs)
	{
		costs = Item2.GetCosts(costs);
		foreach (Item2.Cost cost in costs)
		{
			if (cost.type == costType)
			{
				return cost.currentValue;
			}
		}
		return -999;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00045474 File Offset: 0x00043674
	public void AddCost(Item2.Cost.Type costType, int value, Item2.Modifier.Length length, List<Item2.Cost> costs)
	{
		Item2.Cost.CostModifier costModifier = new Item2.Cost.CostModifier();
		costModifier.value = value;
		costModifier.length = length;
		costs = Item2.GetCosts(costs);
		foreach (Item2.Cost cost in costs)
		{
			if (cost.type == costType)
			{
				cost.costModifiers.Add(costModifier);
				return;
			}
		}
		costs.Add(new Item2.Cost
		{
			type = costType,
			baseValue = 0,
			costModifiers = { costModifier },
			originalCost = false
		});
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00045520 File Offset: 0x00043720
	public void RemoveCost()
	{
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00045524 File Offset: 0x00043724
	private void Start()
	{
		if (!Item2.allItems.Contains(this))
		{
			Item2.allItems.Add(this);
		}
		this.canBeComboed = true;
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.petItem = base.GetComponent<PetItem2>();
		this.scriptedTriggers = base.GetComponentsInChildren<ScriptedTrigger>().ToList<ScriptedTrigger>();
		this.gridObject = base.GetComponent<GridObject>();
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.displayName = Item2.GetDisplayName(base.gameObject.name);
		this.StartProperties();
		this.GetEffectTotals();
		if (this.currentCharges == -999)
		{
			this.currentCharges = 0;
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.bonusDamage);
		if (runProperty != null && Item2.ShareItemTypes(runProperty.itemTypes, new List<Item2.ItemType> { Item2.ItemType.Weapon }))
		{
			bool flag = false;
			using (List<Item2.Modifier>.Enumerator enumerator = this.appliedModifiers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.name == "Run modifier")
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				Item2.Modifier modifier = new Item2.Modifier();
				modifier.length = Item2.Modifier.Length.permanent;
				Item2.Effect effect = new Item2.Effect();
				effect.type = Item2.Effect.Type.Damage;
				effect.value = (float)runProperty.value / 100f;
				effect.mathematicalType = Item2.Effect.MathematicalType.multiplicative;
				modifier.effects = new List<Item2.Effect>();
				modifier.effects.Add(effect);
				modifier.name = "Run modifier";
				this.appliedModifiers.Add(modifier);
			}
		}
		this.ConsiderReplacingSprite();
		MetaProgressSaveManager.main.FindNewItem(base.gameObject);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000456E8 File Offset: 0x000438E8
	public void ConsiderTakingAsLimitedItemGet()
	{
		if (this.itemMovement && this.itemMovement.returnsToOutOfInventoryPosition && !this.itemMovement.inGrid && !this.isOwned)
		{
			GameManager.main.ChangeItemsAllowedToTake(-1);
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00045724 File Offset: 0x00043924
	public static void ReconsiderAllForGhostRoutine()
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (item)
			{
				item.ReconsiderForGhostRoutine();
			}
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00045780 File Offset: 0x00043980
	public void ReconsiderForGhostRoutine()
	{
		if (!this.itemMovement || !this.itemMovement.inGrid)
		{
			return;
		}
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.FindItemsAndGridsinArea(list, list2, new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
		foreach (GridSquare gridSquare in list2)
		{
			if (this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.ghostly))
			{
				gridSquare.itemCountsAsEmpty = true;
			}
			else
			{
				gridSquare.itemCountsAsEmpty = false;
			}
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00045824 File Offset: 0x00043A24
	private void ConsiderReplacingSprite()
	{
		if (!this.spriteRenderer || this.spriteRenderer.sprite)
		{
			return;
		}
		Item2 item2ByName = DebugItemManager.main.GetItem2ByName(Item2.GetDisplayName(base.gameObject.name));
		if (!item2ByName)
		{
			return;
		}
		SpriteRenderer component = item2ByName.GetComponent<SpriteRenderer>();
		if (!component || !component.sprite)
		{
			return;
		}
		this.spriteRenderer.sprite = component.sprite;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x000458A4 File Offset: 0x00043AA4
	private void OnEnable()
	{
		if (!Item2.allItems.Contains(this))
		{
			Item2.allItems.Add(this);
		}
		for (int i = 3; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).name.ToLower().Contains("collider"))
			{
				base.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00045918 File Offset: 0x00043B18
	private void OnDisable()
	{
		Item2.allItems.Remove(this);
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00045926 File Offset: 0x00043B26
	private void OnDestroy()
	{
		Item2.allItems.Remove(this);
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00045934 File Offset: 0x00043B34
	public static void MarkAllAsOwned()
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (item && item.itemMovement)
			{
				item.cost = -999;
				item.isOwned = true;
				item.itemMovement.returnsToOutOfInventoryPosition = false;
			}
		}
		Tote main = Tote.main;
		if (main)
		{
			main.MarkAllAsOwned();
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x000459C8 File Offset: 0x00043BC8
	public static GameObject ChooseRandomFromList(List<GameObject> objects, List<GameObject> fallbacks)
	{
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(fallbacks, true);
		}
		return Item2.ChooseRandomFromList(objects, true);
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x000459E2 File Offset: 0x00043BE2
	public static Item2 ChooseRandomFromList(List<Item2> objects, List<Item2> fallbacks)
	{
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(fallbacks, true);
		}
		return Item2.ChooseRandomFromList(objects, true);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000459FC File Offset: 0x00043BFC
	public static GameObject CopyItem(GameObject item)
	{
		return Item2.CopyItem(item, Vector3.zero);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00045A0C File Offset: 0x00043C0C
	public static GameObject CopyItem(GameObject item, Vector3 pos)
	{
		if (!DebugItemManager.main)
		{
			return null;
		}
		foreach (GameObject gameObject in DebugItemManager.main.items)
		{
			if (Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName(item.name))
			{
				return Object.Instantiate<GameObject>(gameObject, pos, Quaternion.identity);
			}
		}
		return null;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00045A9C File Offset: 0x00043C9C
	public static List<Item2.CategoryClass> BuildCategoryClassesFromItemsInBag()
	{
		List<Item2.CategoryClass> list = new List<Item2.CategoryClass>();
		foreach (Item2 item in Item2.allItems)
		{
			if (item && item.itemMovement && item.itemMovement.inGrid)
			{
				foreach (Item2.ItemGrouping itemGrouping in item.itemGroupings)
				{
					Item2.CategoryClass categoryClass = Item2.GetCategoryClass(itemGrouping, list);
					categoryClass.luck += 1f;
					categoryClass.luck = Mathf.Clamp(categoryClass.luck, 0f, 20f);
					if (!list.Contains(categoryClass))
					{
						list.Add(categoryClass);
					}
				}
			}
		}
		foreach (Item2.CategoryClass categoryClass2 in list)
		{
			categoryClass2.luck = Mathf.Lerp(0f, 6f, categoryClass2.luck / 20f);
		}
		return list;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00045BFC File Offset: 0x00043DFC
	public static Item2.CategoryClass GetCategoryClass(Item2.ItemGrouping itemGrouping, List<Item2.CategoryClass> categoryClasses)
	{
		foreach (Item2.CategoryClass categoryClass in categoryClasses)
		{
			if (categoryClass.itemGrouping == itemGrouping)
			{
				return categoryClass;
			}
		}
		return new Item2.CategoryClass(0f, itemGrouping);
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00045C60 File Offset: 0x00043E60
	public static Item2 ChooseRandomFromListConsideringLuckGroup(List<Item2> objects, List<Item2.CategoryClass> categoryClasses)
	{
		objects = objects.Where((Item2 x) => GameManager.main.ItemValidToSpawn(x, false)).ToList<Item2>();
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(GameManager.main.itemsToSpawn, true);
		}
		List<Item2.ItemClass> list = new List<Item2.ItemClass>();
		float num = 0f;
		foreach (Item2 item in objects)
		{
			float num2 = 1f;
			foreach (Item2.CategoryClass categoryClass in categoryClasses)
			{
				if (item.itemGroupings.Contains(categoryClass.itemGrouping))
				{
					num2 += categoryClass.luck;
				}
			}
			list.Add(new Item2.ItemClass(new Vector2(num, num + num2), item));
			num += num2;
		}
		float num3 = 0f;
		foreach (Item2.ItemClass itemClass in list)
		{
			if (itemClass.range.y - itemClass.range.x > num3)
			{
				num3 = itemClass.range.y;
			}
		}
		float num4 = Random.Range(0f, num);
		foreach (Item2.ItemClass itemClass2 in list)
		{
			if (num4 < itemClass2.range.y)
			{
				return itemClass2.item2;
			}
		}
		return null;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00045E44 File Offset: 0x00044044
	public List<Item2.ItemType> GetComboTypes()
	{
		List<Item2.ItemType> list = new List<Item2.ItemType>();
		foreach (Item2.Modifier modifier in this.modifiers)
		{
			if (modifier.trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
			{
				foreach (Item2.ItemType itemType in modifier.typesToModify)
				{
					if (!list.Contains(itemType))
					{
						list.Add(itemType);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00045EF4 File Offset: 0x000440F4
	public static GameObject ChooseRandomFromList(List<GameObject> objects, bool checkForValid = true)
	{
		if (checkForValid)
		{
			objects = objects.Where((GameObject x) => GameManager.main.ItemValidToSpawn(x.GetComponent<Item2>(), false)).ToList<GameObject>();
		}
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(GameManager.main.itemsToSpawn, true).gameObject;
		}
		int num = Random.Range(0, objects.Count);
		return objects[num];
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00045F64 File Offset: 0x00044164
	public static Item2 ChooseRandomFromList(List<Item2> objects, bool checkForValid = true)
	{
		if (checkForValid)
		{
			objects = objects.Where((Item2 x) => GameManager.main.ItemValidToSpawn(x, false)).ToList<Item2>();
		}
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(GameManager.main.itemsToSpawn, true);
		}
		int num = Random.Range(0, objects.Count);
		return objects[num];
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00045FD0 File Offset: 0x000441D0
	public static GameObject ChooseRandomAndRemoveFromList(List<GameObject> objects)
	{
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(GameManager.main.itemsToSpawn, true).gameObject;
		}
		int num = Random.Range(0, objects.Count);
		if (!objects[num] || !objects[num].GetComponent<Item2>())
		{
			objects.RemoveAt(num);
			return null;
		}
		Component component = objects[num].GetComponent<Item2>();
		objects.RemoveAt(num);
		return component.gameObject;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0004604C File Offset: 0x0004424C
	public static Item2 ChooseRandomAndRemoveFromList(List<Item2> objects, bool checkForValid = true)
	{
		if (checkForValid)
		{
			objects = objects.Where((Item2 x) => GameManager.main.ItemValidToSpawn(x, false)).ToList<Item2>();
		}
		if (objects.Count <= 0)
		{
			return Item2.ChooseRandomFromList(GameManager.main.itemsToSpawn, true);
		}
		int num = Random.Range(0, objects.Count);
		if (!objects[num] || !objects[num].GetComponent<Item2>())
		{
			objects.RemoveAt(num);
			return Item2.ChooseRandomAndRemoveFromList(objects, true);
		}
		Item2 item = objects[num];
		objects.RemoveAt(num);
		return item;
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x000460F0 File Offset: 0x000442F0
	public static List<GameObject> GetItemsOfRarities(List<Item2.Rarity> rarities, List<GameObject> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in items)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (rarities.Contains(component.rarity))
			{
				list.Add(component.gameObject);
			}
		}
		return list;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00046160 File Offset: 0x00044360
	public static List<Item2> GetItemsOfRarities(List<Item2.Rarity> rarities, List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (rarities.Contains(item.rarity))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x000461C4 File Offset: 0x000443C4
	public static List<GameObject> GetItemOfRarity(Item2.Rarity rarity, List<GameObject> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in items)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component.rarity == rarity)
			{
				list.Add(component.gameObject);
			}
		}
		return list;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0004622C File Offset: 0x0004442C
	public static List<Item2> RemoveItemsOfTypes(List<Item2.ItemType> itemTypes, List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (!Item2.ShareItemTypes(itemTypes, item.itemType))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00046290 File Offset: 0x00044490
	public static bool HazardsExist()
	{
		return Item2.GetItemsOfTypes(new List<Item2.ItemType>
		{
			Item2.ItemType.Hazard,
			Item2.ItemType.Blessing
		}, Item2.GetAllItems()).Count > 0;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x000462BC File Offset: 0x000444BC
	public bool CanBeManuallyTransformed()
	{
		using (List<ContextMenuButton.ContextMenuButtonAndCost>.Enumerator enumerator = this.contextMenuOptions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == ContextMenuButton.Type.transform)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00046318 File Offset: 0x00044518
	public static List<Item2> GetItemsOfTypes(Item2.ItemType itemTypes, List<Item2> items)
	{
		return Item2.GetItemsOfTypes(new List<Item2.ItemType> { itemTypes }, items);
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0004632C File Offset: 0x0004452C
	public static List<Item2> GetItemsOfTypes(List<Item2.ItemType> itemTypes, List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (Item2.ShareItemTypes(itemTypes, item.itemType))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00046390 File Offset: 0x00044590
	public ItemMovement GetItemMovement()
	{
		if (this.itemMovement)
		{
			return this.itemMovement;
		}
		return base.GetComponent<ItemMovement>();
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000463AC File Offset: 0x000445AC
	public static List<GameObject> GetItemsOfTypes(List<Item2.ItemType> itemTypes, List<GameObject> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in items)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (Item2.ShareItemTypes(itemTypes, component.itemType))
			{
				list.Add(component.gameObject);
			}
		}
		return list;
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0004641C File Offset: 0x0004461C
	public static List<Item2> GetItemOfType(Item2.ItemType itemType, List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (item.itemType.Contains(itemType) || itemType == Item2.ItemType.Any)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00046484 File Offset: 0x00044684
	public static List<GameObject> GetItemOfType(Item2.ItemType itemType, List<GameObject> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in items)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component.itemType.Contains(itemType) || itemType == Item2.ItemType.Any)
			{
				list.Add(component.gameObject);
			}
		}
		return list;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000464F4 File Offset: 0x000446F4
	public static List<Item2> GetItemsFromGameObjects(GameObject[] gameObjects)
	{
		return Item2.GetItemsFromGameObjects(gameObjects.ToList<GameObject>());
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00046504 File Offset: 0x00044704
	public static List<GameObject> GetGameObjectsFromItems(List<Item2> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Item2 item in items)
		{
			list.Add(item.gameObject);
		}
		return list;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00046560 File Offset: 0x00044760
	public static List<Item2> GetItemsFromGameObjects(List<GameObject> gameObjects)
	{
		List<Item2> list = new List<Item2>();
		foreach (GameObject gameObject in gameObjects)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000465C4 File Offset: 0x000447C4
	public static void GetAllEffectTotals()
	{
		PetItem2.SetupAllPetEffects();
		foreach (Item2 item in Item2.allItems)
		{
			item.ApplyValueChangers();
		}
		foreach (Item2 item2 in Item2.allItems)
		{
			item2.GetEffectTotals();
			item2.SetColor();
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0004665C File Offset: 0x0004485C
	public static bool CheckIfSameTrigger(Item2.Trigger trigger1, Item2.Trigger trigger2)
	{
		if (trigger1.triggerOverrideKey.Length > 1 || trigger2.triggerOverrideKey.Length > 1)
		{
			return trigger1.triggerOverrideKey == trigger2.triggerOverrideKey;
		}
		bool flag = false;
		if ((trigger1.trigger == Item2.Trigger.ActionTrigger.onUse || trigger1.trigger == Item2.Trigger.ActionTrigger.useEarly || trigger1.trigger == Item2.Trigger.ActionTrigger.useLate) && (trigger2.trigger == Item2.Trigger.ActionTrigger.onUse || trigger2.trigger == Item2.Trigger.ActionTrigger.useEarly || trigger2.trigger == Item2.Trigger.ActionTrigger.useLate))
		{
			flag = true;
		}
		if ((trigger1.trigger == Item2.Trigger.ActionTrigger.onComboUse || trigger1.trigger == Item2.Trigger.ActionTrigger.onAlternateUse) && (trigger2.trigger == Item2.Trigger.ActionTrigger.onComboUse || trigger2.trigger == Item2.Trigger.ActionTrigger.onAlternateUse))
		{
			flag = true;
		}
		if ((trigger1.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingLate || trigger1.trigger == Item2.Trigger.ActionTrigger.onSummonCarving || trigger1.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingEarly) && (trigger2.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingLate || trigger2.trigger == Item2.Trigger.ActionTrigger.onSummonCarving || trigger2.trigger == Item2.Trigger.ActionTrigger.onSummonCarvingEarly))
		{
			flag = true;
		}
		if (!flag && trigger1.trigger != trigger2.trigger)
		{
			return false;
		}
		foreach (Item2.Area area in trigger1.areas)
		{
			bool flag2 = false;
			foreach (Item2.Area area2 in trigger2.areas)
			{
				if (area == area2)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				return false;
			}
		}
		foreach (Item2.ItemType itemType in trigger1.types)
		{
			bool flag3 = false;
			foreach (Item2.ItemType itemType2 in trigger2.types)
			{
				if (itemType == itemType2)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				return false;
			}
		}
		return trigger1.requiresActivation == trigger2.requiresActivation;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0004688C File Offset: 0x00044A8C
	public bool IsLocked()
	{
		using (List<Item2.ItemStatusEffect>.Enumerator enumerator = this.activeItemStatusEffects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == Item2.ItemStatusEffect.Type.locked)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000468E8 File Offset: 0x00044AE8
	private void ApplyValueChangers()
	{
		ValueChanger[] components = base.GetComponents<ValueChanger>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].ChangeValue();
		}
		ValueChangerEx[] components2 = base.GetComponents<ValueChangerEx>();
		for (int i = 0; i < components2.Length; i++)
		{
			components2[i].ChangeValue();
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0004692F File Offset: 0x00044B2F
	public static List<Item2> GetAllItemsCopies(List<GameObject> itemsToFindCopiesOf, List<Item2> items)
	{
		return Item2.GetAllItemsCopies(itemsToFindCopiesOf.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>()), items);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0004695C File Offset: 0x00044B5C
	public static List<Item2> GetAllItemsCopies(List<Item2> itemsToFindCopiesOf, List<Item2> itemsToSearch)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in itemsToFindCopiesOf)
		{
			List<Item2> allItemsCopies = Item2.GetAllItemsCopies(item, itemsToSearch);
			list.AddRange(allItemsCopies);
		}
		return list;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x000469B8 File Offset: 0x00044BB8
	public static List<Item2> GetAllItemsCopies(Item2 item, List<Item2> itemsToSearch)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item2 in itemsToSearch)
		{
			if (Item2.GetDisplayName(item2.name) == Item2.GetDisplayName(item.name) && item2.itemMovement.inGrid)
			{
				list.Add(item2);
			}
		}
		return list;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00046A38 File Offset: 0x00044C38
	public static Item2 FindSingleObjectOfType(Item2.ItemType type)
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (item.itemType.Contains(type))
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00046A98 File Offset: 0x00044C98
	public static List<Item2> FilterByTypes(List<Item2.ItemType> types, List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (Item2.ShareItemTypes(types, item.itemType))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00046AFC File Offset: 0x00044CFC
	private void GetModifierTotals()
	{
		this.modifierTotals = new List<Item2.ModifierTotal>();
		foreach (Item2.Modifier modifier in this.appliedModifierModifiers)
		{
			foreach (Item2.Effect effect in modifier.effects)
			{
				if (effect.mathematicalType != Item2.Effect.MathematicalType.multiplicative)
				{
					foreach (Item2.ModifierTotal modifierTotal in this.modifierTotals)
					{
						foreach (Item2.Effect effect2 in modifierTotal.modifier.effects)
						{
							if (effect2.type == effect.type)
							{
								effect2.value += effect.value;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00046C44 File Offset: 0x00044E44
	private bool SearchForExistingEffectTotal(Item2.Effect effect, Item2.Modifier modifier, Item2.Effect.MathematicalType mathematicalType)
	{
		bool flag = false;
		foreach (Item2.EffectTotal effectTotal in this.effectTotals)
		{
			if (this.ApplyToEffectTotal(effectTotal, effect, modifier, mathematicalType))
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00046CA4 File Offset: 0x00044EA4
	public bool ApplyToEffectTotal(Item2.EffectTotal effectTotal, Item2.Effect effect, Item2.Modifier modifier, Item2.Effect.MathematicalType mathematicalType)
	{
		if (effect.mathematicalType != mathematicalType)
		{
			return false;
		}
		if (effectTotal.effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			return false;
		}
		if (effectTotal.effect.type == effect.type && (effect.target == effectTotal.effect.target || effect.target == Item2.Effect.Target.unspecified))
		{
			if (effect.mathematicalType == Item2.Effect.MathematicalType.summative)
			{
				effectTotal.effect.value += effect.value;
			}
			else if (effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
			{
				effectTotal.multiplier += effect.value;
			}
			bool flag = false;
			foreach (Item2.EffectTotal.EffectPiece effectPiece in effectTotal.effectPieces)
			{
				if (effectPiece.name == modifier.name)
				{
					flag = true;
					effectPiece.numberOfTimes++;
					effectPiece.value += effect.value;
				}
			}
			if (!flag)
			{
				Item2.EffectTotal.EffectPiece effectPiece2 = new Item2.EffectTotal.EffectPiece();
				effectPiece2.name = modifier.name;
				effectPiece2.value = effect.value;
				effectPiece2.mathematicalType = mathematicalType;
				effectTotal.effectPieces.Add(effectPiece2);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00046DF0 File Offset: 0x00044FF0
	public static Item2.EffectTotal GetEffectTotalFromCombatEffect(Item2.CombattEffect combattEffect)
	{
		Item2.EffectTotal effectTotal = new Item2.EffectTotal();
		effectTotal.trigger = combattEffect.trigger;
		effectTotal.effect = combattEffect.effect.Clone();
		Item2.EffectTotal.EffectPiece effectPiece = new Item2.EffectTotal.EffectPiece();
		effectPiece.name = "Base";
		if (combattEffect.effect.originName != "")
		{
			effectPiece.name = combattEffect.effect.originName;
		}
		effectPiece.value = combattEffect.effect.value;
		effectPiece.mathematicalType = Item2.Effect.MathematicalType.summative;
		effectTotal.effectPieces = new List<Item2.EffectTotal.EffectPiece> { effectPiece };
		return effectTotal;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00046E84 File Offset: 0x00045084
	public void GetEffectTotals()
	{
		this.effectTotals = new List<Item2.EffectTotal>();
		foreach (Item2.CombattEffect combattEffect in this.combatEffects)
		{
			Item2.EffectTotal effectTotalFromCombatEffect = Item2.GetEffectTotalFromCombatEffect(combattEffect);
			this.effectTotals.Add(effectTotalFromCombatEffect);
		}
		foreach (Item2.Modifier modifier in this.appliedModifiers)
		{
			if (!this.itemType.Contains(Item2.ItemType.Treat) || !modifier.IsSelf())
			{
				foreach (Item2.Effect effect in modifier.effects)
				{
					if (effect.mathematicalType != Item2.Effect.MathematicalType.multiplicative)
					{
						bool flag = this.SearchForExistingEffectTotal(effect, modifier, Item2.Effect.MathematicalType.summative);
						if (!flag && effect.target == Item2.Effect.Target.unspecified)
						{
							Item2.Trigger trigger = new Item2.Trigger();
							if (effect.type == Item2.Effect.Type.Poison || effect.type == Item2.Effect.Type.Slow || effect.type == Item2.Effect.Type.Weak || effect.type == Item2.Effect.Type.Vampire || effect.type == Item2.Effect.Type.Burn || effect.type == Item2.Effect.Type.Damage || effect.type == Item2.Effect.Type.Charm)
							{
								foreach (Item2.CombattEffect combattEffect2 in this.combatEffects)
								{
									if (combattEffect2.effect.type == Item2.Effect.Type.Damage || combattEffect2.effect.type == Item2.Effect.Type.Charm)
									{
										trigger = combattEffect2.trigger;
										Item2.Effect.Target target = combattEffect2.effect.target;
										Item2.EffectTotal effectTotal = new Item2.EffectTotal();
										effectTotal.trigger = trigger;
										effectTotal.effect = new Item2.Effect();
										effectTotal.effect.type = effect.type;
										effectTotal.effect.value = effect.value;
										effectTotal.effect.target = target;
										if (!false)
										{
											Item2.EffectTotal.EffectPiece effectPiece = new Item2.EffectTotal.EffectPiece();
											effectPiece.name = "Base";
											effectPiece.value = 0f;
											effectPiece.mathematicalType = Item2.Effect.MathematicalType.summative;
											Item2.EffectTotal.EffectPiece effectPiece2 = new Item2.EffectTotal.EffectPiece();
											effectPiece2.name = modifier.name;
											effectPiece2.value = effect.value;
											effectPiece2.mathematicalType = effect.mathematicalType;
											effectTotal.effectPieces = new List<Item2.EffectTotal.EffectPiece>();
											effectTotal.effectPieces.Add(effectPiece);
											effectTotal.effectPieces.Add(effectPiece2);
											this.effectTotals.Add(effectTotal);
										}
										flag = true;
									}
								}
							}
						}
						if (!flag)
						{
							Item2.Trigger trigger2 = new Item2.Trigger();
							Item2.Effect.Target target2 = Item2.Effect.Target.player;
							if (effect.type == Item2.Effect.Type.Damage || effect.type == Item2.Effect.Type.Charm || effect.type == Item2.Effect.Type.Vampire)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onUse;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.enemy;
							}
							else if (effect.type == Item2.Effect.Type.Block && this.itemType.Contains(Item2.ItemType.Armor))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if ((effect.type == Item2.Effect.Type.Spikes || effect.type == Item2.Effect.Type.Burn || effect.type == Item2.Effect.Type.Poison || effect.type == Item2.Effect.Type.Slow) && this.itemType.Contains(Item2.ItemType.Shield) && this.itemType.Contains(Item2.ItemType.Carving))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onSummonCarving;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if ((effect.type == Item2.Effect.Type.Spikes || effect.type == Item2.Effect.Type.Burn || effect.type == Item2.Effect.Type.Poison || effect.type == Item2.Effect.Type.Slow) && this.itemType.Contains(Item2.ItemType.Armor))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.AP && this.itemType.Contains(Item2.ItemType.Clothing))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Mana)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.unspecified;
							}
							else if (effect.type == Item2.Effect.Type.Block)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onUse;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Poison || effect.type == Item2.Effect.Type.Slow || effect.type == Item2.Effect.Type.Weak || effect.type == Item2.Effect.Type.Burn)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onUse;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.enemy;
							}
							else if (effect.type == Item2.Effect.Type.Regen && this.itemType.Contains(Item2.ItemType.Ring))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Haste && this.itemType.Contains(Item2.ItemType.Ring))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Mana && this.itemType.Contains(Item2.ItemType.Ring))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Regen)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onUse;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.HP)
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onUse;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							else if (effect.type == Item2.Effect.Type.Haste && this.itemType.Contains(Item2.ItemType.Structure))
							{
								trigger2.trigger = Item2.Trigger.ActionTrigger.onTurnStart;
								trigger2.types = new List<Item2.ItemType> { Item2.ItemType.Any };
								trigger2.areas = new List<Item2.Area> { Item2.Area.self };
								target2 = Item2.Effect.Target.player;
							}
							Item2.EffectTotal effectTotal3;
							if (effect.type != Item2.Effect.Type.ModifierMultiplier)
							{
								Item2.EffectTotal effectTotal2 = new Item2.EffectTotal();
								effectTotal2.trigger = trigger2;
								effectTotal2.effect = effect.Clone();
								Item2.EffectTotal.EffectPiece effectPiece3 = new Item2.EffectTotal.EffectPiece();
								effectPiece3.name = "Base";
								effectPiece3.value = 0f;
								effectPiece3.mathematicalType = Item2.Effect.MathematicalType.summative;
								effectTotal2.effectPieces = new List<Item2.EffectTotal.EffectPiece> { effectPiece3 };
								effectTotal3 = effectTotal2;
							}
							else
							{
								Item2.EffectTotal effectTotal4 = new Item2.EffectTotal();
								effectTotal4.trigger = trigger2;
								effectTotal4.effect = effect.Clone();
								Item2.EffectTotal.EffectPiece effectPiece4 = new Item2.EffectTotal.EffectPiece();
								effectPiece4.name = "Base";
								effectPiece4.value = 1f;
								effectPiece4.mathematicalType = Item2.Effect.MathematicalType.summative;
								effectTotal4.effectPieces = new List<Item2.EffectTotal.EffectPiece> { effectPiece4 };
								effectTotal3 = effectTotal4;
							}
							if (effectTotal3.effect.target == Item2.Effect.Target.unspecified)
							{
								effectTotal3.effect.target = target2;
							}
							Item2.EffectTotal.EffectPiece effectPiece5 = new Item2.EffectTotal.EffectPiece();
							effectPiece5.name = modifier.name;
							effectPiece5.value = effect.value;
							effectPiece5.mathematicalType = Item2.Effect.MathematicalType.summative;
							effectTotal3.effectPieces.Add(effectPiece5);
							this.effectTotals.Add(effectTotal3);
						}
					}
				}
			}
		}
		foreach (Item2.EffectTotal effectTotal5 in this.effectTotals)
		{
			effectTotal5.multiplier = 1f;
			Item2.EffectTotal.EffectPiece effectPiece6 = new Item2.EffectTotal.EffectPiece();
			effectPiece6.name = "All multipliers";
			effectPiece6.value = 1f;
			effectPiece6.mathematicalType = Item2.Effect.MathematicalType.multiplicative;
			effectTotal5.effectPieces.Add(effectPiece6);
		}
		foreach (Item2.Modifier modifier2 in this.appliedModifiers)
		{
			foreach (Item2.Effect effect2 in modifier2.effects)
			{
				if (effect2.mathematicalType != Item2.Effect.MathematicalType.summative)
				{
					foreach (Item2.EffectTotal effectTotal6 in this.effectTotals)
					{
						if (effectTotal6.effect.type == effect2.type && (effect2.target == effectTotal6.effect.target || effect2.target == Item2.Effect.Target.unspecified) && !this.ApplyToEffectTotal(effectTotal6, effect2, modifier2, Item2.Effect.MathematicalType.multiplicative))
						{
							Item2.EffectTotal.EffectPiece effectPiece7 = new Item2.EffectTotal.EffectPiece();
							effectPiece7.name = modifier2.name;
							effectPiece7.value = effect2.value;
							effectPiece7.mathematicalType = Item2.Effect.MathematicalType.multiplicative;
							effectTotal6.effectPieces.Add(effectPiece7);
						}
					}
				}
			}
		}
		foreach (Item2.EffectTotal effectTotal7 in this.effectTotals)
		{
			this.CalculateEffectTotal(effectTotal7);
		}
		float num = 1f;
		foreach (Item2.EffectTotal effectTotal8 in this.effectTotals)
		{
			if (effectTotal8.effect.type == Item2.Effect.Type.ModifierMultiplier)
			{
				num = effectTotal8.effect.value;
			}
		}
		if (num != 1f)
		{
			foreach (Item2.EffectTotal effectTotal9 in this.effectTotals)
			{
				if (effectTotal9.effect.type != Item2.Effect.Type.ModifierMultiplier)
				{
					effectTotal9.effect.value -= effectTotal9.effectPieces[0].value;
					effectTotal9.effect.value = effectTotal9.effect.value * num;
					effectTotal9.effect.value += effectTotal9.effectPieces[0].value - 0.1f;
					for (int i = 1; i < effectTotal9.effectPieces.Count; i++)
					{
						effectTotal9.effectPieces[i].value = effectTotal9.effectPieces[i].value * num;
					}
				}
			}
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00047B18 File Offset: 0x00045D18
	public void CalculateEffectTotal(Item2.EffectTotal effectTotal1)
	{
		if (!this.player)
		{
			this.player = Player.main;
		}
		Status status = null;
		if (this.player)
		{
			status = this.player.stats;
			if (this.itemType.Contains(Item2.ItemType.Pet))
			{
				Status status2 = PetItem2.GetStatus(base.gameObject);
				if (status2)
				{
					status = status2;
				}
			}
		}
		effectTotal1.effect.value *= effectTotal1.multiplier;
		if (effectTotal1.effect.type == Item2.Effect.Type.Damage && effectTotal1.effect.target != Item2.Effect.Target.player)
		{
			int num = -999;
			if (status)
			{
				num = status.GetStatusEffectValue(StatusEffect.Type.rage);
			}
			if (num != -999)
			{
				effectTotal1.effect.value += (float)num;
				effectTotal1.effect.valueFromStatusEffects += (float)num;
				Item2.EffectTotal.EffectPiece effectPiece = new Item2.EffectTotal.EffectPiece();
				effectPiece.name = "se6";
				effectPiece.value = (float)num;
				effectPiece.mathematicalType = Item2.Effect.MathematicalType.summative;
				effectTotal1.effectPieces.Add(effectPiece);
			}
			int num2 = -999;
			if (status)
			{
				num2 = status.GetStatusEffectValue(StatusEffect.Type.weak);
			}
			if (num2 != -999)
			{
				effectTotal1.effect.value -= (float)num2;
				effectTotal1.effect.valueFromStatusEffects -= (float)num2;
				Item2.EffectTotal.EffectPiece effectPiece2 = new Item2.EffectTotal.EffectPiece();
				effectPiece2.name = "se7";
				effectPiece2.value = (float)(num2 * -1);
				effectPiece2.mathematicalType = Item2.Effect.MathematicalType.summative;
				effectTotal1.effectPieces.Add(effectPiece2);
			}
			if (this.gameManager && this.gameManager.targetedEnemy && this.gameManager.targetedEnemy.stats && effectTotal1.effect.target == Item2.Effect.Target.enemy)
			{
				int statusEffectValue = this.gameManager.targetedEnemy.stats.GetStatusEffectValue(StatusEffect.Type.freeze);
				if (statusEffectValue != -999)
				{
					effectTotal1.effect.value += (float)statusEffectValue;
					effectTotal1.effect.valueFromStatusEffects += (float)statusEffectValue;
					Item2.EffectTotal.EffectPiece effectPiece3 = new Item2.EffectTotal.EffectPiece();
					effectPiece3.name = "se9";
					effectPiece3.value = (float)statusEffectValue;
					effectPiece3.mathematicalType = Item2.Effect.MathematicalType.summative;
					effectTotal1.effectPieces.Add(effectPiece3);
				}
			}
		}
		if (effectTotal1.effect.type == Item2.Effect.Type.Block && effectTotal1.effect.target == Item2.Effect.Target.player && effectTotal1.effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			int num3 = -999;
			if (status)
			{
				num3 = status.GetStatusEffectValue(StatusEffect.Type.haste);
			}
			if (num3 != -999)
			{
				effectTotal1.effect.value += (float)num3;
				effectTotal1.effect.valueFromStatusEffects += (float)num3;
				Item2.EffectTotal.EffectPiece effectPiece4 = new Item2.EffectTotal.EffectPiece();
				effectPiece4.name = "se4";
				effectPiece4.value = (float)num3;
				effectPiece4.mathematicalType = Item2.Effect.MathematicalType.summative;
				effectTotal1.effectPieces.Add(effectPiece4);
			}
			int num4 = -999;
			if (status)
			{
				num4 = status.GetStatusEffectValue(StatusEffect.Type.slow);
			}
			if (num4 != -999)
			{
				effectTotal1.effect.value -= (float)num4;
				effectTotal1.effect.valueFromStatusEffects -= (float)num4;
				Item2.EffectTotal.EffectPiece effectPiece5 = new Item2.EffectTotal.EffectPiece();
				effectPiece5.name = "se5";
				effectPiece5.value = (float)(num4 * -1);
				effectPiece5.mathematicalType = Item2.Effect.MathematicalType.summative;
				effectTotal1.effectPieces.Add(effectPiece5);
			}
		}
		if (effectTotal1.effect.value < 0f && (effectTotal1.effect.type == Item2.Effect.Type.Damage || effectTotal1.effect.type == Item2.Effect.Type.Block) && effectTotal1.effect.mathematicalType == Item2.Effect.MathematicalType.summative)
		{
			effectTotal1.effect.value = 0f;
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00047EE8 File Offset: 0x000460E8
	public List<GameObject> FindGridSpaces()
	{
		List<GameObject> list = new List<GameObject>();
		if (!this.gridObject)
		{
			this.gridObject = base.GetComponent<GridObject>();
			if (!this.gridObject)
			{
				this.gridObject = base.gameObject.AddComponent<GridObject>();
			}
			if (!this.gridObject)
			{
				return list;
			}
		}
		bool flag = false;
		if (this.gridObject.gridPositions.Count == 0 && (this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition)))
		{
			this.gridObject.SnapToGrid();
			flag = true;
		}
		foreach (Vector2Int vector2Int in this.gridObject.gridPositions)
		{
			List<GridObject> list2 = GridObject.FilterByType(GridObject.GetItemsAtPosition(vector2Int), GridObject.Type.grid);
			while (list2.Count > 0)
			{
				GridObject gridObject = list2[0];
				list2.RemoveAt(0);
				GridSquare component = gridObject.GetComponent<GridSquare>();
				if (component)
				{
					list.Add(component.gameObject);
					break;
				}
			}
		}
		if (flag)
		{
			this.gridObject.ClearGridPositions();
		}
		return list;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00048010 File Offset: 0x00046210
	private void TestInLine(List<Item2> items, List<GridSquare> grids, Vector2 startPosition, Vector2 direction, Item2.AreaDistance areaDistance = Item2.AreaDistance.all, List<Item2.ItemType> itemTypes = null, List<Item2.ItemType> excludedTypes = null)
	{
		List<Item2> list = new List<Item2>();
		List<GridObject> itemsInLine = GridObject.GetItemsInLine(startPosition, Vector2Int.RoundToInt(direction), 15);
		List<GridObject> list2 = GridObject.FilterByType(itemsInLine, GridObject.Type.item);
		List<GridObject> list3 = GridObject.FilterByType(itemsInLine, GridObject.Type.grid);
		foreach (GridObject gridObject in list2)
		{
			Item2 component = gridObject.GetComponent<Item2>();
			if (component && !list.Contains(component))
			{
				list.Add(component);
			}
		}
		List<GridSquare> list4 = new List<GridSquare>();
		foreach (GridObject gridObject2 in list3)
		{
			GridSquare component2 = gridObject2.GetComponent<GridSquare>();
			if (component2 && !list4.Contains(component2))
			{
				list4.Add(component2);
			}
		}
		foreach (GridObject gridObject3 in list3)
		{
			GridSquare component3 = gridObject3.GetComponent<GridSquare>();
			if (component3 && !grids.Contains(component3))
			{
				grids.Add(component3);
			}
		}
		if (areaDistance == Item2.AreaDistance.closest)
		{
			float num = 999f;
			Item2 item = null;
			for (int i = 0; i < list.Count; i++)
			{
				Item2 item2 = list[i];
				if (!(item2 == this) && (itemTypes == null || Item2.ShareItemTypes(item2.itemType, itemTypes, excludedTypes) || item2.CheckForStatusEffect(Item2.ItemStatusEffect.Type.ductTape)))
				{
					GridObject gridObject4 = item2.gridObject;
					if (gridObject4)
					{
						foreach (Vector2 vector in gridObject4.GetWorldPositionsInLine(startPosition, Vector2Int.RoundToInt(direction), 15))
						{
							float num2 = Vector2.Distance(vector, startPosition);
							if (num2 < num)
							{
								num = num2;
								item = item2;
							}
						}
					}
				}
			}
			if (item && !items.Contains(item))
			{
				items.Add(item);
			}
			float num3 = 999f;
			GridSquare gridSquare = null;
			for (int j = 0; j < list4.Count; j++)
			{
				GridSquare gridSquare2 = list4[j];
				if (!gridSquare2.containsItem)
				{
					GridObject component4 = gridSquare2.GetComponent<GridObject>();
					if (component4)
					{
						foreach (Vector2 vector2 in component4.GetWorldPositionsInLine(startPosition, Vector2Int.RoundToInt(direction), 15))
						{
							float num4 = Vector2.Distance(vector2, startPosition);
							if (num4 < num3)
							{
								num3 = num4;
								gridSquare = gridSquare2;
							}
						}
					}
				}
			}
			if (gridSquare && !grids.Contains(gridSquare))
			{
				grids.Add(gridSquare);
				return;
			}
		}
		else if (areaDistance == Item2.AreaDistance.all)
		{
			items.AddRange(list);
			grids.AddRange(list4);
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00048314 File Offset: 0x00046514
	public static void TestAtVectorPublic(List<Item2> items, List<GridSquare> grids, Vector2 position)
	{
		List<Item2> list = new List<Item2>();
		List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(position);
		List<GridObject> list2 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.item);
		List<GridObject> list3 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.grid);
		foreach (GridObject gridObject in list2)
		{
			Item2 component = gridObject.GetComponent<Item2>();
			if (component && !list.Contains(component))
			{
				list.Add(component);
			}
		}
		items.AddRange(list);
		foreach (GridObject gridObject2 in list3)
		{
			GridSquare component2 = gridObject2.GetComponent<GridSquare>();
			if (component2 && !grids.Contains(component2))
			{
				grids.Add(component2);
			}
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000483F8 File Offset: 0x000465F8
	private void TestAtVector(List<Item2> items, List<GridSquare> grids, Vector2 position)
	{
		List<Item2> list = new List<Item2>();
		List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(position);
		List<GridObject> list2 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.item);
		List<GridObject> list3 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.grid);
		foreach (GridObject gridObject in list2)
		{
			Item2 component = gridObject.GetComponent<Item2>();
			if (component && !list.Contains(component))
			{
				list.Add(component);
			}
		}
		items.AddRange(list);
		foreach (GridObject gridObject2 in list3)
		{
			GridSquare component2 = gridObject2.GetComponent<GridSquare>();
			if (component2 && !grids.Contains(component2))
			{
				grids.Add(component2);
			}
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x000484DC File Offset: 0x000466DC
	public static Item2.Area TranslateArea(Item2.Area Area, Transform transform)
	{
		float num = transform.rotation.eulerAngles.z;
		num = Mathf.Round(num / 90f) * 90f;
		if (Area == Item2.Area.rowThenColumn)
		{
			if (Mathf.Abs(num - 90f) < 1f || Mathf.Abs(num - 90f) < 1f || Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.column;
			}
			else
			{
				Area = Item2.Area.row;
			}
		}
		else if (Area == Item2.Area.columnThenRow)
		{
			if (Mathf.Abs(num - 90f) < 1f || Mathf.Abs(num - 90f) < 1f || Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.row;
			}
			else
			{
				Area = Item2.Area.column;
			}
		}
		else if (Area == Item2.Area.rightRotational)
		{
			if (Mathf.Abs(num) < 1f)
			{
				Area = Item2.Area.right;
			}
			else if (Mathf.Abs(num - 90f) < 1f)
			{
				Area = Item2.Area.top;
			}
			else if (Mathf.Abs(num - 180f) < 1f)
			{
				Area = Item2.Area.left;
			}
			else if (Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.bottom;
			}
		}
		else if (Area == Item2.Area.leftRotational)
		{
			if (Mathf.Abs(num) < 1f)
			{
				Area = Item2.Area.left;
			}
			else if (Mathf.Abs(num - 90f) < 1f)
			{
				Area = Item2.Area.bottom;
			}
			else if (Mathf.Abs(num - 180f) < 1f)
			{
				Area = Item2.Area.right;
			}
			else if (Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.top;
			}
		}
		else if (Area == Item2.Area.topRotational)
		{
			if (Mathf.Abs(num) < 1f)
			{
				Area = Item2.Area.top;
			}
			else if (Mathf.Abs(num - 90f) < 1f)
			{
				Area = Item2.Area.left;
			}
			else if (Mathf.Abs(num - 180f) < 1f)
			{
				Area = Item2.Area.bottom;
			}
			else if (Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.right;
			}
		}
		else if (Area == Item2.Area.bottomRotation)
		{
			if (Mathf.Abs(num) < 1f)
			{
				Area = Item2.Area.bottom;
			}
			else if (Mathf.Abs(num - 90f) < 1f)
			{
				Area = Item2.Area.right;
			}
			else if (Mathf.Abs(num - 180f) < 1f)
			{
				Area = Item2.Area.top;
			}
			else if (Mathf.Abs(num - 270f) < 1f || Mathf.Abs(num + 90f) < 1f)
			{
				Area = Item2.Area.left;
			}
		}
		return Area;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x000487C0 File Offset: 0x000469C0
	public bool IsAtTheEdgeOfGrid()
	{
		List<GameObject> list = this.FindGridSpaces();
		Vector2[] array = new Vector2[]
		{
			Vector2.up,
			Vector2.down,
			Vector2.left,
			Vector2.right
		};
		foreach (GameObject gameObject in list)
		{
			foreach (Vector2 vector in array)
			{
				List<GridSquare> list2 = new List<GridSquare>();
				this.TestAtVector(new List<Item2>(), list2, gameObject.transform.position + vector);
				if (list2.Count == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000488A8 File Offset: 0x00046AA8
	public void FindItemsAndGridsinArea(List<Item2> items, List<GridSquare> grids, List<Item2.Area> Areas, Item2.AreaDistance areaDistance = Item2.AreaDistance.all, List<Item2> connectingItems = null, List<Item2.ItemType> validTypes = null, List<Item2.ItemType> excludedTypes = null, bool allowGhostly = true, bool allowSearchInPouch = false, bool allowConnectingItems = true)
	{
		if (!this.itemMovement || (this.itemMovement.inPouch && !Areas.Contains(Item2.Area.myPlaySpace)))
		{
			return;
		}
		if (validTypes == null)
		{
			validTypes = new List<Item2.ItemType> { Item2.ItemType.Any };
		}
		if (connectingItems == null)
		{
			connectingItems = new List<Item2>();
		}
		List<Item2.Area> list = new List<Item2.Area>(Areas);
		if (this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems) && Areas.Contains(Item2.Area.adjacent))
		{
			list.Add(Item2.Area.myPlaySpace);
		}
		List<GameObject> list2 = this.FindGridSpaces();
		PocketManager main = PocketManager.main;
		foreach (Item2.Area area in list)
		{
			area = Item2.TranslateArea(area, base.transform);
			foreach (GameObject gameObject in list2)
			{
				Vector3 position = gameObject.transform.position;
				if (area == Item2.Area.adjacent)
				{
					this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y);
					this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y);
					this.TestAtVector(items, grids, position + Vector3.left * base.transform.localScale.x);
					this.TestAtVector(items, grids, position + Vector3.right * base.transform.localScale.x);
				}
				else if (area == Item2.Area.comboTarget)
				{
					if (this.comboTarget != null)
					{
						items.Add(this.comboTarget);
					}
				}
				else if (area == Item2.Area.myPlaySpace)
				{
					List<Item2> list3 = new List<Item2>();
					this.TestAtVector(list3, grids, position);
					if (areaDistance == Item2.AreaDistance.zTop)
					{
						float num = 999f;
						Item2 item = null;
						foreach (Item2 item2 in list3)
						{
							if (!(item2 == this) && item2.transform.position.z < num)
							{
								num = item2.transform.position.z;
								item = item2;
							}
						}
						if (item)
						{
							list3.Clear();
							list3.Add(item);
						}
					}
					items.AddRange(list3);
				}
				else
				{
					if (area == Item2.Area.ItemEffectArea)
					{
						using (IEnumerator enumerator4 = base.transform.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								object obj = enumerator4.Current;
								Transform transform = (Transform)obj;
								if (transform.CompareTag("ItemEffectArea"))
								{
									this.TestAtVector(items, grids, transform.position);
								}
							}
							continue;
						}
					}
					if (area == Item2.Area.oneSpaceOver)
					{
						this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y + Vector3.up * base.transform.localScale.y);
						this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y + Vector3.down * base.transform.localScale.y);
						this.TestAtVector(items, grids, position + Vector3.right * base.transform.localScale.x + Vector3.right * base.transform.localScale.x);
						this.TestAtVector(items, grids, position + Vector3.left * base.transform.localScale.x + Vector3.left * base.transform.localScale.x);
					}
					else if (area == Item2.Area.diagonal)
					{
						this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y + Vector3.left * base.transform.localScale.x);
						this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y + Vector3.left * base.transform.localScale.x);
						this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y + Vector3.right * base.transform.localScale.x);
						this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y + Vector3.right * base.transform.localScale.x);
					}
					else if (area == Item2.Area.right)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.right * base.transform.localScale.x);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(1f, 0f), areaDistance, validTypes, null);
						}
					}
					else if (area == Item2.Area.left)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.left * base.transform.localScale.x);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(-1f, 0f), areaDistance, validTypes, null);
						}
					}
					else if (area == Item2.Area.row)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.right * base.transform.localScale.x);
							this.TestAtVector(items, grids, position + Vector3.left * base.transform.localScale.x);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(-1f, 0f), areaDistance, validTypes, null);
							this.TestInLine(items, grids, position, new Vector2(1f, 0f), areaDistance, validTypes, null);
						}
					}
					else if (area == Item2.Area.top)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(0f, 1f), areaDistance, validTypes, null);
						}
					}
					else if (area == Item2.Area.bottom)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(0f, -1f), areaDistance, validTypes, null);
						}
					}
					else if (area == Item2.Area.column)
					{
						if (areaDistance == Item2.AreaDistance.adjacent)
						{
							this.TestAtVector(items, grids, position + Vector3.up * base.transform.localScale.y);
							this.TestAtVector(items, grids, position + Vector3.down * base.transform.localScale.y);
						}
						else
						{
							this.TestInLine(items, grids, position, new Vector2(0f, -1f), areaDistance, validTypes, null);
							this.TestInLine(items, grids, position, new Vector2(0f, 1f), areaDistance, validTypes, null);
						}
					}
					else
					{
						if (area == Item2.Area.connected)
						{
							using (List<PathFinding.TransformAndPath>.Enumerator enumerator5 = this.gameFlowManager.FindAllItemsOfTypeConnected(this).GetEnumerator())
							{
								while (enumerator5.MoveNext())
								{
									PathFinding.TransformAndPath transformAndPath = enumerator5.Current;
									Item2 componentInParent = transformAndPath.t.GetComponentInParent<Item2>();
									if (componentInParent)
									{
										items.Add(componentInParent);
									}
								}
								continue;
							}
						}
						if (area == Item2.Area.connectedViaType)
						{
							Item2.ItemType itemType = Item2.ItemType.Sap;
							if (this.itemType.Count > 0)
							{
								itemType = this.itemType[0];
							}
							using (List<PathFinding.TransformAndPath>.Enumerator enumerator5 = ConnectionManager.FindAllItemsOfTypeConnected(this, new List<Item2.ItemType> { itemType }).GetEnumerator())
							{
								while (enumerator5.MoveNext())
								{
									PathFinding.TransformAndPath transformAndPath2 = enumerator5.Current;
									Item2 componentInParent2 = transformAndPath2.t.GetComponentInParent<Item2>();
									if (componentInParent2)
									{
										items.Add(componentInParent2);
									}
								}
								continue;
							}
						}
						if (area == Item2.Area.diagonalLine)
						{
							for (int i = 1; i <= 4; i++)
							{
								this.TestAtVector(items, grids, position + (Vector3.up * base.transform.localScale.y + Vector3.left * base.transform.localScale.x) * (float)i);
								this.TestAtVector(items, grids, position + (Vector3.down * base.transform.localScale.y + Vector3.left * base.transform.localScale.x) * (float)i);
								this.TestAtVector(items, grids, position + (Vector3.up * base.transform.localScale.y + Vector3.right * base.transform.localScale.x) * (float)i);
								this.TestAtVector(items, grids, position + (Vector3.down * base.transform.localScale.y + Vector3.right * base.transform.localScale.x) * (float)i);
							}
						}
						else
						{
							if (area == Item2.Area.inThisPocket)
							{
								GridSquare component = gameObject.GetComponent<GridSquare>();
								if (!component)
								{
									continue;
								}
								PocketManager.Pocket pocket = main.FindPocketForThisGrid(component);
								if (pocket == null)
								{
									continue;
								}
								using (List<Vector2>.Enumerator enumerator6 = main.GetVector2s(pocket).GetEnumerator())
								{
									while (enumerator6.MoveNext())
									{
										Vector2 vector = enumerator6.Current;
										this.TestAtVector(items, grids, vector);
									}
									continue;
								}
							}
							if (area == Item2.Area.inAnotherPocket)
							{
								List<Item2> list4 = new List<Item2>();
								List<GridSquare> list5 = new List<GridSquare>();
								GridSquare component2 = gameObject.GetComponent<GridSquare>();
								if (component2)
								{
									PocketManager.Pocket pocket2 = main.FindPocketForThisGrid(component2);
									if (pocket2 != null)
									{
										foreach (Vector2 vector2 in main.GetVector2s(pocket2))
										{
											this.TestAtVector(list4, list5, vector2);
										}
									}
								}
								foreach (Item2 item3 in Item2.allItems)
								{
									if (item3 != this && !items.Contains(item3) && item3.itemMovement && item3.itemMovement.inGrid && !list4.Contains(item3))
									{
										items.Add(item3);
									}
								}
								foreach (GridSquare gridSquare in GridSquare.allGrids)
								{
									if (!grids.Contains(gridSquare) && !list5.Contains(gridSquare))
									{
										grids.Add(gridSquare);
									}
								}
							}
						}
					}
				}
			}
			if (area == Item2.Area.board)
			{
				foreach (Item2 item4 in Item2.allItems)
				{
					if (item4 != this && !items.Contains(item4) && item4.GetComponent<ItemMovement>().inGrid)
					{
						items.Add(item4);
					}
				}
				using (List<GridSquare>.Enumerator enumerator7 = GridSquare.allGrids.GetEnumerator())
				{
					while (enumerator7.MoveNext())
					{
						GridSquare gridSquare2 = enumerator7.Current;
						if (!grids.Contains(gridSquare2))
						{
							grids.Add(gridSquare2);
						}
					}
					continue;
				}
			}
			if (area == Item2.Area.AnythingEvenOutOfGrid)
			{
				items.AddRange(Item2.allItems.Where((Item2 x) => !x.destroyed));
				grids.AddRange(GridSquare.allGrids);
			}
			else if (area == Item2.Area.self)
			{
				if (!items.Contains(this))
				{
					items.Add(this);
				}
			}
			else if (area == Item2.Area.toteHand)
			{
				Carving[] array = Object.FindObjectsOfType<Carving>();
				for (int j = 0; j < array.Length; j++)
				{
					Item2 component3 = array[j].GetComponent<Item2>();
					if (component3 && !component3.itemMovement.isPlayedCard)
					{
						items.Add(component3);
					}
				}
			}
		}
		for (int k = 0; k < items.Count; k++)
		{
			Item2 item5 = items[k];
			for (int l = k + 1; l < items.Count; l++)
			{
				if (items[l] == item5)
				{
					items.RemoveAt(l);
					l--;
				}
			}
		}
		for (int m = 0; m < items.Count; m++)
		{
			Item2 item6 = items[m];
			if (allowGhostly && item6.CheckForStatusEffect(Item2.ItemStatusEffect.Type.ghostly) && !list.Contains(Item2.Area.comboTarget) && !Areas.Contains(Item2.Area.self) && !(item6 == this))
			{
				items.RemoveAt(m);
				m--;
			}
		}
		for (int n = 0; n < grids.Count; n++)
		{
			GridSquare gridSquare3 = grids[n];
			for (int num2 = n + 1; num2 < grids.Count; num2++)
			{
				if (grids[num2] == gridSquare3)
				{
					grids.RemoveAt(num2);
					num2--;
				}
			}
		}
		if (!Areas.Contains(Item2.Area.AnythingEvenOutOfGrid))
		{
			for (int num3 = 0; num3 < items.Count; num3++)
			{
				Item2 item7 = items[num3];
				ItemMovement component4 = item7.GetComponent<ItemMovement>();
				if (((!component4.inGrid || (component4.inPouch && !this.itemMovement.inPouch)) && item7 != this) || (item7.parentInventoryGrid != null && item7.parentInventoryGrid != this.parentInventoryGrid && item7.parentInventoryGrid != this.lastParentInventoryGrid))
				{
					items.RemoveAt(num3);
					num3--;
				}
			}
		}
		for (int num4 = 0; num4 < grids.Count; num4++)
		{
			GridSquare gridSquare4 = grids[num4];
			if (gridSquare4.transform.parent != this.parentInventoryGrid && gridSquare4.transform.parent != this.lastParentInventoryGrid)
			{
				grids.RemoveAt(num4);
				num4--;
			}
		}
		if (!this.destroyed && !Areas.Contains(Item2.Area.myPlaySpace))
		{
			for (int num5 = 0; num5 < list2.Count; num5++)
			{
				GridSquare component5 = list2[num5].GetComponent<GridSquare>();
				if (grids.Contains(component5))
				{
					grids.Remove(component5);
				}
			}
		}
		Item2 item8 = null;
		while (allowConnectingItems)
		{
			item8 = null;
			foreach (Item2 item9 in items)
			{
				if (item9.CheckForStatusEffect(Item2.ItemStatusEffect.Type.ductTape) && item9 != this && !connectingItems.Contains(item9))
				{
					item8 = item9;
					break;
				}
			}
			if (item8)
			{
				connectingItems.Add(item8);
				item8.FindItemsAndGridsinArea(items, grids, new List<Item2.Area> { Item2.Area.adjacent }, areaDistance = Item2.AreaDistance.adjacent, connectingItems, null, null, true, false, true);
				if (!items.Contains(item8))
				{
					items.Add(item8);
				}
			}
			if (!item8)
			{
				break;
			}
		}
		if (!Areas.Contains(Item2.Area.self) && !list.Contains(Item2.Area.comboTarget) && items.Contains(this))
		{
			items.Remove(this);
		}
		items = items.Distinct<Item2>().ToList<Item2>();
		items = Item2.SortItemsByPosition(items);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00049AF4 File Offset: 0x00047CF4
	public static bool ShareItemTypes(List<Item2.ItemType> itemsA, List<Item2.ItemType> itemsB)
	{
		return Item2.ShareItemTypes(itemsA, itemsB, null);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00049B00 File Offset: 0x00047D00
	public static bool ShareItemTypes(List<Item2.ItemType> itemsA, List<Item2.ItemType> itemsB, List<Item2.ItemType> excludedTypes)
	{
		if (itemsA == null || itemsB == null || itemsA.Count == 0 || itemsB.Count == 0)
		{
			return false;
		}
		if (excludedTypes != null && excludedTypes.Count > 0)
		{
			foreach (Item2.ItemType itemType in itemsA)
			{
				if (excludedTypes.Contains(itemType))
				{
					return false;
				}
			}
		}
		if (itemsA.Contains(Item2.ItemType.Any) || itemsB.Contains(Item2.ItemType.Any))
		{
			return true;
		}
		foreach (Item2.ItemType itemType2 in itemsA)
		{
			if (itemsB.Contains(itemType2))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00049BD4 File Offset: 0x00047DD4
	public void TemporaryRenableAfterDestroy(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems)
	{
		foreach (Item2 item in activeItems)
		{
			if (item.destroyed && this.gameFlowManager.itemsDestroyedInThisEffectSequence.Contains(item))
			{
				BoxCollider2D[] componentsInChildren = item.GetComponentsInChildren<BoxCollider2D>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = true;
				}
				if (item.itemMovement)
				{
					item.itemMovement.inGrid = true;
				}
			}
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00049C70 File Offset: 0x00047E70
	public void EndTemporaryRenable(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems)
	{
		foreach (Item2 item in activeItems)
		{
			if (item.destroyed && this.gameFlowManager.itemsDestroyedInThisEffectSequence.Contains(item))
			{
				BoxCollider2D[] componentsInChildren = item.GetComponentsInChildren<BoxCollider2D>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
				item.GetComponent<ItemMovement>().inGrid = false;
			}
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00049CFC File Offset: 0x00047EFC
	private bool ContainsTrigger(List<Item2.Trigger.ActionTrigger> triggers, Item2.Trigger trigger, List<Item2> activeItems)
	{
		Item2.Trigger.ActionTrigger trigger2 = trigger.trigger;
		return (!CR8Manager.instance || !CR8Manager.instance.isTesting || trigger.canBeUsedDuringTest) && this.ContainsTrigger(triggers, trigger2, activeItems);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00049D3C File Offset: 0x00047F3C
	private bool ContainsTrigger(List<Item2.Trigger.ActionTrigger> triggers, Item2.Trigger.ActionTrigger actionTrigger, List<Item2> activeItems)
	{
		if (actionTrigger == Item2.Trigger.ActionTrigger.whenScripted)
		{
			using (List<ScriptedTrigger>.Enumerator enumerator = this.scriptedTriggers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CheckTrigger2(triggers))
					{
						return true;
					}
				}
			}
		}
		return triggers.Contains(actionTrigger);
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00049DA8 File Offset: 0x00047FA8
	public void ConsiderMovementEffects(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveMovementEffect> activeMovementEffects)
	{
		foreach (Item2.MovementEffect movementEffect in this.movementEffects)
		{
			if ((!movementEffect.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, movementEffect.trigger, activeItems) && CR8Manager.ValidEntrance(this, movementEffect.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, movementEffect.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((movementEffect.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, movementEffect.trigger.types, movementEffect.trigger.excludedTypes) || movementEffect.trigger.types.Contains(Item2.ItemType.Any))) || (movementEffect.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeMovementEffects.Add(new Item2.ActiveMovementEffect(this, movementEffect));
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00049F40 File Offset: 0x00048140
	public void ConsiderCreateEffects(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveCreateEffect> activeCreateEffects)
	{
		foreach (Item2.CreateEffect createEffect in this.createEffects)
		{
			if ((!createEffect.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, createEffect.trigger, activeItems) && CR8Manager.ValidEntrance(this, createEffect.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, createEffect.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((createEffect.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, createEffect.trigger.types, createEffect.trigger.excludedTypes) || createEffect.trigger.types.Contains(Item2.ItemType.Any))) || (createEffect.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeCreateEffects.Add(new Item2.ActiveCreateEffect(this, createEffect));
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0004A0D8 File Offset: 0x000482D8
	public void ConsiderEffects(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveEffect> activeEffects)
	{
		foreach (Item2.EffectTotal effectTotal in this.effectTotals)
		{
			if ((!effectTotal.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, effectTotal.trigger, activeItems) && CR8Manager.ValidEntrance(this, effectTotal.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, effectTotal.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((effectTotal.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, effectTotal.trigger.types, effectTotal.trigger.excludedTypes) || effectTotal.trigger.types.Contains(Item2.ItemType.Any))) || (effectTotal.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeEffects.Add(new Item2.ActiveEffect(this, effectTotal));
						item.CheckForScriptedTriggerRecursion(triggers);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0004A278 File Offset: 0x00048478
	private void CheckForScriptedTriggerRecursion(List<Item2.Trigger.ActionTrigger> triggers)
	{
		if (!this.checkedForScriptedTriggers)
		{
			this.scriptedTriggers = base.GetComponentsInChildren<ScriptedTrigger>().ToList<ScriptedTrigger>();
			this.checkedForScriptedTriggers = true;
		}
		foreach (ScriptedTrigger scriptedTrigger in this.scriptedTriggers)
		{
			scriptedTrigger.ConsiderRecursion(triggers);
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0004A2EC File Offset: 0x000484EC
	public void ConsiderConstantSelfModifiers(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveModifiers> activeEffects)
	{
		if (!activeItems.Contains(this))
		{
			return;
		}
		if (!this.ContainsTrigger(triggers, Item2.Trigger.ActionTrigger.constant, activeItems) && !this.ContainsTrigger(triggers, Item2.Trigger.ActionTrigger.constantEarly, activeItems) && !this.ContainsTrigger(triggers, Item2.Trigger.ActionTrigger.constantExtraEarly, activeItems))
		{
			return;
		}
		foreach (Item2.Modifier modifier in this.modifiers)
		{
			if (modifier.trigger.types.Contains(Item2.ItemType.Sunshine))
			{
				foreach (Item2.Modifier modifier2 in this.appliedModifiers)
				{
					foreach (Item2.Effect effect in modifier2.effects)
					{
						if (effect.type == Item2.Effect.Type.ProvideSunshine)
						{
							int num = 0;
							while ((float)num < effect.value)
							{
								activeEffects.Add(new Item2.ActiveModifiers(this, modifier));
								num++;
							}
						}
					}
				}
			}
			if ((!modifier.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && (modifier.trigger.trigger == Item2.Trigger.ActionTrigger.constant || modifier.trigger.trigger == Item2.Trigger.ActionTrigger.constantEarly || modifier.trigger.trigger == Item2.Trigger.ActionTrigger.constantExtraEarly) && triggers.Contains(modifier.trigger.trigger) && modifier.trigger.types.Contains(Item2.ItemType.Any) && modifier.trigger.areas.Contains(Item2.Area.self) && modifier.areasToModify.Contains(Item2.Area.self))
			{
				activeEffects.Add(new Item2.ActiveModifiers(this, modifier));
				this.CheckForScriptedTriggerRecursion(triggers);
			}
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0004A4EC File Offset: 0x000486EC
	public void ConsiderModifiers(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveModifiers> activeEffects)
	{
		foreach (Item2.Modifier modifier in this.modifiers)
		{
			bool flag = false;
			using (List<Item2.ActiveModifiers>.Enumerator enumerator2 = activeEffects.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.effect == modifier)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag && (!modifier.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, modifier.trigger, activeItems) && CR8Manager.ValidEntrance(this, modifier.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, modifier.trigger.areas, modifier.trigger.areaDistance, null, modifier.trigger.types, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((modifier.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, modifier.trigger.types, modifier.trigger.excludedTypes) || modifier.trigger.types.Contains(Item2.ItemType.Any))) || (modifier.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeEffects.Add(new Item2.ActiveModifiers(this, modifier));
						item.CheckForScriptedTriggerRecursion(triggers);
						if (!modifier.stackable)
						{
							break;
						}
					}
				}
				List<float> list3 = new List<float>();
				if (modifier.trigger.types.Contains(Item2.ItemType.Grid))
				{
					using (List<GridSquare>.Enumerator enumerator4 = list2.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							GridSquare gridSquare = enumerator4.Current;
							if (modifier.trigger.areas.Contains(Item2.Area.top) || modifier.trigger.areas.Contains(Item2.Area.bottom) || modifier.trigger.areas.Contains(Item2.Area.column))
							{
								if (list3.Contains(gridSquare.transform.position.y))
								{
									continue;
								}
								list3.Add(gridSquare.transform.position.y);
							}
							if (modifier.trigger.areas.Contains(Item2.Area.left) || modifier.trigger.areas.Contains(Item2.Area.right) || modifier.trigger.areas.Contains(Item2.Area.row))
							{
								if (list3.Contains(gridSquare.transform.position.x))
								{
									continue;
								}
								list3.Add(gridSquare.transform.position.x);
							}
							activeEffects.Add(new Item2.ActiveModifiers(this, modifier));
							this.CheckForScriptedTriggerRecursion(triggers);
							if (!modifier.stackable)
							{
								break;
							}
						}
						continue;
					}
				}
				if (modifier.trigger.types.Contains(Item2.ItemType.GridEmpty))
				{
					using (List<GridSquare>.Enumerator enumerator4 = list2.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							if (enumerator4.Current.CountAsEmpty())
							{
								activeEffects.Add(new Item2.ActiveModifiers(this, modifier));
								this.CheckForScriptedTriggerRecursion(triggers);
								if (!modifier.stackable)
								{
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0004A8DC File Offset: 0x00048ADC
	public static List<GridSquare> GetGridSquares(List<Item2.ItemType> itemType, List<GridSquare> gridSquares)
	{
		if (itemType.Contains(Item2.ItemType.GridEmpty))
		{
			List<GridSquare> list = new List<GridSquare>();
			foreach (GridSquare gridSquare in gridSquares)
			{
				if (gridSquare.CountAsEmpty())
				{
					list.Add(gridSquare);
				}
			}
			return list;
		}
		return gridSquares;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0004A944 File Offset: 0x00048B44
	public void ConsiderSpecialEffects(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveSpecialEffects> activeEffects)
	{
		foreach (SpecialItem specialItem in base.GetComponentsInChildren<SpecialItem>())
		{
			if ((!specialItem.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, specialItem.trigger, activeItems) && CR8Manager.ValidEntrance(this, specialItem.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, specialItem.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((specialItem.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, specialItem.trigger.types, specialItem.trigger.excludedTypes) || specialItem.trigger.types.Contains(Item2.ItemType.Any))) || (specialItem.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeEffects.Add(new Item2.ActiveSpecialEffects(this, specialItem));
						item.CheckForScriptedTriggerRecursion(triggers);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0004AAAC File Offset: 0x00048CAC
	public void ConsiderEnergyEffects(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveEnergyEmitterEffects> activeEnergyEmitterEffects)
	{
		if (this.checkedForEnergyEmitterAndItDoesntExist)
		{
			return;
		}
		if (!base.GetComponent<EnergyEmitter>())
		{
			this.checkedForEnergyEmitterAndItDoesntExist = true;
			return;
		}
		foreach (Item2.EnergyEffect energyEffect in this.energyEffects)
		{
			if ((!energyEffect.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, energyEffect.trigger, activeItems) && CR8Manager.ValidEntrance(this, energyEffect.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, energyEffect.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((energyEffect.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, energyEffect.trigger.types, energyEffect.trigger.excludedTypes) || energyEffect.trigger.types.Contains(Item2.ItemType.Any))) || (energyEffect.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeEnergyEmitterEffects.Add(new Item2.ActiveEnergyEmitterEffects(this, energyEffect));
						item.CheckForScriptedTriggerRecursion(triggers);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0004AC6C File Offset: 0x00048E6C
	public void ConsiderValueChangers(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveValueChangers> activeEffects)
	{
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0004AC70 File Offset: 0x00048E70
	public void ConsiderAddModifiers(List<Item2.Trigger.ActionTrigger> triggers, List<Item2> activeItems, List<Item2.ActiveAddModifiers> activeEffects)
	{
		foreach (Item2.AddModifier addModifier in this.addModifiers)
		{
			if ((!addModifier.trigger.requiresActivation || this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated)) && this.ContainsTrigger(triggers, addModifier.trigger, activeItems) && CR8Manager.ValidEntrance(this, addModifier.trigger))
			{
				List<Item2> list = new List<Item2>();
				List<GridSquare> list2 = new List<GridSquare>();
				this.TemporaryRenableAfterDestroy(triggers, activeItems);
				this.FindItemsAndGridsinArea(list, list2, addModifier.trigger.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
				this.EndTemporaryRenable(triggers, activeItems);
				foreach (Item2 item in activeItems)
				{
					if ((addModifier.trigger.trigger != Item2.Trigger.ActionTrigger.onEnergyMove && list.Contains(item) && (Item2.ShareItemTypes(item.itemType, addModifier.trigger.types, addModifier.trigger.excludedTypes) || addModifier.trigger.types.Contains(Item2.ItemType.Any))) || (addModifier.trigger.trigger == Item2.Trigger.ActionTrigger.onEnergyMove && list2.Contains(EnergyBall.currentGrid)))
					{
						activeEffects.Add(new Item2.ActiveAddModifiers(this, addModifier));
						item.CheckForScriptedTriggerRecursion(triggers);
					}
				}
			}
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0004AE10 File Offset: 0x00049010
	public IEnumerator ApplySingleMovement(int spacesNeeded, Item2.MovementEffect movementEffect, Transform movingObject, ItemMovement movingObjectItemMovementScript, Item2 originOfMovement)
	{
		Vector3 position = movingObject.position;
		Vector3 vector = movingObject.position;
		Item2 movingItem = movingObject.GetComponent<Item2>();
		if (!movingItem)
		{
			yield break;
		}
		if (movementEffect.movementVariety == Item2.MovementEffect.MovementVariety.setAmount)
		{
			if (movementEffect.movementType == Item2.MovementEffect.MovementType.local)
			{
				vector += originOfMovement.transform.rotation * movementEffect.movementAmount;
			}
			else
			{
				vector += movementEffect.movementAmount;
			}
		}
		else if (movementEffect.movementVariety == Item2.MovementEffect.MovementVariety.toRandomSpace)
		{
			List<GridSquare> list = new List<GridSquare>(GridSquare.allGrids);
			list = list.Where((GridSquare x) => !x.containsItem).ToList<GridSquare>();
			if (movementEffect.movementAmount != Vector2.zero)
			{
				Vector3 direction = movementEffect.movementAmount;
				if (movementEffect.movementType == Item2.MovementEffect.MovementType.local)
				{
					direction = originOfMovement.transform.rotation * movementEffect.movementAmount;
				}
				list = list.Where((GridSquare x) => Vector3.Dot(x.transform.position - originOfMovement.transform.position, direction) > 0f).ToList<GridSquare>();
			}
			if (list.Count == 0)
			{
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveCollide, originOfMovement, null, true, false);
				yield break;
			}
			GridSquare gridSquare = list[Random.Range(0, list.Count)];
			vector = new Vector3(gridSquare.transform.position.x, gridSquare.transform.position.y, movingObject.position.z);
		}
		List<GameObject> list2;
		List<GameObject> list3;
		movingObjectItemMovementScript.TestAtPosition(vector, out list2, out list3, 1f, false, true, false);
		if (list2.Count == spacesNeeded && list3.Count == 0 && (GridSquare.IsRunicSquareInList(list2) == 0 || (!Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.allowsItemsInIllusorySpaces) && GridSquare.IsRunicSquareInList(list2) < list2.Count)))
		{
			if (movingObjectItemMovementScript.inGrid)
			{
				movingObjectItemMovementScript.RemoveFromGrid();
			}
			movingObject.position = vector;
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMovePast, movingItem, null, true, false);
			movingObjectItemMovementScript.mousePreview.position = position;
			this.spriteRenderer.enabled = false;
			yield return movingObjectItemMovementScript.MoveOverTime(18f);
			this.spriteRenderer.enabled = true;
			if (movementEffect.movementLength == Item2.MovementEffect.MovementLength.untilHit)
			{
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveSuccess, movingItem, null, true, false);
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onUse, this, null, true, false);
			}
			else
			{
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveSuccess, movingItem, null, true, false);
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveFinish, movingItem, null, true, false);
			}
		}
		else
		{
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveCollide, originOfMovement, null, true, false);
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveFinish, movingItem, null, true, false);
		}
		movingObjectItemMovementScript.AddToGrid(false);
		yield break;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0004AE44 File Offset: 0x00049044
	public IEnumerator ApplyMovementEffect(Item2.MovementEffect movementEffect)
	{
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.FindItemsAndGridsinArea(list, list2, movementEffect.itemsToMove, movementEffect.areaDistance, null, null, null, true, false, true);
		foreach (Item2 movingObject in list)
		{
			ItemMovement component = movingObject.GetComponent<ItemMovement>();
			int spacesNeeded = component.GetSpacesNeeded();
			if (movementEffect.movementAmount.magnitude > 0f || movementEffect.movementVariety != Item2.MovementEffect.MovementVariety.setAmount)
			{
				yield return this.ApplySingleMovement(spacesNeeded, movementEffect, movingObject.transform, component, this);
			}
			else if (Mathf.Abs(movementEffect.rotationAmount) > 0f)
			{
				Quaternion rotation = base.transform.rotation;
				movingObject.transform.rotation = Quaternion.Euler(0f, 0f, movingObject.transform.rotation.eulerAngles.z + movementEffect.rotationAmount);
				component.mousePreview.rotation = rotation;
				this.spriteRenderer.enabled = false;
				yield return component.RotateOverTime(0.1f);
				GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onRotate, movingObject, null, true, false);
				this.spriteRenderer.enabled = true;
			}
			movingObject = null;
		}
		List<Item2>.Enumerator enumerator = default(List<Item2>.Enumerator);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
		yield break;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0004AE5C File Offset: 0x0004905C
	private Vector2 GetItemPos(int total, int num)
	{
		return this.player.transform.position + new Vector2(1f, 1f) * (float)total / 2f + new Vector2(-1f, -1f) * (float)num;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0004AEBE File Offset: 0x000490BE
	public IEnumerator ApplyCreateEffect(Item2.CreateEffect createEffect, List<Item2> activeItems)
	{
		this.ReplaceSelfWithPrefab(createEffect);
		yield return new WaitForFixedUpdate();
		if (createEffect.createType == Item2.CreateEffect.CreateType.set || createEffect.createType == Item2.CreateEffect.CreateType.trueRandom || createEffect.createType == Item2.CreateEffect.CreateType.byType || createEffect.createType == Item2.CreateEffect.CreateType.byType || createEffect.createType == Item2.CreateEffect.CreateType.createBlessing || createEffect.createType == Item2.CreateEffect.CreateType.sameSizeItem)
		{
			List<GameObject> newItems = new List<GameObject>();
			if (createEffect.createType == Item2.CreateEffect.CreateType.set)
			{
				int i = createEffect.itemsToCreate.Count;
				List<GameObject> list = new List<GameObject>(createEffect.itemsToCreate);
				list = this.gameManager.ItemsValidToSpawn(list, false);
				while (i > 0)
				{
					if (list.Count <= 0)
					{
						break;
					}
					GameObject gameObject = ItemSpawner.InstantiateItemFree(Item2.ChooseRandomAndRemoveFromList(list));
					newItems.Add(gameObject);
					PlayerStatTracking.main.AddStat("Items created", 1);
					i--;
				}
			}
			else if (createEffect.createType == Item2.CreateEffect.CreateType.sameSizeItem)
			{
				if (activeItems == null || activeItems.Count == 0 || activeItems[0] == null)
				{
					GameFlowManager.main.isWaitingForItemRoutine = false;
					yield break;
				}
				int spacesNeeded = activeItems[0].GetComponent<ItemMovement>().GetSpacesNeeded();
				GameObject gameObject2 = this.gameManager.SpawnItemOfSize(spacesNeeded);
				newItems.Add(gameObject2);
			}
			else if (createEffect.createType == Item2.CreateEffect.CreateType.trueRandom)
			{
				List<GameObject> list2 = new List<GameObject>(createEffect.itemsToCreate);
				list2 = this.gameManager.ItemsValidToSpawn(list2, false);
				GameObject gameObject3 = Object.Instantiate<GameObject>(Item2.ChooseRandomFromList(list2, true), this.player.transform.position + Vector3.back, Quaternion.identity);
				newItems.Add(gameObject3);
				PlayerStatTracking.main.AddStat("Items created", 1);
			}
			else if (createEffect.createType == Item2.CreateEffect.CreateType.byType)
			{
				int num = 1;
				if (createEffect.numberToCreate > 0)
				{
					num = createEffect.numberToCreate;
				}
				List<Item2> list3;
				if (createEffect.raritiesToCreate.Count > 0)
				{
					list3 = ItemSpawner.GetItems(num, createEffect.raritiesToCreate, createEffect.typesToCreate, false, createEffect.allowNonStandard);
				}
				else
				{
					list3 = ItemSpawner.GetItems(num, createEffect.typesToCreate, false, createEffect.allowNonStandard);
				}
				List<GameObject> list4 = ItemSpawner.InstantiateItemsFree(list3, false, default(Vector2));
				foreach (GameObject gameObject4 in list4)
				{
					gameObject4.transform.position = this.GetItemPos(list4.Count, list4.IndexOf(gameObject4));
				}
				newItems.AddRange(list4);
				PlayerStatTracking.main.AddStat("Items created", 1);
			}
			else if (createEffect.createType == Item2.CreateEffect.CreateType.createBlessing)
			{
				List<GameObject> list5 = ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Blessing }, false, true), false, default(Vector2));
				foreach (GameObject gameObject5 in list5)
				{
					gameObject5.transform.position = this.GetItemPos(list5.Count, list5.IndexOf(gameObject5));
				}
				newItems.AddRange(list5);
				PlayerStatTracking.main.AddStat("Items created", 1);
			}
			List<Item2> list6 = new List<Item2>();
			List<GridSquare> list7 = new List<GridSquare>();
			this.FindItemsAndGridsinArea(list6, list7, createEffect.areasToCreateTheItem, Item2.AreaDistance.all, null, null, null, true, false, true);
			List<Vector2> vecs = new List<Vector2>();
			foreach (GridSquare gridSquare in list7)
			{
				vecs.Add(gridSquare.transform.localPosition);
			}
			vecs.AddRange(this.gridObject.gridPositions.ConvertAll<Vector2>((Vector2Int x) => x));
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return new WaitForSeconds(0.1f);
			if (newItems.Count == 0)
			{
				GameFlowManager.main.isWaitingForItemRoutine = false;
				yield break;
			}
			bool flag = false;
			for (int j = 0; j < newItems.Count; j++)
			{
				GameObject gameObject6 = newItems[j];
				if (!gameObject6)
				{
					newItems.RemoveAt(j);
					j--;
				}
				else
				{
					StackableItem component = gameObject6.GetComponent<StackableItem>();
					if (!component || !component.combining)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				GameFlowManager.main.isWaitingForItemRoutine = false;
				yield break;
			}
			yield return this.ConsiderReorganizeEventAfterSpawn(newItems, vecs);
			newItems = null;
			vecs = null;
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.chooseItem)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			this.comboTarget = null;
			EventButton e = base.gameObject.AddComponent<EventButton>();
			e.requiredItemType = createEffect.typesToCreate;
			e.requiredRarities = createEffect.raritiesToCreate;
			e.skippable = createEffect.skippable;
			GameManager.main.ChooseMatchingItem(e, false);
			if (createEffect.descriptor != "")
			{
				string text = createEffect.descriptor.Replace("hidden", "").Trim();
				this.gameManager.ShowPromptTextWithKey(LangaugeManager.main.GetTextByKey(text), text);
			}
			else
			{
				string text2 = Item2.GetDisplayName(base.name);
				this.gameManager.ShowPromptTextWithKey(LangaugeManager.main.GetTextByKey(text2), text2);
			}
			while (GameManager.main.inventoryPhase == GameManager.InventoryPhase.choose)
			{
				yield return null;
			}
			if (e.sacrificedItem)
			{
				this.comboTarget = e.sacrificedItem;
			}
			GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onCombo, this, null, true, true);
			if (e)
			{
				Object.Destroy(e);
			}
			e = null;
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.replace)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			List<GameObject> list8;
			List<GameObject> list9;
			this.itemMovement.TestAtPosition(base.transform.position, out list8, out list9, 1f, false, true, false);
			int num2 = 0;
			foreach (BoxCollider2D boxCollider2D in base.GetComponentsInChildren<BoxCollider2D>())
			{
				num2 += Mathf.RoundToInt(boxCollider2D.size.x * boxCollider2D.size.y);
			}
			int num3 = Random.Range(0, createEffect.itemsToCreate.Count);
			if (list8.Count != num2)
			{
				GameFlowManager.main.isWaitingForItemRoutine = false;
				yield break;
			}
			this.itemMovement.TestAtPosition(base.transform.position, out list8, out list9, 1f, false, true, false);
			if (list9.Count <= 0)
			{
				GameObject newItem = Object.Instantiate<GameObject>(createEffect.itemsToCreate[num3], base.transform.position, base.transform.rotation);
				ItemMovement component2 = newItem.GetComponent<ItemMovement>();
				newItem.GetComponent<Item2>();
				if (this.gridObject.gridPositions.Count > 1 && component2 && component2.GetSpacesNeeded() == 1)
				{
					newItem.transform.position = GridObject.CellToWorld(this.gridObject.gridPositions[0]);
				}
				newItem.transform.localScale = Vector3.one;
				yield return new WaitForEndOfFrame();
				yield return new WaitForFixedUpdate();
				newItem.GetComponent<ItemMovement>().AddToGrid(false);
				PlayerStatTracking.main.AddStat("Items created", 1);
				newItem = null;
			}
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.inOpenSpace || createEffect.createType == Item2.CreateEffect.CreateType.inOpenSpaceRandom)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			bool canGoOverSomething = false;
			if (createEffect.itemsToCreate.Count == 1 && createEffect.itemsToCreate[0].GetComponent<Item2>().CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems))
			{
				canGoOverSomething = true;
			}
			List<GridSquare> list10 = new List<GridSquare>();
			this.FindItemsAndGridsinArea(new List<Item2>(), list10, createEffect.areasToCreateTheItem, createEffect.areaDistance, null, null, null, true, false, true);
			for (int l = 0; l < list10.Count; l++)
			{
				GridSquare gridSquare2 = list10[l];
				int num4 = Random.Range(l, list10.Count);
				list10[l] = list10[num4];
				list10[num4] = gridSquare2;
			}
			int numberToCreate = createEffect.numberToCreate;
			foreach (GridSquare gridSquare3 in list10)
			{
				if (!gridSquare3.containsItem || canGoOverSomething)
				{
					if (canGoOverSomething)
					{
						List<GridSquare> list11 = new List<GridSquare>();
						List<Item2> list12 = new List<Item2>();
						this.TestAtVector(list12, list11, gridSquare3.transform.position);
						if (Item2.GetItemsWithStatusEffectInThisList(list12, Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems).Count > 0)
						{
							continue;
						}
					}
					int num5 = Random.Range(0, createEffect.itemsToCreate.Count);
					GameObject gameObject7 = createEffect.itemsToCreate[num5];
					GameObject newItem = Object.Instantiate<GameObject>(gameObject7, gridSquare3.transform.position + Vector3.back, Quaternion.identity);
					newItem.transform.localScale = Vector3.one;
					yield return new WaitForEndOfFrame();
					yield return new WaitForFixedUpdate();
					newItem.GetComponent<ItemMovement>().AddToGrid(false);
					Carving component3 = newItem.GetComponent<Carving>();
					if (component3)
					{
						Tote.main.AddCarving(component3.gameObject);
					}
					PlayerStatTracking.main.AddStat("Items created", 1);
					int k = numberToCreate;
					numberToCreate = k - 1;
					if (createEffect.createType == Item2.CreateEffect.CreateType.inOpenSpaceRandom && numberToCreate <= 0)
					{
						break;
					}
					newItem = null;
				}
			}
			List<GridSquare>.Enumerator enumerator3 = default(List<GridSquare>.Enumerator);
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.inOpenSpaceByType)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			List<GridSquare> list13 = new List<GridSquare>();
			this.FindItemsAndGridsinArea(new List<Item2>(), list13, createEffect.areasToCreateTheItem, createEffect.areaDistance, null, null, null, true, false, true);
			foreach (GridSquare gridSquare4 in list13)
			{
				if (!gridSquare4.containsItem)
				{
					List<GameObject> list14 = ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(1, createEffect.raritiesToCreate, createEffect.typesToCreate, true, false), false, default(Vector2));
					GameObject newItem = list14[0];
					if (newItem)
					{
						newItem.transform.position = gridSquare4.transform.position + Vector3.back;
						newItem.transform.localScale = Vector3.one;
						yield return new WaitForEndOfFrame();
						yield return new WaitForFixedUpdate();
						newItem.GetComponent<ItemMovement>().AddToGrid(false);
						PlayerStatTracking.main.AddStat("Items created", 1);
					}
					newItem = null;
				}
			}
			List<GridSquare>.Enumerator enumerator3 = default(List<GridSquare>.Enumerator);
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.createCurse)
		{
			List<GameObject> newItems = new List<GameObject>();
			int num6 = createEffect.itemsToCreate.Count;
			List<GameObject> list15 = new List<GameObject>(createEffect.itemsToCreate);
			list15 = this.gameManager.ItemsValidToSpawn(list15, false);
			while (num6 > 0 && list15.Count > 0)
			{
				GameObject gameObject8 = Object.Instantiate<GameObject>(Item2.ChooseRandomAndRemoveFromList(list15), this.GetItemPos(createEffect.itemsToCreate.Count, createEffect.itemsToCreate.Count - num6), Quaternion.identity, this.gameManager.itemsParent);
				newItems.Add(gameObject8);
				PlayerStatTracking.main.AddStat("Items created", 1);
				num6--;
			}
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return this.gameManager.CreateCurseReorg(newItems, false);
			newItems = null;
		}
		else if (createEffect.createType == Item2.CreateEffect.CreateType.createBlessing)
		{
			List<Vector2> vecs = new List<Vector2>();
			foreach (GridSquare gridSquare5 in GridSquare.allGrids)
			{
				vecs.Add(gridSquare5.transform.localPosition);
			}
			GameObject newItem = this.gameManager.SpawnCurseOrBlessing(Random.Range(1, 5), Curse.Type.Blessing);
			newItem.transform.position = base.transform.position;
			newItem.GetComponent<Curse>().Setup();
			yield return new WaitForSeconds(0.3f);
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			this.gameManager.EnterSpecialOrganization(new List<GameObject> { newItem }, vecs, true, this.lastParentInventoryGrid);
			yield return new WaitForSeconds(0.3f);
			yield return new WaitForFixedUpdate();
			vecs = null;
			newItem = null;
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		while (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization)
		{
			yield return null;
		}
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
		yield break;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0004AEDB File Offset: 0x000490DB
	public IEnumerator ApplyEnergyEffect(Item2.EnergyEffect energyEffect)
	{
		EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
		List<EnergyBall> list = new List<EnergyBall>();
		switch (energyEffect.energiesToEffect)
		{
		case Item2.EnergyEffect.EnergiesToEffect.trigger:
			if ((currentEnergyBall && GridObject.WorldToCell(currentEnergyBall.transform.position) == GridObject.WorldToCell(base.transform.position)) || energyEffect.worksWithForeignCurrentEnergyBall)
			{
				list.Add(currentEnergyBall);
			}
			break;
		case Item2.EnergyEffect.EnergiesToEffect.allEnergies:
			list = new List<EnergyBall>(EnergyBall.energyBalls);
			break;
		case Item2.EnergyEffect.EnergiesToEffect.newEnergy:
		{
			EnergyBall component = Object.Instantiate<GameObject>(this.cR8Manager.cr8energyFlowPrefabBackup, base.transform.position + Vector3.back * 0.01f, Quaternion.identity, base.transform.parent).GetComponent<EnergyBall>();
			component.energyValue = 0;
			list.Add(component);
			break;
		}
		}
		foreach (EnergyBall energyBall in list)
		{
			if (energyBall)
			{
				if (energyEffect.effect == Item2.EnergyEffect.Effect.changeEnergy)
				{
					if (energyEffect.value == Item2.EnergyEffect.Value.standard)
					{
						energyBall.energyValue += Mathf.RoundToInt(energyEffect.num2);
					}
					else if (energyEffect.value == Item2.EnergyEffect.Value.currentItem2Charges)
					{
						energyBall.energyValue += Mathf.RoundToInt((float)this.currentCharges * energyEffect.num2);
						this.currentCharges = 0;
					}
					else if (energyEffect.value == Item2.EnergyEffect.Value.triggerEnergyValue)
					{
						if (currentEnergyBall)
						{
							energyBall.energyValue += Mathf.CeilToInt((float)currentEnergyBall.energyValue * energyEffect.num2);
						}
					}
					else if (energyEffect.value == Item2.EnergyEffect.Value.currentAP)
					{
						if (CR8Manager.instance && CR8Manager.instance.isTesting)
						{
							Player.main.AP = 0;
							Player.main.SetAPForNewTurn();
						}
						energyBall.energyValue = Mathf.CeilToInt((float)Player.main.AP * energyEffect.num2);
					}
					if (energyBall.energyValue < 0)
					{
						energyBall.DestroyEnergy();
					}
				}
				if (energyEffect.specialMovementToApply != Item2.EnergyEffect.SpecialMovementToApply.none && energyEffect.specialMovementAmount.Count > 0)
				{
					foreach (Vector2 vector in energyEffect.specialMovementAmount)
					{
						if (energyEffect.specialMovementToApply == Item2.EnergyEffect.SpecialMovementToApply.slide)
						{
							Vector2 vector2 = vector;
							switch (energyEffect.specialMovementType)
							{
							case Item2.EnergyEffect.MovementType.global:
								vector2 = vector;
								break;
							case Item2.EnergyEffect.MovementType.localToThisRotation:
								vector2 = base.transform.rotation * vector;
								break;
							case Item2.EnergyEffect.MovementType.localToEntranceDirection:
								vector2 = EnergyBall.publicCurrentDirection * vector;
								break;
							case Item2.EnergyEffect.MovementType.relative:
							{
								Vector2 vector3 = base.transform.position - energyBall.transform.position;
								vector3 = vector3.normalized;
								vector3 = new Vector2((float)Mathf.RoundToInt(vector3.x), (float)Mathf.RoundToInt(vector3.y));
								vector2 = vector3 * vector;
								break;
							}
							}
							energyBall.AddSpecialMovement(vector2, this);
						}
					}
				}
				if (energyEffect.applyRotation)
				{
					switch (energyEffect.movementType)
					{
					case Item2.EnergyEffect.MovementType.global:
						energyBall.transform.rotation = Quaternion.Euler(0f, 0f, energyEffect.rotationAmount);
						break;
					case Item2.EnergyEffect.MovementType.localToThisRotation:
						energyBall.transform.rotation = base.transform.rotation * Quaternion.Euler(0f, 0f, energyEffect.rotationAmount);
						break;
					case Item2.EnergyEffect.MovementType.localToEntranceDirection:
					{
						Quaternion quaternion = Quaternion.Euler(0f, 0f, 0f);
						if (EnergyBall.publicCurrentDirection == new Vector2(-1f, 0f))
						{
							quaternion = Quaternion.Euler(0f, 0f, 90f);
						}
						else if (EnergyBall.publicCurrentDirection == new Vector2(1f, 0f))
						{
							quaternion = Quaternion.Euler(0f, 0f, 270f);
						}
						else if (EnergyBall.publicCurrentDirection == new Vector2(0f, 1f))
						{
							quaternion = Quaternion.Euler(0f, 0f, 0f);
						}
						else if (EnergyBall.publicCurrentDirection == new Vector2(0f, -1f))
						{
							quaternion = Quaternion.Euler(0f, 0f, 180f);
						}
						Debug.Log(quaternion.eulerAngles);
						energyBall.transform.rotation = quaternion * Quaternion.Euler(0f, 0f, energyEffect.rotationAmount);
						break;
					}
					}
				}
			}
		}
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0004AEF1 File Offset: 0x000490F1
	private IEnumerator ConsiderReorganizeEventAfterSpawn(GameObject newItem)
	{
		yield return this.ConsiderReorganizeEventAfterSpawn(new List<GameObject> { newItem }, GridSquare.GetAllGridVectors());
		yield break;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0004AF07 File Offset: 0x00049107
	private IEnumerator ConsiderReorganizeEventAfterSpawn(List<GameObject> newItems, List<Vector2> vecs)
	{
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			foreach (GameObject gameObject in newItems)
			{
				ItemMovement component = gameObject.GetComponent<ItemMovement>();
				if (component != null)
				{
					component.MoveOut(gameObject.transform.position);
				}
			}
			yield break;
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Debug.Log("ConsiderReorganizeEventAfterSpawn");
		this.gameManager.EnterSpecialOrganization(newItems, vecs, true, this.lastParentInventoryGrid);
		while (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization)
		{
			yield return null;
		}
		Debug.Log("ConsiderReorganizeEventAfterSpawn done");
		yield return new WaitForSeconds(0.3f);
		yield return new WaitForFixedUpdate();
		yield break;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0004AF24 File Offset: 0x00049124
	public IEnumerator ApplyEffect(Item2.EffectTotal effectTotal, List<Enemy> enemiesEffected, List<Status> effectedStatuses = null)
	{
		List<Status> list = new List<Status>();
		Player main = Player.main;
		Status status = main.stats;
		Status status2 = PetItem2.GetStatus(base.gameObject);
		if (status2 != null)
		{
			status = status2;
		}
		Item2.Effect effect = effectTotal.effect.Clone();
		list = Item2.GetMyStats(effect, status, effectedStatuses, this.gameFlowManager, this.gameManager, main, enemiesEffected, this);
		Item2.ApplyMyEffect(effect, list, status, this, main);
		if (base.gameObject.activeInHierarchy && effectTotal.trigger.trigger != Item2.Trigger.ActionTrigger.constant)
		{
			yield return this.itemMovement.PlayAnimation();
		}
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0004AF48 File Offset: 0x00049148
	public static List<Status> GetMyStats(Item2.Effect effect, Status statsOfUser, List<Status> effectedStatuses, GameFlowManager gameFlowManager, GameManager gameManager, Player player, List<Enemy> enemiesEffected, Item2 item = null)
	{
		Item2.Effect.Target target = effect.target;
		List<Status> list = new List<Status>();
		if (target == Item2.Effect.Target.player || target == Item2.Effect.Target.unspecified)
		{
			list.Add(statsOfUser);
		}
		else if (target == Item2.Effect.Target.everyoneButPochette)
		{
			foreach (Enemy enemy in player.transform.parent.GetComponentsInChildren<Enemy>())
			{
				if (!enemy.dead && enemy.isTargetable)
				{
					if ((effect.type == Item2.Effect.Type.Damage || effect.type == Item2.Effect.Type.Vampire) && !enemiesEffected.Contains(enemy))
					{
						enemiesEffected.Add(enemy);
					}
					list.Add(enemy.stats);
				}
			}
			foreach (CombatPet combatPet in player.transform.parent.GetComponentsInChildren<CombatPet>())
			{
				if (combatPet.stats.health > 0)
				{
					list.Add(combatPet.stats);
				}
			}
		}
		else if (target == Item2.Effect.Target.truePlayer)
		{
			list.Add(Player.main.stats);
		}
		else if (target == Item2.Effect.Target.enemy)
		{
			if (!gameManager.targetedEnemy || gameManager.targetedEnemy.dead || !gameManager.targetedEnemy.isTargetable)
			{
				return list;
			}
			if ((effect.type == Item2.Effect.Type.Damage || effect.type == Item2.Effect.Type.Vampire) && !enemiesEffected.Contains(gameManager.targetedEnemy))
			{
				enemiesEffected.Add(gameManager.targetedEnemy);
			}
			list = new List<Status> { gameManager.targetedEnemy.stats };
		}
		else if (target == Item2.Effect.Target.allEnemies || target == Item2.Effect.Target.everyone)
		{
			foreach (Enemy enemy2 in player.transform.parent.GetComponentsInChildren<Enemy>())
			{
				if (!enemy2.dead && enemy2.isTargetable)
				{
					if ((effect.type == Item2.Effect.Type.Damage || effect.type == Item2.Effect.Type.Vampire) && !enemiesEffected.Contains(enemy2))
					{
						enemiesEffected.Add(enemy2);
					}
					list.Add(enemy2.stats);
				}
			}
			if (target == Item2.Effect.Target.everyone)
			{
				list.Add(player.stats);
				foreach (CombatPet combatPet2 in player.transform.parent.GetComponentsInChildren<CombatPet>())
				{
					if (combatPet2.stats.health > 0)
					{
						list.Add(combatPet2.stats);
					}
				}
			}
		}
		else if (target == Item2.Effect.Target.allFriendlies)
		{
			list.Add(player.stats);
			foreach (CombatPet combatPet3 in player.transform.parent.GetComponentsInChildren<CombatPet>())
			{
				if (combatPet3.stats.health > 0)
				{
					list.Add(combatPet3.stats);
				}
			}
		}
		else
		{
			if (target == Item2.Effect.Target.adjacentFriendlies)
			{
				CombatPet[] componentsInChildren = player.transform.parent.GetComponentsInChildren<CombatPet>();
				List<Status> list2 = new List<Status> { player.stats };
				foreach (CombatPet combatPet4 in componentsInChildren)
				{
					list2.Add(combatPet4.stats);
				}
				using (List<Status>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Status status = enumerator.Current;
						if (!(status == statsOfUser) && Mathf.Abs(status.transform.position.x - statsOfUser.transform.position.x) < 2f)
						{
							list.Add(status);
						}
					}
					return list;
				}
			}
			if (target == Item2.Effect.Target.friendliesBehind)
			{
				CombatPet[] componentsInChildren2 = player.transform.parent.GetComponentsInChildren<CombatPet>();
				List<Status> list3 = new List<Status> { player.stats };
				foreach (CombatPet combatPet5 in componentsInChildren2)
				{
					list3.Add(combatPet5.stats);
				}
				using (List<Status>.Enumerator enumerator = list3.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Status status2 = enumerator.Current;
						if (!(status2 == statsOfUser) && status2.transform.position.x < statsOfUser.transform.position.x)
						{
							list.Add(status2);
						}
					}
					return list;
				}
			}
			if (target == Item2.Effect.Target.friendliesInFront)
			{
				CombatPet[] componentsInChildren3 = player.transform.parent.GetComponentsInChildren<CombatPet>();
				List<Status> list4 = new List<Status> { player.stats };
				foreach (CombatPet combatPet6 in componentsInChildren3)
				{
					list4.Add(combatPet6.stats);
				}
				using (List<Status>.Enumerator enumerator = list4.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Status status3 = enumerator.Current;
						if (!(status3 == statsOfUser) && status3.transform.position.x > statsOfUser.transform.position.x)
						{
							list.Add(status3);
						}
					}
					return list;
				}
			}
			if (target == Item2.Effect.Target.backmostFriendly)
			{
				CombatPet[] componentsInChildren4 = player.transform.parent.GetComponentsInChildren<CombatPet>();
				List<Status> list5 = new List<Status> { player.stats };
				foreach (CombatPet combatPet7 in componentsInChildren4)
				{
					list5.Add(combatPet7.stats);
				}
				float num = 999f;
				Status status4 = null;
				foreach (Status status5 in list5)
				{
					if (status5.transform.position.x < num)
					{
						num = status5.transform.position.x;
						status4 = status5;
					}
				}
				list.Add(status4);
			}
			else if (target == Item2.Effect.Target.frontmostFriendly)
			{
				CombatPet[] componentsInChildren5 = player.transform.parent.GetComponentsInChildren<CombatPet>();
				List<Status> list6 = new List<Status> { player.stats };
				foreach (CombatPet combatPet8 in componentsInChildren5)
				{
					list6.Add(combatPet8.stats);
				}
				float num2 = -999f;
				Status status6 = null;
				foreach (Status status7 in list6)
				{
					if (status7.transform.position.x > num2)
					{
						num2 = status7.transform.position.x;
						status6 = status7;
					}
				}
				list.Add(status6);
			}
			else
			{
				if (target == Item2.Effect.Target.reactiveEnemy)
				{
					if (effectedStatuses == null)
					{
						return list;
					}
					using (List<Status>.Enumerator enumerator = effectedStatuses.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Status status8 = enumerator.Current;
							if (status8 && status8.parent && status8.parent.GetComponent<Enemy>())
							{
								list.Add(status8);
							}
						}
						return list;
					}
				}
				if (target == Item2.Effect.Target.statusFromItem && item)
				{
					List<Item2> list7 = new List<Item2>();
					item.FindItemsAndGridsinArea(list7, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
					foreach (Item2 item2 in list7)
					{
						Status status9 = PetItem2.GetStatus(item2.gameObject);
						if (status9 && !list.Contains(status9))
						{
							list.Add(status9);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0004B6F8 File Offset: 0x000498F8
	public static void ApplyMyEffect(Item2.Effect effect, List<Status> stats, Status statsOfUser, Item2 applyingItem, Player player)
	{
		if (effect.valueFromStatusEffects != 0f)
		{
			effect.value -= effect.valueFromStatusEffects;
		}
		foreach (Status status in stats)
		{
			if (!(status == null))
			{
				if (effect.type == Item2.Effect.Type.Damage)
				{
					int num = Mathf.CeilToInt(effect.value) * -1;
					status.Attack(statsOfUser, num, applyingItem, false, false, false);
				}
				else if (effect.type == Item2.Effect.Type.Block)
				{
					status.ChangeArmor(effect.value, effect.mathematicalType);
				}
				else if (effect.type == Item2.Effect.Type.HP)
				{
					status.ChangeHealth(Mathf.CeilToInt(effect.value), null, false);
				}
				else if (effect.type == Item2.Effect.Type.Vampire)
				{
					int num2 = Mathf.CeilToInt(effect.value) * -1;
					if (!applyingItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.projectile))
					{
						status.Vampire(statsOfUser, num2, null);
					}
					else
					{
						status.Vampire(statsOfUser, num2, applyingItem);
					}
				}
				else if (effect.type == Item2.Effect.Type.MaxHP)
				{
					status.SetMaxHP(Mathf.CeilToInt((float)status.maxHealth + effect.value));
				}
				else if (effect.type == Item2.Effect.Type.AP)
				{
					int num3 = Mathf.CeilToInt(effect.value);
					CombatPet component = status.parent.GetComponent<CombatPet>();
					if (status.parent == player.gameObject)
					{
						player.ChangeAP(num3);
					}
					else if (component)
					{
						component.petItem2.ChangeAP(num3);
					}
					else if (applyingItem && applyingItem.petItem && component)
					{
						applyingItem.petItem.ChangeAP(num3);
					}
					else
					{
						player.ChangeAP(num3);
					}
				}
				else if (effect.type == Item2.Effect.Type.NextTurnAP)
				{
					player.APToAddNextTurn += Mathf.CeilToInt(effect.value);
				}
				else if (effect.type == Item2.Effect.Type.Mana)
				{
					Debug.Log("ChangePowerInManaNetwork");
					ConnectionManager.main.ChangePowerInManaNetwork(applyingItem, Mathf.CeilToInt(effect.value));
				}
				else if (effect.type == Item2.Effect.Type.ManaToSelfOnly)
				{
					ManaStone component2 = applyingItem.GetComponent<ManaStone>();
					if (component2)
					{
						component2.ChangeMana(Mathf.RoundToInt(effect.value));
					}
				}
				else
				{
					if (effect.type == Item2.Effect.Type.AllStatusEffects)
					{
						using (IEnumerator enumerator2 = Enum.GetValues(typeof(StatusEffect.Type)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								StatusEffect.Type type = (StatusEffect.Type)obj;
								if (StatusEffect.CanBeCleansed(type))
								{
									status.AddStatusEffect(type, effect.value, effect.mathematicalType);
								}
							}
							continue;
						}
					}
					if (effect.type == Item2.Effect.Type.Poison)
					{
						status.AddStatusEffect(StatusEffect.Type.poison, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Regen)
					{
						status.AddStatusEffect(StatusEffect.Type.regen, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Spikes)
					{
						status.AddStatusEffect(StatusEffect.Type.spikes, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Dodge)
					{
						status.AddStatusEffect(StatusEffect.Type.dodge, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Haste)
					{
						status.AddStatusEffect(StatusEffect.Type.haste, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Slow)
					{
						status.AddStatusEffect(StatusEffect.Type.slow, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Rage)
					{
						status.AddStatusEffect(StatusEffect.Type.rage, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Weak)
					{
						status.AddStatusEffect(StatusEffect.Type.weak, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Burn)
					{
						status.AddStatusEffect(StatusEffect.Type.fire, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Charm)
					{
						status.AddStatusEffect(StatusEffect.Type.charm, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Sleep)
					{
						status.AddStatusEffect(StatusEffect.Type.sleep, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Freeze)
					{
						status.AddStatusEffect(StatusEffect.Type.freeze, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Zombie)
					{
						status.AddStatusEffect(StatusEffect.Type.zombie, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Curse)
					{
						status.AddStatusEffect(StatusEffect.Type.curse, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Exhaust)
					{
						status.AddStatusEffect(StatusEffect.Type.exhaust, effect.value, effect.mathematicalType);
					}
					else if (effect.type == Item2.Effect.Type.Luck)
					{
						player.uncommonLuckFromItems += effect.value * 2f;
						player.rareLuckFromItems += effect.value;
						player.legendaryLuckFromItems += effect.value / 2f;
					}
					else if (effect.type == Item2.Effect.Type.DrawToteCarvings)
					{
						Tote main = Tote.main;
						if (main)
						{
							main.DrawCarvingsFromUndrawn(Mathf.CeilToInt(effect.value));
						}
					}
					else if (effect.type == Item2.Effect.Type.GetGold)
					{
						GameManager.main.ChangeGold(Mathf.RoundToInt(effect.value));
					}
					else if (effect.type == Item2.Effect.Type.SummonPet)
					{
						PetItem2 component3 = applyingItem.GetComponent<PetItem2>();
						if (component3)
						{
							component3.SummonPet();
						}
					}
					else if (effect.type == Item2.Effect.Type.RevivePets && status.health <= 0)
					{
						if (effect.mathematicalType == Item2.Effect.MathematicalType.summative)
						{
							status.SetHealth(Mathf.RoundToInt(effect.value));
						}
						else
						{
							status.SetHealth(Mathf.RoundToInt((float)status.maxHealth * effect.value));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0004BD18 File Offset: 0x00049F18
	public IEnumerator ApplyModifier(Item2.Modifier modifier, List<GameFlowManager.Consideration.ConsiderationItems> alreadyActivatedItems, List<Enemy> enemiesEffected)
	{
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.FindItemsAndGridsinArea(list, list2, modifier.areasToModify, modifier.areaDistance, null, modifier.typesToModify, modifier.excludedTypes, true, false, true);
		bool activateEffect = false;
		using (List<Item2>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Item2.<>c__DisplayClass201_0 CS$<>8__locals1 = new Item2.<>c__DisplayClass201_0();
				CS$<>8__locals1.item = enumerator.Current;
				if (modifier.trigger.trigger != Item2.Trigger.ActionTrigger.onComboUse || CS$<>8__locals1.item.isChosenAsComboRecepient)
				{
					if (Item2.ShareItemTypes(CS$<>8__locals1.item.itemType, modifier.typesToModify, modifier.excludedTypes))
					{
						bool flag = false;
						foreach (Item2.Effect effect in modifier.effects)
						{
							if (effect.type == Item2.Effect.Type.Destroy)
							{
								PlayerStatTracking.main.AddStat("Items destroyed", 1);
								if (Random.Range(0, 2) == 0)
								{
									SoundManager.main.PlaySFX("destroy1");
								}
								else
								{
									SoundManager.main.PlaySFX("destroy2");
								}
								if (!CS$<>8__locals1.item.destroyed)
								{
									GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenThisDestroys, this, null, true, false);
								}
								CS$<>8__locals1.item.GetComponent<ItemMovement>().DelayDestroy();
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.removeFromLootTable)
							{
								RunTypeManager.AddProperty(RunType.RunProperty.Type.cannotFind, DebugItemManager.main.GetPrefabOfItem(CS$<>8__locals1.item));
							}
							else if (effect.type == Item2.Effect.Type.Cleanse)
							{
								CS$<>8__locals1.item.Cleanse();
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.DiscardCarving)
							{
								if (CS$<>8__locals1.item.itemMovement && CS$<>8__locals1.item.itemMovement.inGrid)
								{
									this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onDiscard, CS$<>8__locals1.item, null, true, false);
								}
								if (effect.itemStatusEffect == null || effect.itemStatusEffect.Count == 0)
								{
									effect.itemStatusEffect = new List<Item2.ItemStatusEffect>
									{
										new Item2.ItemStatusEffect
										{
											applyRightAway = false,
											type = Item2.ItemStatusEffect.Type.DiscardCarving,
											length = Item2.ItemStatusEffect.Length.turns
										}
									};
								}
								this.gameFlowManager.itemStatusEffectsToApplyAtEndOfQueuedActions.Add(new GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions(modifier.name, CS$<>8__locals1.item, effect, false));
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.BanishCarving)
							{
								Debug.Log("Banish Carving status effect added to queue");
								if (effect.itemStatusEffect == null || effect.itemStatusEffect.Count == 0)
								{
									effect.itemStatusEffect = new List<Item2.ItemStatusEffect>
									{
										new Item2.ItemStatusEffect
										{
											applyRightAway = false,
											type = Item2.ItemStatusEffect.Type.BanishCarving,
											length = Item2.ItemStatusEffect.Length.turns
										}
									};
								}
								this.gameFlowManager.itemStatusEffectsToApplyAtEndOfQueuedActions.Add(new GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions(modifier.name, CS$<>8__locals1.item, effect, false));
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.AddToStack)
							{
								CS$<>8__locals1.item.GetComponent<StackableItem>().ChangeAmount(Mathf.RoundToInt(effect.value));
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.resetUses)
							{
								if (CS$<>8__locals1.item.itemType.Contains(Item2.ItemType.Relic))
								{
									PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm90"), CS$<>8__locals1.item.transform.position);
								}
								else
								{
									foreach (Item2.LimitedUses limitedUses in CS$<>8__locals1.item.usesLimits)
									{
										if (limitedUses.type == Item2.LimitedUses.Type.total)
										{
											limitedUses.currentValue = limitedUses.value;
										}
									}
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.resetUsesPerCombat)
							{
								if (CS$<>8__locals1.item.itemType.Contains(Item2.ItemType.Relic))
								{
									PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm90"), CS$<>8__locals1.item.transform.position);
								}
								else
								{
									foreach (Item2.LimitedUses limitedUses2 in CS$<>8__locals1.item.usesLimits)
									{
										if (limitedUses2.type == Item2.LimitedUses.Type.perCombat)
										{
											limitedUses2.currentValue = limitedUses2.value;
										}
									}
									CS$<>8__locals1.item.RemoveStatusEffect(Item2.ItemStatusEffect.Type.disabled, Item2.ItemStatusEffect.Length.combats);
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.resetUsesPerTurn)
							{
								if (CS$<>8__locals1.item.itemType.Contains(Item2.ItemType.Relic))
								{
									PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm90"), CS$<>8__locals1.item.transform.position);
								}
								else
								{
									foreach (Item2.LimitedUses limitedUses3 in CS$<>8__locals1.item.usesLimits)
									{
										if (limitedUses3.type == Item2.LimitedUses.Type.perTurn)
										{
											limitedUses3.currentValue = limitedUses3.value;
										}
									}
									CS$<>8__locals1.item.RemoveStatusEffect(Item2.ItemStatusEffect.Type.disabled, Item2.ItemStatusEffect.Length.turns);
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.ItemStatusEffect)
							{
								if (effect.itemStatusEffect.Count > 0 && effect.itemStatusEffect[0].applyRightAway)
								{
									Debug.Log("Item applied right away for " + effect.itemStatusEffect[0].type.ToString() + " to " + CS$<>8__locals1.item.name);
									Item2.ApplyItemStatusEffect(CS$<>8__locals1.item, effect, modifier.name);
								}
								else
								{
									Debug.Log("Item status queed  for " + effect.itemStatusEffect[0].type.ToString() + " to " + CS$<>8__locals1.item.name);
									this.gameFlowManager.itemStatusEffectsToApplyAtEndOfQueuedActions.Add(new GameFlowManager.ItemStatusEffectToApplyAtEndOfQueuedActions(modifier.name, CS$<>8__locals1.item, effect, false));
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.ClearItemStatusEffectOfType)
							{
								if (effect.itemStatusEffect.Count > 0)
								{
									Item2.ClearItemStatusEffect(CS$<>8__locals1.item, effect.itemStatusEffect[0].type);
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.AddToMaxMana)
							{
								ManaStone component = base.GetComponent<ManaStone>();
								if (component)
								{
									component.maxPower += Mathf.RoundToInt(effect.value);
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.Activate)
							{
								if (CS$<>8__locals1.item.itemType.Contains(Item2.ItemType.Core))
								{
									PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm89"), CS$<>8__locals1.item.transform.position);
								}
								else
								{
									bool flag2 = false;
									foreach (GameFlowManager.Consideration.ConsiderationItems considerationItems in alreadyActivatedItems)
									{
										if (considerationItems.modifier == modifier && considerationItems.item == this)
										{
											flag2 = true;
											break;
										}
									}
									if (!flag2)
									{
										if (CS$<>8__locals1.item.CanBeUsed(false, this.costs, true, null))
										{
											EnergyEmitter component2 = CS$<>8__locals1.item.GetComponent<EnergyEmitter>();
											if (component2)
											{
												component2.CreateEnergy();
											}
										}
										activateEffect = true;
										this.gameFlowManager.ConsiderItemUseIndirect(CS$<>8__locals1.item);
									}
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.TransformItemSwitch)
							{
								ItemSwitcher component3 = CS$<>8__locals1.item.GetComponent<ItemSwitcher>();
								if (component3)
								{
									component3.ForceChoose();
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.storeCR8Charge)
							{
								EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
								if (currentEnergyBall)
								{
									this.currentCharges = currentEnergyBall.energyValue;
									currentEnergyBall.DestroyEnergy();
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.resetComponentHeat)
							{
								EnergyEmitter component4 = CS$<>8__locals1.item.GetComponent<EnergyEmitter>();
								if (component4)
								{
									component4.ResetHeat();
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.changeComponenetHeat)
							{
								EnergyEmitter component5 = CS$<>8__locals1.item.GetComponent<EnergyEmitter>();
								if (component5)
								{
									component5.ChangeHeat(Mathf.RoundToInt(effect.value));
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.considerTestRecurison)
							{
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.Duplicate)
							{
								GameObject gameObject = DebugItemManager.main.GetItemByName(Item2.GetDisplayName(CS$<>8__locals1.item.name));
								if (gameObject == null && ModItemLoader.main != null)
								{
									List<Item2> modItems = ModItemLoader.main.modItems;
									Predicate<Item2> predicate;
									if ((predicate = CS$<>8__locals1.<>9__0) == null)
									{
										predicate = (CS$<>8__locals1.<>9__0 = (Item2 x) => Item2.GetDisplayName(x.name) == Item2.GetDisplayName(CS$<>8__locals1.item.name));
									}
									Item2 item = modItems.Find(predicate);
									if (item != null)
									{
										gameObject = item.gameObject;
									}
								}
								GameObject gameObject2 = ItemSpawner.InstantiateItemFree(gameObject);
								gameObject2.transform.position = CS$<>8__locals1.item.transform.position;
								yield return this.ConsiderReorganizeEventAfterSpawn(gameObject2);
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.getItemOfSameType)
							{
								new List<Item2>();
								GameObject gameObject3 = ItemSpawner.InstantiateItemFree(ItemSpawner.GetItems(1, new List<Item2.Rarity> { CS$<>8__locals1.item.rarity }, CS$<>8__locals1.item.itemType, false, !CS$<>8__locals1.item.isStandard));
								if (gameObject3)
								{
									gameObject3.transform.position = CS$<>8__locals1.item.transform.position;
									yield return this.ConsiderReorganizeEventAfterSpawn(gameObject3);
								}
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.GiveDiscount)
							{
								SoundManager.main.PlaySFX("miniGameGood");
								Store store = Object.FindObjectOfType<Store>();
								if (store)
								{
									store.GetFly();
								}
								store.ResetSales();
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.RemoveFromGrid)
							{
								CS$<>8__locals1.item.itemMovement.RemoveFromGrid();
								CS$<>8__locals1.item.itemMovement.MoveOutOfGrid(true, true);
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.AddCharge)
							{
								CS$<>8__locals1.item.AddCharge(Mathf.RoundToInt(effect.value));
								flag = true;
							}
							else if (effect.type == Item2.Effect.Type.SummonPet)
							{
								PetItem2 component6 = CS$<>8__locals1.item.GetComponent<PetItem2>();
								if (component6)
								{
									component6.SummonPet();
								}
							}
						}
						List<Item2.Effect>.Enumerator enumerator2 = default(List<Item2.Effect>.Enumerator);
						if (!flag && (!CS$<>8__locals1.item.appliedModifiers.Contains(modifier) || modifier.stackable))
						{
							CS$<>8__locals1.item.appliedModifiers.Add(modifier);
						}
					}
					CS$<>8__locals1 = null;
				}
			}
		}
		List<Item2>.Enumerator enumerator = default(List<Item2>.Enumerator);
		if (activateEffect)
		{
			alreadyActivatedItems.Add(new GameFlowManager.Consideration.ConsiderationItems(modifier, this));
		}
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
		yield break;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0004BD38 File Offset: 0x00049F38
	public static void ClearItemStatusEffect(Item2 item, Item2.ItemStatusEffect.Type type)
	{
		for (int i = 0; i < item.activeItemStatusEffects.Count; i++)
		{
			if (item.activeItemStatusEffects[i].type == type)
			{
				item.RemoveStatusEffect(i);
				i--;
			}
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0004BD7C File Offset: 0x00049F7C
	public List<Vector2> GetAllEntrances()
	{
		List<Vector2> list = new List<Vector2>();
		foreach (Item2.EffectTotal effectTotal in this.effectTotals)
		{
			list.AddRange(this.GetEntrancesFromTrigger(effectTotal.trigger));
		}
		foreach (Item2.CreateEffect createEffect in this.createEffects)
		{
			list.AddRange(this.GetEntrancesFromTrigger(createEffect.trigger));
		}
		foreach (Item2.Modifier modifier in this.modifiers)
		{
			list.AddRange(this.GetEntrancesFromTrigger(modifier.trigger));
		}
		foreach (Item2.MovementEffect movementEffect in this.movementEffects)
		{
			list.AddRange(this.GetEntrancesFromTrigger(movementEffect.trigger));
		}
		foreach (Item2.AddModifier addModifier in this.addModifiers)
		{
			list.AddRange(this.GetEntrancesFromTrigger(addModifier.trigger));
		}
		foreach (Item2.EnergyEffect energyEffect in this.energyEffects)
		{
			list.AddRange(this.GetEntrancesFromTrigger(energyEffect.trigger));
		}
		list = list.Distinct<Vector2>().ToList<Vector2>();
		return list;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0004BF7C File Offset: 0x0004A17C
	private List<Vector2> GetEntrancesFromTrigger(Item2.Trigger trigger)
	{
		return trigger.validEntrances;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0004BF84 File Offset: 0x0004A184
	public static void ApplyItemStatusEffect(Item2 item, Item2.Effect effect, string name)
	{
		if (effect == null || effect.itemStatusEffect.Count <= 0)
		{
			return;
		}
		effect.itemStatusEffect[0].source = name;
		bool flag = false;
		Item2.Modifier.Length length = Item2.Modifier.Length.whileActive;
		if (effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.whileActive)
		{
			length = Item2.Modifier.Length.whileActive;
		}
		else if (effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.turns)
		{
			length = Item2.Modifier.Length.forTurn;
		}
		else if (effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.combats)
		{
			length = Item2.Modifier.Length.forCombat;
		}
		else if (effect.itemStatusEffect[0].length == Item2.ItemStatusEffect.Length.permanent)
		{
			length = Item2.Modifier.Length.permanent;
		}
		if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToManaCost)
		{
			if (item.costs.Count > 0)
			{
				item.AddCost(Item2.Cost.Type.mana, effect.itemStatusEffect[0].num, length, item.costs);
			}
		}
		else if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToEnergyCost)
		{
			if (item.costs.Count > 0)
			{
				item.AddCost(Item2.Cost.Type.energy, effect.itemStatusEffect[0].num, length, item.costs);
			}
		}
		else if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.AddToGoldCost)
		{
			if (item.costs.Count > 0)
			{
				item.AddCost(Item2.Cost.Type.gold, effect.itemStatusEffect[0].num, length, item.costs);
			}
		}
		else if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.DiscardCarving)
		{
			Tote main = Tote.main;
			Carving component = item.GetComponent<Carving>();
			if (item && item.itemMovement)
			{
				item.itemMovement.RemoveFromGrid();
			}
			if (main && component && main.IsDrawn(component.gameObject))
			{
				main.DiscardCarving(item.gameObject);
			}
		}
		else if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.BanishCarving)
		{
			Tote main2 = Tote.main;
			if (main2)
			{
				main2.BanishCarving(item.gameObject);
			}
		}
		else
		{
			foreach (Item2.ItemStatusEffect itemStatusEffect in item.activeItemStatusEffects)
			{
				if (itemStatusEffect.source == effect.itemStatusEffect[0].source || (itemStatusEffect.type == Item2.ItemStatusEffect.Type.canBeForged && effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.canBeForged))
				{
					flag = true;
					if (effect.itemStatusEffect[0].length != Item2.ItemStatusEffect.Length.permanent && itemStatusEffect.type != Item2.ItemStatusEffect.Type.invisbleCharge)
					{
						itemStatusEffect.num = effect.itemStatusEffect[0].num;
					}
					else
					{
						itemStatusEffect.num += effect.itemStatusEffect[0].num;
					}
				}
			}
			if (!flag)
			{
				Item2.ItemStatusEffect itemStatusEffect2 = effect.itemStatusEffect[0].Clone();
				itemStatusEffect2.showOnCard = true;
				item.AddStatusEffect(itemStatusEffect2);
				if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.conductive)
				{
					ConnectionManager.main.FindManaNetworks();
				}
			}
		}
		if (effect.itemStatusEffect[0].type == Item2.ItemStatusEffect.Type.cannotBeRotated && item.transform.rotation != Quaternion.identity)
		{
			item.transform.rotation = Quaternion.identity;
			if (item.itemMovement.inGrid)
			{
				item.itemMovement.RemoveFromGrid();
				item.itemMovement.MoveOut(item.transform.position);
			}
		}
		item.SetColor();
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0004C340 File Offset: 0x0004A540
	public void AddCharge(int amount)
	{
		this.currentCharges += amount;
		if (this.currentCharges >= this.charges)
		{
			this.currentCharges -= this.charges;
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onFullCharge, null, null, true, false);
		}
		this.itemMovement.SetCharges(this.currentCharges);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0004C3A0 File Offset: 0x0004A5A0
	public void ApplyAddModifier(Item2.AddModifier addModifier, List<GameFlowManager.Consideration.ConsiderationItems> alreadyActivatedItems, List<Enemy> enemiesEffected)
	{
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.FindItemsAndGridsinArea(list, list2, addModifier.areasToModify, addModifier.areaDistance, null, null, null, true, false, true);
		foreach (Item2 item in list)
		{
			if (Item2.ShareItemTypes(item.itemType, addModifier.typesToModify) || addModifier.typesToModify.Contains(Item2.ItemType.Any))
			{
				item.modifiers.Add(addModifier.modifier.Clone());
			}
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0004C444 File Offset: 0x0004A644
	public static void RemoveAllModifiers(List<Item2.Modifier.Length> modifierLengths, int value = -1)
	{
		foreach (Item2 item in Item2.allItems)
		{
			item.RemoveModifiers(modifierLengths, value);
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0004C498 File Offset: 0x0004A698
	public void RemoveModifiers(List<Item2.Modifier.Length> modifierLengths, int value = -1)
	{
		if (!this || !base.gameObject)
		{
			return;
		}
		Carving component = base.GetComponent<Carving>();
		if (component)
		{
			for (int i = 0; i < component.summoningCosts.Count; i++)
			{
				Item2.Cost cost = component.summoningCosts[i];
				for (int j = 0; j < cost.costModifiers.Count; j++)
				{
					Item2.Cost.CostModifier costModifier = cost.costModifiers[j];
					if (modifierLengths.Contains(costModifier.length))
					{
						cost.costModifiers.RemoveAt(j);
						j--;
					}
				}
				if (cost.costModifiers.Count == 0 && cost.baseValue == -999)
				{
					this.costs.RemoveAt(i);
					i--;
				}
			}
		}
		if (modifierLengths.Contains(Item2.Modifier.Length.forTurn))
		{
			foreach (Item2.LimitedUses limitedUses in this.usesLimits)
			{
				if (limitedUses.type == Item2.LimitedUses.Type.perTurn)
				{
					limitedUses.currentValue = limitedUses.value;
				}
			}
		}
		if (modifierLengths.Contains(Item2.Modifier.Length.forCombat))
		{
			foreach (Item2.LimitedUses limitedUses2 in this.usesLimits)
			{
				if (limitedUses2.type == Item2.LimitedUses.Type.perCombat)
				{
					limitedUses2.currentValue = limitedUses2.value;
				}
			}
		}
		for (int k = 0; k < this.costs.Count; k++)
		{
			Item2.Cost cost2 = this.costs[k];
			for (int l = 0; l < cost2.costModifiers.Count; l++)
			{
				Item2.Cost.CostModifier costModifier2 = cost2.costModifiers[l];
				if (modifierLengths.Contains(costModifier2.length))
				{
					cost2.costModifiers.RemoveAt(l);
					l--;
				}
			}
			if (cost2.costModifiers.Count == 0 && cost2.baseValue == -999)
			{
				this.costs.RemoveAt(k);
				k--;
			}
		}
		for (int m = 0; m < this.modifiers.Count; m++)
		{
			Item2.Modifier modifier = this.modifiers[m];
			if (modifierLengths.Contains(modifier.lengthForThisModifier))
			{
				this.modifiers.RemoveAt(m);
				m--;
			}
			if (modifierLengths.Contains(Item2.Modifier.Length.forTurn) && modifier.length == Item2.Modifier.Length.twoTurns)
			{
				modifier.length = Item2.Modifier.Length.forTurn;
			}
		}
		for (int n = 0; n < this.appliedModifiers.Count; n++)
		{
			Item2.Modifier modifier2 = this.appliedModifiers[n];
			if (modifierLengths.Contains(modifier2.length))
			{
				this.appliedModifiers.RemoveAt(n);
				n--;
			}
			if (modifierLengths.Contains(Item2.Modifier.Length.forTurn) && modifier2.length == Item2.Modifier.Length.twoTurns)
			{
				modifier2.length = Item2.Modifier.Length.forTurn;
			}
		}
		int num = 0;
		while (num < this.activeItemStatusEffects.Count)
		{
			Item2.ItemStatusEffect itemStatusEffect = this.activeItemStatusEffects[num];
			if (!modifierLengths.Contains(Item2.Modifier.Length.forTurn) || itemStatusEffect.length != Item2.ItemStatusEffect.Length.turns)
			{
				goto IL_032D;
			}
			itemStatusEffect.num += value;
			if (itemStatusEffect.num > 0)
			{
				goto IL_032D;
			}
			this.RemoveStatusEffect(num);
			num--;
			IL_03AD:
			num++;
			continue;
			IL_032D:
			if (modifierLengths.Contains(Item2.Modifier.Length.forCombat) && itemStatusEffect.length == Item2.ItemStatusEffect.Length.combats)
			{
				itemStatusEffect.num += value;
				if (itemStatusEffect.num <= 0)
				{
					this.RemoveStatusEffect(num);
					num--;
					goto IL_03AD;
				}
			}
			if (modifierLengths.Contains(Item2.Modifier.Length.whileActive) && itemStatusEffect.length == Item2.ItemStatusEffect.Length.whileActive)
			{
				this.RemoveStatusEffect(num);
				num--;
				goto IL_03AD;
			}
			if (modifierLengths.Contains(Item2.Modifier.Length.whileItemIsInInventory) && itemStatusEffect.length == Item2.ItemStatusEffect.Length.whileItemIsInInventory)
			{
				this.RemoveStatusEffect(num);
				num--;
				goto IL_03AD;
			}
			goto IL_03AD;
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0004C888 File Offset: 0x0004AA88
	public void Cleanse()
	{
		if (this.itemType.Contains(Item2.ItemType.Curse))
		{
			this.ChangeStatusEffectValue(Item2.ItemStatusEffect.Type.Cleansed, Item2.ItemStatusEffect.Length.permanent, 1);
			if (this.spriteRenderer)
			{
				EffectParticleSystem.Instance.CopySprite(this.spriteRenderer, EffectParticleSystem.ParticleType.cleanse);
			}
		}
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0004C8C1 File Offset: 0x0004AAC1
	private void AddStatusEffect(Item2.ItemStatusEffect itemStatusEffect)
	{
		this.activeItemStatusEffects.Add(itemStatusEffect);
		this.ReconsiderForGhostRoutine();
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0004C8D5 File Offset: 0x0004AAD5
	private void RemoveStatusEffect(int i)
	{
		this.activeItemStatusEffects.RemoveAt(i);
		this.ReconsiderForGhostRoutine();
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0004C8EC File Offset: 0x0004AAEC
	public void ChangeStatusEffectValue(Item2.ItemStatusEffect.Type type, Item2.ItemStatusEffect.Length length, int value)
	{
		for (int i = 0; i < this.activeItemStatusEffects.Count; i++)
		{
			Item2.ItemStatusEffect itemStatusEffect = this.activeItemStatusEffects[i];
			if (itemStatusEffect.type == type && itemStatusEffect.length == length)
			{
				itemStatusEffect.num += value;
				if (itemStatusEffect.num < 0)
				{
					this.RemoveStatusEffect(i);
					return;
				}
			}
		}
		if (value >= 0)
		{
			this.AddStatusEffect(new Item2.ItemStatusEffect
			{
				type = type,
				length = length,
				num = value
			});
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0004C974 File Offset: 0x0004AB74
	public void RemoveStatusEffect(Item2.ItemStatusEffect.Type type, Item2.ItemStatusEffect.Length length)
	{
		for (int i = 0; i < this.activeItemStatusEffects.Count; i++)
		{
			Item2.ItemStatusEffect itemStatusEffect = this.activeItemStatusEffects[i];
			if (itemStatusEffect.type == type && itemStatusEffect.length == length)
			{
				this.RemoveStatusEffect(i);
				i--;
			}
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0004C9C4 File Offset: 0x0004ABC4
	public bool CheckForStatusEffect(Item2.ItemStatusEffect.Type type)
	{
		foreach (Item2.ItemStatusEffect itemStatusEffect in this.activeItemStatusEffects)
		{
			if (type == itemStatusEffect.type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0004CA20 File Offset: 0x0004AC20
	public int GetStatusEffectValue(Item2.ItemStatusEffect.Type type)
	{
		foreach (Item2.ItemStatusEffect itemStatusEffect in this.activeItemStatusEffects)
		{
			if (type == itemStatusEffect.type)
			{
				return itemStatusEffect.num;
			}
		}
		return -1;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0004CA84 File Offset: 0x0004AC84
	public List<GameObject> GetStatusEffectPrefabs(Item2.ItemStatusEffect.Type type)
	{
		foreach (Item2.ItemStatusEffect itemStatusEffect in this.activeItemStatusEffects)
		{
			if (type == itemStatusEffect.type)
			{
				return itemStatusEffect.prefabs;
			}
		}
		return new List<GameObject>();
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0004CAEC File Offset: 0x0004ACEC
	private static List<Item2> SortByDistanceY(List<Item2> objects)
	{
		return objects.OrderBy((Item2 x) => (x.transform.position.y + x.itemMovement.size.y / 2f) * -1f).ToList<Item2>();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0004CB18 File Offset: 0x0004AD18
	private static List<Item2> SortByDistance(List<Item2> objects)
	{
		return objects.OrderBy((Item2 x) => x.transform.position.x + x.itemMovement.size.x / 2f).ToList<Item2>();
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0004CB44 File Offset: 0x0004AD44
	public static List<Transform> SortTransformsByPosition(List<Transform> transforms)
	{
		transforms = transforms.OrderBy((Transform x) => Mathf.Ceil(x.position.x)).ToList<Transform>();
		transforms = transforms.OrderBy((Transform x) => Mathf.Ceil(x.position.y) * -1f).ToList<Transform>();
		return transforms;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0004CBAA File Offset: 0x0004ADAA
	public static List<Item2> SortItemsByPosition(List<Item2> items)
	{
		items = Item2.SortByDistance(items);
		items = Item2.SortByDistanceY(items);
		return items;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0004CBC0 File Offset: 0x0004ADC0
	public static List<Item2> SortItemsByPositionSimple(List<Item2> items)
	{
		items = items.OrderBy((Item2 x) => Mathf.Ceil(x.transform.position.x) * 1f).ToList<Item2>();
		items = items.OrderBy((Item2 x) => Mathf.Ceil(x.transform.position.y) * -1f).ToList<Item2>();
		return items;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0004CC28 File Offset: 0x0004AE28
	public static int GetEffectValues(Item2.Effect.Type type)
	{
		int num = 0;
		foreach (Item2 item in Item2.GetAllItemsInGrid())
		{
			if (item && !item.destroyed && !item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
			{
				foreach (Item2.EffectTotal effectTotal in item.effectTotals)
				{
					if (effectTotal.effect.type == type)
					{
						num += Mathf.RoundToInt(effectTotal.effect.value);
					}
				}
			}
		}
		return num;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0004CCF0 File Offset: 0x0004AEF0
	public static int GetItemStatusEffectValue(Item2.ItemStatusEffect.Type type)
	{
		int num = -999;
		foreach (Item2 item in Item2.GetAllItemsInGrid())
		{
			foreach (Item2.ItemStatusEffect itemStatusEffect in item.activeItemStatusEffects)
			{
				if (itemStatusEffect.type == type)
				{
					if (num == -999)
					{
						num = 0;
					}
					num += Mathf.RoundToInt((float)itemStatusEffect.num);
				}
			}
		}
		return num;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0004CDA0 File Offset: 0x0004AFA0
	public static bool GetItemStatusEffectBool(Item2.ItemStatusEffect.Type type)
	{
		return Item2.GetItemStatusEffectValue(type) != -999;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0004CDB4 File Offset: 0x0004AFB4
	public static bool UseAllItemsIndirectWithStatusEffect(Item2.ItemStatusEffect.Type itemStatusEffect, Transform parentGrid = null)
	{
		List<Item2> itemsWithStatusEffect = Item2.GetItemsWithStatusEffect(itemStatusEffect, parentGrid, false);
		bool flag = false;
		foreach (Item2 item in itemsWithStatusEffect)
		{
			if (item && item.CanBeUsedActive(false, item.costs, true, true, null, false))
			{
				GameFlowManager main = GameFlowManager.main;
				main.StartCoroutine(main.UseItem(item, true, false, Item2.PlayerAnimation.UseDefault, false, false));
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0004CE3C File Offset: 0x0004B03C
	public static bool UseItemIndirectWithStatusEffect(Item2.ItemStatusEffect.Type itemStatusEffect, Transform parentGrid = null)
	{
		foreach (Item2 item in Item2.GetItemsWithStatusEffect(itemStatusEffect, parentGrid, false))
		{
			if (item && item.CanBeUsedActive(false, item.costs, true, true, null, false))
			{
				GameFlowManager main = GameFlowManager.main;
				main.StartCoroutine(main.UseItem(item, true, false, Item2.PlayerAnimation.UseDefault, false, false));
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0004CEC4 File Offset: 0x0004B0C4
	public static List<Item2> FilterItemsByInventory(List<Item2> items, Transform parentGrid)
	{
		if (parentGrid == null)
		{
			return items;
		}
		List<Item2> list = new List<Item2>(items);
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (!(item.parentInventoryGrid == parentGrid) && !(item.lastParentInventoryGrid == parentGrid))
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return list;
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0004CF24 File Offset: 0x0004B124
	public static Item2 GetItemByName(string name)
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(name))
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0004CF90 File Offset: 0x0004B190
	public static Item2 GetItemWithStatusEffect(Item2.ItemStatusEffect.Type itemStatusEffect, Transform parentGrid = null, bool allowOutside = false)
	{
		List<Item2> list = Item2.GetItemsWithStatusEffect(itemStatusEffect, parentGrid, allowOutside);
		if (parentGrid)
		{
			list = Item2.FilterItemsByInventory(list, parentGrid);
		}
		if (list.Count == 0)
		{
			return null;
		}
		int num = Random.Range(0, list.Count);
		return list[num];
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0004CFD4 File Offset: 0x0004B1D4
	public static bool CheckForItemOfName(Item2 itemToCheck)
	{
		foreach (Item2 item in Item2.GetAllItemsIncludingPouches())
		{
			if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(itemToCheck.name))
			{
				ModdedItem component = item.GetComponent<ModdedItem>();
				ModdedItem component2 = itemToCheck.GetComponent<ModdedItem>();
				if (component == null && component2 == null)
				{
					return true;
				}
				if (component == null == (component2 == null) && component.fromModpack.internalName == component2.fromModpack.internalName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0004D09C File Offset: 0x0004B29C
	public static List<Item2> GetItemsWithStatusEffectInThisList(List<Item2> items, Item2.ItemStatusEffect.Type itemStatusEffect)
	{
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in items)
		{
			if (item.CheckForStatusEffect(itemStatusEffect))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0004D0FC File Offset: 0x0004B2FC
	public static List<Item2> GetItemsWithStatusEffect(Item2.ItemStatusEffect.Type itemStatusEffect, Transform parentGrid = null, bool allowOutside = false)
	{
		List<Item2> allItemsInGrid = Item2.GetAllItemsInGrid();
		if (allowOutside)
		{
			allItemsInGrid = Item2.GetAllItems();
		}
		return (from x in Item2.FilterItemsByInventory(Item2.GetItemsWithStatusEffectInThisList(allItemsInGrid, itemStatusEffect), parentGrid)
			where !x.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled) && x.itemMovement && !x.itemMovement.inPouch
			select x).ToList<Item2>();
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0004D150 File Offset: 0x0004B350
	public static List<Item2> RemoveItemsInGrid(List<Item2> items)
	{
		List<Item2> list = new List<Item2>();
		for (int i = 0; i < items.Count; i++)
		{
			Item2 item = items[i];
			if (item.itemMovement && !item.itemMovement.inGrid && !item.destroyed && !item.itemMovement.isDragging && GameManager.main.reorgnizeItem != item.gameObject && item.itemMovement.moveToItemTransform)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0004D1D7 File Offset: 0x0004B3D7
	public static List<Item2> GetAllItemsIncludingPouches()
	{
		List<Item2> list = Item2.GetAllItems();
		list.AddRange(ItemPouch.GetAllItem2sFromPouches());
		return list;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0004D1EC File Offset: 0x0004B3EC
	public static List<Item2> GetAllItems()
	{
		List<Item2> list = new List<Item2>(Item2.allItems);
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (!item || !item.itemMovement || item.destroyed)
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return list;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0004D248 File Offset: 0x0004B448
	public static List<Item2> GetAllItemsOutsideGrid()
	{
		List<Item2> list = new List<Item2>(Item2.allItems);
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (!item || !item.itemMovement || item.destroyed || item.itemMovement.inGrid || item.itemType.Contains(Item2.ItemType.Carving))
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return Item2.SortItemsByPosition(list);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0004D2C4 File Offset: 0x0004B4C4
	public static void DestroyAllItemsOutsideGrid()
	{
		foreach (Item2 item in Item2.GetAllItemsOutsideGrid())
		{
			ItemMovement component = item.GetComponent<ItemMovement>();
			if (component)
			{
				component.DelayDestroy();
			}
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0004D324 File Offset: 0x0004B524
	public static List<Item2> GetAllItemsInGrid()
	{
		List<Item2> list = new List<Item2>(Item2.allItems);
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (!item || !item.itemMovement || item.destroyed || (!item.itemMovement.inGrid && !item.itemType.Contains(Item2.ItemType.Carving)))
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return Item2.SortItemsByPosition(list);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
	public static void SetAllItemColors()
	{
		foreach (Item2 item in Item2.allItems)
		{
			item.SetColor();
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0004D3F0 File Offset: 0x0004B5F0
	public GameObject ShowCard(Sprite spriteOverride)
	{
		if (!this.itemMovement)
		{
			this.itemMovement = base.GetComponent<ItemMovement>();
			if (!this.itemMovement)
			{
				this.itemMovement = base.GetComponentInParent<ItemMovement>();
			}
			if (!this.itemMovement)
			{
				return null;
			}
		}
		return this.itemMovement.ShowCardDirect(this, spriteOverride);
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0004D44C File Offset: 0x0004B64C
	public void SetColor()
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.gameFlowManager)
		{
			this.gameFlowManager = GameFlowManager.main;
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (!this.itemMovement)
		{
			this.itemMovement = base.GetComponent<ItemMovement>();
		}
		if (!this.cR8Manager)
		{
			this.cR8Manager = CR8Manager.instance;
		}
		if (!this.tutorialManager)
		{
			this.tutorialManager = TutorialManager.main;
		}
		if (this.cR8Manager && this.cR8Manager.isTesting)
		{
			this.spriteRenderer.color = Color.white;
		}
		Carving component = base.GetComponent<Carving>();
		base.GetComponent<EnergyEmitter>();
		if (this.gameManager.dead)
		{
			this.spriteRenderer.color = Color.white;
			this.setMyColor = null;
			return;
		}
		if (this.gameManager.reorgnizeItem == base.gameObject)
		{
			this.spriteRenderer.color = Color.white;
			this.setMyColor = null;
			return;
		}
		if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.organizeChest)
		{
			this.spriteRenderer.color = Color.white;
			this.setMyColor = null;
			return;
		}
		if (this.gameFlowManager.selectedItem)
		{
			if (this.gameFlowManager.selectedItem == this)
			{
				this.spriteRenderer.color = Color.white;
			}
			else if (this.canBeComboed)
			{
				this.spriteRenderer.color = Color.white;
			}
			else
			{
				this.spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}
		else if (component && !this.itemMovement.inGrid)
		{
			if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle || ((Item2.GetCurrentCost(Item2.Cost.Type.gold, component.summoningCosts) < 0 || Item2.GetCurrentCost(Item2.Cost.Type.gold, component.summoningCosts) <= this.gameManager.GetCurrentGold()) && (Item2.GetCurrentCost(Item2.Cost.Type.energy, component.summoningCosts) < 0 || Item2.GetCurrentCost(Item2.Cost.Type.energy, component.summoningCosts) <= this.player.AP)))
			{
				this.spriteRenderer.color = Color.white;
			}
			else
			{
				this.spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}
		else
		{
			int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.mana, this.costs);
			if (this.itemMovement.inGrid && currentCost > 0 && !this.gameFlowManager.TestForMana(this, currentCost))
			{
				this.spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
			else if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.choose && this.gameManager && this.gameManager.eventButton && this.gameManager.eventButton.requirement == EventButton.Requirements.specificItemSacrifice && Item2.GetDisplayName(base.name) == Item2.GetDisplayName(this.gameManager.eventButton.requiredItem.name))
			{
				this.spriteRenderer.color = Color.white;
			}
			else if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.choose && this.gameManager && this.gameManager.eventButton && ((!Item2.ShareItemTypes(this.gameManager.eventButton.requiredItemType, this.itemType) && !this.gameManager.eventButton.requiredItemType.Contains(Item2.ItemType.Any)) || this.gameManager.eventButton.requirement == EventButton.Requirements.specificItemSacrifice))
			{
				this.spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
			else if (this.gameManager.inSpecialReorg && !this.gameManager.itemsForSpecialReorganization.Contains(base.gameObject) && this.gameManager.itemsForSpecialReorganization.Count > 0)
			{
				this.spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
			else if ((this.gameManager.inventoryPhase != GameManager.InventoryPhase.locked && this.gameManager.inventoryPhase != GameManager.InventoryPhase.inCombatMove) || ((!this.petItem || this.petItem.combatPet) && this.CanBeUsedActive(false, this.costs, true, true, null, false)) || (this.petItem && !this.petItem.combatPet && this.CanBeUsedActive(false, this.petItem.summoningCosts, true, true, null, false)))
			{
				this.spriteRenderer.color = Color.white;
			}
			else if (this.itemType.Contains(Item2.ItemType.Carving))
			{
				this.spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
			}
			else
			{
				this.spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
		}
		foreach (object obj in this.itemMovement.itemBackgroundBordersParent)
		{
			ItemBorderBackground component2 = ((Transform)obj).GetComponent<ItemBorderBackground>();
			if (component2)
			{
				component2.SetColor();
			}
		}
		this.setMyColor = null;
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0004DA20 File Offset: 0x0004BC20
	public bool CanBeUsedActive(bool showReason, List<Item2.Cost> costs, bool includeItemLimites = true, bool includeIndirectUse = false, List<SpecificConditionToUse.ConditionTime> conditionTimes = null, bool dontShowNegative = false)
	{
		int num = this.player.AP;
		EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
		if (this.player.characterName == Character.CharacterName.CR8 && EnergyBall.startedByEnergyBall)
		{
			if (currentEnergyBall)
			{
				num = currentEnergyBall.energyValue;
			}
			else
			{
				num = 0;
			}
		}
		if (ItemPouch.FindPouchForItem(base.gameObject) != null)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
			}
			return false;
		}
		Status statusFromInventory = PetMaster.GetStatusFromInventory(this.parentInventoryGrid, this.player);
		if (statusFromInventory == null)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm52"));
			}
			return false;
		}
		if (statusFromInventory != this.player.stats && statusFromInventory.health <= 0)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm53"));
			}
			return false;
		}
		statusFromInventory != this.player.stats;
		if (this.petItem && this.petItem.combatPet)
		{
			num += this.petItem.currentAP;
		}
		if (this.gameFlowManager.selectedItem != null && !this.canBeComboed)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm24"));
			}
			return false;
		}
		if (Item2.GetCosts(costs).Count == 0)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
			}
			return false;
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.canBeUsedIndirectly, costs) >= 0 && !includeIndirectUse)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
			}
			return false;
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.energy, costs) > num)
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
			}
			return false;
		}
		return this.CanBeUsed(showReason, costs, includeItemLimites, conditionTimes);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0004DC80 File Offset: 0x0004BE80
	public static int CalculateGoldCost(int value)
	{
		Store store = Object.FindObjectOfType<Store>();
		if (store)
		{
			int eventPropertyValue = store.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.increaseCost);
			if (eventPropertyValue > 0)
			{
				value += eventPropertyValue;
			}
		}
		return value;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
	public static void DetractCosts(Item2 activeItem, List<Item2.Cost> costs, List<Item2.Cost.Type> costTypes = null)
	{
		if (costTypes == null)
		{
			costTypes = new List<Item2.Cost.Type>
			{
				Item2.Cost.Type.energy,
				Item2.Cost.Type.mana,
				Item2.Cost.Type.gold
			};
		}
		Status statusFromInventory = PetMaster.GetStatusFromInventory(activeItem.parentInventoryGrid, Player.main);
		if (Item2.GetCurrentCost(Item2.Cost.Type.energy, costs) > 0 && costTypes.Contains(Item2.Cost.Type.energy) && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			EnergyBall currentEnergyBall = EnergyBall.GetCurrentEnergyBall();
			if (currentEnergyBall)
			{
				currentEnergyBall.ChangeEnergy(Item2.GetCurrentCost(Item2.Cost.Type.energy, costs) * -1);
			}
			else if (statusFromInventory == Player.main.stats)
			{
				Player.main.ChangeAP(Item2.GetCurrentCost(Item2.Cost.Type.energy, costs) * -1);
			}
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.gold, costs) > 0 && costTypes.Contains(Item2.Cost.Type.gold) && (Player.main.characterName != Character.CharacterName.CR8 || (CR8Manager.instance && !CR8Manager.instance.isTesting)))
		{
			int num = Item2.CalculateGoldCost(Item2.GetCurrentCost(Item2.Cost.Type.gold, costs));
			GameManager.main.ChangeGold(num * -1);
		}
		int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.mana, costs);
		if (currentCost > 0 && costTypes.Contains(Item2.Cost.Type.mana))
		{
			ConnectionManager.main.ChangePowerInManaNetwork(activeItem, currentCost * -1);
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0004DDCC File Offset: 0x0004BFCC
	public bool CanAffordCosts(List<Item2.Cost> costs)
	{
		if (!this.player || !this.gameManager || !this.gameFlowManager)
		{
			return false;
		}
		foreach (Item2.Cost cost in costs)
		{
			if (cost.type == Item2.Cost.Type.energy && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
			{
				if (cost.currentValue > this.player.AP)
				{
					return false;
				}
			}
			else if (cost.type == Item2.Cost.Type.gold)
			{
				if (cost.currentValue > this.gameManager.GetCurrentGold())
				{
					return false;
				}
			}
			else if (cost.type == Item2.Cost.Type.mana && cost.currentValue > ConnectionManager.main.SumAvailableMana(this))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0004DEAC File Offset: 0x0004C0AC
	public bool CanBeUsed(bool showReason, List<Item2.Cost> costs, bool includeItemLimits = true, List<SpecificConditionToUse.ConditionTime> conditionTimes = null)
	{
		if (this.petItem)
		{
			List<CombatPet> combatPets = CombatPet.combatPets;
			if (this.petItem && this.petItem.health <= 0)
			{
				if (showReason)
				{
					SoundManager.main.PlaySFX("negative");
					this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm53"));
				}
				return false;
			}
			if (combatPets.Count >= 3 && !this.petItem.combatPet)
			{
				if (showReason)
				{
					SoundManager.main.PlaySFX("negative");
					this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm55"));
				}
				return false;
			}
		}
		foreach (SpecificConditionToUse specificConditionToUse in base.GetComponentsInChildren<SpecificConditionToUse>())
		{
			if (!specificConditionToUse.CanBeUsed(conditionTimes, null))
			{
				if (showReason)
				{
					if (specificConditionToUse.explanationKey.Length > 1)
					{
						string text = LangaugeManager.main.GetTextByKey(specificConditionToUse.explanationKey);
						text = text.Replace("/x", specificConditionToUse.value.ToString());
						this.gameManager.CreatePopUp(text);
					}
					else
					{
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"));
					}
				}
				return false;
			}
		}
		int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.mana, costs);
		if (currentCost > 0 && !this.gameFlowManager.TestForMana(this, currentCost))
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm26"));
			}
			return false;
		}
		if (Item2.GetCurrentCost(Item2.Cost.Type.gold, costs) > 0 && Item2.GetCurrentCost(Item2.Cost.Type.gold, costs) > this.gameManager.GetCurrentGold())
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27"));
			}
			return false;
		}
		if (this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.isActivated))
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm50"));
			}
			return false;
		}
		if (includeItemLimits)
		{
			foreach (Item2.LimitedUses limitedUses in this.usesLimits)
			{
				if (limitedUses.currentValue <= 0f)
				{
					if (limitedUses.type == Item2.LimitedUses.Type.perCombat && showReason)
					{
						SoundManager.main.PlaySFX("negative");
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm28"));
					}
					if (limitedUses.type == Item2.LimitedUses.Type.perTurn && showReason)
					{
						SoundManager.main.PlaySFX("negative");
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm29"));
					}
					if (limitedUses.type == Item2.LimitedUses.Type.total && showReason)
					{
						SoundManager.main.PlaySFX("negative");
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm30"));
					}
					return false;
				}
			}
		}
		if (this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
		{
			if (showReason)
			{
				SoundManager.main.PlaySFX("negative");
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm31"));
			}
			return false;
		}
		return true;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0004E200 File Offset: 0x0004C400
	private void ReplaceSelfWithPrefab(Item2.CreateEffect createEffect)
	{
		for (int i = 0; i < createEffect.itemsToCreate.Count; i++)
		{
			if (createEffect.itemsToCreate[i] == base.gameObject && this.gameManager)
			{
				foreach (Item2 item in this.gameManager.itemsToSpawn)
				{
					if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(base.gameObject.name))
					{
						createEffect.itemsToCreate[i] = item.gameObject;
						break;
					}
				}
			}
		}
		if (createEffect.raritiesToCreate.Count == 0)
		{
			createEffect.raritiesToCreate = new List<Item2.Rarity>
			{
				Item2.Rarity.Common,
				Item2.Rarity.Uncommon,
				Item2.Rarity.Rare,
				Item2.Rarity.Legendary
			};
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0004E300 File Offset: 0x0004C500
	public void StartProperties()
	{
		if (this.oneOfAKindType == Item2.OneOfAKindType.OnePerRun && !this.CheckForStatusEffect(Item2.ItemStatusEffect.Type.unique))
		{
			Item2.Effect effect = new Item2.Effect();
			effect.type = Item2.Effect.Type.ItemStatusEffect;
			Item2.ItemStatusEffect itemStatusEffect = new Item2.ItemStatusEffect();
			itemStatusEffect.type = Item2.ItemStatusEffect.Type.unique;
			itemStatusEffect.length = Item2.ItemStatusEffect.Length.permanent;
			effect.itemStatusEffect = new List<Item2.ItemStatusEffect> { itemStatusEffect };
			Item2.ApplyItemStatusEffect(this, effect, "");
		}
		foreach (Item2.LimitedUses limitedUses in this.usesLimits)
		{
			if (limitedUses.currentValue == -999f)
			{
				limitedUses.currentValue = limitedUses.value;
			}
		}
		foreach (Item2.MovementEffect movementEffect in this.movementEffects)
		{
			this.SetTriggerToSelf(movementEffect.trigger);
		}
		foreach (Item2.CombattEffect combattEffect in this.combatEffects)
		{
			this.SetTriggerToSelf(combattEffect.trigger);
		}
		foreach (Item2.CreateEffect createEffect in this.createEffects)
		{
			this.ReplaceSelfWithPrefab(createEffect);
			this.SetTriggerToSelf(createEffect.trigger);
		}
		foreach (Item2.Modifier modifier in this.modifiers)
		{
			this.SetTriggerToSelf(modifier.trigger);
			if (modifier.areasToModify.Count == 0)
			{
				modifier.areasToModify = new List<Item2.Area> { Item2.Area.self };
			}
			if (modifier.typesToModify.Count == 0)
			{
				modifier.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
			}
			modifier.name = this.displayName;
			if (modifier.origin.Length == 0)
			{
				modifier.origin = "self";
				modifier.lengthForThisModifier = Item2.Modifier.Length.permanent;
			}
		}
		foreach (Item2.AddModifier addModifier in this.addModifiers)
		{
			this.SetTriggerToSelf(addModifier.trigger);
			Item2.Modifier modifier2 = addModifier.modifier;
			this.SetTriggerToSelf(modifier2.trigger);
			if (modifier2.areasToModify.Count == 0)
			{
				modifier2.areasToModify = new List<Item2.Area> { Item2.Area.self };
			}
			if (modifier2.typesToModify.Count == 0)
			{
				modifier2.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
			}
			if (modifier2.name == "")
			{
				modifier2.name = this.displayName;
			}
			modifier2.lengthForThisModifier = addModifier.lengthForThisModifier;
			modifier2.origin = Item2.GetDisplayName(base.gameObject.name);
		}
		foreach (Item2.EnergyEffect energyEffect in this.energyEffects)
		{
			this.SetTriggerToSelf(energyEffect.trigger);
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0004E698 File Offset: 0x0004C898
	private void SetTriggerToSelf(Item2.Trigger trigger)
	{
		if (trigger.areas.Count == 0)
		{
			trigger.areas = new List<Item2.Area> { Item2.Area.self };
		}
		if (trigger.types.Count == 0)
		{
			trigger.types = new List<Item2.ItemType> { Item2.ItemType.Any };
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0004E6D8 File Offset: 0x0004C8D8
	public static string GetDisplayName(string name)
	{
		string text = name.ToUpper();
		if (text.IndexOf("\\") != -1)
		{
			text = text.Substring(text.LastIndexOf("\\") + 1);
		}
		if (text.IndexOf("(") != -1)
		{
			text = text.Substring(0, text.IndexOf("("));
		}
		if (text.Contains("VARIANT") && text.Length > 7)
		{
			text = text.Substring(0, text.IndexOf("VARIANT"));
		}
		if (text.Contains("OVERWORLD"))
		{
			text = text.Remove(text.IndexOf("OVERWORLD"), 9);
		}
		return text.Trim();
	}

	// Token: 0x040005B5 RID: 1461
	public static List<Item2> allItems = new List<Item2>();

	// Token: 0x040005B6 RID: 1462
	[Header("---------------------------Meta Stats---------------------------")]
	[SerializeField]
	public List<Overworld_ResourceManager.Resource> resourcesToGet = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x040005B7 RID: 1463
	[SerializeField]
	public MetaProgressSaveManager.MetaProgressMarker markerIfBroughtHome;

	// Token: 0x040005B8 RID: 1464
	[SerializeField]
	public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

	// Token: 0x040005B9 RID: 1465
	[SerializeField]
	public Item2.OneOfAKindType oneOfAKindType;

	// Token: 0x040005BA RID: 1466
	[SerializeField]
	public bool isStandardOnlyAfterUnlock;

	// Token: 0x040005BB RID: 1467
	[SerializeField]
	public Item2.AvailabilityType quickGameAvailabilityType;

	// Token: 0x040005BC RID: 1468
	[SerializeField]
	public Item2.AvailabilityType storyModeAvailabilityType;

	// Token: 0x040005BD RID: 1469
	[SerializeField]
	public EventManager.EventType eventTypeAvailabilityType;

	// Token: 0x040005BE RID: 1470
	[Header("---------------------------Universal Stats---------------------------")]
	[SerializeField]
	public string createdBy = "";

	// Token: 0x040005BF RID: 1471
	public Transform parentInventoryGrid;

	// Token: 0x040005C0 RID: 1472
	public Transform lastParentInventoryGrid;

	// Token: 0x040005C1 RID: 1473
	[SerializeField]
	public bool isAvailableInDemo;

	// Token: 0x040005C2 RID: 1474
	[SerializeField]
	public bool isStandard = true;

	// Token: 0x040005C3 RID: 1475
	[SerializeField]
	public bool canBeSpawnedStandardEver = true;

	// Token: 0x040005C4 RID: 1476
	[SerializeField]
	public bool isInAtlas = true;

	// Token: 0x040005C5 RID: 1477
	[HideInInspector]
	public string displayName;

	// Token: 0x040005C6 RID: 1478
	[SerializeField]
	public List<Item2.ItemType> itemType = new List<Item2.ItemType> { Item2.ItemType.Weapon };

	// Token: 0x040005C7 RID: 1479
	[SerializeField]
	public List<Item2.ItemGrouping> itemGroupings = new List<Item2.ItemGrouping>();

	// Token: 0x040005C8 RID: 1480
	[SerializeField]
	public List<Item2.SpawnGrouping> spawnGroupings = new List<Item2.SpawnGrouping>();

	// Token: 0x040005C9 RID: 1481
	[SerializeField]
	public bool hideRarity;

	// Token: 0x040005CA RID: 1482
	[SerializeField]
	public Item2.Rarity rarity;

	// Token: 0x040005CB RID: 1483
	[SerializeField]
	public int originalCost = -999;

	// Token: 0x040005CC RID: 1484
	[NonSerialized]
	public int cost = -999;

	// Token: 0x040005CD RID: 1485
	public bool isOwned;

	// Token: 0x040005CE RID: 1486
	[SerializeField]
	public Item2.PlayType playType;

	// Token: 0x040005CF RID: 1487
	[SerializeField]
	public List<Item2.Cost> costs;

	// Token: 0x040005D0 RID: 1488
	[NonSerialized]
	public int sigilEnergy;

	// Token: 0x040005D1 RID: 1489
	[SerializeField]
	public int charges = -999;

	// Token: 0x040005D2 RID: 1490
	[NonSerialized]
	public int currentCharges = -999;

	// Token: 0x040005D3 RID: 1491
	[SerializeField]
	public Item2 comboTarget;

	// Token: 0x040005D4 RID: 1492
	[SerializeField]
	public Item2.PlayerAnimation playerAnimation;

	// Token: 0x040005D5 RID: 1493
	[SerializeField]
	public Item2.SoundEffect soundEffect;

	// Token: 0x040005D6 RID: 1494
	[SerializeField]
	public List<string> descriptions;

	// Token: 0x040005D7 RID: 1495
	[SerializeField]
	public Item2.Area moveArea;

	// Token: 0x040005D8 RID: 1496
	[SerializeField]
	public Item2.ItemType mustBePlacedOnItemType = Item2.ItemType.Grid;

	// Token: 0x040005D9 RID: 1497
	[SerializeField]
	public Item2.ItemType mustBePlacedOnItemTypeInCombat = Item2.ItemType.Grid;

	// Token: 0x040005DA RID: 1498
	[SerializeField]
	public Item2.AreaDistance moveDistance;

	// Token: 0x040005DB RID: 1499
	[SerializeField]
	public bool showTextForMovement;

	// Token: 0x040005DC RID: 1500
	[Header("-------------------Availability--------------------")]
	[SerializeField]
	public List<DungeonLevel.Zone> validForZones;

	// Token: 0x040005DD RID: 1501
	[SerializeField]
	public List<Character.CharacterName> validForCharacters;

	// Token: 0x040005DE RID: 1502
	[Header("---------------------------Properties---------------------------")]
	[NonSerialized]
	public PetItem2 petItem;

	// Token: 0x040005DF RID: 1503
	[SerializeField]
	public List<Item2.CombattEffect> combatEffects;

	// Token: 0x040005E0 RID: 1504
	[SerializeField]
	public List<Item2.LimitedUses> usesLimits;

	// Token: 0x040005E1 RID: 1505
	[SerializeField]
	public List<Item2.CreateEffect> createEffects;

	// Token: 0x040005E2 RID: 1506
	[SerializeField]
	public List<Item2.Modifier> modifiers;

	// Token: 0x040005E3 RID: 1507
	[SerializeField]
	public List<Item2.MovementEffect> movementEffects;

	// Token: 0x040005E4 RID: 1508
	[HideInInspector]
	public List<Item2.Modifier> modifierModifiers;

	// Token: 0x040005E5 RID: 1509
	[SerializeField]
	public List<Item2.AddModifier> addModifiers;

	// Token: 0x040005E6 RID: 1510
	[SerializeField]
	public List<Item2.EnergyEffect> energyEffects = new List<Item2.EnergyEffect>();

	// Token: 0x040005E7 RID: 1511
	[SerializeField]
	public List<Item2.ItemStatusEffect> activeItemStatusEffects = new List<Item2.ItemStatusEffect>();

	// Token: 0x040005E8 RID: 1512
	[SerializeField]
	public List<ContextMenuButton.ContextMenuButtonAndCost> contextMenuOptions;

	// Token: 0x040005E9 RID: 1513
	[Header("---------------------------Debugging---------------------------")]
	public float offsetOfItemShape;

	// Token: 0x040005EA RID: 1514
	[SerializeField]
	public List<Item2.ModifierTotal> modifierTotals;

	// Token: 0x040005EB RID: 1515
	[SerializeField]
	public List<Item2.EffectTotal> effectTotals;

	// Token: 0x040005EC RID: 1516
	[SerializeField]
	public List<Item2.Modifier> appliedModifierModifiers;

	// Token: 0x040005ED RID: 1517
	[SerializeField]
	public List<Item2.Modifier> appliedModifiers;

	// Token: 0x040005EE RID: 1518
	[HideInInspector]
	private List<ScriptedTrigger> scriptedTriggers = new List<ScriptedTrigger>();

	// Token: 0x040005EF RID: 1519
	[HideInInspector]
	private SpriteRenderer spriteRenderer;

	// Token: 0x040005F0 RID: 1520
	[HideInInspector]
	public bool isDiscounted;

	// Token: 0x040005F1 RID: 1521
	[HideInInspector]
	public ItemMovement itemMovement;

	// Token: 0x040005F2 RID: 1522
	[HideInInspector]
	public GridObject gridObject;

	// Token: 0x040005F3 RID: 1523
	[HideInInspector]
	private Player player;

	// Token: 0x040005F4 RID: 1524
	[HideInInspector]
	private GameManager gameManager;

	// Token: 0x040005F5 RID: 1525
	[HideInInspector]
	private GameFlowManager gameFlowManager;

	// Token: 0x040005F6 RID: 1526
	[HideInInspector]
	private CR8Manager cR8Manager;

	// Token: 0x040005F7 RID: 1527
	[HideInInspector]
	public bool canBeComboed;

	// Token: 0x040005F8 RID: 1528
	[HideInInspector]
	public bool isChosenAsComboRecepient;

	// Token: 0x040005F9 RID: 1529
	[SerializeField]
	public bool isForSale;

	// Token: 0x040005FA RID: 1530
	[SerializeField]
	public bool destroyed;

	// Token: 0x040005FB RID: 1531
	[HideInInspector]
	public int lastNumberOfModifiers;

	// Token: 0x040005FC RID: 1532
	[HideInInspector]
	private TutorialManager tutorialManager;

	// Token: 0x040005FD RID: 1533
	private List<ScriptedTrigger> scripttedTriggers = new List<ScriptedTrigger>();

	// Token: 0x040005FE RID: 1534
	private bool checkedForScriptedTriggers;

	// Token: 0x040005FF RID: 1535
	private bool checkedForEnergyEmitterAndItDoesntExist;

	// Token: 0x04000600 RID: 1536
	private EnergyEmitter energyEmitter;

	// Token: 0x04000601 RID: 1537
	public Coroutine setMyColor;

	// Token: 0x02000328 RID: 808
	public enum Area
	{
		// Token: 0x0400129A RID: 4762
		self,
		// Token: 0x0400129B RID: 4763
		adjacent,
		// Token: 0x0400129C RID: 4764
		diagonal,
		// Token: 0x0400129D RID: 4765
		top,
		// Token: 0x0400129E RID: 4766
		bottom,
		// Token: 0x0400129F RID: 4767
		column,
		// Token: 0x040012A0 RID: 4768
		left,
		// Token: 0x040012A1 RID: 4769
		right,
		// Token: 0x040012A2 RID: 4770
		row,
		// Token: 0x040012A3 RID: 4771
		board,
		// Token: 0x040012A4 RID: 4772
		comboTarget,
		// Token: 0x040012A5 RID: 4773
		oneSpaceOver,
		// Token: 0x040012A6 RID: 4774
		connected,
		// Token: 0x040012A7 RID: 4775
		rowThenColumn,
		// Token: 0x040012A8 RID: 4776
		columnThenRow,
		// Token: 0x040012A9 RID: 4777
		rightRotational,
		// Token: 0x040012AA RID: 4778
		bottomRotation,
		// Token: 0x040012AB RID: 4779
		leftRotational,
		// Token: 0x040012AC RID: 4780
		topRotational,
		// Token: 0x040012AD RID: 4781
		undefined,
		// Token: 0x040012AE RID: 4782
		ItemEffectArea,
		// Token: 0x040012AF RID: 4783
		myPlaySpace,
		// Token: 0x040012B0 RID: 4784
		toteHand,
		// Token: 0x040012B1 RID: 4785
		diagonalLine,
		// Token: 0x040012B2 RID: 4786
		inThisPocket,
		// Token: 0x040012B3 RID: 4787
		inAnotherPocket,
		// Token: 0x040012B4 RID: 4788
		inAUniquePocket,
		// Token: 0x040012B5 RID: 4789
		connectedViaType,
		// Token: 0x040012B6 RID: 4790
		AnythingEvenOutOfGrid
	}

	// Token: 0x02000329 RID: 809
	public enum AreaDistance
	{
		// Token: 0x040012B8 RID: 4792
		all,
		// Token: 0x040012B9 RID: 4793
		closest,
		// Token: 0x040012BA RID: 4794
		adjacent,
		// Token: 0x040012BB RID: 4795
		zTop
	}

	// Token: 0x0200032A RID: 810
	public enum PlayType
	{
		// Token: 0x040012BD RID: 4797
		Active,
		// Token: 0x040012BE RID: 4798
		Combo,
		// Token: 0x040012BF RID: 4799
		Movable
	}

	// Token: 0x0200032B RID: 811
	public enum ItemType
	{
		// Token: 0x040012C1 RID: 4801
		Any,
		// Token: 0x040012C2 RID: 4802
		Grid,
		// Token: 0x040012C3 RID: 4803
		GridEmpty,
		// Token: 0x040012C4 RID: 4804
		Weapon,
		// Token: 0x040012C5 RID: 4805
		Armor,
		// Token: 0x040012C6 RID: 4806
		Shield,
		// Token: 0x040012C7 RID: 4807
		Accessory,
		// Token: 0x040012C8 RID: 4808
		Bow,
		// Token: 0x040012C9 RID: 4809
		Arrow,
		// Token: 0x040012CA RID: 4810
		Consumable,
		// Token: 0x040012CB RID: 4811
		Gold,
		// Token: 0x040012CC RID: 4812
		Mana,
		// Token: 0x040012CD RID: 4813
		Key,
		// Token: 0x040012CE RID: 4814
		Curse,
		// Token: 0x040012CF RID: 4815
		Ingredient,
		// Token: 0x040012D0 RID: 4816
		Wand,
		// Token: 0x040012D1 RID: 4817
		Book,
		// Token: 0x040012D2 RID: 4818
		Cleaver,
		// Token: 0x040012D3 RID: 4819
		Clothing,
		// Token: 0x040012D4 RID: 4820
		Helmet,
		// Token: 0x040012D5 RID: 4821
		Footwear,
		// Token: 0x040012D6 RID: 4822
		Fish,
		// Token: 0x040012D7 RID: 4823
		Relic,
		// Token: 0x040012D8 RID: 4824
		Glove,
		// Token: 0x040012D9 RID: 4825
		Magic,
		// Token: 0x040012DA RID: 4826
		Gem,
		// Token: 0x040012DB RID: 4827
		ManaStone,
		// Token: 0x040012DC RID: 4828
		Structure,
		// Token: 0x040012DD RID: 4829
		undefined,
		// Token: 0x040012DE RID: 4830
		Ring,
		// Token: 0x040012DF RID: 4831
		Rune,
		// Token: 0x040012E0 RID: 4832
		Sigil,
		// Token: 0x040012E1 RID: 4833
		Carving,
		// Token: 0x040012E2 RID: 4834
		Component,
		// Token: 0x040012E3 RID: 4835
		Potion,
		// Token: 0x040012E4 RID: 4836
		Core,
		// Token: 0x040012E5 RID: 4837
		Instrument,
		// Token: 0x040012E6 RID: 4838
		Shuriken,
		// Token: 0x040012E7 RID: 4839
		Plant,
		// Token: 0x040012E8 RID: 4840
		Melee,
		// Token: 0x040012E9 RID: 4841
		Drum,
		// Token: 0x040012EA RID: 4842
		Hammer,
		// Token: 0x040012EB RID: 4843
		Blessing,
		// Token: 0x040012EC RID: 4844
		Scary,
		// Token: 0x040012ED RID: 4845
		Pet,
		// Token: 0x040012EE RID: 4846
		Present,
		// Token: 0x040012EF RID: 4847
		Festive,
		// Token: 0x040012F0 RID: 4848
		Totem,
		// Token: 0x040012F1 RID: 4849
		Dart,
		// Token: 0x040012F2 RID: 4850
		Kin,
		// Token: 0x040012F3 RID: 4851
		Sap,
		// Token: 0x040012F4 RID: 4852
		Treat,
		// Token: 0x040012F5 RID: 4853
		Hazard,
		// Token: 0x040012F6 RID: 4854
		FloppyDisk,
		// Token: 0x040012F7 RID: 4855
		Etching,
		// Token: 0x040012F8 RID: 4856
		Hymn,
		// Token: 0x040012F9 RID: 4857
		Fragment,
		// Token: 0x040012FA RID: 4858
		Loot,
		// Token: 0x040012FB RID: 4859
		Gear,
		// Token: 0x040012FC RID: 4860
		Summer,
		// Token: 0x040012FD RID: 4861
		Sunshine
	}

	// Token: 0x0200032C RID: 812
	public enum ItemGrouping
	{
		// Token: 0x040012FF RID: 4863
		Cleavers,
		// Token: 0x04001300 RID: 4864
		Melee,
		// Token: 0x04001301 RID: 4865
		Magic,
		// Token: 0x04001302 RID: 4866
		Archery,
		// Token: 0x04001303 RID: 4867
		Fish,
		// Token: 0x04001304 RID: 4868
		Spikes,
		// Token: 0x04001305 RID: 4869
		Keys,
		// Token: 0x04001306 RID: 4870
		Poison,
		// Token: 0x04001307 RID: 4871
		Hammers,
		// Token: 0x04001308 RID: 4872
		Shuriken,
		// Token: 0x04001309 RID: 4873
		Charmer,
		// Token: 0x0400130A RID: 4874
		Structure,
		// Token: 0x0400130B RID: 4875
		Alchemy,
		// Token: 0x0400130C RID: 4876
		Cursed,
		// Token: 0x0400130D RID: 4877
		Berserker,
		// Token: 0x0400130E RID: 4878
		Flame,
		// Token: 0x0400130F RID: 4879
		Armored,
		// Token: 0x04001310 RID: 4880
		Gold,
		// Token: 0x04001311 RID: 4881
		Shield,
		// Token: 0x04001312 RID: 4882
		Dodger,
		// Token: 0x04001313 RID: 4883
		Destructibles,
		// Token: 0x04001314 RID: 4884
		Pacifist,
		// Token: 0x04001315 RID: 4885
		Scratch,
		// Token: 0x04001316 RID: 4886
		Festive,
		// Token: 0x04001317 RID: 4887
		TownLoot,
		// Token: 0x04001318 RID: 4888
		SunlightSummer
	}

	// Token: 0x0200032D RID: 813
	public enum SpawnGrouping
	{
		// Token: 0x0400131A RID: 4890
		unspecified,
		// Token: 0x0400131B RID: 4891
		relic1,
		// Token: 0x0400131C RID: 4892
		relic2,
		// Token: 0x0400131D RID: 4893
		relic3
	}

	// Token: 0x0200032E RID: 814
	public enum Rarity
	{
		// Token: 0x0400131F RID: 4895
		Common,
		// Token: 0x04001320 RID: 4896
		Uncommon,
		// Token: 0x04001321 RID: 4897
		Rare,
		// Token: 0x04001322 RID: 4898
		Legendary
	}

	// Token: 0x0200032F RID: 815
	public enum PlayerAnimation
	{
		// Token: 0x04001324 RID: 4900
		Attack,
		// Token: 0x04001325 RID: 4901
		OverheadAttack,
		// Token: 0x04001326 RID: 4902
		UseItem,
		// Token: 0x04001327 RID: 4903
		Block,
		// Token: 0x04001328 RID: 4904
		Hurt,
		// Token: 0x04001329 RID: 4905
		FireArrow,
		// Token: 0x0400132A RID: 4906
		Command,
		// Token: 0x0400132B RID: 4907
		UseDefault
	}

	// Token: 0x02000330 RID: 816
	public enum SoundEffect
	{
		// Token: 0x0400132D RID: 4909
		None,
		// Token: 0x0400132E RID: 4910
		cymbal,
		// Token: 0x0400132F RID: 4911
		flute,
		// Token: 0x04001330 RID: 4912
		guitar,
		// Token: 0x04001331 RID: 4913
		piano,
		// Token: 0x04001332 RID: 4914
		trumpet,
		// Token: 0x04001333 RID: 4915
		violin,
		// Token: 0x04001334 RID: 4916
		genericThud,
		// Token: 0x04001335 RID: 4917
		electricItem
	}

	// Token: 0x02000331 RID: 817
	[Serializable]
	public class Trigger
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x000BCF21 File Offset: 0x000BB121
		public static bool IsConstant(Item2.Trigger.ActionTrigger trigger)
		{
			return trigger == Item2.Trigger.ActionTrigger.constant || trigger == Item2.Trigger.ActionTrigger.constantEarly || trigger == Item2.Trigger.ActionTrigger.constantExtraEarly || trigger == Item2.Trigger.ActionTrigger.constantClearWhile;
		}

		// Token: 0x04001336 RID: 4918
		public List<Vector2> validEntrances = new List<Vector2>();

		// Token: 0x04001337 RID: 4919
		public Item2.Trigger.ActionTrigger trigger = Item2.Trigger.ActionTrigger.onUse;

		// Token: 0x04001338 RID: 4920
		public List<Item2.ItemType> types = new List<Item2.ItemType> { Item2.ItemType.Any };

		// Token: 0x04001339 RID: 4921
		public List<Item2.ItemType> excludedTypes = new List<Item2.ItemType>();

		// Token: 0x0400133A RID: 4922
		public List<Item2.Area> areas = new List<Item2.Area> { Item2.Area.self };

		// Token: 0x0400133B RID: 4923
		public Item2.AreaDistance areaDistance;

		// Token: 0x0400133C RID: 4924
		public string triggerOverrideKey = "";

		// Token: 0x0400133D RID: 4925
		public bool requiresActivation;

		// Token: 0x0400133E RID: 4926
		public bool canBeUsedDuringTest;

		// Token: 0x0200049E RID: 1182
		public enum ActionTrigger
		{
			// Token: 0x04001B0D RID: 6925
			constant,
			// Token: 0x04001B0E RID: 6926
			onUse,
			// Token: 0x04001B0F RID: 6927
			onCombo,
			// Token: 0x04001B10 RID: 6928
			onDestroy,
			// Token: 0x04001B11 RID: 6929
			onAdd,
			// Token: 0x04001B12 RID: 6930
			onRemove,
			// Token: 0x04001B13 RID: 6931
			onTurnStart,
			// Token: 0x04001B14 RID: 6932
			onTurnEnd,
			// Token: 0x04001B15 RID: 6933
			onCombatStart,
			// Token: 0x04001B16 RID: 6934
			onCombatEnd,
			// Token: 0x04001B17 RID: 6935
			onKillEnemy,
			// Token: 0x04001B18 RID: 6936
			onStart,
			// Token: 0x04001B19 RID: 6937
			onRotate,
			// Token: 0x04001B1A RID: 6938
			onOutOfUses,
			// Token: 0x04001B1B RID: 6939
			onComboSelect,
			// Token: 0x04001B1C RID: 6940
			[DocsGenerator.NoDocsAttribute]
			onComboUse,
			// Token: 0x04001B1D RID: 6941
			onTakeDamage,
			// Token: 0x04001B1E RID: 6942
			constantEarly,
			// Token: 0x04001B1F RID: 6943
			useEarly,
			// Token: 0x04001B20 RID: 6944
			onKillNonSummonEnemy,
			// Token: 0x04001B21 RID: 6945
			onMovePast,
			// Token: 0x04001B22 RID: 6946
			onMoveFinish,
			// Token: 0x04001B23 RID: 6947
			onMoveCollide,
			// Token: 0x04001B24 RID: 6948
			onFullCharge,
			// Token: 0x04001B25 RID: 6949
			onMoveCombat,
			// Token: 0x04001B26 RID: 6950
			onScratch,
			// Token: 0x04001B27 RID: 6951
			onSummonCarving,
			// Token: 0x04001B28 RID: 6952
			constantClearWhile,
			// Token: 0x04001B29 RID: 6953
			useLate,
			// Token: 0x04001B2A RID: 6954
			whenAttacked,
			// Token: 0x04001B2B RID: 6955
			onSummonCarvingLate,
			// Token: 0x04001B2C RID: 6956
			onSummonCarvingEarly,
			// Token: 0x04001B2D RID: 6957
			onDie,
			// Token: 0x04001B2E RID: 6958
			constantExtraEarly,
			// Token: 0x04001B2F RID: 6959
			onDiscard,
			// Token: 0x04001B30 RID: 6960
			onClearCarvings,
			// Token: 0x04001B31 RID: 6961
			[DocsGenerator.NoDocsAttribute]
			whenScripted,
			// Token: 0x04001B32 RID: 6962
			onSummonPet,
			// Token: 0x04001B33 RID: 6963
			onSummonAnyPet,
			// Token: 0x04001B34 RID: 6964
			onPetDies,
			// Token: 0x04001B35 RID: 6965
			onEnemyAsleep,
			// Token: 0x04001B36 RID: 6966
			whenZombied,
			// Token: 0x04001B37 RID: 6967
			whenAnEnemyIsDefeated,
			// Token: 0x04001B38 RID: 6968
			endOfActions,
			// Token: 0x04001B39 RID: 6969
			whenManaFlowsThrough,
			// Token: 0x04001B3A RID: 6970
			onFirstUse,
			// Token: 0x04001B3B RID: 6971
			whenLeftBehind,
			// Token: 0x04001B3C RID: 6972
			onAlternateUse,
			// Token: 0x04001B3D RID: 6973
			whenNotPlayed,
			// Token: 0x04001B3E RID: 6974
			onReorganize,
			// Token: 0x04001B3F RID: 6975
			recursive,
			// Token: 0x04001B40 RID: 6976
			clearActivatedItems,
			// Token: 0x04001B41 RID: 6977
			onUseUntilOverheat,
			// Token: 0x04001B42 RID: 6978
			onOverheat,
			// Token: 0x04001B43 RID: 6979
			onEnergyMove,
			// Token: 0x04001B44 RID: 6980
			onHeatReset,
			// Token: 0x04001B45 RID: 6981
			whenThisDestroys,
			// Token: 0x04001B46 RID: 6982
			onUseUntilOverheatLate,
			// Token: 0x04001B47 RID: 6983
			onMoveSuccess,
			// Token: 0x04001B48 RID: 6984
			onSale,
			// Token: 0x04001B49 RID: 6985
			whenEnemyRuns
		}
	}

	// Token: 0x02000332 RID: 818
	[Serializable]
	public class Effect
	{
		// Token: 0x060015FD RID: 5629 RVA: 0x000BCF98 File Offset: 0x000BB198
		public Item2.Effect Clone()
		{
			Item2.Effect effect = (Item2.Effect)base.MemberwiseClone();
			List<Item2.ItemStatusEffect> list = new List<Item2.ItemStatusEffect>();
			foreach (Item2.ItemStatusEffect itemStatusEffect in this.itemStatusEffect)
			{
				Item2.ItemStatusEffect itemStatusEffect2 = itemStatusEffect.Clone();
				itemStatusEffect2.showOnCard = true;
				list.Add(itemStatusEffect2);
			}
			effect.itemStatusEffect = list;
			return effect;
		}

		// Token: 0x0400133F RID: 4927
		public Item2.Effect.Type type;

		// Token: 0x04001340 RID: 4928
		public float value;

		// Token: 0x04001341 RID: 4929
		[NonSerialized]
		public float valueFromStatusEffects;

		// Token: 0x04001342 RID: 4930
		public Item2.Effect.Target target;

		// Token: 0x04001343 RID: 4931
		[NonSerialized]
		public string originName = "";

		// Token: 0x04001344 RID: 4932
		public Item2.Effect.MathematicalType mathematicalType;

		// Token: 0x04001345 RID: 4933
		[SerializeField]
		public List<Item2.ItemStatusEffect> itemStatusEffect = new List<Item2.ItemStatusEffect>();

		// Token: 0x04001346 RID: 4934
		public string effectOverrideKey = "";

		// Token: 0x0200049F RID: 1183
		public enum Type
		{
			// Token: 0x04001B4B RID: 6987
			Damage,
			// Token: 0x04001B4C RID: 6988
			Block,
			// Token: 0x04001B4D RID: 6989
			HP,
			// Token: 0x04001B4E RID: 6990
			AP,
			// Token: 0x04001B4F RID: 6991
			MaxHP,
			// Token: 0x04001B50 RID: 6992
			Luck,
			// Token: 0x04001B51 RID: 6993
			Poison,
			// Token: 0x04001B52 RID: 6994
			Regen,
			// Token: 0x04001B53 RID: 6995
			Spikes,
			// Token: 0x04001B54 RID: 6996
			Haste,
			// Token: 0x04001B55 RID: 6997
			Slow,
			// Token: 0x04001B56 RID: 6998
			Rage,
			// Token: 0x04001B57 RID: 6999
			Weak,
			// Token: 0x04001B58 RID: 7000
			resetUses,
			// Token: 0x04001B59 RID: 7001
			resetUsesPerCombat,
			// Token: 0x04001B5A RID: 7002
			resetUsesPerTurn,
			// Token: 0x04001B5B RID: 7003
			ToughHide,
			// Token: 0x04001B5C RID: 7004
			[DocsGenerator.NoDocsAttribute]
			xxxOLD2xxx,
			// Token: 0x04001B5D RID: 7005
			[DocsGenerator.NoDocsAttribute]
			xxxOLD3xxx,
			// Token: 0x04001B5E RID: 7006
			Destroy,
			// Token: 0x04001B5F RID: 7007
			Activate,
			// Token: 0x04001B60 RID: 7008
			AddToStack,
			// Token: 0x04001B61 RID: 7009
			ModifierMultiplier,
			// Token: 0x04001B62 RID: 7010
			Vampire,
			// Token: 0x04001B63 RID: 7011
			ItemStatusEffect,
			// Token: 0x04001B64 RID: 7012
			Mana,
			// Token: 0x04001B65 RID: 7013
			Dodge,
			// Token: 0x04001B66 RID: 7014
			AddCharge,
			// Token: 0x04001B67 RID: 7015
			ResetCharges,
			// Token: 0x04001B68 RID: 7016
			AllStatusEffects,
			// Token: 0x04001B69 RID: 7017
			AddDamageToScratch,
			// Token: 0x04001B6A RID: 7018
			AddToMaxMana,
			// Token: 0x04001B6B RID: 7019
			DrawToteCarvings,
			// Token: 0x04001B6C RID: 7020
			[DocsGenerator.NoDocsAttribute]
			xxxOldxxx,
			// Token: 0x04001B6D RID: 7021
			DiscardCarving,
			// Token: 0x04001B6E RID: 7022
			Burn,
			// Token: 0x04001B6F RID: 7023
			BanishCarving,
			// Token: 0x04001B70 RID: 7024
			ChangeCostOfReorganize,
			// Token: 0x04001B71 RID: 7025
			Charm,
			// Token: 0x04001B72 RID: 7026
			Sleep,
			// Token: 0x04001B73 RID: 7027
			Freeze,
			// Token: 0x04001B74 RID: 7028
			GetGold,
			// Token: 0x04001B75 RID: 7029
			MaxHPPassive,
			// Token: 0x04001B76 RID: 7030
			SummonPet,
			// Token: 0x04001B77 RID: 7031
			RevivePets,
			// Token: 0x04001B78 RID: 7032
			AddFood,
			// Token: 0x04001B79 RID: 7033
			AddMaterial,
			// Token: 0x04001B7A RID: 7034
			AddTreasure,
			// Token: 0x04001B7B RID: 7035
			AddPopulation,
			// Token: 0x04001B7C RID: 7036
			AddGiftItem,
			// Token: 0x04001B7D RID: 7037
			ChangeCostOfClear,
			// Token: 0x04001B7E RID: 7038
			Zombie,
			// Token: 0x04001B7F RID: 7039
			ClearItemStatusEffectOfType,
			// Token: 0x04001B80 RID: 7040
			ManaToSelfOnly,
			// Token: 0x04001B81 RID: 7041
			Exhaust,
			// Token: 0x04001B82 RID: 7042
			Cleanse,
			// Token: 0x04001B83 RID: 7043
			Curse,
			// Token: 0x04001B84 RID: 7044
			Duplicate,
			// Token: 0x04001B85 RID: 7045
			RemoveFromGrid,
			// Token: 0x04001B86 RID: 7046
			NextTurnAP,
			// Token: 0x04001B87 RID: 7047
			TransformItemSwitch,
			// Token: 0x04001B88 RID: 7048
			storeCR8Charge,
			// Token: 0x04001B89 RID: 7049
			resetComponentHeat,
			// Token: 0x04001B8A RID: 7050
			changeComponenetHeat,
			// Token: 0x04001B8B RID: 7051
			considerTestRecurison,
			// Token: 0x04001B8C RID: 7052
			removeFromLootTable,
			// Token: 0x04001B8D RID: 7053
			getItemOfSameType,
			// Token: 0x04001B8E RID: 7054
			GiveDiscount,
			// Token: 0x04001B8F RID: 7055
			ProvideSunshine
		}

		// Token: 0x020004A0 RID: 1184
		public enum Target
		{
			// Token: 0x04001B91 RID: 7057
			unspecified,
			// Token: 0x04001B92 RID: 7058
			player,
			// Token: 0x04001B93 RID: 7059
			enemy,
			// Token: 0x04001B94 RID: 7060
			allEnemies,
			// Token: 0x04001B95 RID: 7061
			everyone,
			// Token: 0x04001B96 RID: 7062
			reactiveEnemy,
			// Token: 0x04001B97 RID: 7063
			allFriendlies,
			// Token: 0x04001B98 RID: 7064
			adjacentFriendlies,
			// Token: 0x04001B99 RID: 7065
			friendliesInFront,
			// Token: 0x04001B9A RID: 7066
			friendliesBehind,
			// Token: 0x04001B9B RID: 7067
			statusFromItem,
			// Token: 0x04001B9C RID: 7068
			frontmostFriendly,
			// Token: 0x04001B9D RID: 7069
			backmostFriendly,
			// Token: 0x04001B9E RID: 7070
			truePlayer,
			// Token: 0x04001B9F RID: 7071
			everyoneButPochette
		}

		// Token: 0x020004A1 RID: 1185
		public enum MathematicalType
		{
			// Token: 0x04001BA1 RID: 7073
			summative,
			// Token: 0x04001BA2 RID: 7074
			multiplicative
		}
	}

	// Token: 0x02000333 RID: 819
	[Serializable]
	public class EffectTotal
	{
		// Token: 0x04001347 RID: 4935
		public Item2.Trigger trigger;

		// Token: 0x04001348 RID: 4936
		public List<Item2.EffectTotal.EffectPiece> effectPieces;

		// Token: 0x04001349 RID: 4937
		public Item2.Effect effect;

		// Token: 0x0400134A RID: 4938
		[NonSerialized]
		public float multiplier;

		// Token: 0x020004A2 RID: 1186
		public class EffectPiece
		{
			// Token: 0x04001BA3 RID: 7075
			public string name = "";

			// Token: 0x04001BA4 RID: 7076
			public float value;

			// Token: 0x04001BA5 RID: 7077
			public int numberOfTimes = 1;

			// Token: 0x04001BA6 RID: 7078
			public Item2.Effect.MathematicalType mathematicalType;
		}
	}

	// Token: 0x02000334 RID: 820
	[Serializable]
	public class ItemStatusEffect
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x000BD045 File Offset: 0x000BB245
		public Item2.ItemStatusEffect Clone()
		{
			return (Item2.ItemStatusEffect)base.MemberwiseClone();
		}

		// Token: 0x0400134B RID: 4939
		public bool applyRightAway;

		// Token: 0x0400134C RID: 4940
		public Item2.ItemStatusEffect.Type type;

		// Token: 0x0400134D RID: 4941
		public Item2.ItemStatusEffect.Length length;

		// Token: 0x0400134E RID: 4942
		public int num;

		// Token: 0x0400134F RID: 4943
		[NonSerialized]
		public string source;

		// Token: 0x04001350 RID: 4944
		[SerializeField]
		public List<GameObject> prefabs;

		// Token: 0x04001351 RID: 4945
		[SerializeField]
		public bool showOnCard = true;

		// Token: 0x020004A3 RID: 1187
		public enum Type
		{
			// Token: 0x04001BA8 RID: 7080
			disabled,
			// Token: 0x04001BA9 RID: 7081
			enflamed,
			// Token: 0x04001BAA RID: 7082
			locked,
			// Token: 0x04001BAB RID: 7083
			heavy,
			// Token: 0x04001BAC RID: 7084
			conductive,
			// Token: 0x04001BAD RID: 7085
			buoyant,
			// Token: 0x04001BAE RID: 7086
			projectile,
			// Token: 0x04001BAF RID: 7087
			AddToManaCost,
			// Token: 0x04001BB0 RID: 7088
			AddToEnergyCost,
			// Token: 0x04001BB1 RID: 7089
			canBePlayedOverOtherItems,
			// Token: 0x04001BB2 RID: 7090
			ductTape,
			// Token: 0x04001BB3 RID: 7091
			canBeUsedByCR8Directly,
			// Token: 0x04001BB4 RID: 7092
			runsAutomaticallyOnCoreUse,
			// Token: 0x04001BB5 RID: 7093
			strengthBasedOnDistance,
			// Token: 0x04001BB6 RID: 7094
			cannotBeFoundInStores,
			// Token: 0x04001BB7 RID: 7095
			AddToGoldCost,
			// Token: 0x04001BB8 RID: 7096
			piercing,
			// Token: 0x04001BB9 RID: 7097
			cantBeSold,
			// Token: 0x04001BBA RID: 7098
			spawnsEvent,
			// Token: 0x04001BBB RID: 7099
			allowsItemsInIllusorySpaces,
			// Token: 0x04001BBC RID: 7100
			CR8ChargesReverseWhenOffGrid,
			// Token: 0x04001BBD RID: 7101
			canBeForged,
			// Token: 0x04001BBE RID: 7102
			cannotBeRotated,
			// Token: 0x04001BBF RID: 7103
			isActivated,
			// Token: 0x04001BC0 RID: 7104
			canBeMovedInCombat,
			// Token: 0x04001BC1 RID: 7105
			canBeMovedInCombatButReturnsToOriginalPosition,
			// Token: 0x04001BC2 RID: 7106
			allowsFreeMove,
			// Token: 0x04001BC3 RID: 7107
			petsAreSummonedBehindPochette,
			// Token: 0x04001BC4 RID: 7108
			canOnlyBeHeldByPochette,
			// Token: 0x04001BC5 RID: 7109
			effigy,
			// Token: 0x04001BC6 RID: 7110
			doubleScratch,
			// Token: 0x04001BC7 RID: 7111
			DiscardCarving,
			// Token: 0x04001BC8 RID: 7112
			BanishCarving,
			// Token: 0x04001BC9 RID: 7113
			Natural,
			// Token: 0x04001BCA RID: 7114
			crackedBottle,
			// Token: 0x04001BCB RID: 7115
			reverseHourglass,
			// Token: 0x04001BCC RID: 7116
			invisbleCharge,
			// Token: 0x04001BCD RID: 7117
			onFullInventory,
			// Token: 0x04001BCE RID: 7118
			cannotBeLeft,
			// Token: 0x04001BCF RID: 7119
			[DocsGenerator.NoDocsAttribute]
			cannotBeDestroyed,
			// Token: 0x04001BD0 RID: 7120
			Cleansed,
			// Token: 0x04001BD1 RID: 7121
			cannotBeDisabledByHazards,
			// Token: 0x04001BD2 RID: 7122
			countAsEmpty,
			// Token: 0x04001BD3 RID: 7123
			ghostly,
			// Token: 0x04001BD4 RID: 7124
			invincible,
			// Token: 0x04001BD5 RID: 7125
			energyCarriesBetweenTurns,
			// Token: 0x04001BD6 RID: 7126
			cannotPlaceItemsOfSameTypeAdjacent,
			// Token: 0x04001BD7 RID: 7127
			roundDamageTo1,
			// Token: 0x04001BD8 RID: 7128
			takingDamageDoesStartOfTurnEffects,
			// Token: 0x04001BD9 RID: 7129
			pauperRun,
			// Token: 0x04001BDA RID: 7130
			cannotGainGold,
			// Token: 0x04001BDB RID: 7131
			blockIsNotRemoved,
			// Token: 0x04001BDC RID: 7132
			scalingEnergy,
			// Token: 0x04001BDD RID: 7133
			statusEffectsAreConvertedToBurn,
			// Token: 0x04001BDE RID: 7134
			cannotBePlacedAtEdge,
			// Token: 0x04001BDF RID: 7135
			mustBePlacedAtEdge,
			// Token: 0x04001BE0 RID: 7136
			unique,
			// Token: 0x04001BE1 RID: 7137
			vampiric,
			// Token: 0x04001BE2 RID: 7138
			effectsGravity,
			// Token: 0x04001BE3 RID: 7139
			temporary,
			// Token: 0x04001BE4 RID: 7140
			dontTakeDamageFromBurn
		}

		// Token: 0x020004A4 RID: 1188
		public enum Length
		{
			// Token: 0x04001BE6 RID: 7142
			whileActive,
			// Token: 0x04001BE7 RID: 7143
			turns,
			// Token: 0x04001BE8 RID: 7144
			combats,
			// Token: 0x04001BE9 RID: 7145
			permanent,
			// Token: 0x04001BEA RID: 7146
			whileItemIsInInventory,
			// Token: 0x04001BEB RID: 7147
			whileCoveredByHazards
		}
	}

	// Token: 0x02000335 RID: 821
	[Serializable]
	public class LimitedUses
	{
		// Token: 0x04001352 RID: 4946
		public Item2.LimitedUses.Type type;

		// Token: 0x04001353 RID: 4947
		public float value;

		// Token: 0x04001354 RID: 4948
		[ES3Serializable]
		[NonSerialized]
		public float currentValue = -999f;

		// Token: 0x020004A5 RID: 1189
		public enum Type
		{
			// Token: 0x04001BED RID: 7149
			total,
			// Token: 0x04001BEE RID: 7150
			perCombat,
			// Token: 0x04001BEF RID: 7151
			perTurn
		}
	}

	// Token: 0x02000336 RID: 822
	[Serializable]
	public class CombattEffect
	{
		// Token: 0x04001355 RID: 4949
		public Item2.Trigger trigger;

		// Token: 0x04001356 RID: 4950
		public Item2.Effect effect;
	}

	// Token: 0x02000337 RID: 823
	[Serializable]
	public class MovementEffect
	{
		// Token: 0x04001357 RID: 4951
		public string descriptionKey = "";

		// Token: 0x04001358 RID: 4952
		public Item2.Trigger trigger;

		// Token: 0x04001359 RID: 4953
		public List<Item2.Area> itemsToMove;

		// Token: 0x0400135A RID: 4954
		public Item2.AreaDistance areaDistance;

		// Token: 0x0400135B RID: 4955
		public Vector2 movementAmount;

		// Token: 0x0400135C RID: 4956
		public float rotationAmount;

		// Token: 0x0400135D RID: 4957
		public Item2.MovementEffect.MovementVariety movementVariety;

		// Token: 0x0400135E RID: 4958
		public Item2.MovementEffect.MovementType movementType;

		// Token: 0x0400135F RID: 4959
		public Item2.MovementEffect.MovementLength movementLength;

		// Token: 0x020004A6 RID: 1190
		public enum MovementVariety
		{
			// Token: 0x04001BF1 RID: 7153
			setAmount,
			// Token: 0x04001BF2 RID: 7154
			toRandomSpace
		}

		// Token: 0x020004A7 RID: 1191
		public enum MovementType
		{
			// Token: 0x04001BF4 RID: 7156
			global,
			// Token: 0x04001BF5 RID: 7157
			local
		}

		// Token: 0x020004A8 RID: 1192
		public enum MovementLength
		{
			// Token: 0x04001BF7 RID: 7159
			oneSpace,
			// Token: 0x04001BF8 RID: 7160
			untilHit,
			// Token: 0x04001BF9 RID: 7161
			random
		}
	}

	// Token: 0x02000338 RID: 824
	[Serializable]
	public class Modifier
	{
		// Token: 0x06001605 RID: 5637 RVA: 0x000BD090 File Offset: 0x000BB290
		public Item2.Modifier Clone()
		{
			List<Item2.Effect> list = new List<Item2.Effect>();
			Item2.Modifier modifier = (Item2.Modifier)base.MemberwiseClone();
			foreach (Item2.Effect effect in modifier.effects)
			{
				list.Add(effect.Clone());
			}
			modifier.effects = list;
			return modifier;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000BD104 File Offset: 0x000BB304
		public bool IsSelf()
		{
			return this.trigger != null && (this.trigger.areas == null || this.trigger.areas.Count <= 0 || (this.trigger.areas.Contains(Item2.Area.self) && this.trigger.areas.Count <= 1)) && (this.trigger.types == null || this.trigger.types.Count <= 0 || (this.trigger.types.Contains(Item2.ItemType.Any) && this.trigger.types.Count <= 1)) && (this.trigger.trigger == Item2.Trigger.ActionTrigger.constant || this.trigger.trigger == Item2.Trigger.ActionTrigger.constantExtraEarly || this.trigger.trigger == Item2.Trigger.ActionTrigger.constantEarly || this.trigger.trigger == Item2.Trigger.ActionTrigger.constantClearWhile) && (this.typesToModify == null || this.typesToModify.Count <= 0 || (this.typesToModify.Count <= 1 && this.typesToModify.Contains(Item2.ItemType.Any))) && (this.areasToModify == null || this.areasToModify.Count <= 0 || (this.areasToModify.Count <= 1 && this.areasToModify.Contains(Item2.Area.self)));
		}

		// Token: 0x04001360 RID: 4960
		public string descriptionKey = "";

		// Token: 0x04001361 RID: 4961
		[ES3Serializable]
		[NonSerialized]
		public string triggerKey = "";

		// Token: 0x04001362 RID: 4962
		[ES3Serializable]
		[NonSerialized]
		public float triggerDisplayValue = -999f;

		// Token: 0x04001363 RID: 4963
		[ES3Serializable]
		[NonSerialized]
		public float descriptionDisplayValue = -999f;

		// Token: 0x04001364 RID: 4964
		[ES3Serializable]
		[HideInInspector]
		public float originalValue;

		// Token: 0x04001365 RID: 4965
		public Item2.Trigger trigger;

		// Token: 0x04001366 RID: 4966
		public List<Item2.Effect> effects;

		// Token: 0x04001367 RID: 4967
		public List<Item2.ItemType> typesToModify;

		// Token: 0x04001368 RID: 4968
		public List<Item2.ItemType> excludedTypes;

		// Token: 0x04001369 RID: 4969
		public List<Item2.Area> areasToModify;

		// Token: 0x0400136A RID: 4970
		public Item2.AreaDistance areaDistance;

		// Token: 0x0400136B RID: 4971
		public Item2.Modifier.Length length;

		// Token: 0x0400136C RID: 4972
		public string name = "";

		// Token: 0x0400136D RID: 4973
		public bool stackable;

		// Token: 0x0400136E RID: 4974
		[ES3Serializable]
		[NonSerialized]
		public string origin = "";

		// Token: 0x0400136F RID: 4975
		[ES3Serializable]
		[NonSerialized]
		public Item2.Modifier.Length lengthForThisModifier = Item2.Modifier.Length.permanent;

		// Token: 0x020004A9 RID: 1193
		public enum Length
		{
			// Token: 0x04001BFB RID: 7163
			whileActive,
			// Token: 0x04001BFC RID: 7164
			forTurn,
			// Token: 0x04001BFD RID: 7165
			forCombat,
			// Token: 0x04001BFE RID: 7166
			untilRotate,
			// Token: 0x04001BFF RID: 7167
			untilUse,
			// Token: 0x04001C00 RID: 7168
			permanent,
			// Token: 0x04001C01 RID: 7169
			whileComboing,
			// Token: 0x04001C02 RID: 7170
			twoTurns,
			// Token: 0x04001C03 RID: 7171
			untilDiscard,
			// Token: 0x04001C04 RID: 7172
			whileItemIsInInventory,
			// Token: 0x04001C05 RID: 7173
			untilUnzombied
		}
	}

	// Token: 0x02000339 RID: 825
	[Serializable]
	public class ModifierTotal
	{
		// Token: 0x04001370 RID: 4976
		public Item2.Trigger trigger;

		// Token: 0x04001371 RID: 4977
		public List<Item2.ModifierTotal.EffectPiece> effectPieces;

		// Token: 0x04001372 RID: 4978
		public Item2.Modifier modifier;

		// Token: 0x020004AA RID: 1194
		public class EffectPiece
		{
			// Token: 0x04001C06 RID: 7174
			public string name = "";

			// Token: 0x04001C07 RID: 7175
			public float value;

			// Token: 0x04001C08 RID: 7176
			public Item2.Effect.MathematicalType mathematicalType;
		}
	}

	// Token: 0x0200033A RID: 826
	[Serializable]
	public class AddModifier
	{
		// Token: 0x04001373 RID: 4979
		public string descriptionKey = "";

		// Token: 0x04001374 RID: 4980
		public Item2.Trigger trigger;

		// Token: 0x04001375 RID: 4981
		public List<Item2.ItemType> typesToModify;

		// Token: 0x04001376 RID: 4982
		public List<Item2.Area> areasToModify;

		// Token: 0x04001377 RID: 4983
		public Item2.AreaDistance areaDistance;

		// Token: 0x04001378 RID: 4984
		public Item2.Modifier modifier;

		// Token: 0x04001379 RID: 4985
		public Item2.Modifier.Length lengthForThisModifier;
	}

	// Token: 0x0200033B RID: 827
	[Serializable]
	public class CreateEffect
	{
		// Token: 0x0400137A RID: 4986
		public Item2.Trigger trigger;

		// Token: 0x0400137B RID: 4987
		public Item2.CreateEffect.CreateType createType;

		// Token: 0x0400137C RID: 4988
		public List<Item2.Area> areasToCreateTheItem;

		// Token: 0x0400137D RID: 4989
		public Item2.AreaDistance areaDistance;

		// Token: 0x0400137E RID: 4990
		public bool skippable;

		// Token: 0x0400137F RID: 4991
		public bool allowNonStandard;

		// Token: 0x04001380 RID: 4992
		public List<GameObject> itemsToCreate;

		// Token: 0x04001381 RID: 4993
		public List<Item2.ItemType> typesToCreate;

		// Token: 0x04001382 RID: 4994
		public List<Item2.Rarity> raritiesToCreate;

		// Token: 0x04001383 RID: 4995
		public string descriptor;

		// Token: 0x04001384 RID: 4996
		public int luckBonus;

		// Token: 0x04001385 RID: 4997
		public int numberToCreate = 1;

		// Token: 0x020004AB RID: 1195
		public enum CreateType
		{
			// Token: 0x04001C0A RID: 7178
			set,
			// Token: 0x04001C0B RID: 7179
			replace,
			// Token: 0x04001C0C RID: 7180
			inOrder,
			// Token: 0x04001C0D RID: 7181
			trueRandom,
			// Token: 0x04001C0E RID: 7182
			byType,
			// Token: 0x04001C0F RID: 7183
			inOpenSpace,
			// Token: 0x04001C10 RID: 7184
			inOpenSpaceByType,
			// Token: 0x04001C11 RID: 7185
			createCurse,
			// Token: 0x04001C12 RID: 7186
			createBlessing,
			// Token: 0x04001C13 RID: 7187
			inOpenSpaceRandom,
			// Token: 0x04001C14 RID: 7188
			chooseItem,
			// Token: 0x04001C15 RID: 7189
			sameSizeItem
		}
	}

	// Token: 0x0200033C RID: 828
	[Serializable]
	public class EnergyEffect
	{
		// Token: 0x04001386 RID: 4998
		public string descriptionKey = "";

		// Token: 0x04001387 RID: 4999
		public Item2.Trigger trigger;

		// Token: 0x04001388 RID: 5000
		public Item2.EnergyEffect.EnergiesToEffect energiesToEffect = Item2.EnergyEffect.EnergiesToEffect.trigger;

		// Token: 0x04001389 RID: 5001
		public bool worksWithForeignCurrentEnergyBall = true;

		// Token: 0x0400138A RID: 5002
		[Header(" _____________Effect_____________")]
		public Item2.EnergyEffect.Effect effect;

		// Token: 0x0400138B RID: 5003
		public float num2;

		// Token: 0x0400138C RID: 5004
		public Item2.EnergyEffect.Value value;

		// Token: 0x0400138D RID: 5005
		[Header("_____________Special Movement_____________")]
		public Item2.EnergyEffect.SpecialMovementToApply specialMovementToApply;

		// Token: 0x0400138E RID: 5006
		public List<Vector2> specialMovementAmount;

		// Token: 0x0400138F RID: 5007
		public Item2.EnergyEffect.MovementType specialMovementType;

		// Token: 0x04001390 RID: 5008
		[Header("_____________Rotation_____________")]
		public bool applyRotation = true;

		// Token: 0x04001391 RID: 5009
		public Item2.EnergyEffect.MovementType movementType;

		// Token: 0x04001392 RID: 5010
		public float rotationAmount;

		// Token: 0x020004AC RID: 1196
		public enum EnergiesToEffect
		{
			// Token: 0x04001C17 RID: 7191
			none,
			// Token: 0x04001C18 RID: 7192
			trigger,
			// Token: 0x04001C19 RID: 7193
			storedEnergy,
			// Token: 0x04001C1A RID: 7194
			allEnergies,
			// Token: 0x04001C1B RID: 7195
			newEnergy
		}

		// Token: 0x020004AD RID: 1197
		public enum Effect
		{
			// Token: 0x04001C1D RID: 7197
			none,
			// Token: 0x04001C1E RID: 7198
			slide,
			// Token: 0x04001C1F RID: 7199
			changeEnergy
		}

		// Token: 0x020004AE RID: 1198
		public enum Value
		{
			// Token: 0x04001C21 RID: 7201
			standard,
			// Token: 0x04001C22 RID: 7202
			currentItem2Charges,
			// Token: 0x04001C23 RID: 7203
			currentAP,
			// Token: 0x04001C24 RID: 7204
			triggerEnergyValue
		}

		// Token: 0x020004AF RID: 1199
		public enum SpecialMovementToApply
		{
			// Token: 0x04001C26 RID: 7206
			none,
			// Token: 0x04001C27 RID: 7207
			slide
		}

		// Token: 0x020004B0 RID: 1200
		public enum MovementType
		{
			// Token: 0x04001C29 RID: 7209
			global,
			// Token: 0x04001C2A RID: 7210
			localToThisRotation,
			// Token: 0x04001C2B RID: 7211
			localToEntranceDirection,
			// Token: 0x04001C2C RID: 7212
			relative
		}
	}

	// Token: 0x0200033D RID: 829
	public class ActiveMovementEffect
	{
		// Token: 0x0600160C RID: 5644 RVA: 0x000BD2FE File Offset: 0x000BB4FE
		public ActiveMovementEffect(Item2 _item, Item2.MovementEffect _movementEffect)
		{
			this.item = _item;
			this.movementEffect = _movementEffect;
		}

		// Token: 0x04001393 RID: 5011
		public Item2 item;

		// Token: 0x04001394 RID: 5012
		public Item2.MovementEffect movementEffect;
	}

	// Token: 0x0200033E RID: 830
	public class ActiveCreateEffect
	{
		// Token: 0x0600160D RID: 5645 RVA: 0x000BD314 File Offset: 0x000BB514
		public ActiveCreateEffect(Item2 _item, Item2.CreateEffect _createEffect)
		{
			this.item = _item;
			this.createEffect = _createEffect;
		}

		// Token: 0x04001395 RID: 5013
		public Item2 item;

		// Token: 0x04001396 RID: 5014
		public Item2.CreateEffect createEffect;
	}

	// Token: 0x0200033F RID: 831
	public class ActiveEffect
	{
		// Token: 0x0600160E RID: 5646 RVA: 0x000BD32A File Offset: 0x000BB52A
		public ActiveEffect(Item2 _item, Item2.EffectTotal _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x04001397 RID: 5015
		public Item2 item;

		// Token: 0x04001398 RID: 5016
		public Item2.EffectTotal effect;
	}

	// Token: 0x02000340 RID: 832
	public class ActiveModifiers
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x000BD340 File Offset: 0x000BB540
		public ActiveModifiers(Item2 _item, Item2.Modifier _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x04001399 RID: 5017
		public Item2 item;

		// Token: 0x0400139A RID: 5018
		public Item2.Modifier effect;
	}

	// Token: 0x02000341 RID: 833
	public class ActiveAddModifiers
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x000BD356 File Offset: 0x000BB556
		public ActiveAddModifiers(Item2 _item, Item2.AddModifier _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x0400139B RID: 5019
		public Item2 item;

		// Token: 0x0400139C RID: 5020
		public Item2.AddModifier effect;
	}

	// Token: 0x02000342 RID: 834
	public class ActiveSpecialEffects
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x000BD36C File Offset: 0x000BB56C
		public ActiveSpecialEffects(Item2 _item, SpecialItem _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x0400139D RID: 5021
		public Item2 item;

		// Token: 0x0400139E RID: 5022
		public SpecialItem effect;
	}

	// Token: 0x02000343 RID: 835
	public class ActiveEnergyEmitterEffects
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x000BD382 File Offset: 0x000BB582
		public ActiveEnergyEmitterEffects(Item2 _item, Item2.EnergyEffect _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x0400139F RID: 5023
		public Item2 item;

		// Token: 0x040013A0 RID: 5024
		public Item2.EnergyEffect effect;
	}

	// Token: 0x02000344 RID: 836
	public class ActiveValueChangers
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x000BD398 File Offset: 0x000BB598
		public ActiveValueChangers(Item2 _item, ValueChanger _effect)
		{
			this.item = _item;
			this.effect = _effect;
		}

		// Token: 0x040013A1 RID: 5025
		public Item2 item;

		// Token: 0x040013A2 RID: 5026
		public ValueChanger effect;
	}

	// Token: 0x02000345 RID: 837
	[Serializable]
	public class Cost
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x000BD3B0 File Offset: 0x000BB5B0
		public void GetCurrentValue()
		{
			int num = this.baseValue;
			if (num == -999 && this.costModifiers.Count == 0)
			{
				this.currentValue = -999;
				return;
			}
			num = Mathf.Max(num, 0);
			foreach (Item2.Cost.CostModifier costModifier in this.costModifiers)
			{
				if (costModifier.value >= -900)
				{
					num += costModifier.value;
				}
				if (this.baseValue > 0 && num < 1 && costModifier.length == Item2.Modifier.Length.forCombat && this.lowestAllowedValue == Item2.Cost.LowestAllowedValue.one)
				{
					num = 1;
				}
				if (num < 0 && this.lowestAllowedValue == Item2.Cost.LowestAllowedValue.zero)
				{
					num = 0;
				}
			}
			using (List<Item2.Cost.CostModifier>.Enumerator enumerator = this.costModifiers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.value < -900)
					{
						this.currentValue = 0;
						return;
					}
				}
			}
			this.currentValue = Mathf.Max(num, 0);
		}

		// Token: 0x040013A3 RID: 5027
		[NonSerialized]
		public bool originalCost = true;

		// Token: 0x040013A4 RID: 5028
		public bool displayAsX;

		// Token: 0x040013A5 RID: 5029
		public Item2.Cost.Type type;

		// Token: 0x040013A6 RID: 5030
		public int baseValue;

		// Token: 0x040013A7 RID: 5031
		public Item2.Cost.LowestAllowedValue lowestAllowedValue;

		// Token: 0x040013A8 RID: 5032
		[NonSerialized]
		public int currentValue;

		// Token: 0x040013A9 RID: 5033
		[NonSerialized]
		public string source;

		// Token: 0x040013AA RID: 5034
		public List<Item2.Cost.CostModifier> costModifiers = new List<Item2.Cost.CostModifier>();

		// Token: 0x020004B1 RID: 1201
		public enum Type
		{
			// Token: 0x04001C2E RID: 7214
			energy,
			// Token: 0x04001C2F RID: 7215
			mana,
			// Token: 0x04001C30 RID: 7216
			gold,
			// Token: 0x04001C31 RID: 7217
			canBeUsedIndirectly,
			// Token: 0x04001C32 RID: 7218
			[DocsGenerator.NoDocsAttribute]
			mustMeetASpecificCondition,
			// Token: 0x04001C33 RID: 7219
			sunShine
		}

		// Token: 0x020004B2 RID: 1202
		public enum LowestAllowedValue
		{
			// Token: 0x04001C35 RID: 7221
			one,
			// Token: 0x04001C36 RID: 7222
			zero,
			// Token: 0x04001C37 RID: 7223
			negatives
		}

		// Token: 0x020004B3 RID: 1203
		[Serializable]
		public class CostModifier
		{
			// Token: 0x04001C38 RID: 7224
			public Item2.Modifier.Length length;

			// Token: 0x04001C39 RID: 7225
			public int value;
		}
	}

	// Token: 0x02000346 RID: 838
	public enum AvailabilityType
	{
		// Token: 0x040013AC RID: 5036
		Always,
		// Token: 0x040013AD RID: 5037
		Never,
		// Token: 0x040013AE RID: 5038
		MarkerDependent,
		// Token: 0x040013AF RID: 5039
		UnlockDependent
	}

	// Token: 0x02000347 RID: 839
	public enum OneOfAKindType
	{
		// Token: 0x040013B1 RID: 5041
		NoLimit,
		// Token: 0x040013B2 RID: 5042
		OnePerRun,
		// Token: 0x040013B3 RID: 5043
		OneTotal
	}

	// Token: 0x02000348 RID: 840
	public class CategoryClass
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x000BD4EA File Offset: 0x000BB6EA
		public CategoryClass(float _luck, Item2.ItemGrouping _itemGrouping)
		{
			this.luck = _luck;
			this.itemGrouping = _itemGrouping;
		}

		// Token: 0x040013B4 RID: 5044
		public float luck;

		// Token: 0x040013B5 RID: 5045
		public Item2.ItemGrouping itemGrouping;
	}

	// Token: 0x02000349 RID: 841
	private class ItemClass
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x000BD500 File Offset: 0x000BB700
		public ItemClass(Vector2 _range, Item2 _item2)
		{
			this.range = _range;
			this.item2 = _item2;
		}

		// Token: 0x040013B6 RID: 5046
		public Vector2 range;

		// Token: 0x040013B7 RID: 5047
		public Item2 item2;
	}
}

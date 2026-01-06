using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class ItemSpawner : MonoBehaviour
{
	// Token: 0x060001E6 RID: 486 RVA: 0x0000BBB8 File Offset: 0x00009DB8
	public static GameObject InstantiateItemFree(List<Item2> items)
	{
		if (items == null || items.Count == 0)
		{
			return null;
		}
		return ItemSpawner.InstantiateItemsFree(items, false, default(Vector2))[0];
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000BBE8 File Offset: 0x00009DE8
	public static GameObject InstantiateItemFree(GameObject item)
	{
		return ItemSpawner.InstantiateItemFree(item.GetComponent<Item2>());
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000BBF8 File Offset: 0x00009DF8
	public static GameObject InstantiateItemFree(Item2 item)
	{
		if (item == null)
		{
			return null;
		}
		return ItemSpawner.InstantiateItemsFree(new List<Item2> { item }, false, default(Vector2))[0];
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000BC34 File Offset: 0x00009E34
	public static List<GameObject> InstantiateItemsFree(List<ItemSpawner.ItemToSpawn> itemsToSpawn, bool initiateMove = true, Vector2 position = default(Vector2))
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < itemsToSpawn.Count; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(itemsToSpawn[i].item.gameObject);
			GameManager.main.ShowGotLucky(gameObject.transform, itemsToSpawn[i].gotLucky);
			gameObject.SetActive(true);
			if (position != default(Vector2))
			{
				gameObject.transform.position = position;
			}
			list.Add(gameObject);
		}
		if (GameManager.main && initiateMove)
		{
			GameManager.main.MoveAllItems();
		}
		return list;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000BCD4 File Offset: 0x00009ED4
	public static List<GameObject> InstantiateItemsFree(List<Item2> items, bool initiateMove = true, Vector2 position = default(Vector2))
	{
		List<GameObject> list = new List<GameObject>();
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(items[i].gameObject);
			gameObject.SetActive(true);
			if (position != default(Vector2))
			{
				gameObject.transform.position = position;
			}
			list.Add(gameObject);
		}
		if (GameManager.main && initiateMove)
		{
			GameManager.main.MoveAllItems();
		}
		return list;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000BD58 File Offset: 0x00009F58
	public static GameObject InstatntiateItems(ItemSpawner.ItemToSpawn itemToSpawn)
	{
		List<GameObject> list = ItemSpawner.InstantiateItems(new List<ItemSpawner.ItemToSpawn> { itemToSpawn });
		if (list.Count == 0)
		{
			return null;
		}
		return list[0];
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000BD88 File Offset: 0x00009F88
	public static List<GameObject> InstantiateItems(List<ItemSpawner.ItemToSpawn> itemsToSpawn)
	{
		List<GameObject> list = ItemSpawner.InstantiateItems(itemsToSpawn.Select((ItemSpawner.ItemToSpawn x) => x.item).ToList<Item2>());
		if (GameManager.main)
		{
			int num = 0;
			while (num < itemsToSpawn.Count && list.Count > num && itemsToSpawn.Count > num)
			{
				GameManager.main.ShowGotLucky(list[num].transform, itemsToSpawn[num].gotLucky);
				num++;
			}
		}
		return list;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000BE18 File Offset: 0x0000A018
	public static List<ItemSpawner.ItemToSpawn> ConvertToItemToSpawn(List<Item2> items)
	{
		List<ItemSpawner.ItemToSpawn> list = new List<ItemSpawner.ItemToSpawn>();
		foreach (Item2 item in items)
		{
			list.Add(new ItemSpawner.ItemToSpawn(item, false));
		}
		return list;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000BE74 File Offset: 0x0000A074
	public static GameObject InstantiateItems(Item2 item)
	{
		if (item == null)
		{
			return null;
		}
		return ItemSpawner.InstantiateItems(new List<Item2> { item })[0];
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000BE98 File Offset: 0x0000A098
	public static List<GameObject> InstantiateItems(List<Item2> items)
	{
		List<GameObject> list = new List<GameObject>();
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(items[i].gameObject);
			float num = -5f;
			float num2 = ((float)i - (float)(count - 1) / 2f) * 1.5f;
			if (count > 5)
			{
				if (i < 4)
				{
					num2 = ((float)i - 1.5f) * 1.5f;
					num = -4f;
				}
				else
				{
					num2 = ((float)(i - 4) - (float)(count - 4 - 1) / 2f) * 1.5f;
					num = -6f;
				}
			}
			gameObject.transform.position = new Vector3(num2, num, 0f);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (component)
			{
				if (gameObject.GetComponent<ModdedItem>())
				{
					component.outOfInventoryPosition = gameObject.transform.position;
				}
				else
				{
					component.outOfInventoryPosition = gameObject.transform.localPosition;
				}
				component.outOfInventoryRotation = Quaternion.identity;
				component.returnsToOutOfInventoryPosition = true;
				list.Add(gameObject);
			}
		}
		return list;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000BFAC File Offset: 0x0000A1AC
	public static List<ItemSpawner.ItemToSpawn> GetItemsWithLuck(int numOfItems)
	{
		return ItemSpawner.GetItemsWithLuck(numOfItems, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false, 0f);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000BFC7 File Offset: 0x0000A1C7
	public static List<ItemSpawner.ItemToSpawn> GetItemsWithLuck(int numOfItems, List<Item2.ItemType> itemTypes, bool allowRepeats = true, bool allowNonStandard = false, float luckBonus = 0f)
	{
		return ItemSpawner.GetItemsWithLuck(numOfItems, itemTypes, new List<Item2.ItemType>(), allowRepeats, allowNonStandard, luckBonus);
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000BFDC File Offset: 0x0000A1DC
	public static List<ItemSpawner.ItemToSpawn> GetItemsWithLuck(int numOfItems, List<Item2.ItemType> itemTypes, List<Item2.ItemType> excludedItemTypes, bool allowRepeats = true, bool allowNonStandard = false, float luckBonus = 0f)
	{
		List<ItemSpawner.ItemToSpawn> list = new List<ItemSpawner.ItemToSpawn>();
		for (int i = 0; i < numOfItems; i++)
		{
			bool flag;
			Item2.Rarity rarity = GameManager.main.ChooseRarity(out flag, luckBonus, true);
			bool flag2 = true;
			int num = 0;
			while (flag2 && num < 5)
			{
				List<Item2> items = ItemSpawner.GetItems(new List<Item2.Rarity> { rarity }, itemTypes, new List<Item2.ItemType>(), excludedItemTypes, 1, allowRepeats, allowNonStandard, false, null);
				if (items.Count > 0)
				{
					Item2 item = items[0];
					list.Add(new ItemSpawner.ItemToSpawn(item, flag));
				}
				flag2 = ItemSpawner.RemoveDuplicates(list, allowRepeats);
				num++;
			}
		}
		return list;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000C070 File Offset: 0x0000A270
	private static bool RemoveDuplicates(List<ItemSpawner.ItemToSpawn> items, bool allowRepeats)
	{
		bool flag = false;
		for (int i = 0; i < items.Count; i++)
		{
			if (!allowRepeats || items[i].item.oneOfAKindType != Item2.OneOfAKindType.NoLimit)
			{
				for (int j = i + 1; j < items.Count; j++)
				{
					if (items[i].item == items[j].item)
					{
						flag = true;
						items.RemoveAt(j);
						j--;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
	private static bool RemoveDuplicates(List<Item2> items, bool allowRepeats)
	{
		bool flag = false;
		for (int i = 0; i < items.Count; i++)
		{
			if (!allowRepeats || items[i].oneOfAKindType != Item2.OneOfAKindType.NoLimit)
			{
				for (int j = i + 1; j < items.Count; j++)
				{
					if (items[i] == items[j])
					{
						flag = true;
						items.RemoveAt(j);
						j--;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000C150 File Offset: 0x0000A350
	public static List<Item2> GetItems(int numOfItems, List<Item2.ItemType> itemTypes, List<Item2.ItemType> idealItemTypes, List<Item2.ItemType> excludedTypes)
	{
		return ItemSpawner.GetItems(new List<Item2.Rarity>
		{
			Item2.Rarity.Common,
			Item2.Rarity.Uncommon,
			Item2.Rarity.Rare,
			Item2.Rarity.Legendary
		}, itemTypes, idealItemTypes, excludedTypes, numOfItems, false, true, false, null);
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000C18C File Offset: 0x0000A38C
	public static List<Item2> GetItems(int numOfItems, List<Item2.ItemType> itemTypes, bool allowRepeats = true, bool allowNonStandard = false)
	{
		return ItemSpawner.GetItems(new List<Item2.Rarity>
		{
			Item2.Rarity.Common,
			Item2.Rarity.Uncommon,
			Item2.Rarity.Rare,
			Item2.Rarity.Legendary
		}, itemTypes, new List<Item2.ItemType>(), new List<Item2.ItemType>(), numOfItems, allowRepeats, allowNonStandard, false, null);
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
	public static List<Item2> GetItems(int numOfItems, List<Item2.ItemType> itemTypes, List<Item2.SpawnGrouping> spawnGroupings, bool allowRepeats = true, bool allowNonStandard = false)
	{
		return ItemSpawner.GetItems(new List<Item2.Rarity>
		{
			Item2.Rarity.Common,
			Item2.Rarity.Uncommon,
			Item2.Rarity.Rare,
			Item2.Rarity.Legendary
		}, itemTypes, new List<Item2.ItemType>(), new List<Item2.ItemType>(), numOfItems, allowRepeats, allowNonStandard, false, spawnGroupings);
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000C214 File Offset: 0x0000A414
	public static List<Item2> GetItems(int numOfItems, List<Item2.Rarity> rarities, bool allowRepeats = true, bool allowNonStandard = false)
	{
		return ItemSpawner.GetItems(rarities, new List<Item2.ItemType> { Item2.ItemType.Any }, new List<Item2.ItemType>(), new List<Item2.ItemType>(), numOfItems, allowRepeats, allowNonStandard, false, null);
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000C244 File Offset: 0x0000A444
	public static List<Item2> GetItems(int numOfItems, List<Item2.Rarity> rarities, List<Item2.ItemType> itemTypes, List<Item2.ItemType> excludedItems, bool allowRepeats = true, bool allowNonStandard = false, bool ignoreStandard = false)
	{
		return ItemSpawner.GetItems(rarities, itemTypes, new List<Item2.ItemType>(), excludedItems, numOfItems, allowRepeats, allowNonStandard, ignoreStandard, null);
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000C268 File Offset: 0x0000A468
	public static List<Item2> GetItems(int numOfItems, List<Item2.Rarity> rarities, List<Item2.ItemType> itemTypes, bool allowRepeats = true, bool allowNonStandard = false)
	{
		return ItemSpawner.GetItems(rarities, itemTypes, new List<Item2.ItemType>(), new List<Item2.ItemType>(), numOfItems, allowRepeats, allowNonStandard, false, null);
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000C28C File Offset: 0x0000A48C
	public static List<Item2> GetItems(int numOfItems, bool allowRepeats = true, bool allowNonStandard = false)
	{
		return ItemSpawner.GetItems(new List<Item2.Rarity>
		{
			Item2.Rarity.Common,
			Item2.Rarity.Uncommon,
			Item2.Rarity.Rare,
			Item2.Rarity.Legendary
		}, new List<Item2.ItemType> { Item2.ItemType.Any }, new List<Item2.ItemType>(), new List<Item2.ItemType>(), numOfItems, allowRepeats, allowNonStandard, false, null);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000C2DC File Offset: 0x0000A4DC
	private static List<Item2> GetItems(List<Item2.Rarity> rarities, List<Item2.ItemType> itemTypes, List<Item2.ItemType> idealItemTypes, List<Item2.ItemType> excludedTypes, int numOfItems, bool allowRepeats, bool allowNonStandard, bool ignoreStandard = false, List<Item2.SpawnGrouping> spawnGroupings = null)
	{
		if (rarities.Count == 0)
		{
			rarities = new List<Item2.Rarity>
			{
				Item2.Rarity.Common,
				Item2.Rarity.Uncommon,
				Item2.Rarity.Rare,
				Item2.Rarity.Legendary
			};
		}
		List<Item2> allValidItems = ItemSpawner.GetAllValidItems(rarities, itemTypes, idealItemTypes, excludedTypes, allowNonStandard, ignoreStandard, spawnGroupings);
		List<Item2> list = new List<Item2>();
		int num = 0;
		while (num < numOfItems && allValidItems.Count != 0)
		{
			int num2 = Random.Range(0, allValidItems.Count);
			list.Add(allValidItems[num2]);
			if (!allowRepeats)
			{
				allValidItems.RemoveAt(num2);
			}
			num++;
		}
		return list;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000C368 File Offset: 0x0000A568
	private static List<Item2> GetAllValidItems(List<Item2.Rarity> rarities, List<Item2.ItemType> itemTypes, List<Item2.ItemType> idealItemTypes, List<Item2.ItemType> excludedItemTypes, bool allowNonStandard = false, bool ignoreStandard = false, List<Item2.SpawnGrouping> spawnGroupings = null)
	{
		IEnumerable<Item2> enumerable = DebugItemManager.main.item2s;
		if (ModItemLoader.main != null)
		{
			enumerable = DebugItemManager.main.item2s.Concat(ModItemLoader.main.modItems);
		}
		List<Item2> list = new List<Item2>();
		foreach (Item2 item in enumerable)
		{
			if (item && item.canBeSpawnedStandardEver && (!item.isStandard || !ignoreStandard) && (!item.isStandardOnlyAfterUnlock || MetaProgressSaveManager.main.itemsDiscovered.Contains(Item2.GetDisplayName(item.name))) && (item.isStandard || allowNonStandard) && rarities.Contains(item.rarity) && Item2.ShareItemTypes(itemTypes, item.itemType) && (excludedItemTypes == null || excludedItemTypes.Count <= 0 || !Item2.ShareItemTypes(excludedItemTypes, item.itemType)) && (!GameManager.main || GameManager.main.ItemValidToSpawn(item, false)) && (idealItemTypes == null || idealItemTypes.Count <= 0 || Item2.ShareItemTypes(idealItemTypes, item.itemType)) && (spawnGroupings == null || spawnGroupings.Count <= 0 || spawnGroupings.Intersect(item.spawnGroupings).Any<Item2.SpawnGrouping>()) && (!Singleton.Instance.isDemo || item.isAvailableInDemo))
			{
				list.Add(item);
			}
		}
		if (idealItemTypes.Count != 0 && list.Count == 0)
		{
			return ItemSpawner.GetAllValidItems(rarities, itemTypes, new List<Item2.ItemType>(), excludedItemTypes, allowNonStandard, ignoreStandard, null);
		}
		return list;
	}

	// Token: 0x02000277 RID: 631
	public class ItemToSpawn
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x000AFD5C File Offset: 0x000ADF5C
		public ItemToSpawn(Item2 item, bool gotLucky)
		{
			this.item = item;
			this.gotLucky = gotLucky;
		}

		// Token: 0x04000F51 RID: 3921
		public Item2 item;

		// Token: 0x04000F52 RID: 3922
		public bool gotLucky;
	}
}

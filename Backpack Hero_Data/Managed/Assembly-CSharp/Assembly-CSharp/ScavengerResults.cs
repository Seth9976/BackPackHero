using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class ScavengerResults : MonoBehaviour
{
	// Token: 0x0600060B RID: 1547 RVA: 0x0003B862 File Offset: 0x00039A62
	private void Start()
	{
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0003B864 File Offset: 0x00039A64
	private void SpawnItems()
	{
		GameManager main = GameManager.main;
		DungeonSpawner main2 = DungeonSpawner.main;
		int num = 2;
		int num2 = 5;
		int dungeonPropertyValue = main2.GetDungeonPropertyValue(DungeonSpawner.DungeonProperty.Type.scavenger);
		if (dungeonPropertyValue == 1)
		{
			num = 3;
			num2 = 20;
		}
		else if (dungeonPropertyValue == 2)
		{
			num = 4;
			num2 = 25;
		}
		else if (dungeonPropertyValue == 3)
		{
			num = 6;
			num2 = 35;
		}
		string dungeonPropertyString = main2.GetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.scavenger);
		Item2.ItemType itemType = Item2.ItemType.Any;
		if (dungeonPropertyString == "0")
		{
			itemType = Item2.ItemType.Weapon;
		}
		else if (dungeonPropertyString == "1")
		{
			itemType = Item2.ItemType.Armor;
		}
		else if (dungeonPropertyString == "2")
		{
			itemType = Item2.ItemType.Shield;
		}
		else if (dungeonPropertyString == "3")
		{
			itemType = Item2.ItemType.Consumable;
		}
		else if (dungeonPropertyString == "4")
		{
			itemType = Item2.ItemType.Accessory;
		}
		for (int i = 0; i < num; i++)
		{
			bool flag;
			Item2.Rarity rarity = main.ChooseRarity(out flag, (float)num2, true);
			GameObject gameObject = main.SpawnItem(new List<Item2.ItemType> { itemType }, new List<Item2.Rarity> { rarity }, false, null);
			GameManager main3 = GameManager.main;
			if (main3 != null)
			{
				main3.ShowGotLucky(gameObject.transform, flag);
			}
			gameObject.transform.position = base.transform.position;
		}
		main2.ClearPropertiesByType(DungeonSpawner.DungeonProperty.Type.scavenger);
		main.MoveAllItems();
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0003B994 File Offset: 0x00039B94
	private void Update()
	{
		if (!this.randomEventMaster)
		{
			this.randomEventMaster = base.GetComponent<RandomEventMaster>();
		}
		this.randomEventMaster.npc.SetText(new List<string> { LangaugeManager.main.GetTextByKey("ev27result") });
		this.SpawnItems();
		this.randomEventMaster.dungeonEvent.FinishEvent();
		this.randomEventMaster.EndEvent();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040004DE RID: 1246
	private RandomEventMaster randomEventMaster;
}

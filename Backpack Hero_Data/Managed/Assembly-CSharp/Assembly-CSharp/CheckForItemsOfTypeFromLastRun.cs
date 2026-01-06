using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000029 RID: 41
[CreateMenu("CheckForItemsOfTypeFromLastRun", 0)]
public class CheckForItemsOfTypeFromLastRun : ConditionDataBase
{
	// Token: 0x060000F5 RID: 245 RVA: 0x000072C4 File Offset: 0x000054C4
	public override bool OnGetIsValid(INode parent)
	{
		if ((DateTime.UtcNow - CheckForItemsOfTypeFromLastRun.lastCheckTime).TotalSeconds > 1.0 || (DateTime.UtcNow - CheckForItemsOfTypeFromLastRun.lastCheckTime).TotalSeconds < -1.0 || CheckForItemsOfTypeFromLastRun.itemTypesStatic.Count == 0)
		{
			CheckForItemsOfTypeFromLastRun.lastCheckTime = DateTime.UtcNow;
			CheckForItemsOfTypeFromLastRun.itemTypesStatic.Clear();
			foreach (string text in MetaProgressSaveManager.main.lastRun.itemsDiscovered)
			{
				Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
				if (item2ByName)
				{
					CheckForItemsOfTypeFromLastRun.itemTypesStatic.AddRange(item2ByName.itemType);
				}
			}
		}
		int num = 0;
		foreach (Item2.ItemType itemType in this.itemTypes)
		{
			for (int i = 0; i < CheckForItemsOfTypeFromLastRun.itemTypesStatic.Count; i++)
			{
				if (CheckForItemsOfTypeFromLastRun.itemTypesStatic[i] == itemType)
				{
					num++;
				}
			}
		}
		return num >= this.necessaryNumberOfItems;
	}

	// Token: 0x0400008D RID: 141
	[SerializeField]
	private int necessaryNumberOfItems = 1;

	// Token: 0x0400008E RID: 142
	[SerializeField]
	private List<Item2.ItemType> itemTypes = new List<Item2.ItemType>();

	// Token: 0x0400008F RID: 143
	[SerializeField]
	private static List<Item2.ItemType> itemTypesStatic = new List<Item2.ItemType>();

	// Token: 0x04000090 RID: 144
	private static DateTime lastCheckTime = DateTime.UtcNow;
}

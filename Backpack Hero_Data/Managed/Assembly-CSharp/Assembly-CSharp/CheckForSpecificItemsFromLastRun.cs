using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000032 RID: 50
[CreateMenu("CheckForSpecificItemsFromLastRun", 0)]
public class CheckForSpecificItemsFromLastRun : ConditionDataBase
{
	// Token: 0x06000108 RID: 264 RVA: 0x000077CC File Offset: 0x000059CC
	public override bool OnGetIsValid(INode parent)
	{
		int num = 0;
		foreach (string text in MetaProgressSaveManager.main.lastRun.itemsDiscovered)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (Item2.GetDisplayName(text) == Item2.GetDisplayName(this.items[i].name))
				{
					num++;
				}
			}
		}
		return num >= this.necessaryNumberOfItems;
	}

	// Token: 0x040000A2 RID: 162
	[SerializeField]
	private List<GameObject> items = new List<GameObject>();

	// Token: 0x040000A3 RID: 163
	public int necessaryNumberOfItems = 1;
}
